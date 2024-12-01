using System;
using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(fileName = "New Player Level", menuName = "SGGames/Player Level")]
    public class PlayerLevelData : ScriptableObject   
    {
        [SerializeField] private LevelData[] m_levelData;

        public int MaxLevel=> m_levelData.Length;
        
        public int GetMaxXPOfLevel(int level)
        {
            return m_levelData[level-1].MaxXP;
        }
    }

    [Serializable]
    public struct LevelData
    {
        public int Level;
        public int MaxXP;
    }
}


