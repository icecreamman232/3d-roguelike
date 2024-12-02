using System;
using UnityEngine;
using Random = UnityEngine.Random;


namespace SGGames.Scripts.Pickables
{
    [CreateAssetMenu(menuName = "SGGames/Data/Loot Data",fileName = "New Loot Data")]
    public class LootData : ScriptableObject
    {
        [SerializeField] private LootTable[] m_LootTables;

        [ContextMenu("Compute Drop Chance")]
        private void ComputeDropChance()
        {
            var totalWeight = 0f;
            for (int i = 0; i < m_LootTables.Length; i++)
            {
                totalWeight += m_LootTables[i].Weight;
            }

            var m_chance = 0f;
            
            for (int i = 0; i < m_LootTables.Length; i++)
            {
                m_LootTables[i].LowerChance = m_chance;
                m_chance += m_LootTables[i].Weight/ totalWeight * 100f;
                m_LootTables[i].UpperChance = m_chance;
            }
        }

        public LootTable[] LootTables => m_LootTables;

        public GameObject GetNextLoot()
        {
            var chance = Random.Range(0, 100f);

            for (int i = 0; i < m_LootTables.Length; i++)
            {
                if (chance >= m_LootTables[i].LowerChance && chance <= m_LootTables[i].UpperChance)
                {
                    return m_LootTables[i].LootPrefab;
                }
            }

            return null;
        }
    }

    [Serializable]
    public struct LootTable
    {
        public GameObject LootPrefab;
        public int Amount;
        public float Weight;
        
        //These params for compute drop chances
        public float LowerChance;
        public float UpperChance;
    }
}
