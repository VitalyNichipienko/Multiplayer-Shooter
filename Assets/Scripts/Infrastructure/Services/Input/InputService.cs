using UnityEngine;

namespace DefaultNamespace
{
    public class InputService : MonoBehaviour, IInputService
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        public Vector2 Axis => new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));
    }
}