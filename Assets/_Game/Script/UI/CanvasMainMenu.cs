using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMainMenu : UICanvas
{
    public Button btnPlay;
    public void PlayButton() {
        Close(0);
        UIManager.Instance.Open<CanavsGamePlay>();
        GameManager.Instance.UpdateGameState(GameManager.GameState.PlayGame);
    }
    public void ShopButton() 
    {
        Close(0);
        UIManager.Instance.Open<CanvasShop>();
    }
    public void WeaponButton()
    {

    }
}

