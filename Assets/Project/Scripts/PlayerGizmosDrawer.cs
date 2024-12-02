using UnityEngine;

public class PlayerGizmosDrawer : MonoBehaviour
{
    private CharacterController _characterController;

    private void OnDrawGizmos()
    {
        if (_characterController != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, Vector3.right + Vector3.forward + Vector3.up * _characterController.height);
        }
    }

    public void Initialize(CharacterController characterController)
    {
        _characterController = characterController;
    }
}