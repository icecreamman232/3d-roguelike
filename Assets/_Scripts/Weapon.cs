using SGGames.Scripts.Managers;
using UnityEngine;

namespace SGGames.Scripts.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] protected bool m_canShoot;
        [SerializeField] protected ObjectPooler m_bulletPooler;
        
        public virtual void StartShooting()
        {
            m_canShoot = true;
        }

        public virtual void StopShooting()
        {
            m_canShoot = false;
        }
        
    }
}
