using DefaultNamespace;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] private InputService _inputService; 
    
    private void Update()
    {
        if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 direction = new Vector3(_inputService.Axis.x, 0, _inputService.Axis.y).normalized;
        transform.position += direction * Time.deltaTime * speed;
    }
}
