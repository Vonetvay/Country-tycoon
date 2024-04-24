using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavesResetter : MonoBehaviour
{
    [ContextMenu("RESET")]
    public void RESET()
    {
        DataLoader.Erase();
    }
}
