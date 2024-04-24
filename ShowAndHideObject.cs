using UnityEngine;

public class ShowAndHideObject : MonoBehaviour
{
    [SerializeField] private bool _active;

    private void Awake()
    {
        gameObject.SetActive(_active);
    }

    public void ChangeActive()
    {
        _active = !_active;
        gameObject.SetActive(_active);
    }
}
