using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KenDev
{
    public class Portal : MonoBehaviour
    {
        public Portal targetPortal;
        public LayerMask whatIsPortable;
        public float teleportDelay = 1f;
        public bool canTeleport = true;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject obj = collision.gameObject;
            if(MyUtilities.Instance.CheckCollisionWithLayer(obj.layer, whatIsPortable))
            {
                if(canTeleport)
                {
                    canTeleport = false;
                    targetPortal.canTeleport = false;
                    StartCoroutine(Teleport(obj));
                }
            }
        }

        public IEnumerator Teleport(GameObject obj)
        {
            yield return new WaitForSeconds(teleportDelay);
            obj.transform.position = targetPortal.transform.position;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            canTeleport = true;
        }
    }
}
