using System;
using UnityEngine;

[Serializable]
public struct TimeLerpComponent
{
    public float FullTime;
    public float PassedTime;
    public AnimationCurve Curve;
}

