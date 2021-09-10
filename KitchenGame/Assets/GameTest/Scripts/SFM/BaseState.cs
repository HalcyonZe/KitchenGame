using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MouseState
{
    Nothing,
    HasFoods,
    HasPlate,
    HasTools,
    HasPan,
}


public abstract class BaseState 
{
    protected LayerMask layer;

    protected bool Mouse = false;

    public virtual void OnEnter()
    {
        Mouse = true;
    }

    public virtual void OnUpdate()
    {
        if (Mouse)
        {
            ClickAction(layer);
        }
    }

    public virtual void OnExit()
    {
        Mouse = false;
    }

    private void ClickAction(LayerMask layer)
    {
        //射线检测
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100, layer.value))
        {
            //划出射线，只有在scene视图中才能看到
            Debug.DrawLine(ray.origin, hitInfo.point);
            UIController.Instance.ToClick();

            if (Input.GetMouseButtonUp(0))
            {
                GameObject gameObj = hitInfo.collider.gameObject;

                MouseAction(gameObj);

                UIController.Instance.ToPoint();
            }

        }
        else
        {
            UIController.Instance.ToPoint();
        }
    }

    public virtual void MouseAction(GameObject Obj)
    {

    }


}
