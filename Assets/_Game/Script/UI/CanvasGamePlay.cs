using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanavsGamePlay : UICanvas
{
    [SerializeField] TextMeshProUGUI rankText;
    private void Update()
    {
        rankText.text = LevelManager.Instance.totalRank.ToString();
    }
    public void SettingsButton() { 
        
    }
}
