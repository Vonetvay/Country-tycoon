using UnityEngine;

public class HomeTel : MonoBehaviour
{
    [SerializeField] private Transform _home;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = _home.position;
            print(1);
        }
    }
}
