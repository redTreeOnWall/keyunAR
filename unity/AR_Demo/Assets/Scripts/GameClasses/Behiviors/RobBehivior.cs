
using RobStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
namespace Behiviors {
    /**
     * 小人行为
     */
    public class RobBehivior :MonoBehaviour {
        //关键点
        public Transform[] points;
        public int nextIndex = 0;

        //小人行走速度
        public float walkSpeed;

        //小人目标
        public Transform aimTransform;
        //小人状态机  
        public BaseState thisState;
        public Dictionary<String, BaseState> states = new Dictionary<string, BaseState>();
        public BaseState lastState;
        void Start()
        {
            aimTransform.position = new Vector3(points[nextIndex].position.x, points[nextIndex].position.y,points[nextIndex].position.z);

            var st = this.newState("RobSleepState");
            this.newState("RobInitState");
            this.newState("RotWalkRandomState");
            this.newState("RotWalkAimState");
            this.newState("RotTouchedState");
            this.thisState = st;
            thisState.stateInit();
        }

        void Update()
        {
            this.thisState.stateLoop();
        }

        public RobStateBase newState(string className)
        {
            Type t = Type.GetType("Behiviors." + className);
            RobStateBase robStateBase = (RobStateBase)Activator.CreateInstance(t, new RobBehivior[] { this });
            robStateBase.stateName = className;
            states.Add(robStateBase.stateName, robStateBase);
            
            return robStateBase;
        }
        
        //走向一个目标，如果到了，就返回true；
        public bool walkToAim(Transform aim) {
            var dx = this.transform.position.x - aim.position.x;
            var dy = this.transform.position.z - aim.position.z;
            var erlu = -Mathf.Atan2(dy, dx);
            erlu = erlu - Mathf.PI / 2;
            var v3 = new Vector3(this.transform.eulerAngles.x, erlu * 180 / Mathf.PI, this.transform.eulerAngles.z);
            this.transform.eulerAngles = v3;
            this.transform.Translate(Vector3.forward * Time.deltaTime * this.walkSpeed);
            var s = Vector3.Distance(this.transform.position, aim.position);
            if (s <= 0.01)
            {
                return true;
            }
            else {
                return false;
            }
        }
    }

    public class RobStateBase : BaseState
    {
        public RobBehivior robBehivior;
        public RobStateBase(RobBehivior b)
        {
            this.robBehivior = b;
        }
        
        public override void stateEnd()
        {
            this.robBehivior.lastState = this;
        }

        public override void stateInit()
        {
            Debug.Log("Robot state init:" +this.stateName);
        }

        public override void stateUpdate()
        {
            
        }

    }

    public  class RobSleepState : RobStateBase
    {
        public RobSleepState(RobBehivior b) : base(b)
        {

        }
        public override void stateInit()
        {
            base.stateInit();

        }
        public override void stateUpdate()
        {
            base.stateUpdate();
            if (GameBehivior.thisState==GameBehivior.mainGameState) {
                this.changeState(ref this.robBehivior.thisState,this.robBehivior.states["RobInitState"]);
            }
            
        }
    }

    public class RobInitState : RobStateBase
    {
        public RobInitState(RobBehivior b) : base(b)
        {
        }
        public override void stateInit()
        {
            base.stateInit();
            this.robBehivior.gameObject.GetComponent<Animation>().Play("Hellow");
        }
        public override void stateUpdate()
        {
            if (this.robBehivior.gameObject.GetComponent<Animation>()["Hellow"].normalizedTime > 0.99f) {
                this.changeState(ref this.robBehivior.thisState, this.robBehivior.states["RotWalkRandomState"]);
            }
            base.stateUpdate();
            

        }

    }

    public class RotWalkRandomState : RobStateBase
    {
        public RotWalkRandomState(RobBehivior b) : base(b)
        {

        }
        public override void stateInit()
        {
            base.stateInit();
            this.robBehivior.gameObject.GetComponent<Animation>().Play("Walk");
        }

        

        public override void stateUpdate()
        {
            base.stateUpdate();

            //走向一个关键点
            var intoPoint = this.robBehivior.walkToAim(this.robBehivior.points[robBehivior.nextIndex]);
            if (intoPoint) {
                robBehivior.nextIndex++;
                if (robBehivior.nextIndex == robBehivior.points.Length)
                {
                    robBehivior.nextIndex = 0;
                }
            }
            
            //判断是否点击了屏幕
            if (GameBehivior.isPosition) {

                Debug.LogError("========click======== ");
                //射线检测
                Ray ray = Camera.main.ScreenPointToRay(GameBehivior.InputPosise);
                RaycastHit hitInfo;
                string HitObjName = "";
                if (Physics.Raycast(ray, out hitInfo))
                {
                    HitObjName = hitInfo.collider.gameObject.name;
                    Debug.LogError("click object name is " + HitObjName);
                }

                //判断是否点击了小人
                if (HitObjName.Equals("zou")) {
                    Debug.LogError("zou " );
                    //    this.changeState(ref this.robBehivior.thisState, this.robBehivior.states["RotTouchedState"]);
                }
                //  判断是否点击了地面
                if (HitObjName.Equals("Papercollider")) {
                    Debug.LogError("paper ");
                    this.robBehivior.aimTransform.position=new Vector3( hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
                    //    this.changeState(ref this.robBehivior.thisState, this.robBehivior.states["RotWalkAimState"]);
                }
                
            }


        }
    }

    public class RotWalkAimState : RobStateBase
    {
        public RotWalkAimState(RobBehivior b) : base(b)
        {
        }
    }

    public class RotTouchedState : RobStateBase
    {
        Vector3 aimPosition;
        public RotTouchedState(RobBehivior b) : base(b)
        {
        }
        public override void stateInit()
        {
            base.stateInit();
        }
        public override void stateUpdate()
        {
            base.stateUpdate();
        }
        public override void stateEnd()
        {
            base.stateEnd();
        }
    }


}