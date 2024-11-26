using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasVictory : UICanvas
{
    public TMP_Text coinTxt;
    private void Start()
    {
        coinTxt.text = DataManager.Instance.CoinData.ToString();
    }
    public void ContinueButton()
    {
        LevelManager.Instance.ResetGame();
        UIManager.Instance.CloseAll();
        UIManager.Instance.Open<CanvasMainMenu>();
    }
}

