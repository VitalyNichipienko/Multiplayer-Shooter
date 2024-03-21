using System.Collections.Generic;
using System.Linq;
using Colyseus.Schema;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyMove enemyMove;
        private List<float> _receiveTimeInterval = new List<float> { 0, 0, 0, 0, 0 };
        private float _lastReceiveTime = 0;

        private void SaveReceiveTime()
        {
            float interval = Time.time - _lastReceiveTime;
            _lastReceiveTime = Time.time;

            _receiveTimeInterval.Add(interval);
            _receiveTimeInterval.RemoveAt(0);
        }

        public void OnChange(List<DataChange> changes)
        {
            SaveReceiveTime();

            Vector3 position = enemyMove.TargetPosition;
            Vector3 velocity = Vector3.zero;

            foreach (var dataChange in changes)
            {
                switch (dataChange.Field)
                {
                    case "positionX":
                        position.x = (float)dataChange.Value;
                        break;
                    case "positionY":
                        position.y = (float)dataChange.Value;
                        break;
                    case "positionZ":
                        position.z = (float)dataChange.Value;
                        break;

                    case "velocityX":
                        velocity.x = (float)dataChange.Value;
                        break;
                    case "velocityY":
                        velocity.y = (float)dataChange.Value;
                        break;
                    case "velocityZ":
                        velocity.z = (float)dataChange.Value;
                        break;
                }
            }

            enemyMove.SetMovement(position, velocity, _receiveTimeInterval.Average());
        }
    }
}