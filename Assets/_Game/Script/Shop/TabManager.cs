using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    public GameObject[] Tabs;
    public Image[] TabButton;
    public Sprite inactiveTabBG, activeTabBG;
    private void Start()
    {
        SwitchToTab(0);
    }
    public void SwitchToTab(int TabID) {
        foreach (GameObject go in Tabs) {
            go.SetActive(false);
        }
        Tabs[TabID].SetActive(true);
        foreach(Image im in TabButton) {
            im.sprite = inactiveTabBG;
        }
        TabButton[TabID].sprite = activeTabBG;
    }
}
