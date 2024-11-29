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

        public void Initialize(EnemyWaveData data, Transform player)
        {
            m_enemyHealth.Initialize(data.HealthMultiplier);
            m_movement.Initialize(player);
        }
    }
}
