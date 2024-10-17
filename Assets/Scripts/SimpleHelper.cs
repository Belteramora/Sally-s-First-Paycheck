using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleHelper : MonoBehaviour
{
    public bool cursorLocked = false;

    private void Start()
    {
        SetCursor();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {

            SetCursor();

            cursorLocked = !cursorLocked;
        }
    }

    public void SetCursor()
    {
        if (cursorLocked)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }
}
