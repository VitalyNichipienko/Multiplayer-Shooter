﻿using UnityEngine;

namespace Enemy
{
    public class EnemyMove : MonoBehaviour
    {
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}