using UnityEngine;

namespace KenDev
{
    public class Projectile2D : MonoBehaviour
    {
        Rigidbody2D rb;

        [Header("Starting Properties")]
        public bool gravityEnabled = false;
        public bool moveOnStart = true;
        public float startingSpeed = 100f;
        private Vector3 defaultScale;
        private Vector2 defaultRight;

        [Space]
        [Header("Projectile Damage")]
        public LayerMask whatIsDamageable;
        public int damage;
        private bool hitSomething = false;

        private bool movingEnabled = true;

        [Header("Homing Projectiles")]
        public GameObject targetScanArea;
        public Transform targetTransform;
        private bool isHoming = false;
        private bool targetFound = false;
        [Range(0f, 0.01f)]
        public float homingSpeedDelta = 0f;
        public float homingSpeed = 0f;

        private void Start()
        {
            defaultRight = transform.right;
            defaultScale = transform.localScale;
            rb = GetComponent<Rigidbody2D>();

            if (gravityEnabled)
                EnableGravity();
            else
                DisableGravity();

            if (moveOnStart)
                EnableMovement();
            else
                DisableMovement();
        }

        private void Update()
        {
            if (isHoming && targetFound)
            {
                HandleHoming();
            }
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            print(collision.gameObject.layer);
            // if this projectile already hit something, don't continue with hit logic, it should be destroyed
            if (hitSomething)
                return;


            if (MyUtilities.Instance.CheckCollisionWithLayer(collision.gameObject.layer, whatIsDamageable))
            {
                IDamageable damageableObj = collision.gameObject.GetComponent<IDamageable>();
                Debug.Log("Hit something");
                if (damageableObj != null)
                {
                    hitSomething = true;
                    Debug.Log("Hit Damageable");
                    damageableObj.TakeDamage(damage);
                }
            }

            Destroy(gameObject);
        }

        // ---------------power ups-----------------
        public void SetHoming(bool _set)
        {
            if (_set)
            {
                isHoming = true;
                targetScanArea.SetActive(true);
            }
            else
            {
                targetScanArea.SetActive(false);
            }
        }

        public void SetHomingTarget(Transform _target)
        {
            targetTransform = _target;
            targetFound = true;
            DisableMovement(); // disable velocity to allow translate to take control
        }

        private void HandleHoming()
        {
            //in case target was already destroyed by a different projectile 
            if (targetTransform != null)
            {
                Vector2 targetPos = targetTransform.position - transform.position;
                transform.right = Vector2.Lerp(defaultRight, targetPos, homingSpeed);
            }

            //transform.right = targetPos;
            homingSpeed = Mathf.Clamp(homingSpeed + homingSpeedDelta, 0f, 1f);
            transform.Translate(new Vector3(startingSpeed * Time.deltaTime, 0, 0));
        }
        // -----------------------------------------

        void EnableMovement()
        {
            movingEnabled = true;
            rb.velocity = transform.right * startingSpeed;
        }

        void DisableMovement()
        {
            movingEnabled = false;
            rb.velocity = Vector2.zero;
        }

        void EnableGravity()
        {
            rb.gravityScale = 1;
        }

        void DisableGravity()
        {
            rb.gravityScale = 0;
        }

        public void SetScaleMultiplier(int _multiplier)
        {
            transform.localScale = _multiplier * transform.localScale;
        }

        public void ResetScale()
        {
            transform.localScale = defaultScale;
        }
    }
}

