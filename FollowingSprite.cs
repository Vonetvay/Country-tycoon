using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class FollowingSprite : MonoBehaviour
{
    private Transform _target;
    public bool Follow = true;
    public bool FreezeX = true;
    public bool FreezeY = false;
    public bool FreezeZ = true;

    private Quaternion _startRotation;

    void Start()
    {
        _target = VehicleController.Instance.transform;
        if (_target == null) { Debug.LogError("Cannot find VehicleController!"); return; }

        _startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Follow)
        {
            if (_target != null)
            {
                transform.LookAt(Camera.main.transform);
                transform.rotation = Quaternion.Euler(FreezeX == true ? 0 : transform.rotation.eulerAngles.x, FreezeY == true ? 0 : transform.rotation.eulerAngles.y, FreezeZ == true ? 0 : transform.rotation.eulerAngles.z);
            }
        }
        else
        {
            transform.rotation = _startRotation;
        }
    }
}
