using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;

public class ShowNHideOpacity : ShowNHide
{
    [SerializeField]
    List<Image> _images;

    [SerializeField]
    List<TextMeshProUGUI> _texts;

    [SerializeField]
    float _maxOpacity = 1f;

    [SerializeField]
    bool isHideOnStart;

    CompositeDisposable _routine = new CompositeDisposable();

    private void Start()
    {
        if (isHideOnStart) Hide(true);
    }

    public override float Hide(bool isMomentum = false)
    {
        if (isMomentum)
        {
            ChangeImagesOpacity(0);
            ChangeTextOpacity(0);
        }
        else if(_routine.Count == 0) Observable.FromMicroCoroutine(_ => Hiding()).Subscribe().AddTo(_routine);

        return _hideTime;
    }

    IEnumerator Hiding()
    {
        var time = 0f;

        float progress = 0f;

        ChangeRaycastImageState(false);

        while (time < _hideTime)
        {
            time += Time.deltaTime;

            progress = _maxOpacity - (_moveCurve.Evaluate(time / _hideTime) * _maxOpacity);

            ChangeImagesOpacity(progress);
            ChangeTextOpacity(progress);

            yield return null;
        }

        ChangeImagesOpacity(0);
        ChangeTextOpacity(0);

        ChangeGameObjectsState(false);

        _routine.Clear();
    }

    public override float Show(bool isMomentum = false)
    {
        if (isMomentum)
        {
            ChangeImagesOpacity(_maxOpacity);
            ChangeTextOpacity(_maxOpacity);
        }
        else if (_routine.Count == 0) Observable.FromMicroCoroutine(_ => Showing()).Subscribe().AddTo(_routine);

        return _showTime;
    }

    IEnumerator Showing()
    {
        var time = 0f;

        float progress = 0f;

        ChangeGameObjectsState(true);

        while (time < _showTime)
        {
            time += Time.deltaTime;

            progress = _moveCurve.Evaluate(time / _hideTime) * _maxOpacity;

            ChangeImagesOpacity(progress);
            ChangeTextOpacity(progress);

            yield return null;
        }

        ChangeImagesOpacity(_maxOpacity);
        ChangeTextOpacity(_maxOpacity);

        ChangeRaycastImageState(true);

        _routine.Clear();
    }

    public void ChangeImagesOpacity(float opacity)
    {
        opacity = Mathf.Clamp01(opacity);

        foreach (var image in _images)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
        }
    }

    public void ChangeTextOpacity(float opacity)
    {
        opacity = Mathf.Clamp01(opacity);

        foreach (var text in _texts)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, opacity);
        }
    }

    public void ChangeRaycastImageState(bool isRaycastTarget)
    {
        foreach (var image in _images)
        {
            image.raycastTarget = isRaycastTarget;
        }
    }

    public void ChangeGameObjectsState(bool isActive)
    {
        foreach(var image in _images)
        {
            image.gameObject.SetActive(isActive);
        }

        foreach (var text in _texts)
        {
            text.gameObject.SetActive(isActive);
        }
    }

    private void OnDestroy()
    {
        _routine.Dispose();
    }
}
