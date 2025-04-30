using UnityEngine;

public class Wheel : MonoBehaviour
{
    private TruckController _truck;

    private void Awake()
    {
        _truck = GetComponentInParent<TruckController>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.back, _truck.Speed/2);
    }
}
