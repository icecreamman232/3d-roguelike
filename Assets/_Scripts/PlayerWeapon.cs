using UnityEngine;

namespace SGGames.Scripts.Weapons
{
    public class PlayerWeapon : Weapon
    {
        [SerializeField] protected float m_numBulletPerShot;
        [SerializeField] protected float m_delayBetween2Shots;
        [SerializeField] protected Vector3 m_bulletOffsetPosition;
        
        private float m_delayTime;

        private void Start()
        {
            m_delayTime = m_delayBetween2Shots;
            StartShooting();
        }

        private void Update()
        {
            if (!m_canShoot) return;
            
            if (m_delayTime <= 0)
            {
                Shoot();
                m_delayTime = m_delayBetween2Shots;
            }
            else
            {
                m_delayTime -= Time.deltaTime;
            }
        }

        private void Shoot()
        {
            for (int i = 0; i < m_numBulletPerShot; i++)
            {
                var bulletObject = m_bulletPooler.GetPooledGameObject();
                var bullet = bulletObject.GetComponent<Bullet>();
                var rotation = Quaternion.LookRotation(transform.forward);
                bullet.SpawnBullet(transform.forward,transform.position + m_bulletOffsetPosition,rotation);
            }
        }
    }
}
