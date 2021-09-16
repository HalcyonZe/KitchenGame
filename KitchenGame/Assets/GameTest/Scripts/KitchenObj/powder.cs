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
        bean,
        sugar,
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
        if (Input.GetMouseButtonDown(1) && UseMouse)
        {
            UseMouse = false;

            Cursor.lockState = CursorLockMode.Locked;
            PickObjs();
            GameController.Instance.PlayerPlay();
            cap.SetActive(true);
            UIController.Instance.CloseTip();
        }
        
    }

    public override void MouseAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AudioController.Instance.SetAudioPlay("Salt");

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
                UIController.Instance.OpenTip("Ê³ÑÎ",5);
                if (!obj.GetComponent<Foods>().m_foodItem.handleScoreDic.ContainsKey("salt"))
                {
                    obj.GetComponent<Foods>().m_foodItem.handleScoreDic.Add("salt", 5);
                }
                break;
            case CookingTools.bean:
                UIController.Instance.OpenTip("¶¹ôù", 5);
                if (!obj.GetComponent<Foods>().m_foodItem.handleScoreDic.ContainsKey("bean"))
                {
                    obj.GetComponent<Foods>().m_foodItem.handleScoreDic.Add("bean", 5);
                }
                break;
            case CookingTools.pepper:
                UIController.Instance.OpenTip("»¨½·", 5);
                if (!obj.GetComponent<Foods>().m_foodItem.handleScoreDic.ContainsKey("pepper"))
                {
                    obj.GetComponent<Foods>().m_foodItem.handleScoreDic.Add("pepper", 5);
                }
                break;
            case CookingTools.sugar:
                UIController.Instance.OpenTip("ÌÇ", 5);
                if (!obj.GetComponent<Foods>().m_foodItem.handleScoreDic.ContainsKey("sugar"))
                {
                    obj.GetComponent<Foods>().m_foodItem.handleScoreDic.Add("sugar", 5);
                }
                break;
        }
    }

}
