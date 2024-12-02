using UnityEngine;

namespace SGGames.Scripts.Abilities
{
    public enum AbilityState
    {
        DISABLED,
        READY,
        USE,
        COOLDOWN,
    }
    public class AbilityBase : ScriptableObject
    {
        [Header("Base Settings")] 
        [SerializeField] protected AbilityID m_abilityID;
        [SerializeField] protected AbilityState m_abilityState = AbilityState.DISABLED;
        [SerializeField] protected float m_baseCooldown;
        
        protected float m_cooldownTimer;
        protected float m_currentCooldown;
        
        public AbilityID AbilityID => m_abilityID;
        public (float current, float max) Cooldown => (m_cooldownTimer, m_currentCooldown);
        
        public virtual void OnEquipAbility()
        {
            m_abilityState = AbilityState.READY;
            m_currentCooldown = m_baseCooldown;
            m_cooldownTimer = 0;
        }

        public virtual void OnAbilityUpdate()
        {
            if (m_abilityState == AbilityState.DISABLED) return;

            switch (m_abilityState)
            {
                case AbilityState.DISABLED:
                    break;
                case AbilityState.READY:
                    if (CanUseAbility())
                    {
                        m_abilityState = AbilityState.USE;
                    }
                    break;
                case AbilityState.USE:
                    m_cooldownTimer = m_currentCooldown;
                    UseAbility();
                    m_abilityState = AbilityState.COOLDOWN;
                    break;
                case AbilityState.COOLDOWN:
                    m_cooldownTimer -= Time.deltaTime;
                    if (m_cooldownTimer <= 0)
                    {
                        m_abilityState = AbilityState.READY;
                    }
                    break;
            }
        }

        protected virtual bool CanUseAbility()
        {
            return false;
        }

        protected virtual void UseAbility()
        {
            
        }
        
        public virtual void OnLevelUpAbility(){}

        public virtual void CleanUp()
        {
            m_abilityState = AbilityState.DISABLED;
            m_cooldownTimer = 0;
            m_currentCooldown = 0;
        }
    }

    public enum AbilityID
    {
        KNIGHT_SWORD = 0,
        
        
        
        LAST_WEAPON_ABILITY =50,
        RECOVERY_HP = 51,
    }
}

