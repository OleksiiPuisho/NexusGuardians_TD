using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _cameraSpeedOffset;
    [SerializeField] private float _cameraInterpolate;
    [SerializeField] private float _distanceCamera;

    [SerializeField] private float _speedZoom;
    [SerializeField] private Vector2 _clampZoom;

    private Vector3 _targetPosition;
    private Vector3 _targetRotation;
    private float _cameraRotation;
    [SerializeField] private Vector2 _clampCameraX;

    private Touch _touchOne;
    private Touch _touchTwo;

    void Awake()
    {
        _targetPosition = transform.position;
        _targetRotation = transform.rotation.eulerAngles;
        _cameraRotation = _cameraTransform.localEulerAngles.x;
    }

    void LateUpdate()
    {
        if (transform.position != _targetPosition)
        {
            transform.position = Vector3.LerpUnclamped(transform.position, _targetPosition, _cameraInterpolate);
        }
        if (transform.rotation.eulerAngles != _targetRotation)
        {
            transform.rotation = Quaternion.LerpUnclamped(transform.rotation, Quaternion.Euler(_targetRotation), _cameraInterpolate);
        }
        if(_cameraTransform.localEulerAngles.x != _cameraRotation)
        {
            _cameraTransform.localEulerAngles = new(Mathf.Lerp(_cameraTransform.localEulerAngles.x, _cameraRotation, _cameraInterpolate), 0, 0);
        }
    }
    private void Update()
    {
        ZoomHandler();
        CorrectedDistance();
        MovementHandler();
        RotationHandler();
    }

    private void MovementHandler()
    {
        #region TouchControl
#if (PLATFORM_ANDROID)

#endif
        #endregion

        #region EditorControl
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.W))
        {
            _targetPosition = GetDirectionMovement(transform.forward);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _targetPosition = GetDirectionMovement(-transform.forward);
        }
        if (Input.GetKey(KeyCode.A))
        {
            _targetPosition = GetDirectionMovement(-transform.right);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _targetPosition = GetDirectionMovement(transform.right);
        }
#endif
        #endregion
    }

    private Vector3 GetDirectionMovement(Vector3 direction)
    {
        return Vector3.MoveTowards(_targetPosition, _targetPosition + direction, _cameraSpeedOffset);
    }

    private void RotationHandler()
    {
        #region TouchControl
#if (PLATFORM_ANDROID)

#endif
        #endregion

        #region EditorControl
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.E))
        {
            _targetRotation = new(_targetRotation.x, _targetRotation.y + _cameraSpeedOffset, _targetRotation.z);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            _targetRotation = new(_targetRotation.x, _targetRotation.y - _cameraSpeedOffset, _targetRotation.z);
        }
        if(Input.GetKey(KeyCode.R))
        {
            _cameraRotation -= _cameraSpeedOffset;
            _cameraRotation = Mathf.Clamp(_cameraRotation, _clampCameraX.x, _clampCameraX.y);
        }
        if (Input.GetKey(KeyCode.T))
        {
            _cameraRotation += _cameraSpeedOffset;
            _cameraRotation = Mathf.Clamp(_cameraRotation, _clampCameraX.x, _clampCameraX.y);
        }
#endif
        #endregion
    }

    private void CorrectedDistance()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
        {
            float distancceCorected = transform.position.y + (_distanceCamera - hit.distance);

            if (_targetPosition.y != distancceCorected)
            {                
                _targetPosition.y = Mathf.Lerp(_targetPosition.y, distancceCorected, _cameraInterpolate);
            }
        }
    }

    private void ZoomHandler()
    {
        #region TouchControl
#if (PLATFORM_ANDROID)

#endif
        #endregion

        #region EditorControl
#if UNITY_EDITOR
        if (Input.mouseScrollDelta.y != 0)
        {
            _distanceCamera -= Input.mouseScrollDelta.y * _speedZoom;
            _distanceCamera = Mathf.Clamp(_distanceCamera, _clampZoom.x, _clampZoom.y);
        }
#endif
        #endregion
    }
}
