
using System.Collections;
using SGGames.Scripts.Data;
using UnityEngine;

namespace SGGames.Scripts.Pickables
{
    public class ExperienceShard : MonoBehaviour, Pickable
    {
        [SerializeField] private float m_flySpeed;
        [SerializeField] private int m_expValue;
        [SerializeField] private PickablePickedEvent m_pickedEvent;
        private bool m_hasStartedPicking;
        private bool m_canFly;
        private Transform m_playerTransform;
        private float m_delayBeforeFly = 0.5f;

        private void OnEnable()
        {
            m_hasStartedPicking = false;
        }

        public void StartPick(Transform target)
        {
            if (m_hasStartedPicking) return;
            m_hasStartedPicking = true;
            m_playerTransform = target;
            StartCoroutine(DelayBeforeFly());
        }

        public void Picked()
        {
            m_pickedEvent.Raise(PickableType.Experience_Shard,m_expValue);
            Destroy(this.gameObject);
        }

        private IEnumerator DelayBeforeFly()
        {
            m_canFly = false;
            yield return new WaitForSeconds(m_delayBeforeFly);
            m_canFly = true;
        }

        private void Update()
        {
            if (!m_canFly) return;
            if (m_playerTransform == null) return;
            transform.position = Vector3.MoveTowards(transform.position,m_playerTransform.position , m_flySpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, m_playerTransform.position) < 0.5f)
            {
                Picked();
            }
        }
    }
}

