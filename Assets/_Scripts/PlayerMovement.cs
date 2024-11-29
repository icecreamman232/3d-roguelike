using UnityEngine;


namespace SGGames.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float m_moveSpeed;
        [SerializeField] private float m_rotationSpeed; 
        [SerializeField] private Transform m_modelTransform;
        [SerializeField] private LayerMask m_obstacleMask;
        
        private PlayerInputAction m_PlayerInputAction;
        private Vector3 m_direction;
        private Vector3 m_nextTargetPos;
        private Camera m_camera;
        private Animator m_animator;
        private bool m_hasKeyBoardInput;
        private int m_runningAnimParam = Animator.StringToHash("Running");

        private void Awake()
        {
            m_camera = Camera.main;
        }

        private void Start()
        {
            m_animator = GetComponentInChildren<Animator>();
            m_PlayerInputAction = new PlayerInputAction();
            m_PlayerInputAction.Player.Enable();
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
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                var pos = GetMouseWorldPosition();
                m_nextTargetPos = new Vector3(pos.x, transform.position.y, pos.z);
                m_direction = (m_nextTargetPos - transform.position).normalized;
                m_hasKeyBoardInput = false;
            }
            else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
            {
                m_direction = Vector3.zero;
                m_nextTargetPos = transform.position;
            }
            
            var keyboardInput = m_PlayerInputAction.Player.Keyboard_Move.ReadValue<Vector2>();
            
            if(keyboardInput!= Vector2.zero)
            {
                m_direction = Vector3.right * keyboardInput.y + Vector3.forward * -keyboardInput.x;
                m_hasKeyBoardInput = true;
            }
            else
            {
                if (m_hasKeyBoardInput)
                {
                    m_direction = Vector3.zero;
                }
            }

            if (m_hasKeyBoardInput)
            {
                if (m_direction != Vector3.zero)
                {
                    if (!HasObstacle())
                    {
                        m_modelTransform.forward = Vector3.Slerp(m_modelTransform.forward, m_direction, m_rotationSpeed * Time.deltaTime);
                        transform.Translate(m_direction * (Time.deltaTime * m_moveSpeed));
                    }
                    else
                    {
                        m_direction = Vector3.zero;
                        m_nextTargetPos = transform.position;
                    }
                }
            }
            else
            {
                if (m_nextTargetPos != transform.position)
                {
                    if (!HasObstacle())
                    {
                        m_modelTransform.forward = Vector3.Slerp(m_modelTransform.forward, m_direction, m_rotationSpeed * Time.deltaTime);
                        transform.position = Vector3.MoveTowards(transform.position, m_nextTargetPos, m_moveSpeed * Time.deltaTime);
                    }
                    else
                    {
                        m_direction = Vector3.zero;
                        m_nextTargetPos = transform.position;
                    }
                }
            }
            

            UpdateAnimator();
        }

        private bool HasObstacle()
        {
            var hit = Physics.BoxCast(transform.position,new Vector3(0.75f,1,0.75f)
                ,m_direction,Quaternion.Euler(0,0,0),1,m_obstacleMask);
            
            return hit;
        }

        private void UpdateAnimator()
        {
            m_animator.SetBool(m_runningAnimParam, m_direction != Vector3.zero);
        }
    }
}


