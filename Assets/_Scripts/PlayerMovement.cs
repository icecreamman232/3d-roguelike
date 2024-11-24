using System;
using UnityEngine;

namespace SGGames.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float m_moveSpeed;
        [SerializeField] private float m_rotationSpeed; 
        [SerializeField] private Transform m_modelTransform;
        
        private Vector3 m_direction;
        private Vector3 m_nextTargetPos;
        private Camera m_camera;
        private Animator m_animator;
        private int m_runningAnimParam = Animator.StringToHash("Running");

        private void Awake()
        {
            m_camera = Camera.main;
        }

        private void Start()
        {
            m_animator = GetComponentInChildren<Animator>();
        }

        private Vector3 GetMouseWorldPosition()
        {
            var ray = m_camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                return hit.point;
            }
            return Vector3.zero;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var pos = GetMouseWorldPosition();
                m_nextTargetPos = new Vector3(pos.x, transform.position.y, pos.z);
                m_direction = (m_nextTargetPos - transform.position).normalized;
            }

            if (m_nextTargetPos != transform.position)
            {
                transform.forward = Vector3.Slerp(transform.forward, m_direction, m_rotationSpeed * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, m_nextTargetPos, m_moveSpeed * Time.deltaTime);
            }

            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            m_animator.SetBool(m_runningAnimParam, m_nextTargetPos != transform.position);
        }
    }
}


