using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAroundWithMouse : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private GameObject cinemachineCameraTarget;

    [Header("Settings")]
    [SerializeField] private float rotationSpeed = 1.0f;

    private float _rotationVelocity;
    private float _topClamp = 90f;
    private float _bottomClamp = -90f;
    private float _cinemachineTargetPitch;

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    private void LateUpdate()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        if (inputReader.AimPosition.sqrMagnitude >= 0.01f)
        {
            _cinemachineTargetPitch += inputReader.AimPosition.y * rotationSpeed;
            _rotationVelocity = inputReader.AimPosition.x * rotationSpeed;

            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, _bottomClamp, _topClamp);

            cinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

            transform.Rotate(Vector3.up * _rotationVelocity);

        }
    }
}
