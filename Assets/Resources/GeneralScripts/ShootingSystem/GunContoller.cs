using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KenDev
{
    public class GunContoller : MonoBehaviour
    {
        [Space]
        [Header("Game Objects")]
        public GameObject projectile;
        public Transform shootPoint;

        [Space]
        [Header("Shooting Parameters")]
        public float timeBtwShots = 0.1f;
        public bool isShooting = false;
        public bool sprayPattern = false;
        public float maxSprayDegree = 3f;
        private float counter = 0f;
        private bool canShoot;

        private void Awake()
        {
            ResetShooting();
        }

        private void ResetShooting()
        {
            counter = 0;
            isShooting = false;
            canShoot = true;
        }

        private void Update()
        {
            if (isShooting)
            {
                if(canShoot)
                    Shoot();

                canShoot = false;
                counter += Time.deltaTime;

                if (counter >= timeBtwShots)
                {
                    canShoot = true;
                    counter = 0f;
                }
            }   
        }

        public void HandleShooting(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                isShooting = true;
            }
            else if(context.canceled)
            {
                ResetShooting();
            }
        }

        

        public void Shoot()
        {
            Vector3 projectileRotation;
            if (sprayPattern)
                projectileRotation = new Vector3(0f, transform.rotation.y * 180f, Random.Range(-maxSprayDegree, maxSprayDegree));
            else
                projectileRotation = transform.rotation.eulerAngles;

            Instantiate(projectile, shootPoint.position, Quaternion.Euler(projectileRotation));
        }
    }
}
