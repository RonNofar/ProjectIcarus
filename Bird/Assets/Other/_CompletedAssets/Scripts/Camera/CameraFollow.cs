using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class CameraFollow : MonoBehaviour
    {
        public GameObject targetObject;
        private Transform targetTransform;            // The position that that camera will be following.
        public float smoothing = 1f;        // The speed with which the camera will be following.

        private Transform m_Transform;

        Vector3 offset;                     // The initial offset from the target.


        void Start () {
            // Calculate the initial offset.
            m_Transform = transform;
            if (targetObject != null) targetTransform = targetObject.transform;
            else Debug.Log("No GameObject found.");
            offset = m_Transform.position - targetTransform.position;
        }


        void FixedUpdate () {

            if (targetTransform != null) {
                // Create a postion the camera is aiming for based on the offset from the target.
                Vector3 targetCamPos = targetTransform.position + offset;

                // Smoothly interpolate between the camera's current position and it's target position.
                m_Transform.position = Vector3.Lerp(m_Transform.position, targetCamPos, smoothing * Time.deltaTime);
            }
        }
    }
}