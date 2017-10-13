using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBehivior_01 : RobotStateBase {

    public Transform ppp;
    public Transform[] points;//路过的目标点
    public int nextIndex;
    public float walkSpeed =0.0001f;
    public int stateChange = 0;
    private void Awake()
    {
        //如果GameState ==1，就是已经识别出图像
        if (MyGameState.GameState ==1)
        {
            this.enabled = true;
        }
       
    }
    protected override void OnStart()
    {
        base.OnStart();
        nextIndex = 0;
    }

    Vector3 ignoreY(Vector3 v3)
    {
        return new Vector3(v3.x, 0, v3.z);
    }
    private void Update()
    {

        switch (stateChange)
        {
            //打招呼
            case 0:
                CheckState(RobotSate.Hellow);

                if (ani["Hellow"].normalizedTime > 0.99)
                {
                    stateChange = 1;
                    Debug.Log("helllo  end");
                }
                break;
            //走路
            case 1:
                var dx = this.transform.position.x - points[nextIndex].position.x;
                var dy = this.transform.position.z - points[nextIndex].position.z;
                var erlu = -Mathf.Atan2(dy, dx);
                erlu = erlu - Mathf.PI / 2;
                var v3 = new Vector3(transform.eulerAngles.x, erlu * 180 / Mathf.PI, transform.eulerAngles.z);
                this.transform.eulerAngles = v3;
                CheckState(RobotSate.Walk);
                transform.Translate(Vector3.forward * Time.deltaTime * 0.02f);
                var s = Vector3.Distance(transform.position, points[nextIndex].transform.position);
                if (s<=0.05) {
                    nextIndex++;
                    if (nextIndex == points.Length)
                    {
                        nextIndex = 0;
                    }
                    else {
                    }
                }
                //切换点击互动动画
                
                if (Input.GetMouseButton(0))
                {
                    Debug.Log("鼠标点击");
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitInfo;
                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        GameObject gameObj = hitInfo.collider.gameObject;
                        Debug.Log("click object name is " + gameObj.name);
                        if (gameObj.tag == "Role")
                        {
                            Debug.Log("pick up!");
                            stateChange = 2;
                        }
                    }
                }
                break;
            case 2:
                //暂时用“hello”这个动作。
                CheckState(RobotSate.Hellow);
                if (ani["Hellow"].normalizedTime > 0.99)
                {
                    stateChange = 1;
                }
                break;
        }
        
    }

}
