using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip_Trigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string content;
    public string header;

    private static LTDescr delay;

    public void OnPointerEnter(PointerEventData eventData)
    {
        delay = LeanTween.delayedCall(0.5f, () => {
            ToolTip_System.Instance.Show(this.content, this.header);
        });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(delay.uniqueId);
        ToolTip_System.Instance.Hide();
    }
}
