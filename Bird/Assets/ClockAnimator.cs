using UnityEngine;
using System.Collections;
using System;

[ExecuteInEditMode]
public class ClockAnimator : MonoBehaviour {

    enum Hands { Hour, Minute, Second }

    private readonly float[] toDegrees = { 360/12, 360/60, 360/60 };


    private Transform m_Transform;
    private Transform[] handsTransforms;

    private int handsLength;
    private int rotDir = 1;

    public bool analog;
    public bool reverseRotation;

	void Start () {
        SetInitialReferences();
    }

    void SetInitialReferences () {
        m_Transform = GetComponent<Transform>();
        var handsValues = Enum.GetValues(typeof(Hands));
        handsLength = handsValues.Length;
        handsTransforms = new Transform[handsLength];
        for (int i = 0; i < handsLength; ++i) { handsTransforms[i] = m_Transform.GetChild(i); }

    }

    void UpdateReferences() {
        if (reverseRotation) rotDir = -1;
        else rotDir = 1;
    }

    void FixedUpdate () {
        TransformHands();
    }

    private void TransformHands() {
        double[] times = new double[handsLength];
        if (analog) {
            TimeSpan timespan = DateTime.Now.TimeOfDay;
            times = new double[] { timespan.TotalHours, timespan.TotalMinutes, timespan.TotalSeconds };
        } else {
            DateTime time = DateTime.Now;
            times = new double[] { time.Hour, time.Minute, time.Second };
        }
        float[] fTimes = CastToFloat(times);
        for (int i = 0; i < fTimes.Length; ++i) {
            handsTransforms[i].localRotation = Quaternion.Euler(0, 0, fTimes[i] * toDegrees[i] * rotDir);
        }
    }

    private float[] CastToFloat ( double[] oArr ) {
        int arrLen = oArr.Length;
        float[] rArr = new float[arrLen];
        for (int i = 0; i < arrLen; ++i) {
            rArr[i] = (float)oArr[i];
        }
        return rArr;
    }


    /*
    private float[] CastToFloat<T>( T[] oArr ) {
        int arrLen = oArr.Length;
        float[] rArr = new float[arrLen];
        for (int i = 0; i < arrLen; ++i) {
            rArr[i] = Convert.ChangeType(oArr[i], typeof(float));
        }
        return rArr;
    }*/
}

