using System.Collections;
using UnityEngine;

namespace SGGames.Scripts.Healths
{
    public class EnemyHealth : Health
    {
        protected int m_deathAnimParam = Animator.StringToHash("Dead");
        
        protected override IEnumerator OnDeathFlow()
        {
            m_isInvincible = true;
            m_animator.SetBool(m_deathAnimParam,true);
            yield return new WaitForSeconds(m_delayBeforeDeath);
            this.gameObject.SetActive(false);
        }
    }
}

