using UnityEngine;

public class BaseHealth : MonoBehaviour 
{
    [field: SerializeField] public LayerMask Own { get; protected set; }
}