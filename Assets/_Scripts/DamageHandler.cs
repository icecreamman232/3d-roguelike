using System;
using SGGames.Scripts.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SGGames.Scripts.Weapons
{
    public class DamageHandler : MonoBehaviour
    {
        [SerializeField] protected float m_minDamage;
        [SerializeField] protected float m_maxDamage;
        [SerializeField] protected LayerMask m_damageableMask;
        [SerializeField] protected LayerMask m_nondamageableMask;

        public Action<GameObject> OnHitDamageable;
        public Action<GameObject> OnHitNonDamageable;
        
        
        protected virtual float GetDamage()
        {
            return Random.Range(m_minDamage, m_maxDamage);
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
            OnHitDamageable?.Invoke(target);
            Debug.Log("<color=orange>Hit Damageable</color>");
        }
        
        protected virtual void HitNonDamageable(GameObject target)
        {
            OnHitNonDamageable?.Invoke(target);
            Debug.Log("<color=orange>Hit NonDamageable</color>");
        }
    }
}

