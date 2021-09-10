using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cooking : BasicObj
{


    protected bool UseMouse = false;
    protected float ObjY = 0;


    private void FixedUpdate()
    {
        
        if (UseMouse)
        {
            ObjCooking();
            StopCooking();
        }
        else
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public override void PickObjs()
    {
        
        this.GetComponent<Collider>().enabled = false;

        base.PickObjs();


        MouseSFM.Instance.SwitchState(MouseState.HasTools);
    }


    private void ObjCooking()
    {
        this.transform.eulerAngles = new Vector3(160, 0, 0);
        // Û±Í“∆∂Ø
        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = screenPos.z;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        //transform.position = worldPos;
        transform.position = new Vector3(worldPos.x, ObjY, worldPos.z);


        MouseAction();

    }
    
    public virtual void StopCooking()
    {
        if (Input.GetMouseButtonDown(1) && UseMouse)
        {
            UseMouse = false;

            Cursor.lockState = CursorLockMode.Locked;
            //cap.SetActive(true);
            PickObjs();
            GameController.Instance.PlayerPlay();
        }
    }

    public virtual void MouseAction()
    {
        
    }

    

    

}
