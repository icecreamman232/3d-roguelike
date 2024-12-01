using System;
using JustGame.Scripts.ScriptableEvent;
using SGGames.Scripts.Abilities;
using UnityEngine;

namespace SGGames.Scripts.Players
{
    public class PlayerAbilityHandler : MonoBehaviour
    {
        [SerializeField] private bool m_canUpdate;
        [SerializeField] private AbilityCooldownChangedEvent[] m_abilityCooldownChangedEvents;
        [SerializeField] private AbilityBase[] m_abilities;
        
        private void Start()
        {
            //Init default ability of hero
            if (m_abilities[0].AbilityID < AbilityID.LAST_WEAPON_ABILITY)
            {
                ((WeaponAbility)m_abilities[0]).AssignWeaponAttachment(transform);
                m_abilities[0].OnEquipAbility();
            }
            else if(m_abilities[0].AbilityID > AbilityID.LAST_WEAPON_ABILITY)
            {
                //TODO:Init other type of ability here
            }

            m_canUpdate = true;
        }

        private void Update()
        {
            if (!m_canUpdate) return;
            
            for (int i = 0; i < m_abilities.Length; i++)
            {
                if (m_abilities[i] != null)
                {
                    m_abilities[i].OnAbilityUpdate();
                    m_abilityCooldownChangedEvents[i].Raise(m_abilities[i].Cooldown.current,m_abilities[i].Cooldown.max);
                }
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < m_abilities.Length; i++)
            {
                if (m_abilities[i] != null)
                {
                    m_abilities[i].CleanUp();
                }
            }
        }
    }
}

