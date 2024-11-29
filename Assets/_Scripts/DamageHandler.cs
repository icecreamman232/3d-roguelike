using System;
using SGGames.Scripts.Healths;
using SGGames.Scripts.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SGGames.Scripts.Weapons
{
    public class DamageHandler : MonoBehaviour
    {
        [SerializeField] protected float m_minDamage;
        [SerializeField] protected float m_maxDamage;
        [Header("Damageable Settings")]
        [SerializeField] protected float m_damageableInvicibleDuration;
        [SerializeField] protected LayerMask m_damageableMask;
        [Header("Non-Damageable Settings")]
        [SerializeField] protected float m_nonDamageableInvicibleDuration;
        [SerializeField] protected LayerMask m_nondamageableMask;

        public Action<GameObject> OnHitDamageable;
        public Action<GameObject> OnHitNonDamageable;
        
        
        protected virtual float GetDamage()
        {
            return Mathf.Round(Random.Range(m_minDamage, m_maxDamage));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (LayerManager.IsInLayerMask(other.gameObject.layer, m_damageableMask))
            {
                HitDamageable(other.gameObject);
            }
            else if (LayerManager.IsInLayerMask(other.gameObject.layer, m_nondamageableMask))
            {
                HitNonDamageable(other.gameObject);
            }
        }
        
        protected virtual void HitDamageable(GameObject target)
        {
            var health = target.GetComponent<Health>();
            if (health == null) return;
            health.TakeDamage(GetDamage(),this.gameObject,m_damageableInvicibleDuration);
            OnHitDamageable?.Invoke(target);
        }
        
        protected virtual void HitNonDamageable(GameObject target)
        {
            OnHitNonDamageable?.Invoke(target);
        }
    }
}

