﻿using System.Collections.Generic;
using Colyseus.Schema;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        public void OnChange(List<DataChange> changes)
        {
            Vector3 position = transform.position;

            foreach (var dataChange in changes)
            {
                switch (dataChange.Field)
                {
                    case "x":
                        position.x = (float)dataChange.Value;
                        break;
                    case "y":
                        position.z = (float)dataChange.Value;
                        break;
                }
            }
            
            transform.position = position;
        }
    }
}