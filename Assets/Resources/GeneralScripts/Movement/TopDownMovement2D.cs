using UnityEngine;

namespace KenDev
{
    public class TopDownMovement2D : MonoBehaviour
    {    
        // _________________ Signelton Pattern _________________//

        public enum FacingType2D {FourDirections, TwoDirections};
        public enum eFacing { FacingRight, FacingLeft, FacingDown, FacingUp};

        [Space]
        [Header("Movement Parameters")]
        public float speed      = 6f;
        public FacingType2D facingType2D = FacingType2D.FourDirections;
        public eFacing facing   = eFacing.FacingRight;

        [Space]
        [Header("Player Flags")]
        public bool isWalking   = false;

        private Rigidbody2D rb;

        // _________________ Private Methods _________________//
        protected virtual void Awake()
        { 
            rb = GetComponent<Rigidbody2D>();
            if(rb == null)
                print("*** Object Has no RigidBody2D Component ***");
        }

        protected virtual void FixedUpdate()
        {
            // Physics based movement
            HandleMovment();
        }

        private void HandleMovment()
        {
            // set player velocity according to inputs
            if (PlayerInput.Instance == null)
                return;
            Vector2 vel = (PlayerInput.Instance.moveDir * speed);
            if (vel.sqrMagnitude != 0f)
                isWalking = true;
            else
                isWalking = false;

            rb.velocity = Vector2.Lerp(rb.velocity, vel, Time.fixedDeltaTime * 10);

        

            //make sure player facing right way
            FacePlayer(); 
        }


        private eFacing prevFaceing = eFacing.FacingRight;
        public bool doPlayeFacing = true;
        private void FacePlayer()
        {
            if (!doPlayeFacing)
                return;

            if      (PlayerInput.Instance.xDir > 0.1f) facing = eFacing.FacingRight;
            else if (PlayerInput.Instance.xDir < -0.1f) facing = eFacing.FacingLeft;
            
            if (facingType2D == FacingType2D.FourDirections) // if Four facing type then check yDir aswel
            {
                if      (PlayerInput.Instance.yDir > 0f) facing = eFacing.FacingUp;
                else if (PlayerInput.Instance.yDir < 0f) facing = eFacing.FacingDown;
            }

            //face player only if there was a change in direction
            if(facing != prevFaceing)
                transform.rotation = Quaternion.Euler(0f, (int)facing * 180f, 0f);
            prevFaceing = facing;
        }
        // _________________ Private Methods _________________//
    }
}
