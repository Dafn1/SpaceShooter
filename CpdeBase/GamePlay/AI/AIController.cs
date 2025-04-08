using System.Net;
using UnityEngine;
using Common;

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {
        public enum AIBehaviour
        {
            Null,
            Patrol,
            PatrolRoute
        }

        [SerializeField] private AIBehaviour m_AIBehaviour;

        [SerializeField] private AIPointPatrol m_PatrolPoint;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationLinear;//Перемещение врага

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular;//вращение врага

        [SerializeField] private float m_RandomSelectMovePointTime;

        [SerializeField] private float m_FindNewTargetTime;

        [SerializeField] private float m_ShootDelay;

        [SerializeField] private float m_EvadeRayLength;

        [SerializeField] private AIPointPatrol[] m_PatrolPoints;//тип данных в массива, нужен радиус
        private int m_CurrentMoveIndex;

        private SpaceShip m_SpaceShip;

        private Vector3 m_MovePosition;

        private Destructible m_SelectedTarget;

        private Timer m_RandomizeDirectionTimer;

        private Timer m_FireTimer;

        private Timer m_FindNewTargetTimer;

        [SerializeField] private AIController m_Enemy;
        [SerializeField] private Projectile m_Velocity;
        private Vector2 LeadPoint;
        [SerializeField] private GameObject LeadPointInUnity;
        private float TimeForLead;
        private int index;

       

        private Vector3 LeadOffset;

        private void Start()
        {
            m_SpaceShip = GetComponent<SpaceShip>();

            LeadOffset = new Vector3(1, 1, 0);

            InitTimers();

            m_CurrentMoveIndex = 0;
        }

        private void Update()
        {
            UpdateTimers();
            UpdateAI();
        }

        private void UpdateAI()
        {
            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                UpdateBehaviourPatrol();
                
            }
        }

        private void UpdateBehaviorPatrolRoute()
        {

            ActionFindNewMovePosition();
            ActionControlShip();
            ActionFindNewAttackTarget();
            var dir = m_SelectedTarget.transform.position - transform.position;
            RaycastHit2D hit =Physics2D.Raycast(transform.position, dir, Mathf.Infinity,LayerMask.GetMask("ShipDier"));
            Debug.DrawRay(transform.position, dir);

            if(hit.collider != null)
            {
                ActionFire();
            }

            ActionEvadeCollision();
        }
        private void UpdateBehaviourPatrol()
        {
            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                ActionFindNewMovePosition();
            }

            ActionFindNewMovePosition();
            ActionControlShip();
            ActionFindNewAttackTarget();
            ActionFire();
            ActionEvadeCollision();
        }

        private void ActionFindNewMovePosition(float projectileSpeedAmmount = 1.0f)
        {
            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                

                if (m_SelectedTarget != null)    
                {
                    
                    m_MovePosition = m_SelectedTarget.transform.position;
                }
                else
                {
                    if (m_PatrolPoint != null)
                    {
                        bool isInsidePatrolZone = (m_PatrolPoint.transform.position - transform.position).sqrMagnitude < m_PatrolPoint.Radius * m_PatrolPoint.Radius;

                        if (isInsidePatrolZone == true)
                        {
                            if (m_RandomizeDirectionTimer.IsFinished == true)
                            {
                                Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_PatrolPoint.Radius + m_PatrolPoint.transform.position;

                                m_MovePosition = newPoint;

                                m_RandomizeDirectionTimer.StartTimer(m_RandomSelectMovePointTime);
                            }
                        }
                        else
                        {
                            m_MovePosition = m_PatrolPoint.transform.position;
                        }
                    }
                }
            }
        }

        private void MakeLead(Destructible selectedTarget)
        {
            Vector3 rbVelositi= new Vector2(selectedTarget.GetComponent<Rigidbody2D>().velocity.x,selectedTarget.GetComponent <Rigidbody2D>().velocity.y);
            if (selectedTarget == null) return;
            Debug.Log(selectedTarget.Velocity);

            LeadPoint = selectedTarget.transform.position + rbVelositi * TimeForLead;
            LeadPointInUnity.transform.position = LeadPoint;
        }

        private void ActionEvadeCollision()
        {
            if (Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLength) == true)
            {
                m_MovePosition = transform.position + transform.right* 100.0f;
            }
        }

        private void ActionControlShip()
        {
            m_SpaceShip.ThrustControl = m_NavigationLinear;
            m_SpaceShip.TorqueControl = ComputeAlignTorqueNormalized(m_MovePosition, m_SpaceShip.transform) * m_NavigationAngular;
        }

        private const float MAX_ANGLE = 45.0f;

        private static float ComputeAlignTorqueNormalized(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;

            return -angle;
        }

        private void ActionFindNewAttackTarget()
        {
            if (m_FindNewTargetTimer.IsFinished == true)
            {
                m_SelectedTarget = FindNearestDestructableTarget();

                m_FindNewTargetTimer.StartTimer(m_ShootDelay);
            }
        }

        private void ActionFire()
        {
            if (m_SelectedTarget != null)
            {
                if (m_FireTimer.IsFinished == true)
                {
                    m_SpaceShip.Fire(TurretMode.Primary);

                    m_FireTimer.StartTimer(m_ShootDelay);
                }
            }
        }

        private Destructible FindNearestDestructableTarget()
        {
            float maxDist = float.MaxValue;

            Destructible potentialTarget = null;

            foreach (var v in Destructible.AllDestructibles)
            {
                if (v.GetComponent<SpaceShip>() == m_SpaceShip) continue;

                if (v.TeamId == Destructible.TeamIdNeutral) continue;

                if (v.TeamId == m_SpaceShip.TeamId) continue;

                float dist = Vector2.Distance(m_SpaceShip.transform.position, v.transform.position);

                if (dist < maxDist)
                {
                    maxDist = dist;

                    potentialTarget = v;
                }
            }

            return potentialTarget;
        }

        #region Timers
        private void InitTimers()
        {
            m_RandomizeDirectionTimer = new Timer(m_RandomSelectMovePointTime);
            m_FireTimer = new Timer(m_ShootDelay);
            m_FindNewTargetTimer = new Timer(m_FindNewTargetTime);
        }

        private void UpdateTimers()
        {
            m_RandomizeDirectionTimer.RemoveTime(Time.deltaTime);
            m_FireTimer.RemoveTime(Time.deltaTime);
            m_FindNewTargetTimer.RemoveTime(Time.deltaTime);
        }

        public void SetPatrolBehaviour(AIPointPatrol point)
        {
            m_AIBehaviour = AIBehaviour.Patrol;
            m_PatrolPoint = point;
        }
        #endregion

    }
}