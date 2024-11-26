using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasShop : UICanvas
{
    public void ExitButton()
    {
        Close(0);
        UIManager.Instance.Open<CanvasMainMenu>();
    }
}
