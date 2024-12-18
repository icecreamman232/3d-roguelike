using System;
using JustGame.Scripts.ScriptableEvent;
using SGGames.Scripts.Data;
using SGGames.Scripts.Pickables;
using UnityEngine;

namespace SGGames.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PickablePickedEvent m_expShardPickedEvent;
        [SerializeField] private PlayerExpChangedEvent m_playerExpChangedEvent;
        [SerializeField] private IntEvent m_playerLevelChangedEvent;
        [SerializeField] private PlayerLevelData m_playerLevelData;
        [SerializeField] private int m_expCollected;
        [SerializeField] private int m_currentLevel;
        [SerializeField] private int m_maxLevel;
        private void Start()
        {
            m_currentLevel = 1;
            m_maxLevel = m_playerLevelData.MaxLevel;
            
            m_playerExpChangedEvent.Raise(0,m_playerLevelData.GetMaxXPOfLevel(m_currentLevel));
            m_playerLevelChangedEvent.Raise(m_currentLevel);
            m_expShardPickedEvent.AddListener(OnPickXpShard);
        }

        private void OnDestroy()
        {
            m_expShardPickedEvent.RemoveListener(OnPickXpShard);
        }

        private void OnPickXpShard(PickableType type, int amount)
        {
            m_expCollected += amount;
            var maxXP = m_playerLevelData.GetMaxXPOfLevel(m_currentLevel);
            if (m_expCollected >= maxXP)
            {
                m_expCollected = 0;
                m_currentLevel++;
                if (m_currentLevel > m_maxLevel) return;
                m_playerExpChangedEvent.Raise(m_expCollected,m_playerLevelData.GetMaxXPOfLevel(m_currentLevel));
                m_playerLevelChangedEvent.Raise(m_currentLevel);
            }
            else
            {
                m_playerExpChangedEvent.Raise(m_expCollected,m_playerLevelData.GetMaxXPOfLevel(m_currentLevel));
            }
        }
    }
}
    
