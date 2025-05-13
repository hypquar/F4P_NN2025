using CarGame;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private List<AxisInfo> _axisInfos;
    [SerializeField] private float _maxTorque;
    [SerializeField] private float _maxSteeringAngle;

    private CarControlls _inputActions;
    private Vector2 _moveInput;

    private void Awake()
    {
        _inputActions = new CarControlls();
        _inputActions.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _inputActions.Player.Move.canceled += ctx => _moveInput = Vector2.zero;
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    private void FixedUpdate()
    {
        float motor = _maxTorque * _moveInput.y;
        float steering = _maxSteeringAngle * _moveInput.x;

        foreach (AxisInfo axisInfo in _axisInfos)
        {
            if (axisInfo.IsMotor)
            {
                axisInfo.RightWheel.motorTorque = motor;
                axisInfo.LeftWheel.motorTorque = motor;
            }
            if (axisInfo.IsSteering)
            {
                axisInfo.RightWheel.steerAngle = steering;
                axisInfo.LeftWheel.steerAngle= steering;
            }

            AllignVisualWheel(axisInfo.LeftWheel);
            AllignVisualWheel(axisInfo.RightWheel);
        }
    }

    private void AllignVisualWheel(WheelCollider collider)
    {
        if (collider.transform.childCount == 0) return;

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;

        collider.GetWorldPose(out position, out rotation);

        visualWheel.position = position;
        visualWheel.rotation = rotation;
    }
}
