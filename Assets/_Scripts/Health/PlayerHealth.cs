using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace SGGames.Scripts.Healths
{
    public class PlayerHealth : Health
    {
        [SerializeField] private PlayerHealthChangedEvent m_playerHealthChangedEvent;

        protected override void Start()
        {
            base.Start();
            UpdateHealthBar();
        }

        protected override void UpdateHealthBar()
        {
            m_playerHealthChangedEvent.Raise(m_curHealth, m_maxHealth);
            base.UpdateHealthBar();
        }
    }
}

