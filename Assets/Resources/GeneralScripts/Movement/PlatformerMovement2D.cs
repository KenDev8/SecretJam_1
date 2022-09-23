
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KenDev
{
    public class PlatformerMovement2D : MonoBehaviour
    {
        public enum eFacing { FacingLeft, FacingRight };

        [Space]
        [Header("Movement Parameters")]
        public float speed = 6f;
        public eFacing facing = eFacing.FacingRight;
        public float maxFallSpeed = 15f;

        [Space]
        [Header("Jump Parameters")]
        public float jumpInitForce = 15f;
        public float jumpHoldForce = 0.69f;
        public float jumpHoldDuration = 1.5f;
        public float gravityFallIncreas = 1.5f;
        private float jumpTime = 0f;
        private float fallGravity;
        private float initGravityScale;

        [Space]
        [Header("Ray Parameters")]
        public float xGeneralGroundOffset = 0f;
        public float xGroundOffset = 0.65f;
        public float yGroundOffset = 0;
        public float rayLength = 0.25f;
        public LayerMask whatIsGround;

        [Space]
        [Header("Player Flags")]
        public bool isGrounded = true;
        public bool isJumping = false;
        public bool isMoving = false;
        public bool isWalking = false;

        private bool jumpPress;
        private bool jumpHold;
        private float xDir;

        private Rigidbody2D rb;


        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            initGravityScale = rb.gravityScale;
            fallGravity = initGravityScale + gravityFallIncreas;
        }


        private void Update()
        {
            ReadInputs();
        }
        void FixedUpdate()
        {
            EnviromentCheck();

            // Physics based movement
            HandleMovment();
            HandleJump();
        }

        private void ReadInputs()
        {
            xDir = PlayerInput.Instance.xDir;
            if (PlayerInput.Instance.spacePress)
                jumpPress = true;
            jumpHold = PlayerInput.Instance.spaceHold;
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

        private void HandleMovment()
        {
            // Make sure player fall speed isn't too fast
            if (rb.velocity.y < -maxFallSpeed)
                rb.velocity = new Vector2(rb.velocity.x, -maxFallSpeed);

            // Horizontal
            rb.velocity = new Vector2(xDir * speed, rb.velocity.y);
            if (xDir > 0f)
            {
                isMoving = true;
                facing = eFacing.FacingRight;
            }
            else if (xDir < 0f)
            {
                isMoving = true;
                facing = eFacing.FacingLeft;
            }
            else
            {
                isMoving = false;
            }

            if (isGrounded && isMoving)
                isWalking = true;
            else
                isWalking = false;

            FlipPlayer(); //make sure player facing right way
        }

        private void HandleJump()
        {
            // Increase gravity scale when player is falling.
            if (!isGrounded && rb.velocity.y <= 0)
            {
                isJumping = false;
                rb.gravityScale = fallGravity;
            }

            // Return gravity scale back to normal when landed.
            if (isGrounded && rb.gravityScale > initGravityScale)
            {
                rb.gravityScale = initGravityScale;
            }


            if (jumpPress && !isJumping && isGrounded)
            {
                jumpPress = false;
                // Jump initial height 
                isJumping = true;
                isGrounded = false;
                jumpTime = Time.time + jumpHoldDuration;    // Record time for jump key hold boost
                //rb.AddForce(new Vector2(0f, jumpInitForce), ForceMode2D.Impulse);
                rb.velocity = Vector2.zero;
                rb.velocity = Vector2.up * jumpInitForce;
            }
            else if (isJumping)
            {
                // Higher jump by holding jump key and withing the jumpTime
                if (jumpHold)
                    rb.AddForce(new Vector2(0f, jumpHoldForce), ForceMode2D.Impulse);

                if (Time.time >= jumpTime)
                    isJumping = false;
            }
        }

        private void FlipPlayer()
        {
            // Rotate on Y axis 180 degrees depending on current facing value (left or right)
            transform.rotation = Quaternion.Euler(0f, (int)facing * 180f, 0f);
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
