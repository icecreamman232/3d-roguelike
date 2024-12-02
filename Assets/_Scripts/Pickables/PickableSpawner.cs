using System;
using SGGames.Scripts.Healths;
using UnityEngine;
using Random = UnityEngine.Random;


namespace SGGames.Scripts.Pickables
{
    public class PickableSpawner : MonoBehaviour
    {
        [SerializeField] private float m_spawnForce = 10;
        [SerializeField] private float m_spreadRadius = 0.5f;
        [SerializeField] private float m_spawnHeight;

        [SerializeField] private LootData m_lootData;
        
        private Health m_health;

        private void Start()
        {
            m_health = GetComponentInParent<Health>();
            m_health.OnDeath += Spawn;
        }

        private void Spawn()
        {
            m_health.OnDeath -= Spawn;
            for (int i = 0; i < m_lootData.LootTables.Length; i++)
            {
                var spawnObj = Instantiate(m_lootData.LootTables[i].LootPrefab, transform.position, Quaternion.identity);
                var rigidbody = spawnObj.GetComponent<Rigidbody>();
                var randomSpreadForce = Random.insideUnitCircle * m_spreadRadius;
                
                rigidbody.AddForce(new Vector3(randomSpreadForce.x,1,randomSpreadForce.y) * m_spawnForce, ForceMode.Impulse);
            }
        }

        [ContextMenu("Test Spawn")]
        private void TestSpawn()
        {
            for (int i = 0; i < m_lootData.LootTables.Length; i++)
            {
                var spawnObj = Instantiate(m_lootData.LootTables[i].LootPrefab, transform.position, Quaternion.identity);
                var rigidbody = spawnObj.GetComponent<Rigidbody>();
                var randomSpreadForce = Random.insideUnitCircle * m_spreadRadius;
                
                rigidbody.AddForce(new Vector3(randomSpreadForce.x,1,randomSpreadForce.y) * m_spawnForce, ForceMode.Impulse);
            }
        }
    }
}

