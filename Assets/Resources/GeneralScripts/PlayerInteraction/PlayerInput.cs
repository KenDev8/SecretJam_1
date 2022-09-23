using UnityEngine;

namespace KenDev
{
    // Ensure inputs script runs before all other player scripts
    [DefaultExecutionOrder(-100)]
    public class PlayerInput : MonoBehaviour
    {
        // _________________ Signelton Pattern _________________//
        public static PlayerInput _instance;
        public static PlayerInput Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<PlayerInput>();
                }
                return _instance;
            }
        }
        // _________________ Signelton Pattern _________________//

        private bool readyToClear = true;
        private bool canMove = true;

        [Space]
        [Header("Player Movement & parameters")]
        public float xDir = 0f;
        public float yDir = 0f;
        public Vector2 moveDir;

        [Space]
        [Header("Keyboard Inputs")]
        public bool leftArrowPress = false;
        public bool rightArrowPress = false;
        public bool upArrowPress = false;
        public bool downArrowPress = false;
        public bool spacePress = false;
        public bool tHold = false;
        public bool spaceHold = false;
        public bool ePress = false;

        // _________________ Private Methods _________________//
        private void Update()
        {
            ClearInputs();
            ProcessInputs();
        }

        private void FixedUpdate()
        {
            readyToClear = true;
        }

        private void ProcessInputs()
        {
            // Horizontal input && Vertical TopDown Movement
            if (canMove)
            {
                xDir = Mathf.Clamp(xDir + Input.GetAxisRaw("Horizontal"), -1f, 1f);
                yDir = Mathf.Clamp(yDir + Input.GetAxisRaw("Vertical"), -1f, 1f);
            }
            moveDir = new Vector2(xDir, yDir).normalized;

            // Keyboard presses
            leftArrowPress = Input.GetKeyDown(KeyCode.LeftArrow);
            rightArrowPress = Input.GetKeyDown(KeyCode.RightArrow);
            upArrowPress = Input.GetKeyDown(KeyCode.UpArrow);
            downArrowPress = Input.GetKeyDown(KeyCode.DownArrow);
            spacePress = Input.GetKeyDown(KeyCode.Space);
            ePress = Input.GetKeyDown(KeyCode.E);


            //Keyboard Holds
            tHold = Input.GetKey(KeyCode.T);
            spaceHold = Input.GetKey(KeyCode.Space);
        }

        private void ClearInputs()
        {
            if (!readyToClear)
                return;

            xDir = 0f;
            yDir = 0f;
            leftArrowPress = false;
            rightArrowPress = false;
            upArrowPress = false;
            downArrowPress = false;
            spacePress = false;
            tHold = false;
            spaceHold = false;
            ePress = false;

            readyToClear = false;
        }
        // _________________ Private Methods _________________//


        // _________________ Public Methods _________________//
        public void DisableMovement()
        {
            canMove = false;
        }

        public void EnableMovement()
        {
            canMove = true;
        }
        // _________________ Public Methods _________________//
    }

}
