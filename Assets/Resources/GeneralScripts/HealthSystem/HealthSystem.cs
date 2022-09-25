using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KenDev
{
    public class HealthSystem : MonoBehaviour
    {
        public float maxHealth = 100;
        public bool isInvulnerable { get; set; }
        private float currentHealth;
        private bool isDead = false;

        public delegate void OnReduceHealthHandler(float _amount);
        public event OnReduceHealthHandler OnReaduceHP;

        public delegate void OnIncreaseHealthHandler(float _amount);
        public event OnIncreaseHealthHandler OnIncreaseHP;

        public delegate void OnUpdateHealthHandler(float _currentHealth);
        public event OnUpdateHealthHandler OnUpdateHealth;

        public delegate void OnInvulnerableHitHandler();
        public event OnInvulnerableHitHandler OnInvulnerableHit;

        public delegate void OnDeathHandler();
        public event OnDeathHandler OnDeath;

        private void Awake()
        {
            UpdateHealth(maxHealth);
        }

        public float GetHP()
        {
            return currentHealth;
        }

        public void ReduceHP(float _amount)
        {
            if (isDead)
                return;

            if (isInvulnerable)
            {
                OnInvulnerableHit?.Invoke();
                return;
            }

            UpdateHealth(-Mathf.Abs(_amount));
            OnReaduceHP?.Invoke(_amount);

            if (currentHealth <= 0)
            {
                isDead = true;
                OnDeath?.Invoke();
            }
        }

        public void IncreaseHP(float _amount)
        {
            if (isDead)
                return;

            if (isInvulnerable)
            {
                OnInvulnerableHit?.Invoke();
                return;
            }

            UpdateHealth(Mathf.Abs(_amount));
            OnIncreaseHP?.Invoke(_amount);

        }

        public void UpdateHealth(float _amount)
        {
            currentHealth += _amount;
            //print("Health: " + currentHealth);
            OnUpdateHealth?.Invoke(currentHealth);
        }

        public bool HealthPercenLeft(float _percent)
        {
            if ((currentHealth / maxHealth) * 100f <= _percent)
            {
                return true; // for e.x - return true if less then 30% hp
            }
            return false;
        }
        public bool IsDead()
        {
            return isDead;
        }
    }

}
