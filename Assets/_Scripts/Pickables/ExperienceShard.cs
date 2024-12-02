using SGGames.Scripts.Data;
using UnityEngine;

namespace SGGames.Scripts.Pickables
{
    public class ExperienceShard : Pickable
    {
        [SerializeField] private int m_expValue;
        [SerializeField] private PickablePickedEvent m_pickedEvent;
        
        public override void Picked()
        {
            m_pickedEvent.Raise(PickableType.Experience_Shard,m_expValue);
            Destroy(this.gameObject);
        }
    }
}

