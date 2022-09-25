
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace KenDev
{
    public class PlatformerMovement2D : MonoBehaviour
    {
        public enum eFacing { FacingRight, FacingLeft};

        [Space]
        [Header("Movement Parameters")]
        public bool canMove = true;
        public float moveSpeed = 6f;
        public bool isFacingRight = true;
        private float horizontal;

        [Space]
        [Header("Jump Parameters")]
        public float jumpSpeed = 15f;
        private float jumpTime = 0f;
        public float maxFallSpeed = 15f;

        [Space]
        [Header("Ground Check Parameters")]
        public float xGeneralGroundOffset = 0f;
        public float xGroundOffset = 0.65f;
        public float yGroundOffset = 0;
        public float rayLength = 0.25f;
        public LayerMask whatIsGround;

        [Space]
        [Header("Player Flags")]
        public bool isGrounded = true;
        public bool isMoving = false;

        private Rigidbody2D rb;


        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }


        private void Update()
        {
            // Make sure player fall speed isn't too fast
            if (rb.velocity.y < -maxFallSpeed)
                rb.velocity = new Vector2(rb.velocity.x, -maxFallSpeed);

            // horizontal velocity set
            if(canMove)
                rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
            else
            {
                rb.velocity = Vector2.zero;
            }

            // flip the player
            if (horizontal > 0f && !isFacingRight)
            {
                FlipPlayer();
            }
            else if (horizontal < 0f && isFacingRight)
            {
                FlipPlayer();
            }
        }

        public void Move(InputAction.CallbackContext context)
        {
            
            horizontal = context.ReadValue<Vector2>().x;
            
        }

        public void Jump(InputAction.CallbackContext context)
        {
            if (context.started && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }
            if (context.canceled && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }

        void FixedUpdate()
        {
            EnviromentCheck();
        }

        private void EnviromentCheck()
        {
            // Start by assuming slime isn't grounded
            isGrounded = false;

            // Check if player is grounded
            Vector2 orgLeft = new Vector2(transform.position.x + xGroundOffset + xGeneralGroundOffset, transform.position.y - yGroundOffset);
            Vector2 orgMid = new Vector2(transform.position.x + xGeneralGroundOffset, transform.position.y - yGroundOffset);
            Vector2 orgRight = new Vector2(transform.position.x - xGroundOffset + xGeneralGroundOffset, transform.position.y - yGroundOffset);
            RaycastHit2D leftCheck = Raycast(orgLeft, Vector2.down, rayLength, whatIsGround, true);
            RaycastHit2D midCheck = Raycast(orgMid, Vector2.down, rayLength, whatIsGround, true);
            RaycastHit2D rightCheck = Raycast(orgRight, Vector2.down, rayLength, whatIsGround, true);
            if (leftCheck || midCheck || rightCheck)
                isGrounded = true;
        }

        private void FlipPlayer()
        {
            // Rotate on Y axis 180 degrees depending on current facing value (left or right)
            isFacingRight = !isFacingRight;
            if (isFacingRight)
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            else
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);


            //Vector3 localScale = transform.localScale;
            //localScale.x *= -1f;
            //transform.localScale = localScale;
        }

        public RaycastHit2D Raycast(Vector2 _origin, Vector2 _direction, bool _debug = false)
        {
            return Physics2D.Raycast(_origin, _direction);
        }

        public RaycastHit2D Raycast(Vector2 _origin, Vector2 _direction, float _length, LayerMask _layer, bool _debug = false)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(_origin, _direction, _length, _layer);

            // if Ray debug is desired then draw (default is false)
            if (_debug)
            {
                // if hit then ray is red, if didnt hit then green
                Color color = hitInfo ? Color.red : Color.green;
                Debug.DrawRay(_origin, _direction * _length, color);
            }

            return hitInfo;
        }
    }

}
