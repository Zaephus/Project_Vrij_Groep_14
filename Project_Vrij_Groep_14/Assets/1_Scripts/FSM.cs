using System.Collections.Generic;

public class FSM {

    private Dictionary<System.Type,BaseState> stateDict = new Dictionary<System.Type,BaseState>();
    public BaseState currentState;

    public FSM(System.Type startState,params BaseState[] states) {
        foreach(BaseState state in states) {
            state.Initialize(this);
            stateDict.Add(state.GetType(),state);
        }
        SwitchState(startState);
    }

    public void OnUpdate() {
        currentState?.OnUpdate();
    }

    public void SwitchState(System.Type newStateType) {
        currentState?.OnExit();
        currentState = stateDict[newStateType];
        currentState?.OnEnter();
    }
    
}