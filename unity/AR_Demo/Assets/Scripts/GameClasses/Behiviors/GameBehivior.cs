
using GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Behiviors{

    
    /**
     * 游戏行为，继承MonoBehavior
     */
    public class GameBehivior :MonoBehaviour {
        /// <summary>
        /// 鼠标或者触控点在屏幕中的位置，如果没有点击则为（-1f,-1f,-1f）
        /// </summary>
        public static Vector3 InputPosise = new Vector3(-1f,-1f,-1f);
        /// <summary>
        ///是否被点击了一次或者触摸了一次
        /// </summary>
        public static bool isPosition = false;
        //上一帧是否被点击了
        public static bool isDeltPos = false;
        //这一帧是否被点击了
        public static bool isThisPos = false;

        public Text logText;

        public static bool isFonded = false;
        /**
         * 状态机的历史纪录
        */
        public List<GameState> gameStateList;
        public static Dictionary<string, GameState> states = new Dictionary<string, GameState>();


        // 所有状态
        public static InitGameSate initGameSate;
        public static CommentGameState commentGameState ;
        public static ContactGameState contactGameState;
        public static MainGameState mainGameState ;
        public static ReFindingGameState reFindingGameState ;
        public static CompanyGameState companyGameState ;
        public static DiyGameState diyGameState;

        /**
         * 当前状态
         */
        public static BaseState thisState;
        
        public void Start()
        {
            
            //初始化所有状态机
            initGameSate = new InitGameSate();
            initGameSate.stateName = "initGameSate";
             commentGameState = new CommentGameState();
            commentGameState.stateName = "commentGameState";
             contactGameState = new ContactGameState();
            contactGameState.stateName = "contactGameState";
             mainGameState = new MainGameState();
            mainGameState.stateName = "mainGameState";
             reFindingGameState = new ReFindingGameState();
            reFindingGameState.stateName="reFindingGameState";
             companyGameState = new CompanyGameState();
            companyGameState.stateName = "companyGameState";
             diyGameState = new DiyGameState();
            diyGameState.stateName = "diyGameState";
            states.Add(initGameSate.stateName, initGameSate);
            states.Add(commentGameState.stateName,commentGameState);
            states.Add(contactGameState.stateName,contactGameState);
            states.Add(mainGameState.stateName,mainGameState);
            states.Add(reFindingGameState.stateName,reFindingGameState);
            states.Add(companyGameState.stateName,companyGameState);
            states.Add(diyGameState.stateName,diyGameState);

            thisState = states["initGameSate"];
            thisState.stateInit();

        }
        public void Update()
        {
            //游戏状态机更新
            thisState.stateLoop();
            //控制器更新
            ControllerUpdate();
        }


        //将输入信息赋值给控制器
        public void ControllerUpdate() {

            
            isThisPos = false;
            if (Input.GetMouseButton(0)) {
                isThisPos = true;
                InputPosise.Set(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            }
            if (Input.touchCount>0) {
                isThisPos = true;
                InputPosise.Set(Input.touches[0].position.x, Input.touches[0].position.y, 1.0f);
            }
            //上一帧没有点击且这一帧点击了（放开后再点击）
            if (isThisPos && !isDeltPos)
            {
                isPosition = true;
            }
            else {
                isPosition = false;
            }
            
            isDeltPos = isThisPos;
        }
    }
}