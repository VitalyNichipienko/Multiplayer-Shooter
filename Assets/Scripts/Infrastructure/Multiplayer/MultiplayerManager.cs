using System.Collections.Generic;
using Colyseus;
using Enemy;
using PlayerLogic;
using UnityEngine;

namespace Infrastructure.Multiplayer
{
    public class MultiplayerManager : ColyseusManager<MultiplayerManager>
    {
        [SerializeField] private PlayerMove playerPrefab;
        [SerializeField] private EnemyController enemyPrefab;
        
        private ColyseusRoom<State> _room;
        
        protected override void Awake()
        {
            base.Awake();
            
            Instance.InitializeClient();
            Connect();
        }

        public void SendMessage(string key, Dictionary<string, object> data)
        {
            _room.Send(key, data);
        }
        
        private async void Connect()
        {
            _room = await Instance.client.JoinOrCreate<State>("state_handler");

            _room.OnStateChange += OnStateChange;
        }

        private void OnStateChange(State state, bool isFirstState)
        {
            if (isFirstState == false)
                return;
            
            state.players.ForEach((string key, Player player) =>
            {
                if (key == IsPlayer())
                {
                    CreatePlayer(player);
                }
                else
                {
                    CreateEnemy(key, player);
                }
            });

            _room.State.players.OnAdd += CreateEnemy;
            _room.State.players.OnRemove += RemoveEnemy;
        }

        private void CreatePlayer(Player player)
        {
            var playerPosition = new Vector3(player.x, 0, player.y);
            
            Instantiate(playerPrefab, playerPosition, Quaternion.identity);
        }

        private void CreateEnemy(string key, Player enemy)
        {
            var enemyPosition = new Vector3(enemy.x, 0, enemy.y);
            
            EnemyController enemyController = Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);
            enemy.OnChange += enemyController.OnChange;
        }

        private void RemoveEnemy(string key, Player value)
        {
            
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();

            _room.Leave();
        }

        private string IsPlayer() => 
            _room.SessionId;
    }
}