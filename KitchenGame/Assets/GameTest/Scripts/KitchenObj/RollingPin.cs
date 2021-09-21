using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RollingPin : BasicObj
{
    protected bool UseMouse = false;
    protected float ObjY = 0;


    private void FixedUpdate()
    {
        if (UseMouse)
        {
            Rolling();
            StopRolling();
        }
        /*else
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
        }*/
    }

    public override void PickObjs()
    {

        this.GetComponent<Collider>().enabled = false;

        base.PickObjs();


        MouseSFM.Instance.SwitchState(MouseState.HasTools);
    }

    public override void UseTools(GameObject Obj)
    {
        MouseSFM.Instance.PickObj.transform.parent = null;

        GameController.Instance.PlayerPause();

        ObjY = Obj.transform.position.y + 0.1f;

        transform.DOMove(new Vector3(Obj.transform.position.x, ObjY, Obj.transform.position.z), 0.3f).
            OnComplete(() => {
                this.transform.localEulerAngles = new Vector3(0, 90, 0);

                Cursor.lockState = CursorLockMode.None;

                UseMouse = true;

            });
    }

    private void Rolling()
    {
        //this.transform.eulerAngles = new Vector3(160, 0, 0);
        // Û±Í“∆∂Ø
        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = screenPos.z;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = new Vector3(worldPos.x, ObjY, worldPos.z);


        MouseAction();

    }

    public virtual void StopRolling()
    {
        if (Input.GetMouseButtonDown(1) && UseMouse)
        {
            UseMouse = false;

            Cursor.lockState = CursorLockMode.Locked;
            PickObjs();
            GameController.Instance.PlayerPlay();
        }
    }

    public void MouseAction()
    {
        if (Input.GetMouseButtonUp(0))
        {
            transform.DOShakePosition(0.3f, new Vector3(0.3f, 0, 0));

            Ray ray = new Ray(transform.position, -transform.up);
            RaycastHit hit;
            LayerMask layer = LayerMask.GetMask("dough");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                GameObject obj = hit.transform.gameObject;
                obj.GetComponent<dough>().SetDoughY();
            }
        }
    }
}
