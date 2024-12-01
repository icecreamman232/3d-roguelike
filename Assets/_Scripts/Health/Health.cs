using System;
using System.Collections;
using UnityEngine;

namespace SGGames.Scripts.Healths
{
    public class Health : MonoBehaviour
    {
        [SerializeField] protected float m_maxHealth;
        [SerializeField] protected float m_curHealth;
        [SerializeField] protected float m_delayBeforeDeath;
        [SerializeField] protected Animator m_animator;
        [SerializeField] protected bool m_cheatNoDamage;
        
        protected bool m_isInvincible;
        
        public Action OnDeath;

        protected virtual void Start()
        {
            m_curHealth = m_maxHealth;
        }
        
        public virtual void TakeDamage(float damage, GameObject source, float invincibilityDuration)
        {
            if (m_isInvincible) return;

            if (m_cheatNoDamage) return;
            m_curHealth -= damage;
            UpdateHealthBar();

            //Debug.Log($"<color=orange>{this.gameObject.name} took {damage} from {source.name}</color>");
            
            if (m_curHealth <= 0)
            {
                Kill();
            }
            else
            {
                StartCoroutine(OnInvincibility(invincibilityDuration));
            }
        }

        protected virtual void UpdateHealthBar()
        {
            
        }

        protected virtual void Kill()
        {
            OnDeath?.Invoke();
            StartCoroutine(OnDeathFlow());
        }

        protected virtual IEnumerator OnDeathFlow()
        {
            m_isInvincible = true;
            yield return new WaitForSeconds(m_delayBeforeDeath);
            this.gameObject.SetActive(false);
        }

        protected virtual IEnumerator OnInvincibility(float duration)
        {
            m_isInvincible = true;
            yield return new WaitForSeconds(duration);
            m_isInvincible = false;
        }
    }
}
