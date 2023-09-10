using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _cameraSpeedOffset;
    [SerializeField] private float _cameraInterpolate;
    [SerializeField] private float _distanceCamera;

    [SerializeField] private float _speedZoom;

    private Vector3 _targetPosition;

    private Touch _touchOne;
    private Touch _touchTwo;

    void Awake()
    {
        _targetPosition = transform.position;
    }

    void LateUpdate()
    {
        
        if (transform.position != _targetPosition)
        {
            transform.position = Vector3.Lerp(_targetPosition, transform.position, _cameraInterpolate);
        }
    }
    private void Update()
    {
        ZoomHandler();
        CorrectedDistance();
        MovementHandler();
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
            _targetPosition = new Vector3(_targetPosition.x, _targetPosition.y, _targetPosition.z + _cameraSpeedOffset);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _targetPosition = new Vector3(_targetPosition.x, _targetPosition.y, _targetPosition.z - _cameraSpeedOffset);
        }
        if (Input.GetKey(KeyCode.A))
        {
            _targetPosition = new Vector3(_targetPosition.x - _cameraSpeedOffset, _targetPosition.y, _targetPosition.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _targetPosition = new Vector3(_targetPosition.x + _cameraSpeedOffset, _targetPosition.y, _targetPosition.z);
        }
#endif
        #endregion
    }

    private void RotationHandler()
    {
        #region TouchControl
        #if (PLATFORM_ANDROID)

        #endif
        #endregion

        #region EditorControl
        #if UNITY_EDITOR

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
            _distanceCamera = Mathf.Clamp(_distanceCamera, 3f, 30f);
        }
#endif
        #endregion
    }
}
