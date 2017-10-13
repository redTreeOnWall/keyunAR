
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Behiviors;
using UnityEngine.UI;

namespace GameStates{
    /**
     * 
     */
    public class InitGameSate : GameState {
        public Text FindingText;
        public Color textColor = new Color(1,1,1,1);

        /**
         * 
         */
        public  InitGameSate() {

        }
        
        public override void stateEnd()
        {
            this.FindingText.transform.localScale = new Vector3(0, 0, 0);
        }

        public override void stateInit()
        {

            GameObject text = GameObject.Find("FindingText");
            text.transform.localScale = new Vector3(1, 1, 1);
            this.FindingText = text.GetComponent<Text>();
        }


        public override void stateUpdate()
        {
            if (this.textColor.a > 0.9f)
            {
                this.textColor.a = 0f;
            }
            this.textColor.a = this.textColor.a + 0.01f;
            this.FindingText.color = this.textColor;
           // Debug.Log("initUpdata");
            if (GameBehivior.isFonded) {
                this.changeState(ref GameBehivior.thisState, GameBehivior.mainGameState);
            }

        }
    }
}