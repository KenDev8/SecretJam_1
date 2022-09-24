using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KenDev
{
    public class CollectableItem : MonoBehaviour
    {
        private Rigidbody2D rb;

        public LayerMask whatCanCollect;

        [Header("On Spawn Paramaters")]
        public bool flyOutOnSpawn = true;
        public float spread = 2f;
        public float height = 4f;
        public float rotSpeed = 30f;


        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            if(flyOutOnSpawn)
            {
                Vector2 startVel = new Vector2
                    (
                        Random.Range(-spread, spread),
                        Random.Range(1f, height)
                    );
                rb.velocity = startVel;
                rb.angularVelocity = Random.Range(-rotSpeed, rotSpeed);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(MyUtilities.Instance.CheckCollisionWithLayer(collision.gameObject.layer, whatCanCollect))
            {
                ICollector collector = collision.gameObject.GetComponent<ICollector>();
                collector.Collect();
                Destroy(gameObject);
            }
        }
    }
}
