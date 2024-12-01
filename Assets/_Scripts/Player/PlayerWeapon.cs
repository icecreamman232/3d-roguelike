using System;
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

        public bool CanShoot()
        {
            m_numTarget = FindTargetInRange();
            return m_numTarget > 0;
        }

        public virtual void UseWeapon()
        {
            if (m_numTarget <= 0) return;
            
            Shoot(m_numTarget >= m_numBulletPerShot ? m_numBulletPerShot : m_numTarget);
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
            var resultArray = new Collider[10];
            
            var numTarget = Physics.OverlapSphereNonAlloc(transform.position, m_attackRange, resultArray, m_targetMask);
            
            // Sort the result array based on distance to the current transform position
            Array.Sort(resultArray, (collider1, collider2) =>
            {
                if (collider1 == null || collider2 == null)
                    return 0;

                float distance1 = Vector3.Distance(collider1.transform.position, transform.position);
                float distance2 = Vector3.Distance(collider2.transform.position, transform.position);

                return distance1.CompareTo(distance2);
            });

            //Copy sorted result array to stored array
            for (int i = 0; i < m_hitColliders.Length; i++)
            {
                m_hitColliders[i] = resultArray[i];
            }
            
            return numTarget;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position,m_attackRange);
        }
    }
}
