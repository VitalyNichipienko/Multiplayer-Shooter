using System.Collections.Generic;
using Infrastructure.Multiplayer;
using Infrastructure.Services.Input;
using UnityEngine;

namespace PlayerLogic
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private float speed = 2;
        [SerializeField] private InputService _inputService;
        
        [SerializeField] private Transform head;
        [SerializeField] private float _mouseSensetivity;
        private float _rotateY;

        private void FixedUpdate()
        {
            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                Move();
                SendMove();
            }

            if (_inputService.MouseAxis.sqrMagnitude > Constants.Epsilon)
            {
                _rotateY += _inputService.MouseAxis.x * _mouseSensetivity;
                RotateY();
                RotateZ(-_inputService.MouseAxis.y * _mouseSensetivity);
            }
        }
    
        private void Move()
        {
            Vector3 velocity = (transform.forward * _inputService.Axis.y + transform.right * _inputService.Axis.x)
                .normalized * speed;
            rigidbody.velocity = velocity;
        }
    
        private void SendMove()
        {
            Vector3 position = transform.position;
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"positionX", position.x},
                {"positionY", position.y},
                {"positionZ", position.z},
                {"velocityX", rigidbody.velocity.x},
                {"velocityY", rigidbody.velocity.y},
                {"velocityZ", rigidbody.velocity.z}
            };
            
            MultiplayerManager.Instance.SendMessage("move", data);
        }

        private void RotateY()
        {
            rigidbody.angularVelocity = new Vector3(0, _rotateY, 0);
            _rotateY = 0;
        }
        
        private void RotateZ(float value)
        {
            head.Rotate(0, 0, value);
        }
    }
}
