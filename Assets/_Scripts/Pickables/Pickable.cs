
using UnityEngine;

namespace SGGames.Scripts.Pickables
{
    public enum PickableType
    {
        Experience_Shard,
        Coin,
    }
    
    public interface Pickable
    {
        public void StartPick(Transform target) { }
        public void Picked() { }
    }
}

