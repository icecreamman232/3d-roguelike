
using UnityEngine;

namespace SGGames.Scripts.Pickables
{
    public enum PickableType
    {
        Experience_Shard,
        Coin,
        Health_Potion,
    }
    
    public interface Pickable
    {
        public void StartPick(Transform target) { }
        public void Picked() { }
    }
}

