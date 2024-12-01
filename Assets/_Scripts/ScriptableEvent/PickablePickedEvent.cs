using System;
using SGGames.Scripts.Pickables;
using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(menuName = "SGGames/Event/Pickable Picked Event")]
    public class PickablePickedEvent : ScriptableObject
    {
        protected Action<PickableType, int> m_listeners;

        public void AddListener(Action<PickableType, int> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<PickableType, int> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(PickableType type,int amount)
        {
            m_listeners?.Invoke(type,amount);
        }
    }
}

