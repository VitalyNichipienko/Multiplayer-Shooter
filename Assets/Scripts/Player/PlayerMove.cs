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

        [SerializeField] private Transform cameraPoint;
        [SerializeField] private float minHeadAngle = 90;
        [SerializeField] private float maxHeadAngle = -90;

        [SerializeField] private float jumpForce;

        private float _rotateY;
        private float _currentRotateZ;
        private bool _isFly;

        private void Start()
        {
            Transform camera = Camera.main.transform;
            camera.parent = cameraPoint;
            camera.localPosition = Vector3.zero;
            camera.localRotation = Quaternion.identity;
        }

        private void Update()
        {
            if (_inputService.Space)
            {
                Jump();
            }
        }

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

        private void OnCollisionStay(Collision other)
        {
            ContactPoint[] contactPoints = other.contacts;

            for (int i = 0; i < contactPoints.Length; i++)
            {
                if (contactPoints[i].normal.y > 0.45f)
                    _isFly = false;
            }
        }

        private void OnCollisionExit(Collision other)
        {
            _isFly = true;
        }

        private void Move()
        {
            Vector3 velocity = (transform.forward * _inputService.Axis.y + transform.right * _inputService.Axis.x)
                .normalized * speed;
            velocity.y = rigidbody.velocity.y;

            rigidbody.velocity = velocity;
        }

        private void SendMove()
        {
            Vector3 position = transform.position;
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "positionX", position.x },
                { "positionY", position.y },
                { "positionZ", position.z },
                { "velocityX", rigidbody.velocity.x },
                { "velocityY", rigidbody.velocity.y },
                { "velocityZ", rigidbody.velocity.z }
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
            _currentRotateZ = Mathf.Clamp(_currentRotateZ + value, minHeadAngle, maxHeadAngle);
            head.localEulerAngles = new Vector3(0, 0, _currentRotateZ);
        }

        private void Jump()
        {
            if (_isFly)
                return;

            rigidbody.AddForce(0, jumpForce, 0, ForceMode.VelocityChange);
        }
    }
}