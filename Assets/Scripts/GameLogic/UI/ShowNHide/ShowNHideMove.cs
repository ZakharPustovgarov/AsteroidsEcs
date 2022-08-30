using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ShowNHideMove : ShowNHide
{
    [SerializeField]
    List<RectTransform> _up;
    [SerializeField]
    List<RectTransform> _down;
    [SerializeField]
    List<RectTransform> _left;
    [SerializeField]
    List<RectTransform> _right;

    List<Vector3> _upStartPos = new List<Vector3>();
    List<Vector3> _downStartPos = new List<Vector3>();
    List<Vector3> _leftStartPos = new List<Vector3>();
    List<Vector3> _rightStartPos = new List<Vector3>();

    List<Vector3> _upEndPos = new List<Vector3>();
    List<Vector3> _downEndPos = new List<Vector3>();
    List<Vector3> _leftEndPos = new List<Vector3>();
    List<Vector3> _rightEndPos = new List<Vector3>();

    [SerializeField]
    float offset = 1000;

    //[SerializeField]
    //float _showTime;
    //[SerializeField]
    //float _hideTime;

    //[SerializeField]
    //AnimationCurve _moveCurve;

    [SerializeField]
    bool _isStarting = false;


    CompositeDisposable _movingRoutine = new CompositeDisposable();

    private void Start()
    {

        if (_upStartPos.Count == 0 && _leftStartPos.Count == 0 && _rightStartPos.Count == 0 && _downStartPos.Count == 0 && _upEndPos.Count == 0 && _leftEndPos.Count == 0 && _rightEndPos.Count == 0 && _downEndPos.Count == 0)
        {
            SetStartPos();
            SetEndPos();
        }

        if(!_isStarting)
        {
            Hide(true);
        }

        //Debug.Log("ShowNHideMove initialized");
    }

    public void RecalculateEdgePositions()
    {
        if(_upStartPos.Count == 0 && _leftStartPos.Count == 0 && _rightStartPos.Count == 0 && _downStartPos.Count == 0 && _upEndPos.Count == 0 && _leftEndPos.Count == 0 && _rightEndPos.Count == 0 && _downEndPos.Count == 0)
        {
            SetStartPos();
            SetEndPos();
        }
        else
        {
            ResetStartPos();
            ResetEndPos();        
        }
    }

    private void SetStartPos()
    {
        foreach (var trans in _up)
        {
            _upStartPos.Add(trans.localPosition);
        }

        foreach (var trans in _down)
        {
            _downStartPos.Add(trans.localPosition);
        }

        foreach (var trans in _left)
        {
            _leftStartPos.Add(trans.localPosition);
        }

        foreach (var trans in _right)
        {
            _rightStartPos.Add(trans.localPosition);
        }
    }

    private void ResetStartPos()
    {
        for (int i = 0; i < _up.Count; i++)
        {
            _upStartPos[i] = _up[i].localPosition;
        }

        for (int i = 0; i < _down.Count; i++)
        {
            _downStartPos[i] = _down[i].localPosition;
        }

        for (int i = 0; i < _left.Count; i++)
        {
            _leftStartPos[i] = _left[i].localPosition;
        }

        for (int i = 0; i < _right.Count; i++)
        {
            _rightStartPos[i] = _right[i].localPosition;
        }
    }

    private void SetEndPos()
    {
        foreach (var trans in _up)
        {
            _upEndPos.Add(new Vector3(trans.localPosition.x, trans.localPosition.y + offset, trans.localPosition.z));
        }

        foreach (var trans in _down)
        {
            _downEndPos.Add(new Vector3(trans.localPosition.x, trans.localPosition.y - offset, trans.localPosition.z));
        }

        foreach (var trans in _left)
        {
            _leftEndPos.Add(new Vector3(trans.localPosition.x - offset, trans.localPosition.y, trans.localPosition.z));
        }

        foreach (var trans in _right)
        {
            _rightEndPos.Add(new Vector3(trans.localPosition.x + offset, trans.localPosition.y, trans.localPosition.z));
        }
    }

    private void ResetEndPos()
    {
        for (int i = 0; i < _up.Count; i++)
        {
            _upEndPos[i] = new Vector3(_up[i].localPosition.x, _up[i].localPosition.y + offset, _up[i].localPosition.z);
        }

        for (int i = 0; i < _down.Count; i++)
        {
            _downEndPos[i] = new Vector3(_down[i].localPosition.x, _down[i].localPosition.y - offset, _down[i].localPosition.z);
        }

        for (int i = 0; i < _left.Count; i++)
        {
            _leftEndPos[i] = new Vector3(_left[i].localPosition.x - offset, _left[i].localPosition.y, _left[i].localPosition.z);
        }

        for (int i = 0; i < _right.Count; i++)
        {
            _rightEndPos[i] = new Vector3(_right[i].localPosition.x + offset, _right[i].localPosition.y, _right[i].localPosition.z);
        }
    }

    public override float Hide(bool isMomentum = false)
    {
        if (_isHided) return 0;

        _isHided = true;

        if (isMomentum)
        {
            for(int i = 0; i < _up.Count; i++)
            {
                _up[i].localPosition = _upEndPos[i];
            }

            for (int i = 0; i < _down.Count; i++)
            {
                _down[i].localPosition = _downEndPos[i];
            }

            for (int i = 0; i < _left.Count; i++)
            {
                _left[i].localPosition = _leftEndPos[i];
            }

            for (int i = 0; i < _right.Count; i++)
            {
                _right[i].localPosition = _rightEndPos[i];
            }

            return 0;
        }
        else
        {
            if (_movingRoutine.Count > 0) _movingRoutine.Clear(); ;
            Observable.FromMicroCoroutine(_ => Hiding()).Subscribe().AddTo(this);

            return _hideTime;
        }

    }

    IEnumerator Hiding()
    {
        var time = 0f;

        while (time < _hideTime)
        {
            time += Time.deltaTime;

            float progress = _moveCurve.Evaluate(time / _hideTime);

            for (int i = 0; i < _up.Count; i++)
            {
                _up[i].localPosition = Vector3.Lerp(_upStartPos[i], _upEndPos[i], progress);
            }

            for (int i = 0; i < _down.Count; i++)
            {
                _down[i].localPosition = Vector3.Lerp(_downStartPos[i], _downEndPos[i], progress);
            }

            for (int i = 0; i < _left.Count; i++)
            {
                _left[i].localPosition = Vector3.Lerp(_leftStartPos[i], _leftEndPos[i], progress);
            }

            for (int i = 0; i < _right.Count; i++)
            {
                _right[i].localPosition = Vector3.Lerp(_rightStartPos[i], _rightEndPos[i], progress);
            }

            yield return null;
        }

        Hide(true);

        _movingRoutine.Clear();
    }

    public override float Show(bool isMomentum = false)
    {
        if (!_isHided) return 0;

        _isHided = false;

        if (isMomentum)
        {
            for (int i = 0; i < _up.Count; i++)
            {
                _up[i].localPosition = _upStartPos[i];
            }

            for (int i = 0; i < _down.Count; i++)
            {
                _down[i].localPosition = _downStartPos[i];
            }

            for (int i = 0; i < _left.Count; i++)
            {
                _left[i].localPosition = _leftStartPos[i];
            }

            for (int i = 0; i < _right.Count; i++)
            {
                _right[i].localPosition = _rightStartPos[i];
            }

            return 0;
        }
        else
        {
            if (_movingRoutine.Count > 0) _movingRoutine.Clear(); ;
            Observable.FromMicroCoroutine(_ => Showing()).Subscribe().AddTo(this);

            return _showTime;
        }
    }

    IEnumerator Showing()
    {
        var time = 0f;

        while (time < _showTime)
        {
            time += Time.deltaTime;

            float progress = _moveCurve.Evaluate(time / _showTime);

            for (int i = 0; i < _up.Count; i++)
            {
                _up[i].localPosition = Vector3.Lerp(_upEndPos[i], _upStartPos[i], progress);
            }

            for (int i = 0; i < _down.Count; i++)
            {
                _down[i].localPosition = Vector3.Lerp(_downEndPos[i], _downStartPos[i], progress);
            }

            for (int i = 0; i < _left.Count; i++)
            {
                _left[i].localPosition = Vector3.Lerp(_leftEndPos[i], _leftStartPos[i], progress);
            }

            for (int i = 0; i < _right.Count; i++)
            {
                _right[i].localPosition = Vector3.Lerp(_rightEndPos[i], _rightStartPos[i], progress);
            }

            yield return null;
        }

        Show(true);

        _movingRoutine.Clear();
    }

    private void OnDestroy()
    {
        _movingRoutine.Dispose();
    }
}
