using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Platform : MonoBehaviour
{
    public event Action PlayerTouchedPlatform;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealth playerHealth))
        {
            PlayerTouchedPlatform?.Invoke();
        }
    }
}