using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotStateBase : MonoBehaviour {

    public Animation ani;
    public RobotSate currentState = RobotSate.None;
    public enum RobotSate
    {
        None = 0,
        Idle,
        Walk,
        Talk,
        Hellow,
        Swerve,
        Fly
    }
    void Start()
    {
        Init();
        OnStart();

    }
    protected void Init()
    {
        if (null == ani)
        {
            ani = GetComponent<Animation>();
        }
    }
    protected void CheckState(RobotSate state)
    {
        if (!currentState.Equals(state))
        {
            currentState = state;
        }
        else
            return;
        switch (currentState)
        {
            case RobotSate.Talk:
                if (!ani.IsPlaying("Talk"))
                {
                    ani.CrossFade("Talk");
                    ani["Talk"].wrapMode = WrapMode.Loop;
                }
                break;
            case RobotSate.Idle:
                if (!ani.IsPlaying("Idle"))
                {
                    ani.CrossFade("Idle");
                    ani["Idle"].wrapMode = WrapMode.Loop;
                    ani.Stop("Walk");

                }
                break;

            case RobotSate.Walk:
                if (!ani.IsPlaying("Walk"))
                {
                    ani.CrossFade("Walk");
                    ani["Walk"].wrapMode = WrapMode.Loop;
                }
                break;
            case RobotSate.Hellow:
                if (!ani.IsPlaying("Hellow"))
                {
                    ani.CrossFade("Hellow");
                }
                break;
            default:
                break;
        }
    }
    protected virtual void OnStart()
    {
        Init();
    }
}
