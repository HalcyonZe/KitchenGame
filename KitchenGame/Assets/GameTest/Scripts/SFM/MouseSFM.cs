using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSFM : MonoBehaviour
{
    private Dictionary<MouseState, BaseState> stateDic = new Dictionary<MouseState, BaseState>();
    private BaseState curState;
    public GameObject PickObj = null;
    public bool CanClick = true;

    public static MouseSFM Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        stateDic.Add(MouseState.Nothing, new NothingState());
        stateDic.Add(MouseState.HasFoods, new HasFoodsState());
        stateDic.Add(MouseState.HasPlate, new HasPlateState());
        stateDic.Add(MouseState.HasTools, new HasToolsState());
        stateDic.Add(MouseState.HasPan, new HasPanState());

        curState = stateDic[MouseState.Nothing];
        curState.OnEnter();

    }

    private void Update()
    {
        if (curState != null && CanClick)
        {
            curState.OnUpdate();
        }
    }

    public void SwitchState(MouseState state)
    {
        curState.OnExit();
        curState = stateDic[state];
        curState.OnEnter();
    }

}
