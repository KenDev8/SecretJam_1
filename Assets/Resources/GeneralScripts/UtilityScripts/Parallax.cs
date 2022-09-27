using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KenDev
{
    public class Parallax : MonoBehaviour
    {
        private Transform cam;
        private float startX;
        private float parallaxEffectX;
        private float parallaxEffectY;

        [Space]
        [Header("Parallax Parameters")]
        public bool yAxisParallax = false;
        public float parallaxStrengthX = 1f; // Determined by transform.z
        public float parallaxStrengthY = 1f;
        public int yOffset = 0;
        public int xOffset = 0;

        private void Start()
        {
            cam = Camera.main.transform;
            startX = transform.position.x;  // Record initial transform.x of object
        }

        void Update()
        {
            parallaxEffectX = transform.position.z * parallaxStrengthX / 10;
            parallaxEffectY = transform.position.z * parallaxStrengthY / 10;
            float dist = cam.position.x * parallaxEffectX;
            float distY = cam.position.y * parallaxEffectY;

            // Update new position according to camera current X position and dist value 
            Vector3 newPos = new Vector3(startX + dist + xOffset, cam.position.y + yOffset, transform.position.z);
            if (yAxisParallax)
                newPos = new Vector3(newPos.x, newPos.y + distY, newPos.z);

            transform.position = newPos;
        }   
    }
}
