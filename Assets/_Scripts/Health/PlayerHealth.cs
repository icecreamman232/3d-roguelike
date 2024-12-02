using JustGame.Scripts.ScriptableEvent;
using SGGames.Scripts.Data;
using SGGames.Scripts.Pickables;
using UnityEngine;

namespace SGGames.Scripts.Healths
{
    public class PlayerHealth : Health
    {
        [SerializeField] private PlayerHealthChangedEvent m_playerHealthChangedEvent;
        [SerializeField] private PickablePickedEvent m_healthPickedEvent;
        
        protected override void Start()
        {
            base.Start();
            UpdateHealthBar();
            m_healthPickedEvent.AddListener(OnPickHealthPotion);
        }
        
        private void OnDestroy()
        {
            m_healthPickedEvent.RemoveListener(OnPickHealthPotion);
        }
        
        private void OnPickHealthPotion(PickableType type, int amount)
        {
            CustomHealing(m_maxHealth);
        }

        private void CustomHealing(float amount)
        {
            m_curHealth = Mathf.Clamp(m_curHealth + amount, 0, m_maxHealth);
        }

        protected override void UpdateHealthBar()
        {
            m_playerHealthChangedEvent.Raise(m_curHealth, m_maxHealth);
            base.UpdateHealthBar();
        }
    }
}

