using System.Collections.Generic;
using Infrastructure.Multiplayer;
using Infrastructure.Services.Input;
using UnityEngine;

namespace PlayerLogic
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float speed = 2;
        [SerializeField] private InputService _inputService; 
    
        private void Update()
        {
            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                Move();
                SendMove();
            }
        }
    
        private void Move()
        {
            Vector3 direction = new Vector3(_inputService.Axis.x, 0, _inputService.Axis.y).normalized;
            transform.position += direction * Time.deltaTime * speed;
        }
    
        private void SendMove()
        {
            Vector3 position = transform.position;
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"x", position.x},
                {"y", position.z}
            };
            
            MultiplayerManager.Instance.SendMessage("move", data);
        }
    }
}
