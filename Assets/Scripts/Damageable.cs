using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KenDev
{
    public class Damageable : MonoBehaviour, IDamageable
    {
        private HealthSystem hs;

        void Start()
        {
            hs = GetComponent<HealthSystem>();    
        }

        void Update()
        {
            
        }

        public void TakeDamage(int _damage)
        {
            hs.ReduceHP(_damage);
        }
        public void Die()
        {
            Destroy(gameObject);
        }
    }
}
