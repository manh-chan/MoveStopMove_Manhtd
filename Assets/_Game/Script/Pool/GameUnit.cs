using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUnit : MonoBehaviour
{
    public ObjectType ObjectType;
    private Transform tf;
    public Transform TF 
    {
        get 
        {
            if (tf == null) { 
                tf = transform;
            }
            return tf;
        }
    }
   
}

