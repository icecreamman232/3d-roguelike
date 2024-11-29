using System;
using UnityEngine;

namespace SGGames.Scripts.Weapons
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] protected float m_speed;
        [SerializeField] protected Vector3 m_direction;
        [SerializeField] protected float m_lifeTime;
        [SerializeField] protected bool m_isAlive;
        [SerializeField] protected DamageHandler m_damageHandler;
        
        protected float m_curLiveTime;

        private void Awake()
        {
            m_damageHandler.OnHitDamageable += OnHitDamageable;
        }

        public virtual void SpawnBullet(Vector3 direction, Vector3 position, Quaternion rotation)
        {
            m_direction = direction;
            transform.rotation = rotation;
            transform.position = position;
            m_isAlive = true;
            m_curLiveTime = m_lifeTime;
        }

        protected virtual void Update()
        {
            if (!m_isAlive) return;
            
            m_curLiveTime -= Time.deltaTime;
            if (m_curLiveTime <= 0)
            {
                DestroyBullet();
            }
            
            transform.Translate(Vector3.forward * (m_speed * Time.deltaTime));
        }

        protected virtual void OnHitDamageable(GameObject damageable)
        {
            DestroyBullet();
        }

        protected virtual void DestroyBullet()
        {
            this.gameObject.SetActive(false);
            m_isAlive = false;
        }

        protected virtual void OnDestroy()
        {
            m_damageHandler.OnHitDamageable -= OnHitDamageable;
        }
    }
}

