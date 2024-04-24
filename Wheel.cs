using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private WheelCollider _collider;
    [SerializeField] private GameObject _visualWheel;
    [SerializeField] private float _rotateSpeed;

    public bool active;

    private void Update()
    {
        Vector3 position;
        Quaternion rotation;
        _collider.GetWorldPose(out position, out rotation);
        _visualWheel.transform.rotation = rotation;
    }

    public void AddTorque(float force)
    {
        if (active) 
        {
            _collider.motorTorque = force;
        }
    }

    public void SetSteerAngle(float direction, float maxSteerAngle)
    {
        _collider.steerAngle = direction * maxSteerAngle;
    }
}
