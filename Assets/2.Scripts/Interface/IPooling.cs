using System;
using UnityEngine;

public interface IPooling
{
    public Enum Type { get; }
    public GameObject Obj { get; }
    public void Return();
}