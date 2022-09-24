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
            HandleFollow();
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
    }
}
