using System.Collections;
using UnityEngine;

namespace SGGames.Scripts.Healths
{
    public class EnemyHealth : Health
    {
        protected int m_deathAnimParam = Animator.StringToHash("Dead");

        public virtual void Initialize(float multiplier)
        {
            m_maxHealth *= multiplier;
            m_curHealth = m_maxHealth;
        }
        
        protected override IEnumerator OnDeathFlow()
        {
            m_isInvincible = true;
            m_animator.SetBool(m_deathAnimParam,true);
            yield return new WaitForSeconds(m_delayBeforeDeath);
            Destroy(this.gameObject);
        }
    }
}

