using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lid : BasicObj
{
    public FoodSteamer foodSteamer;

    public override void PickObjs()
    {

        this.GetComponent<Collider>().enabled = false;

        base.PickObjs();
        foodSteamer.HasLib = false;

        MouseSFM.Instance.SwitchState(MouseState.HasTools);
    }
}
