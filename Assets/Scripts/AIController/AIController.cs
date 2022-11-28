using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace SpaceShooter {

    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {
    // Start is called before the first frame update
   
        // ���������.
        public enum AIBehaviour
        {
            Null,
            Patrol
        }


        // ��� ��������� ���������. 
        [SerializeField] private AIBehaviour m_AIBehaviour;
        // ��������� � ������� 
        [SerializeField] private AIPointPatrol m_PatrolPoint;

   //     [SerializeField] private AIPointRay m_AIPointRay;

        // �������� �����������.
        [Range(0.0F, 1.0F)]
        [SerializeField] private float m_NavigationLiner;
        // �������� ��������. 
        [Range(0.0F, 1.0F)]
        [SerializeField] private float m_NavigationAngular;
        //������ ����� ��� 3� ����
        [SerializeField] private float m_RandomSelectMovePointTime;

        [SerializeField] private float m_FindNewTargetTime;

        [SerializeField] private float m_ShootDelay;
        // ����� ��������. 
        [SerializeField] private float m_EvadeRayLength;

        [SerializeField] private GameObject GameObject;

        [SerializeField] bool Make;

        // 3 ����������� ��� ��������� 
        // ������ �� ���� ...)
        private SpaceShip m_SpaceShip;
        // �����, �������� � ����������� �����. 
        private Vector3 m_MovePosition;
        // �������� ���� 
        private Destructible m_SelectedTarget;
        // �������� ������ 
        private Timer m_RandomizeDirectionTimer;

        private Timer m_FireTimer;
        private Timer m_FindNewTargetTimer;


        private void Start()
        {
            m_SpaceShip = GetComponent<SpaceShip>();



            InitTimers();
        }

        

        private void Update()
        {
            UpdateTimers();

            UpdateAI();
        }

        [SerializeField] float maxDist;
        private Destructible FindNearestDestrutableTarget()
        {
         //   float maxDist = float.MaxValue; 

            Destructible potentialTarget = null;

            foreach (var entity in Destructible.AllDestructibles)
            {
                if (entity.GetComponent<SpaceShip>() == m_SpaceShip) continue;

                if (entity.TeamId == Destructible.TeamIdNeutral) continue;

                if (entity.TeamId == m_SpaceShip.TeamId) continue;

                float distance = Vector2.Distance(m_SpaceShip.transform.position, entity.transform.position);
                Debug.Log(distance + "1");
                if (distance < maxDist)
                {
                    maxDist = distance;
                    potentialTarget = entity;

                }
                if (potentialTarget == null)
                {
                    maxDist = 30;
                }
                Debug.Log(distance);
            }
            return potentialTarget;

            
        }
        #region Timers
        //������������ �������
        private void InitTimers()
        {
            m_RandomizeDirectionTimer = new Timer(m_RandomSelectMovePointTime);

            m_FireTimer = new Timer(m_ShootDelay);

            m_FindNewTargetTimer = new Timer(m_FindNewTargetTime);
        }
        // ���������� �������� 
        private void UpdateTimers()
        {
            m_RandomizeDirectionTimer.RemoveTime(Time.deltaTime);
            m_FireTimer.RemoveTime(Time.deltaTime);
            m_FindNewTargetTimer.RemoveTime(Time.deltaTime);
        }
        // ����� ���������� 
        public void SetPatrolBehavior(AIPointPatrol point)
        {
            m_AIBehaviour = AIBehaviour.Patrol;
            m_PatrolPoint = point;
        }

        public void SetPatrolPoint(AIPointPatrol point)
        {
            m_PatrolPoint = point;
        }
        #endregion
        private void UpdateAI()
        {
            if (m_AIBehaviour == AIBehaviour.Null)
            {

            }
            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                UpdateBehaviourPatrol();
            }


        }
        [SerializeField] private bool _ActionFindMovePositionPoint;
        [SerializeField] private bool _ActionFindMovePosition;
        private void UpdateBehaviourPatrol()
        {
            if (_ActionFindMovePositionPoint == true)
            {
                ActionFindMovePositionPoint();
            }
            if (_ActionFindMovePosition == true)
            {
                ActionFindMovePosition();
            }
            ActionControlShip();
            ActionFindNewAttckTarget();
            ActionFire();
            ActionEvadeCollision();
        }



        private void ActionFindNewAttckTarget()
        {

            if (m_FindNewTargetTimer.IsFinished == true)
            {
                m_SelectedTarget = FindNearestDestrutableTarget();

                m_FindNewTargetTimer.Start(m_ShootDelay);
            }

        }

        private void ActionFire()
        {
            if (m_SelectedTarget != null)
            {
                if (m_FireTimer.IsFinished == true)
                {
                    m_SpaceShip.Fire(TurretMode.Primary);

                    m_FireTimer.Start(m_ShootDelay);
                }
            }
        }

        private void ActionControlShip()
        {
            m_SpaceShip.ThrustControl = m_NavigationLiner;

            m_SpaceShip.TorqueControl = ComputeAliginTorqueNormalized(m_MovePosition, m_SpaceShip.transform) * m_NavigationAngular;
        }

        private const float MAX_ANGLE = 45.0F;
        //������� ������� � ���� ���������� ������� 
        private static float ComputeAliginTorqueNormalized(Vector3 targetPosition, Transform ship)
        {
            // �������� � ��������� ���������� ����������. � ������ ������
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);
            // ���� ����� ����� ��������� SignedAngle- ���� �� ������ (������ ������� � ����) �������� ���� ����� ������ � �����. 
            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);


            // ������������ ��� �� �� ���� �� -45 �� 45
            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;

            //Debug.Log(angle);

            return -angle;
        }


        [SerializeField] private List<Transform> m_targets;

        [SerializeField] private float m_SpeedPoint;

        private int m_indexTarget;

        private void ActionFindMovePositionPoint()
        {
            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                if (m_SelectedTarget != null)
                {
                    if (Make == true)
                    {
                        MakeLead();
                    }
                    else
                    m_MovePosition = m_SelectedTarget.transform.position;

                }

                else

                {
                    //if (m_PatrolPoint != null)
                    //{// � ���� �� ��� (���������� - ������� ��������� � � z)
                    //    bool isInsidePatrolZone = (m_PatrolPoint.transform.position - transform.position).sqrMagnitude
                    //        < m_PatrolPoint.Radius * m_PatrolPoint.Radius;
                    // 4f - ��������
                    if ((m_targets[m_indexTarget].transform.position - m_SpaceShip.transform.position).sqrMagnitude < 50f)
                    {
                        m_indexTarget++;
                        if (m_indexTarget >= m_targets.Count)
                        {
                            m_indexTarget = 0;
                        }


                    }

                    m_MovePosition = Vector2.MoveTowards(transform.position, m_targets[m_indexTarget].transform.position,
                                     m_SpeedPoint * Time.deltaTime);
                    //   s
                }
            }
        }


        private const float K = 0.5f;

        private void MakeLead()
        {
            m_MovePosition = m_SelectedTarget.transform.position + m_SelectedTarget.transform.up * K;
        }

        private void ActionFindMovePosition()
        {
            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                // ���� ������� ������ �� ����, �� ��������� � �������. 
                if (m_SelectedTarget != null)
                {
                    if (Make == true)
                    {
                        MakeLead();
                    }
                    else
                        m_MovePosition = m_SelectedTarget.transform.position;
                }
                else
                { // ���� �������(���� �� ����)
                    if (m_PatrolPoint != null)
                    {// � ���� �� ��� (���������� - ������� ��������� � � z)
                        bool isInsidePatrolZone = (m_PatrolPoint.transform.position - transform.position).sqrMagnitude
                            < m_PatrolPoint.Radius * m_PatrolPoint.Radius;

                        if (isInsidePatrolZone == true)
                        {
                            if (m_RandomizeDirectionTimer.IsFinished == true)
                            {
                                // ��������� ����� ���� ������ ����� UnityEngine.Random.onUnitSphere -����� � ����� � �������� ������������ ������.
                                Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_PatrolPoint.Radius + m_PatrolPoint.transform.position;



                                m_MovePosition = newPoint;
                                // m_RandomizeDirectionTimer �  m_RandomSelectMovePointTime ������ !!!!!!!!!!!!!
                                m_RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
                            }
                        }
                        else
                        { // ���������� � ����
                            m_MovePosition = m_PatrolPoint.transform.position;
                        }
                    }
                }
            }
        }


        private void ActionEvadeCollision()
        {

            // ���� ��������� ������� 
            if (Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLength) == true)
            {
                //������� �� �����. 
                m_MovePosition = transform.position + transform.right * 100.0f;


            }
        }
    }
}
