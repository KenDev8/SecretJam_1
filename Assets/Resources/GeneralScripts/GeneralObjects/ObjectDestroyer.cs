using UnityEngine;


namespace KenDev
{
    public class ObjectDestroyer : MonoBehaviour
    {
        public LayerMask whatToDestroy;

        private void OnTriggerExit2D(Collider2D other) {
        
            if (MyUtilities.Instance.CheckCollisionWithLayer(other.gameObject.layer, whatToDestroy))
                return;

            Destroy(other.gameObject);
        }
    }
}
