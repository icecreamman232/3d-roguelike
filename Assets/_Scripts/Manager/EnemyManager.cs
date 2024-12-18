using System;
using SGGames.Scripts.Data;
using SGGames.Scripts.Enemies;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SGGames.Scripts.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private bool m_canSpawn;
        [SerializeField] private int m_curWaveIndex;
        [SerializeField] private float m_delayNextSpawn;
        [SerializeField] private Transform m_playerRef;
        [SerializeField] private float m_spawnRadius;
        [SerializeField] private EnemyWaveData[] m_waveData;
        [SerializeField] private GameObject[] m_enemyPrefabs;
        
        private float m_spawnTimer;
        private int m_numEnemySpawned;

        private void Start()
        {
            m_curWaveIndex = 0;
        }

        private void Update()
        {
            if (!m_canSpawn) return;
            
            if (m_spawnTimer >= m_delayNextSpawn)
            {
                SpawnEnemy();
                m_numEnemySpawned++;
                CheckToIncreaseWaveQuality();
                m_spawnTimer = 0;
            }
            m_spawnTimer += Time.deltaTime;
        }

        private void CheckToIncreaseWaveQuality()
        {
            if (m_numEnemySpawned >= m_waveData[m_curWaveIndex].MaxNumberOfEnemies)
            {
                m_curWaveIndex++;
                if (m_curWaveIndex >= m_waveData.Length)
                {
                    m_curWaveIndex = m_waveData.Length - 1;
                }
            }
        }

        private Vector3 GetSpawnPosition()
        {
            var randomPos = Random.insideUnitCircle * m_spawnRadius;
            return m_playerRef.position + new Vector3(randomPos.x, 0, randomPos.y);
        }

        private void SpawnEnemy()
        {
            var enemyObject = Instantiate(m_enemyPrefabs[0],GetSpawnPosition(),Quaternion.identity);
            var controller = enemyObject.GetComponent<EnemyController>();
            controller.Initialize(m_waveData[m_curWaveIndex],m_playerRef);
        }
    }
}

