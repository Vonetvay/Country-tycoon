using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleZone : MonoBehaviour
{
    [SerializeField] private Mission _attachedMission;
    [SerializeField] private MeshRenderer[] _zoneMarkers = new MeshRenderer[0];
    [SerializeField] private Material _normalMaterial;
    [SerializeField] private Material _targetMaterial;

    private bool _interactible = true;

    public string ID
    {
        get
        {
            return _ID;
        }
    }
    [SerializeField] private string _ID;


    private void Start()
    {
        if (_zoneMarkers.Length == 0)
        {
            Debug.LogError("Must contain at least one Zone marker!");
            return;
        }
        SetNormal();
        _attachedMission.Initialize();
    }

    public void SetTarget()
    {
        foreach (var marker in _zoneMarkers)
        {
            marker.enabled = true;
            marker.material = _targetMaterial;
        }
        Compass.Instance.SetTarget(this.transform);
        _interactible = true;
    }

    public void Hide()
    {
        foreach (var marker in _zoneMarkers)
        {
            marker.enabled = false;
        }

        _interactible = false;
    }

    public void SetNormal()
    {
        foreach (var marker in _zoneMarkers)
        {
            marker.enabled = true;
            marker.material = _normalMaterial;
        }

        _interactible = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && _interactible)
        {
            if (_attachedMission != null)
            {
                _attachedMission.FireInteraction(_ID);
            }
        }
    }
}
