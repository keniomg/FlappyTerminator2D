using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hit.rigidbody.velocity = Vector3.up * 100f;
    }
}