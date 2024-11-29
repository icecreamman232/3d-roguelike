using System.Collections;
using UnityEngine;

namespace SGGames.Scripts.Healths
{
    public class Health : MonoBehaviour
    {
        [SerializeField] protected float m_maxHealth;
        [SerializeField] protected float m_curHealth;
        
        protected bool m_isInvincible;

        protected virtual void Start()
        {
            m_curHealth = m_maxHealth;
        }
        
        public virtual void TakeDamage(float damage, GameObject source, float invincibilityDuration)
        {
            if (m_isInvincible) return;
            
            m_curHealth -= damage;

            Debug.Log($"<color=orange>{this.gameObject.name} took {damage} from {source.name}</color>");
            
            if (m_curHealth <= 0)
            {
                Kill();
            }
            else
            {
                StartCoroutine(OnInvincibility(invincibilityDuration));
            }
        }

        protected virtual void Kill()
        {
            m_isInvincible = true;
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
