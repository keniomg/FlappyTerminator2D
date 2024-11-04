using UnityEngine;

public class Window : MonoBehaviour
{
    [SerializeField] protected SoundEventsInvoker SoundInvoker;

    public virtual void OnOpenWindowButtonClicked()
    {
        gameObject.SetActive(true);
        SoundInvoker.Invoke(SoundTypes.ButtonClicked);
    }

    public virtual void OnCloseWindowButtonClicked()
    {
        gameObject.SetActive(false);
        SoundInvoker.Invoke(SoundTypes.ButtonClicked);
    }
}