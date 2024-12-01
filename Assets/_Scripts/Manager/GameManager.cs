using System;
using SGGames.Scripts.Data;
using SGGames.Scripts.Pickables;
using UnityEngine;

namespace SGGames.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PickablePickedEvent m_expShardPickedEvent;

        [SerializeField] private int m_expCollected;
        private void Start()
        {
            m_expShardPickedEvent.AddListener(OnPickXpShard);
        }

        private void OnDestroy()
        {
            m_expShardPickedEvent.RemoveListener(OnPickXpShard);
        }

        private void OnPickXpShard(PickableType type, int amount)
        {
            m_expCollected += amount;
        }
    }
}
    
