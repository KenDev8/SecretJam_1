using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KenDev
{
    public class Skeleton : MonoBehaviour, IDamageable
    {
        private Rigidbody2D rb;
        private HealthSystem hs;
        private Transform target;
        private Dropper dropper;
        public float speed;
        public bool isFacingRight = true;


        void Start()
        {
            dropper = GetComponent<Dropper>();
            rb = GetComponent<Rigidbody2D>();
            hs = GetComponent<HealthSystem>();
            target = Betty.Instance.transform;

            hs.OnDeath += Die;
        }

        void Update()
        {
            EnviromentCheck();

            HandleFollow();


            // flip the player
            if (target.position.x > transform.position.x && !isFacingRight)
            {
                Flip();
            }
            else if (target.position.x < transform.position.x && isFacingRight)
            {
                Flip();
            }
        }

        private void HandleFollow()
        {
            float xSpeed = new Vector2(target.position.x - transform.position.x, 0).normalized.x;
            rb.velocity = new Vector2(xSpeed, rb.velocity.y);

        }

        public void TakeDamage(int _damage)
        {
            hs.ReduceHP(_damage);
        }
        public void Die()
        {
            dropper.Drop();
            Destroy(gameObject);
        }

        private void Flip()
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


        public LayerMask whatIsGround;
        public float yOffset;
        public float rayLength = 1f;
        public bool isGrounded = false;
        public float jumpSpeed = 8f;
        private void EnviromentCheck()
        {
            isGrounded = false;
            // Check if player is grounded

            Vector2 orgBot = new Vector2(transform.position.x, transform.position.y);
            Vector2 orgRight = new Vector2(transform.position.x, transform.position.y - yOffset);
            RaycastHit2D rightCheck =  MyUtilities.Instance.Raycast(orgRight, transform.right, rayLength, whatIsGround, true);
            RaycastHit2D botCheck = MyUtilities.Instance.Raycast(orgBot, Vector2.down, rayLength/4, whatIsGround, true);

            if (botCheck)
            {
                isGrounded = true;
            }

            if (rightCheck && isGrounded)
            {
                Jump();
            }
        }

        private void Jump()
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }
}
