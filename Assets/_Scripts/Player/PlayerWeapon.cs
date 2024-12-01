using System;
using System.Linq;
using UnityEngine;

namespace SGGames.Scripts.Weapons
{
    public class PlayerWeapon : Weapon
    {
        [SerializeField] protected int m_numBulletPerShot;
        [SerializeField] protected float m_delayBetween2Shots;
        [SerializeField] protected Vector3 m_bulletOffsetPosition;
        [SerializeField] protected float m_attackRange;
        [SerializeField] protected LayerMask m_targetMask;
        
        private float m_delayTime;
        private int m_numTarget;
        private Collider[] m_hitColliders;

        private void Start()
        {
            m_hitColliders = new Collider[m_numBulletPerShot];
            m_delayTime = m_delayBetween2Shots;
            StartShooting();
        }

        // private void Update()
        // {
        //     if (!m_canShoot) return;
        //     
        //     if (m_delayTime <= 0)
        //     {
        //         m_numTarget = FindTargetInRange();
        //         if (m_numTarget >= m_numBulletPerShot)
        //         {
        //             Shoot(m_numTarget >= m_numBulletPerShot ? m_numBulletPerShot : m_numTarget);
        //             m_delayTime = m_delayBetween2Shots;
        //         }
        //     }
        //     else
        //     {
        //         m_delayTime -= Time.deltaTime;
        //     }
        // }

        public virtual void UseWeapon()
        {
            m_numTarget = FindTargetInRange();
            if (m_numTarget >= m_numBulletPerShot)
            {
                Shoot(m_numTarget >= m_numBulletPerShot ? m_numBulletPerShot : m_numTarget);
            }
        }

        private void Shoot(int numShots)
        {
            if (numShots <= 0) return;

            for (int i = 0; i < numShots; i++)
            {
                var bulletObject = m_bulletPooler.GetPooledGameObject();
                var bullet = bulletObject.GetComponent<Bullet>();
                var direction = (m_hitColliders[i].transform.position - transform.position).normalized;
                var rotation = Quaternion.LookRotation(direction);
                bullet.SpawnBullet(direction,transform.position + m_bulletOffsetPosition,rotation);
            }
            
        }

        private int FindTargetInRange()
        {
            m_hitColliders = new Collider[m_numBulletPerShot];
            var numTarget = Physics.OverlapSphereNonAlloc(transform.position, m_attackRange, m_hitColliders, m_targetMask);
            
            // Sort the m_hitColliders array based on distance to the current transform position
            Array.Sort(m_hitColliders, (collider1, collider2) =>
            {
                if (collider1 == null || collider2 == null)
                    return 0;

                float distance1 = Vector2.Distance(collider1.transform.position, transform.position);
                float distance2 = Vector2.Distance(collider2.transform.position, transform.position);

                return distance1.CompareTo(distance2);
            });
            
            return numTarget;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position,m_attackRange);
        }
    }
}
