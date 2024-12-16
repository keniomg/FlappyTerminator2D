using UnityEngine;

public class WakeUper : MonoBehaviour
{
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.rigidbody != null)
        {
            hit.rigidbody.WakeUp();
        }
    }
}