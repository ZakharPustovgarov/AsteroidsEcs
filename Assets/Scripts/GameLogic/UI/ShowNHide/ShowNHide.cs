using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShowNHide : MonoBehaviour
{
    [SerializeField] protected float _showTime;
    [SerializeField] protected float _hideTime;

    [SerializeField] protected AnimationCurve _moveCurve;

    protected bool _isHided;

    public abstract float Show(bool IsMomentum = false);


    public abstract float Hide(bool IsMomentum = false);
}
