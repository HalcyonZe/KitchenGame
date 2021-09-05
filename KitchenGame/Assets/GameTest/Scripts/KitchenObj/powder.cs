using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class powder : Cooking
{
    public enum CookingTools
    {
        salt,
        pepper,
    }

    public CookingTools m_cooking;

    private GameObject cap;
    private ParticleSystem ps;

    private void Awake()
    {
        cap = this.transform.GetChild(0).gameObject;
        ps = this.transform.GetChild(4).GetComponent<ParticleSystem>();
    }

    public override void UseTools(GameObject Obj)
    {

        cap.SetActive(false);

        MouseSFM.Instance.PickObj.transform.parent = null;

        GameController.Instance.PlayerPause();

        ObjY = Obj.transform.position.y + 0.3f;

        transform.DOMove(new Vector3(Obj.transform.position.x, ObjY, Obj.transform.position.z), 0.3f).
            OnComplete(() => {


                Cursor.lockState = CursorLockMode.None;

                UseMouse = true;

            });
    }

    public override void StopCooking()
    {
        base.StopCooking();
        cap.SetActive(true);
    }

    public override void MouseAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            transform.DOShakePosition(0.3f, new Vector3(0, 0.15f, 0));
            ps.Play();

            Ray ray = new Ray(transform.position, transform.up);
            RaycastHit hit;
            LayerMask layer = LayerMask.GetMask("CutFoods") | LayerMask.GetMask("Foods");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                GameObject obj = hit.transform.gameObject;
                SetCooking(obj);
            }
        }
    }

    public void SetCooking(GameObject obj)
    {
        switch (m_cooking)
        {
            case CookingTools.salt:
                if (!obj.GetComponent<Foods>().m_foodItem.handleScoreDic.ContainsKey("salt"))
                {
                    obj.GetComponent<Foods>().m_foodItem.handleScoreDic.Add("salt", 5);
                }
                break;
            case CookingTools.pepper:
                if (!obj.GetComponent<Foods>().m_foodItem.handleScoreDic.ContainsKey("pepper"))
                {
                    obj.GetComponent<Foods>().m_foodItem.handleScoreDic.Add("pepper", 5);
                }
                break;
        }
    }

}
