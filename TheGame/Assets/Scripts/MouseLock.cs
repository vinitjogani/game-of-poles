using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLock : MonoBehaviour
{
    CursorLockMode wantedMode;

    private void Start()
    {
        SwitchLockState();
    }

    // Apply requested cursor state
    void SwitchLockState()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) SwitchLockState();
    }
}
