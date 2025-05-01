using System;
using UnityEngine;

public class InputManager
{
    public event Action<Vector2> OnInputClick;

    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            OnInputClick?.Invoke(Input.mousePosition);
        }
    }
}