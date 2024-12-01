using System;
using UnityEngine;


namespace JustGame.Scripts.ScriptableEvent
{
    [CreateAssetMenu(menuName = "SGGames/Event/Player Health Changed Event")]
    public class PlayerHealthChangedEvent : ScriptableObject
    {
        protected Action<float,float> m_listeners;

        public void AddListener(Action<float,float> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<float,float> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(float current, float max)
        {
            m_listeners?.Invoke(current, max);
        }
    }
}