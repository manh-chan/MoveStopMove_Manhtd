using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    Dictionary<System.Type, UICanvas> canvasActives = new Dictionary<System.Type, UICanvas>();
    Dictionary<System.Type, UICanvas> canvasPrefabs = new Dictionary<System.Type, UICanvas>();
    [SerializeField] Transform parent;
    
    private void Awake()
    {
        UICanvas[] prefabs = Resources.LoadAll<UICanvas>("UI/"); // load UI prefab tu resources
        for (int i = 0; i < prefabs.Length; i++)
        {
            canvasPrefabs.Add(prefabs[i].GetType(), prefabs[i]);
        }
    }

    //mo canvas
    public T Open<T>() where T : UICanvas
    {
        T canvas = GetUI<T>();
        canvas.Setup();
        canvas.Open();
        return canvas as T;
    }

    //dong canvas sau time
    public void CloseUI<T>(float time) where T : UICanvas
    {
        if (IsLoaded<T>())
        {
            canvasActives[typeof(T)].Close(time);
        }
    }

    //dong canvas truc tiep
    public void CloseUIDirectly<T>() where T : UICanvas
    {
        if (IsLoaded<T>())
        {
            canvasActives[typeof(T)].CloseDirectly();
        }
    }

    //kiem tra canvas da duoc tao chua
    public bool IsLoaded<T>() where T : UICanvas
    {
        return canvasActives.ContainsKey(typeof(T)) && canvasActives[typeof(T)] != null;
    }

    //kiem tra canvas duoc active hay chua
    public bool IsOpend<T>() where T : UICanvas
    {
        return IsLoaded<T>() && canvasActives[typeof(T)].gameObject.activeSelf;
    }

    //lay canvas
    public T GetUI<T>() where T : UICanvas
    {
        if (!IsLoaded<T>())
        {
            T prefab = GetUIPrefab<T>();
            T canvas = Instantiate(prefab);
            canvasActives[typeof(T)] = canvas;
        }
        return canvasActives[typeof(T)] as T;
    }

    //get prefab
    private T GetUIPrefab<T>() where T : UICanvas
    {
        return canvasPrefabs[typeof(T)] as T;
    }

    //dong tat ca
    public void CloseAll()
    {
        foreach (var canvas in canvasActives)
        {
            if (canvas.Value != null && canvas.Value.gameObject.activeSelf)
            {
                canvas.Value.Close(0);
            }
        }
    }
}
