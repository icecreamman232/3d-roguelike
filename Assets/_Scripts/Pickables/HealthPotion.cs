using SGGames.Scripts.Data;
using UnityEngine;

namespace SGGames.Scripts.Pickables
{
    public class HealthPotion : Pickable
    {
        [SerializeField] private PickablePickedEvent m_pickedEvent;
  
        public override void Picked()
        {
            m_pickedEvent.Raise(PickableType.Health_Potion,1);
            Destroy(this.gameObject);
        }
    }
}