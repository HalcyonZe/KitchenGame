using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : BasicObj
{
    public ResultPlane resultPlane;
    public override void PickObjs()
    {
        resultPlane.CalcResult();
    }
}
