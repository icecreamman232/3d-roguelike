using UnityEngine;

namespace SGGames.Scripts.Abilities
{
    public class AbilityBase : ScriptableObject
    {
        [Header("Base Settings")] 
        [SerializeField] protected AbilityID m_abilityID;
        [SerializeField] protected float m_baseCooldown;
        [SerializeField] protected bool m_isEquiped;

        protected float m_cooldownTimer;
        protected float m_currentCooldown;
        
        public AbilityID AbilityID => m_abilityID;
        public (float current, float max) Cooldown => (m_cooldownTimer, m_currentCooldown);
        
        public virtual void OnEquipAbility()
        {
            m_isEquiped = true;
            m_currentCooldown = m_baseCooldown;
            m_cooldownTimer = m_currentCooldown;
        }

        public virtual void OnAbilityUpdate()
        {
            if (!m_isEquiped) return;
            m_cooldownTimer -= Time.deltaTime;
            if (m_cooldownTimer <= 0)
            {
                m_cooldownTimer = m_currentCooldown;
                UseAbility();
            }
        }

        protected virtual void UseAbility()
        {
            
        }
        
        public virtual void OnLevelUpAbility(){}

        public virtual void CleanUp()
        {
            m_isEquiped = false;
        }
    }

    public enum AbilityID
    {
        KNIGHT_SWORD = 0,
        
        
        
        LAST_WEAPON_ABILITY =50,
        RECOVERY_HP = 51,
    }
}

