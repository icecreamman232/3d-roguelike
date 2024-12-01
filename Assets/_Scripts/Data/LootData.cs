using System;
using UnityEngine;


namespace SGGames.Scripts.Pickables
{
    [CreateAssetMenu(menuName = "SGGames/Data/Loot Data",fileName = "New Loot Data")]
    public class LootData : ScriptableObject
    {
        [SerializeField] private LootTable[] m_LootTables;

        public LootTable[] LootTables => m_LootTables;
    }

    [Serializable]
    public struct LootTable
    {
        public GameObject LootPrefab;
        public int Amount;
    }
}
