using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KenDev
{
    public class PortalTrigger : MonoBehaviour
    {
        public GameObject portal;
        public PortalTrigger linkedWall;
        public LayerMask whatCanTrigger;
        public bool destroyTriggerObject = false;
        public bool portalActive = false;

        private void Awake()
        {
            SetPortalState(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            

            GameObject obj = collision.gameObject;
            if (MyUtilities.Instance.CheckCollisionWithLayer(obj.layer, whatCanTrigger))
            {
                if (portalActive)
                    return;

                AudioManager.Instance.PlayPortalOpen();
                SetPortalState(true);
                linkedWall.ActivatePortal();
                if (destroyTriggerObject)
                    Destroy(obj);
            }

        }

        public void DeactivatePortal()
        {
            if(portalActive)
                SetPortalState(false);
        }

        public void ActivatePortal()
        {
            if (!portalActive)
                SetPortalState(true);
        }


        private void SetPortalState(bool _state)
        {
            portalActive = _state;
            portal.SetActive(portalActive);
        }

    }
}
