using SGGames.Scripts.Data;
using UnityEngine;

namespace SGGames.Scripts.Pickables
{
    public class Coin : Pickable
    {
        [SerializeField] private int m_coinValue;
        [SerializeField] private PickablePickedEvent m_pickedEvent;
  
        public override void Picked()
        {
            m_pickedEvent.Raise(PickableType.Coin,m_coinValue);
            Destroy(this.gameObject);
        }
    }
}

