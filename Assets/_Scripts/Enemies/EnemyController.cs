using System;
using SGGames.Scripts.Data;
using SGGames.Scripts.Healths;
using UnityEngine;

namespace SGGames.Scripts.Enemies
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] protected EnemyHealth m_enemyHealth;
        [SerializeField] protected EnemyMovement m_movement;
        [SerializeField] protected GameObject[] m_disableOnDeathList;

        public void Initialize(EnemyWaveData data, Transform player)
        {
            m_enemyHealth.Initialize(data.HealthMultiplier);
            m_movement.Initialize(player,data.SpeedMultiplier);
            m_enemyHealth.OnDeath += OnDeathProcess;
        }

        private void OnDeathProcess()
        {
            m_enemyHealth.OnDeath -= OnDeathProcess;
            foreach (GameObject obj in m_disableOnDeathList)
            {
                obj.SetActive(false);
            }
        }
    }
}
