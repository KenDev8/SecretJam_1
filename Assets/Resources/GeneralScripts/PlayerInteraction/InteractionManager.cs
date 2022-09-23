using UnityEngine;
using UnityEngine.EventSystems;

namespace KenDev
{
    public enum InteractionType { LeftClick, RightClick }

    public class InteractionManager : MonoBehaviour
    {
        // _________________ Signelton Pattern _________________//
        public static InteractionManager _instance;
        public static InteractionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<InteractionManager>();
                }
                return _instance;
            }
        }
        // _________________ Signelton Pattern _________________//

        public LayerMask intractableLayer;
        private bool interactionEnabled = true;

        public void EnableInteraction()
        {
            interactionEnabled = true;
        }

        public void DisableInteraction()
        {
            interactionEnabled = false;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown((int)InteractionType.LeftClick))
            {
                // check first if mouse is over UI element when clicked - if yes then UI should block the interaction
                //if (EventSystem.current.IsPointerOverGameObject())
                    //return;

                // check if clicked on interactable
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 100f, intractableLayer);


                if (hit.collider != null)
                {
                    // if clicked on intractable object
                    GameObject obj = hit.collider.gameObject;
                    IInteractable inter = obj.GetComponent<IInteractable>();
                    if(inter != null)
                        inter.Interact();

                    print("# Left clicked on: " + obj.name);
                }
            }
            else if (Input.GetMouseButtonDown((int)InteractionType.RightClick))
            {
                // check if clicked on interactable
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 100f, intractableLayer);


                if (hit.collider != null)
                {
                    // if clicked on intractable object
                    GameObject obj = hit.collider.gameObject;
                    IInteractable inter = obj.GetComponent<IInteractable>();
                    if (inter != null)
                        inter.Interact(InteractionType.RightClick);

                    //print("# Right clicked on: " + obj.name);
                }
            }
        }
    }
}

