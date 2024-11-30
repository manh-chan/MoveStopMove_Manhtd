using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : Singleton<FollowCamera>
{
    public Transform Taget;

    public Vector3 offest;

    public float value;

    public void OnInit(){
        offest = new Vector3(0,3.5f,-3f);
    }
    private void LateUpdate()
    {
        Vector3 pos = Taget.position + offest;

        transform.position = Vector3.Lerp(transform.position, pos, value * Time.deltaTime);
    }
}