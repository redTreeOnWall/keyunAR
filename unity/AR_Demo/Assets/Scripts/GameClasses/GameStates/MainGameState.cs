
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Behiviors;

namespace GameStates{
    /**
     * 
     */
    public class MainGameState : GameState {

        /**
         * 
         */
        public MainGameState() {
        }

        

        public override void stateEnd()
        {
           // Debug.Log("mainGameState end");
        }

        public override void stateInit()
        {
           // Debug.Log("mainGameState satrt");
        }

        public override void stateUpdate()
        {
          //  Debug.Log("mainGameState ");
            if (!GameBehivior.isFonded) {
                this.changeState(ref GameBehivior.thisState,GameBehivior.reFindingGameState);
            }
        }
    }
}