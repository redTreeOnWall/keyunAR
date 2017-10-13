
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Behiviors{
    /**
     * 小人行为
     */
    public class RobBehivior {

        /**
         * 小人行为
         */
        public RobBehivior() {
        }

        /**
         * 
         */
        public GameBehivior gameBehivior;

        /**
         * 
         */
        public RobState thisState;

        /**
         * 
         */
        public List<RobBehivior> robStateList;

    }
}