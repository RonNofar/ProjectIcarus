using UnityEngine;
using System.Collections;
using System;

namespace Bezier
{
    public enum BezierControlPointMode { // Must be used outside a class so can be used elsewhere
        Free,
        Aligned,
        Mirrored
    }

    public class BezierSpline : MonoBehaviour
    {

        public static class Bezier
        {
            public static Vector3 GetPoint ( Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t ) {
                t = Mathf.Clamp01(t);
                float oneMinusT = 1f - t;
                //return Vector3.Lerp(Vector3.Lerp(p0, p2, t), Vector3.Lerp(p1, p2, t), t);
                return
                    oneMinusT * oneMinusT * oneMinusT * p0 +
                    3f * oneMinusT * oneMinusT * t * p1 +
                    3f * oneMinusT * t * t * p2 +
                    t * t * t * p3;
            }
            public static Vector3 GetFirstDerivative ( Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t ) {
                t = Mathf.Clamp01(t);
                float oneMinusT = 1f - t;
                return
                    3f * oneMinusT * oneMinusT * (p1 - p0) +
                    6f * oneMinusT * t * (p2 - p1) +
                    3f * t * t * (p3 - p2);
            }
        }

        [SerializeField]
        private Vector3[] points;

        [SerializeField]
        private BezierControlPointMode[] modes;

        public void AddCurve () {
            Vector3 point = points[points.Length - 1];
            Array.Resize(ref points, points.Length + 3);
            point.x += 1f;
            points[points.Length - 3] = point;
            point.x += 1f;
            points[points.Length - 2] = point;
            point.x += 1f;
            points[points.Length - 1] = point;

            Array.Resize(ref modes, modes.Length + 1);
            modes[modes.Length - 1] = modes[modes.Length - 2];
        }

        public int CurveCount {
            get {
                return (points.Length - 1) / 3;
            }
        }

        public int ControlPointCount {
            get {
                return points.Length;
            }
        }

        public Vector3 GetControlPoint ( int index ) {
            return points[index];
        }

        public void SetControlPoint ( int index, Vector3 point ) {
            points[index] = point;
        }

        public BezierControlPointMode GetControlPointMode ( int index ) {
            return modes[(index + 1) / 3];
        }

        public void SetControlPointMode ( int index, BezierControlPointMode mode ) {
            modes[(index + 1) / 3] = mode;
        }

        public Vector3 GetPoint ( float t ) {
            int i;
            if (t >= 1f) {
                t = 1f;
                i = points.Length - 4;
            } else {
                t = Mathf.Clamp01(t) * CurveCount;
                i = (int)t;
                t -= i;
                i *= 3;
            }
            return transform.TransformPoint(Bezier.GetPoint(
                points[i], points[i + 1], points[i + 2], points[i + 3], t));
        }

        public Vector3 GetVelocity ( float t ) {
            int i;
            if (t >= 1f) {
                t = 1f;
                i = points.Length - 4;
            } else {
                t = Mathf.Clamp01(t) * CurveCount;
                i = (int)t;
                t -= i;
                i *= 3;
            }
            return transform.TransformPoint(
                Bezier.GetFirstDerivative(
                    points[i],
                    points[i + 1],
                    points[i + 2],
                    points[i + 3], t)) - transform.position;
        }

        public Vector3 GetDirection ( float t ) {
            return GetVelocity(t).normalized;
        }

        public void Reset () {
            points = new Vector3[] {
                new Vector3(1f, 0f, 0f),
                new Vector3(2f, 0f, 0f),
                new Vector3(3f, 0f, 0f),
                new Vector3(4f, 0f, 0f)
            };
            modes = new BezierControlPointMode[] {
                BezierControlPointMode.Free,
                BezierControlPointMode.Free
            };
        }
    }
}
