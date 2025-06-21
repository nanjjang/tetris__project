using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animation ani;

    private void Awake()
    {
        ani = GetComponent<Animation>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ani.clip = ani.GetClip("ButtonUp");
        ani.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ani.clip = ani.GetClip("ButtonDown");
        ani.Play();
    }
}
