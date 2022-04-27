using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIPointer : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private RectTransform _pointerRectTransform;
    [SerializeField] private float _borderSize;
    [SerializeField] private float _fadeSpeed;

    private Transform _targetTransform;
    private Rect _screen;
    private Vector3 _targetPositionScreenPoint;
    private bool _isOffScreen;
    private Image _image;
    private bool _isFaded;

    private void Awake()
    {
        _screen = new Rect(0, 0, Screen.width, Screen.height);
        _image = GetComponent<Image>();
        _isFaded = false;
    }

    private void LateUpdate()
    {
        Rotate();
        Move();
        SetColorFade();
    }

    public void SetTarget(Transform targetTransform)
    {
        _targetTransform = targetTransform;
    }

    private void Rotate()
    {
        Vector3 toPosition = _targetTransform.position;
        Vector3 fromPosition = _playerTransform.position;

        fromPosition.y = 0f;

        Vector3 direction = (toPosition - fromPosition).normalized;
        float angle = (Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg) % 360;

        _pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }

    private void Move()
    {
        _targetPositionScreenPoint = Camera.main.WorldToScreenPoint(_targetTransform.position);

        _isOffScreen = !_screen.Contains(_targetPositionScreenPoint);

        if (_isOffScreen)
        {
            _targetPositionScreenPoint.x = Mathf.Clamp(_targetPositionScreenPoint.x, _borderSize, Screen.width - _borderSize);
            _targetPositionScreenPoint.y = Mathf.Clamp(_targetPositionScreenPoint.y, _borderSize, Screen.height - _borderSize);

            _pointerRectTransform.anchoredPosition = new Vector3(_targetPositionScreenPoint.x, _targetPositionScreenPoint.y, 0f);
        }
    }

    private void SetColorFade()
    {
        if (_isOffScreen)
        {
            if (_isFaded)
            {
                _isFaded = false;
                StopCoroutine(Hide());
                StartCoroutine(Show());
            }
        }
        else
        {
            if (!_isFaded)
            {
                _isFaded = true;
                StopCoroutine(Show());
                StartCoroutine(Hide());
            }
        }
    }

    private IEnumerator Hide()
    {
        while (_image.color.a >= 0f)
        {
            Color temporaryColor = _image.color;

            temporaryColor.a -= _fadeSpeed * Time.deltaTime;
            _image.color = temporaryColor;

            yield return null;
        }

    }

    private IEnumerator Show()
    {
        while (_image.color.a <= 1f)
        {
            Color temporaryColor = _image.color;

            temporaryColor.a += _fadeSpeed * Time.deltaTime;
            _image.color = temporaryColor;
            yield return null;
        }
    }
}
