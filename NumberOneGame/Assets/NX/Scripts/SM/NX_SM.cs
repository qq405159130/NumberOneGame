using UnityEngine;
using System.Collections;

public class NX_SM : MonoBehaviour {

    private Stack m_stateHeap = new Stack();
    public enum AddStateType
    {
        Override,
        Sequence,
        Return
    }
    public void AddState(AddStateType type, BState state)
    {
        switch (type)
        {
            case AddStateType.Override:
                ChangeState(state);
                break;
            case AddStateType.Sequence:
                m_stateHeap.Push(m_curState);
                ChangeState(state);
                break;
            case AddStateType.Return:
                NextState();
                break;
        }
    }

    public bool NextState()
    {
        if (m_stateHeap != null && m_stateHeap.Count != 0)
        {
            BState newState = m_stateHeap.Pop() as BState;
            ChangeState(newState);
            return true;
        }
        ChangeState(m_curState);
        return false;
    }

    private BState m_curState;
    public void ChangeState(BState state)
    {
        BState preState = m_curState;
        m_curState = state;

        preState.OnStateExit();
        m_curState.OnStateEnter();
    }

    void Start()
    {
        if (m_stateHeap != null && m_stateHeap.Count != 0)
        {
            m_curState = m_stateHeap.Pop() as BState;
        }
        else{
            m_curState = new sIdle();
        }
    }

    void Update()
    {
        if (m_curState.OnStateStay())
        {
            NextState();
        }
    }
}
