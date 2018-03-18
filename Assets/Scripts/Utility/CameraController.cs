using UnityEngine;

namespace Utility
{
    public class CameraController : MonoBehaviour
    {
        public string ForwardAxis = "Vertical";
        public string HorizontalAxis = "Horizontal";
        public string UpDownAxis = "UpDown";

        public float Speed = 20;
        
        private void Update()
        {
            ProcessMovement();
            ProcessRotation();
        }

        private void ProcessMovement()
        {
            var movement = new Vector3();
            movement.x = Input.GetAxis(HorizontalAxis);
            movement.y = Input.GetAxis(UpDownAxis);
            movement.z = Input.GetAxis(ForwardAxis);

            movement *= Speed * Time.deltaTime;

            transform.Translate(movement);
        }
        
        private void ProcessRotation()
        {
            
        }
    }
}
