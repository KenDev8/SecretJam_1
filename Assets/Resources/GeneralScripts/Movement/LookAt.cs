using UnityEngine;

namespace KenDev
{
    public enum LookAtTarget
    {
        mouse,
        gameObject
    }


    public class LookAt : MonoBehaviour
    {
        public bool enabledOnAwake = false;
        public LookAtTarget target;
        public Transform gameObjTarget;

        private Vector3 lookAtDirection;
        private float angle;

        private void Awake()
        {
            if (!enabledOnAwake)
                enabled = false;
        }


        // Update is called once per frame
        void Update()
        {
            CalcDirection();
            Rotate();
        }

        private void CalcDirection()
        {
            switch (target)
            {
                case LookAtTarget.mouse:
                    lookAtDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
                    break;
                case LookAtTarget.gameObject:
                    if (gameObjTarget == null)
                        Debug.Log("* Didn't set game object target to follow");
                    else
                        lookAtDirection = gameObjTarget.position - transform.position;
                    break;
            }
            angle = Mathf.Atan2(lookAtDirection.y, lookAtDirection.x) * Mathf.Rad2Deg;
        }

        private void Rotate()
        {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}

