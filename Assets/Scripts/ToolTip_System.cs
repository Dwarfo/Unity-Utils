using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTip_System : Singleton_MB<ToolTip_System>
{

    public ToolTip tooltip;

    public void Show(string content, string header=""){
        tooltip.SetText(content, header);
        Instance.tooltip.gameObject.SetActive(true);
    }

    public void Hide(){
        Instance.tooltip.gameObject.SetActive(false);
    }
}
