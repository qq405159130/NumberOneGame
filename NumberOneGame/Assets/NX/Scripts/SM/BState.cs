using UnityEngine;
using System.Collections;
using System;

public abstract class BState : MonoBehaviour {
    public abstract void OnStateEnter();
    public abstract bool OnStateStay() ;
    public abstract void OnStateExit() ;
}

public class sIdle : BState
{
    public override void OnStateEnter()
    {
        throw new NotImplementedException();
    }

    public override void OnStateExit()
    {
        throw new NotImplementedException();
    }

    public override bool OnStateStay()
    {
        throw new NotImplementedException();
    }
}
