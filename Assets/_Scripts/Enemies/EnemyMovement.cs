using SGGames.Scripts.Healths;
using UnityEngine;

namespace SGGames.Scripts.Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] protected float m_moveSpeed;
        [SerializeField] protected float m_rotationSpeed;
        [SerializeField] protected bool m_canMove;
        [SerializeField] protected Transform m_target;
        [SerializeField] protected Vector3 m_direction;
        [SerializeField] protected Animator m_animator;

        protected EnemyHealth m_health;
        protected int m_runningAnimParam = Animator.StringToHash("Running");
        
        protected virtual void Start()
        {
            m_health = GetComponent<EnemyHealth>();
            m_health.OnDeath += OnEnemyDeath;
        }

        public virtual void Initialize(Transform target)
        {
            m_target = target;
            m_canMove = true;
        }
        
        protected virtual void OnEnemyDeath()
        {
            m_health.OnDeath -= OnEnemyDeath;
            m_canMove = false;
        }

        protected virtual void Update()
        {
            if (!m_canMove) return;

            if (m_target == null) return;
            
            m_direction = (m_target.position - transform.position).normalized;
            
            transform.forward = Vector3.Slerp(transform.forward, m_direction, m_rotationSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * (Time.deltaTime * m_moveSpeed));

            UpdateAnimator();
        }

        protected virtual void UpdateAnimator()
        {
            m_animator.SetBool(m_runningAnimParam, m_direction!= Vector3.zero);
        }
    }
}

