using System;
using UnityEngine;

public class InputManager
{
    public event Action<Vector2> OnInputClick;
    public event Action OnInputClickUp;

    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            OnInputClick?.Invoke(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnInputClickUp?.Invoke();
        }
    }
}