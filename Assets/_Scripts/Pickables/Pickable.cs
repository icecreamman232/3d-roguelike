using System.Collections;
using UnityEngine;

namespace SGGames.Scripts.Pickables
{
    public enum PickableType
    {
        Experience_Shard,
        Coin,
        Health_Potion,
    }
    
    public class Pickable : MonoBehaviour
    {
        [SerializeField] private float m_flySpeed;
        protected bool m_hasStartedPicking;
        protected bool m_canFly;
        protected Transform m_playerTransform;
        protected float m_delayBeforeFly = 0.5f;
        
        protected void OnEnable()
        {
            m_hasStartedPicking = false;
        }

        public virtual void StartPick(Transform target)
        {
            if (m_hasStartedPicking) return;
            m_hasStartedPicking = true;
            m_playerTransform = target;
            StartCoroutine(DelayBeforeFly());
        }
        public virtual void Picked() { }
        
        protected virtual void Update()
        {
            if (!m_canFly) return;
            if (m_playerTransform == null) return;
            transform.position = Vector3.MoveTowards(transform.position,m_playerTransform.position , m_flySpeed * Time.deltaTime);
    
            if (Vector3.Distance(transform.position, m_playerTransform.position) < 0.5f)
            {
                Picked();
            }
        }
        
        protected virtual IEnumerator DelayBeforeFly()
        {
            m_canFly = false;
            yield return new WaitForSeconds(m_delayBeforeFly);
            m_canFly = true;
        }
    }
}

