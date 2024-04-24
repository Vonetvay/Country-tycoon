using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [Header("DriveType")]
    [SerializeField] private DriveType _driveType;

    [Header("Wheels")]
    [SerializeField] private Wheel _rightFrontWheel;
    [SerializeField] private Wheel _rightBackWheel;
    [SerializeField] private Wheel _leftFrontWheel;
    [SerializeField] private Wheel _leftBackWheel;

    [Header("Setup")]
    [SerializeField] private float _speed;
    [SerializeField] private float _maxSteerAngle;

    private void OnValidate()
    {
        switch (_driveType) 
        {
            case DriveType.Front:
                SetDrive(true, false);
                break;
            case DriveType.Back:
                SetDrive(false, true);
                break;
            case DriveType.Full:
                SetDrive(true, true);
                break;
        }
    }

    private void Update()
    {
        float forwardDirection = Input.GetAxis("Vertical");
        Drive(forwardDirection * _speed);
        
        float rotateDirection = Input.GetAxis("Horizontal");
        Turn(rotateDirection);
    }

    private void Drive(float speed) 
    {
        _rightFrontWheel.AddTorque(speed);
        _rightBackWheel.AddTorque(speed);
        _leftFrontWheel.AddTorque(speed);
        _leftBackWheel.AddTorque(speed);
    }

    private void Turn(float rotateDirection) 
    {
        _rightFrontWheel.SetSteerAngle(rotateDirection, _maxSteerAngle);
        _leftFrontWheel.SetSteerAngle(rotateDirection, _maxSteerAngle);
    }

    private void SetDrive(bool frontActive, bool backActive) 
    {
        _rightFrontWheel.active = frontActive;
        _leftFrontWheel.active = frontActive;
        _rightBackWheel.active = backActive;
        _leftBackWheel.active = backActive;
    }
}
