using UnityEngine;

public class Window : MonoBehaviour
{
    public virtual void OnOpenWindowButtonClicked()
    {
        gameObject.SetActive(true);
    }

    public virtual void OnCloseWindowButtonClicked()
    {
        gameObject.SetActive(false);
    }
}