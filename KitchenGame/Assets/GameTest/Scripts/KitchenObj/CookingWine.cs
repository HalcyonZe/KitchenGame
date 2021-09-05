using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CookingWine : Cooking
{
    private ParticleSystem ps;

    private void Awake()
    {
        ps = this.transform.GetChild(3).GetComponent<ParticleSystem>();
        ps.Stop();
    }

    public override void UseTools(GameObject Obj)
    {
        MouseSFM.Instance.PickObj.transform.parent = null;

        GameController.Instance.PlayerPause();

        ObjY = Obj.transform.position.y + 0.4f;

        transform.DOMove(new Vector3(Obj.transform.position.x, ObjY, Obj.transform.position.z), 0.3f).
            OnComplete(() => {


                Cursor.lockState = CursorLockMode.None;

                UseMouse = true;

            });
    }

    public override void MouseAction()
    {
        if (Input.GetMouseButton(0))
        {
            //transform.DOShakePosition(0.3f, new Vector3(0, 0.15f, 0));
            ps.Play();

            Ray ray = new Ray(transform.position, transform.up);
            RaycastHit hit;
            LayerMask layer = LayerMask.GetMask("CutFoods") | LayerMask.GetMask("Foods");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                GameObject obj = hit.transform.gameObject;

                if (!obj.GetComponent<Foods>().m_foodItem.handleScoreDic.ContainsKey("CookingWine"))
                {
                    obj.GetComponent<Foods>().m_foodItem.handleScoreDic.Add("CookingWine", 5);
                }
            }
        }
        else
        {
            ps.Stop();
        }
    }

}
