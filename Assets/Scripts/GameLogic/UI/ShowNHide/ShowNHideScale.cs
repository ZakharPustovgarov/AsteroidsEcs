using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ShowNHideScale : ShowNHide
{
    //[SerializeField]
    //float _secsToAppear = 0.5f;
    //[SerializeField]
    //float _secsToDisappear = 0.5f;

    [SerializeField]
    float _targetScale = 1f;

    CompositeDisposable disposables = new CompositeDisposable();    

    public override float Show(bool IsMomentum = false)
    {
        if(IsMomentum)
        {
            transform.localScale = new Vector3(_targetScale, _targetScale, _targetScale);

            return 0;
        }

        if (disposables.Count > 0) disposables.Clear();
        Observable.FromMicroCoroutine(_ => Showing()).Subscribe().AddTo(disposables);

        return _showTime;
    }

    IEnumerator Showing()
    {
        var time = 0f;

        while (time < _showTime)
        {
            time += Time.deltaTime;

            var progress = _moveCurve.Evaluate(time / _showTime);//Mathf.Clamp01(time / _showTime);

            transform.localScale = new Vector3(progress * _targetScale, progress * _targetScale, progress * _targetScale);

            yield return null;
        }

        transform.localScale = new Vector3(_targetScale, _targetScale, _targetScale);
    }

    public override float Hide(bool IsMomentum = false)
    {
        if (IsMomentum)
        {
            transform.localScale = new Vector3(0, 0, 0);

            return 0;
        }

        if (disposables.Count > 0) disposables.Clear();
        Observable.FromMicroCoroutine(_ => Hiding()).Subscribe().AddTo(disposables);

        return _hideTime;
    }

    IEnumerator Hiding()
    {
        var time = 0f;

        while (time < _hideTime)
        {
            time += Time.deltaTime;

            var progress = _moveCurve.Evaluate(time / _hideTime);//Mathf.Clamp01(time / _secsToDisappear);

            transform.localScale = new Vector3(_targetScale - progress * _targetScale, _targetScale - progress * _targetScale, _targetScale - progress * _targetScale);

            yield return null;
        }

        transform.localScale = new Vector3(0f, 0f, 0f);
    }

    public float FullCycle(float scale)
    {
        StartCoroutine(FullShowing(scale));

        return _showTime + _hideTime;
    }

    IEnumerator FullShowing(float scale)
    {
        var time = 0f;

        while (time < _showTime)
        {
            time += Time.deltaTime;

            var progress = (time / _showTime) * scale;

            transform.localScale = new Vector3(progress, progress, progress);

            yield return null;
        }

        transform.localScale = new Vector3(scale, scale, scale);

        yield return new WaitForSeconds(_showTime);

        time = 0f;

        while (time < _hideTime)
        {
            time += Time.deltaTime;

            var progress = (time / _hideTime) * scale;

            transform.localScale = new Vector3(scale - progress, scale - progress, scale - progress);

            yield return null;
        }

        transform.localScale = new Vector3(0f, 0f, 0f);
    }

    public float GetSecsToAppear()
    {
        return _showTime;
    }

    public void SetSecsToDisappear(float time)
    {
        _hideTime = time;
    }

    private void OnDestroy()
    {
        disposables.Dispose();
    }
}
