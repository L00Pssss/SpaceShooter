using UnityEngine;
using UnityEngine.EventSystems;

namespace SpaceShooter
{
    public class MovementController : MonoBehaviour
    {
        public enum ControlMode
        {
            Keyboard,
            Mobile,
            Other
        }
        /// <summary>
        ///  сс≥лка на корабель 
        /// </summary>
        [SerializeField] private SpaceShip m_TargetShip;


        [SerializeField] private VirtualJoystick m_MobileJoystick;
        // ”правление чем ...
        [SerializeField] private ControlMode m_ControlMode;

        private void SetTargetShipp(SpaceShip ship) => m_TargetShip = ship;

        [SerializeField] private PointerClickHold m_MobileFirePrimary;

        [SerializeField] private PointerClickHold m_MobileFireSecondary;

        private void Start()
        {
            if (m_ControlMode == ControlMode.Keyboard)
            {
                m_MobileJoystick.gameObject.SetActive(false);
                //m_MobileFirePrimary.gameObject.SetActive(false);
                //m_MobileFireSecondary.gameObject.SetActive(false);
            }
            else
            {
                m_MobileJoystick.gameObject.SetActive(true);

                //m_MobileFirePrimary.gameObject.SetActive(true);
                //m_MobileFireSecondary.gameObject.SetActive(true);
            }


            //if (Application.isMobilePlatform)
            //{
            //    m_ControlMode = ControlMode.Mobile;
            //    m_MobileJoystick.gameObject.SetActive(true);
            //}
            //else
            //{
            //    m_ControlMode = ControlMode.Keyboard;
            //    m_MobileJoystick.gameObject.SetActive(false);
            //}
        }

        private void Update()
        {

            // что б≥ не б≥ло ошибки. при уничтожении коробл€. 
            if (m_TargetShip == null) return;



            if (m_ControlMode == ControlMode.Keyboard)
            {
                ControlKeyboard();

            }

            if (m_ControlMode == ControlMode.Mobile)
            {
                ControlMobile();

            }



        }

        private void ControlMobile()
        {

            var dir = m_MobileJoystick.Value;
            m_TargetShip.ThrustControl = dir.y;
            m_TargetShip.TorqueControl = -dir.x;

            if (m_MobileFirePrimary.IsHold == true)
            {
                m_TargetShip.Fire(TurretMode.Primary);
            }

            if (m_MobileFireSecondary.IsHold == true)
            {
                m_TargetShip.Fire(TurretMode.Secondary);
            }

            //Vector3 dir = m_MobileJoystick.Value;

            //var dot = Vector2.Dot(dir, m_TargetShip.transform.up);
            //var dot2 = Vector2.Dot(dir, m_TargetShip.transform.right);


            //m_TargetShip.ThrustControl = Mathf.Max(0, dot);
            //m_TargetShip.TorqueControl =  -dot2;


        }

        private void ControlKeyboard()
        {
            float thrust = 0;
            float torque = 0;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                m_TargetShip.trail.emitting = true;
                thrust = 1.0f;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                thrust = -1.0f;
                 m_TargetShip.trail.emitting = false;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                torque = 1.0f;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                torque = -1.0f;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                m_TargetShip.Fire(TurretMode.Primary);
            }

            if (Input.GetKey(KeyCode.X))
            {
                m_TargetShip.Fire(TurretMode.Secondary);
            }

            m_TargetShip.ThrustControl = thrust;
            m_TargetShip.TorqueControl = torque;

        }

        //public void SetTargetShip(SpaceShip ship) => m_TargetShip = ship; такой же метод ниже 
        public void SetTargetShip(SpaceShip ship)
        {
            m_TargetShip = ship;
        }

        //private void ControlKeybAndJoyst()
        //{
        //    float thrust = 0;
        //    float torque = 0;

        //    if (Input.GetKey(KeyCode.UpArrow))
        //        thrust = 1.0f;

        //    if (Input.GetKey(KeyCode.DownArrow))
        //        thrust = -1.0f;

        //    if (Input.GetKey(KeyCode.LeftArrow))
        //        torque = 1.0f;

        //    if (Input.GetKey(KeyCode.RightArrow))
        //        torque = -1.0f;

        //    m_TargetShipK.ThrustControl = thrust;
        //    m_TargetShipK.TorqueControl = torque;


        //    Vector3 dir = m_MobileJoystick.Value;

        //    var dot = Vector2.Dot(dir, m_TargetShip.transform.up);
        //    var dot2 = Vector2.Dot(dir, m_TargetShip.transform.right);


        //    m_TargetShip.ThrustControl = Mathf.Max(0, dot);
        //    m_TargetShip.TorqueControl = -dot2;




        //}
    }
}