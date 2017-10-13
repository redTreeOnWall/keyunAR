
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 状态机基类
 */
public abstract class BaseState {

    /**
     * 状态机基类
     */
    public BaseState() {
    }

    /**
     * 状态机名字
     */
    public string stateName;

    /**
     * 初始化状态机
     */
    public abstract void stateInit(); 

    /**
     * 每一帧循环
     */
    public abstract void stateUpdate();

    public void stateLoop (){
        this.stateUpdate();
    }

    /**
     * 
     */
    public abstract void stateEnd();

    /**
     * @param nextState
     */
    public void changeState(ref BaseState thisSate, BaseState nextState) {
        thisSate.stateEnd();
        thisSate = nextState;
        nextState.stateInit();
    }

}