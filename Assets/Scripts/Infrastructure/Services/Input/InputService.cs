using UnityEngine;

namespace Infrastructure.Services.Input
{
    public class InputService : MonoBehaviour, IInputService
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        private const string MouseX = "Mouse X";
        private const string MouseY = "Mouse Y";
        

        public Vector2 Axis => new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));

        public Vector2 MouseAxis => new Vector2(UnityEngine.Input.GetAxis(MouseX), UnityEngine.Input.GetAxis(MouseY));
    }
}