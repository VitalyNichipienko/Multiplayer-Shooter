using UnityEngine;

namespace PlayerLogic
{
    public class CheckFly : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float radius;

        public bool IsFly { get; private set; }

        private void Update() => 
            IsFly = !Physics.CheckSphere(transform.position, radius, layerMask);

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
#endif
    }
}