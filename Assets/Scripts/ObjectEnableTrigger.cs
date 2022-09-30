using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KenDev
{
    public class ObjectEnableTrigger : MonoBehaviour
    {
        public GameObject obj;
        public LayerMask whatIsTriggering;

        private void Awake()
        {
            obj.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (MyUtilities.Instance.CheckCollisionWithLayer(collision.gameObject.layer, whatIsTriggering))
            {
                obj.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
