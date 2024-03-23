using UnityEngine;

namespace PlayerLogic
{
    public class CheckFly : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float radius;
        [SerializeField] private float coyoteTime = 0.15f;

        private float _flyTimer;
        
        public bool IsFly { get; private set; }

        private void Update()
        {
            if (Physics.CheckSphere(transform.position, radius, layerMask))
            {
                IsFly = false;
                _flyTimer = 0;
            }
            else
            {
                _flyTimer += Time.deltaTime;

                if (_flyTimer > coyoteTime)
                {
                    IsFly = true;
                }
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
#endif
    }
}