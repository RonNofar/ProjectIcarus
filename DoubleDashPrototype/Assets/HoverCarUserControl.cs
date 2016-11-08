using UnityEngine;
using System.Collections;

namespace HoverCar
{
    [RequireComponent(typeof(HoverCarController))]
    public class HoverCarUserControl : MonoBehaviour
    {

        private HoverCarController m_HoverCar;

        // Use this for initialization
        private void Awake () {
            try { m_HoverCar = GetComponent<HoverCarController>(); }
            catch { Debug.Log("No HoverCArController found."); }
            
        }

        // Update is called once per frame
        void Update () {

        }

        private void FixedUpdate () {
            float accel = -Input.GetAxis("LeftTrigger");
            float steer = Input.GetAxis("Horizontal");

            m_HoverCar.Move(accel, steer);
        }
    }
}
