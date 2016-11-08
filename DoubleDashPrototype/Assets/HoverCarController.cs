using UnityEngine;
using System.Collections;

namespace HoverCar
{
    public class HoverCarController : MonoBehaviour
    {

        [SerializeField]
        private float accelRate = 100;
        [SerializeField]
        private float steerRate = 0.1f;
        [SerializeField]
        private float maxSteerAngle = 25;

        [SerializeField]
        private GameObject cameraToRotate;
        [SerializeField]
        private GameObject finToRotate;

        private Transform m_Transform;
        private Rigidbody m_Rigidbody;

        private Transform m_finTransform;
        private Transform m_cameraTransform;

        // Use this for initialization
        void Start () {
            SetInitialReferences();
        }

        void SetInitialReferences () {
            m_Transform = GetComponent<Transform>();
            m_Rigidbody = GetComponent<Rigidbody>();

            if (finToRotate != null) m_finTransform = finToRotate.GetComponent<Transform>();
            else Debug.Log("No finToRotate found.");

            if (cameraToRotate != null) m_cameraTransform = GetComponent<Transform>();
            else Debug.Log("No cameraToRotate found.");
        }

        // Update is called once per frame
        void Update () {

        }

        public void Move ( float accel, float steer ) {
            if (accel != 0) {
                ApplyAccel(accel);
            }
            if (steer != 0) {
                ApplySteer(steer);
                RotateFin(m_finTransform, steer);
                RotateCamera(m_cameraTransform, steer);
            }
        }

        private void ApplyAccel (float accel) {
            m_Rigidbody.AddForce(m_Transform.forward * accel * accelRate);
        }

        private void ApplySteer (float steer) {
            Vector3 currVelocity = getVelocity( m_Rigidbody );
            if (currVelocity != new Vector3(0, 0, 0)) m_Rigidbody.rotation = RotationAboutTransform(m_Transform, steer, steerRate);//Quaternion.Euler(eulerAngles.x, eulerAngles.y + steer * steerRate, eulerAngles.z);//(m_Transform.up * steer * steerRate);
        }

        private void RotateFin (Transform finTran, float rot ) {
            //Vector3 eulerAngles = getEulerAngles( m_Transform );
            //m_finTransform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y + rot * maxSteerAngle, eulerAngles.z);
            finTran.rotation = RotationAboutTransform(m_Transform, rot, maxSteerAngle);
        }

        private void RotateCamera (Transform cameraTran, float steer) {
            //cameraTran.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y + steer * steerRate, eulerAngles.z);
            cameraTran.rotation = RotationAboutTransform(m_Transform, steerRate, steerRate);
        }

        private Quaternion RotationAboutTransform ( Transform rotateAbout, float rot, float rotRate ) {
            Vector3 eulerAngles = getEulerAngles(rotateAbout);
            return Quaternion.Euler(eulerAngles.x, eulerAngles.y + rot * rotRate, eulerAngles.z);
        }

        public Vector3 getEulerAngles( Transform tran ) {
            return tran.rotation.eulerAngles;
        }

        public Vector3 getVelocity ( Rigidbody rbody ) {
            return rbody.velocity;
        }

        public Transform getTransform() {
            return m_Transform;
        }

        public Rigidbody getRigidbody() {
            return m_Rigidbody;
        }
    }
}


// m_Transform.rotation = Quaternion.Euler(0, steer * maxSteerAngle, 0);
