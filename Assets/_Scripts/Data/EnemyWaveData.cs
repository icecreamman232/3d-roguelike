using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(menuName = "SGGames/Enemy Wave Data",fileName = "EnemyWaveData")]
    public class EnemyWaveData : ScriptableObject
    {
        [SerializeField] private int m_waveNumber;
        [SerializeField] private int m_maxNumberOfEnemies;
        [SerializeField] private float m_healthMultiplier;
        [SerializeField] private float m_speedMultiplier;
        
        public int WaveNumber => m_waveNumber;
        public int MaxNumberOfEnemies => m_maxNumberOfEnemies;
        public float HealthMultiplier => m_healthMultiplier;
        public float SpeedMultiplier => m_speedMultiplier;
    }
}

