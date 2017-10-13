using UnityEngine;
using System.Collections;

public class TestAiCtr : RobotStateBase {

    [SerializeField]
    private float movedSpeed = 8f;
    public TestWayPoint targetWayPoint;
    public TestWayPoint startWayPoint;
    private static float flo = 0.5f;
    private Ray ray;
    private float _thinkTime = 3f;
    private float _currentTime = 0f;

    protected override void OnStart()
    {
        base.OnStart();
        Init2();
        //Additem();
    }
    public void getHurt()
    {
        // pb = GameObject.Find("Fg").GetComponent<UISprite>();
        //if (ui.fillAmount > 0f)
        //{
        //    ui.fillAmount -= 0.5f;
        //    // Debug.Log(pb.fillAmount);
        //}
    }
    Vector3 DistanceIgnoreYAxis(Vector3 pos)
    {
        pos = new Vector3(pos.x, 0, pos.z);
        return pos;
    }
    
    void Init2()
    {

        if (Vector3.Distance(DistanceIgnoreYAxis(startWayPoint.transform.position),DistanceIgnoreYAxis( transform.position)) < 0.5f)
        {
            
            //character.SimpleMove(direction * movedSpeed);
            targetWayPoint = startWayPoint.nextPoint;
           

        }
        else
        {
            // CheckState(PeopleSate.Walk);
            targetWayPoint = startWayPoint;
        }
        StartCoroutine(MoveLogic());
    }
    IEnumerator MoveLogic()
    {
        while (true)
        {
            Debug.Log(Vector3.Distance(DistanceIgnoreYAxis(startWayPoint.transform.position), DistanceIgnoreYAxis(transform.position)));
            if (Vector3.Distance(DistanceIgnoreYAxis(startWayPoint.transform.position), DistanceIgnoreYAxis(transform.position)) < 0.5f)
            {
                targetWayPoint = targetWayPoint.nextPoint;
               // CheckState(PeopleSate.Idle);
                yield return new WaitForSeconds(2f);
            }
            Vector3 targetPosIgno = targetWayPoint.transform.position;
            targetPosIgno.y = transform.position.y;
            Vector3 direction = targetPosIgno - transform.position;
            direction = direction.normalized;
            transform.rotation = Quaternion.LookRotation(direction);
            transform.Translate(direction * Time.deltaTime * -20f);

          
           // CheckState(PeopleSate.Walk);
           // character.SimpleMove(direction * movedSpeed);
            yield return new WaitForEndOfFrame();
        }
    }
}

