using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class smallPlate : BasicObj
{
    private GameObject _water;
    public bool HasTMJ = false;

    public Dictionary<string, int> soupDict = new Dictionary<string, int>();

    private void Awake()
    {
        _water = transform.GetChild(0).gameObject;
    }
    
    public override void PickObjs()
    {
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.transform.DOMove(MouseSFM.Instance.transform.GetChild(0).position, 0.1f).
                        OnComplete(() => {

                            this.transform.parent = MouseSFM.Instance.transform;
                            MouseSFM.Instance.SwitchState(MouseState.HasTools);
                        });
        MouseSFM.Instance.PickObj = this.gameObject;

    }

    public override void UseTools(GameObject Obj)
    {
        Soup soup = Obj.GetComponent<Soup>();
        Debug.Log(Obj);
        /*if (soup.TMJsauce)
        {
            HasTMJ = true;
            _water.SetActive(true);
            Color color = Obj.GetComponent<Renderer>().material.GetColor("_Color");
            _water.GetComponent<Renderer>().material.SetColor("_Color", color);
        }*/
        if (soup.MatDict.Count > 0)
        {
            foreach(var i in soup.MatDict.Keys)
            {
                if (!soupDict.ContainsKey(i))
                {
                    Debug.Log(i);
                    soupDict.Add(i,1);
                }
            }
            _water.SetActive(true);
            Color color = Obj.GetComponent<Renderer>().material.GetColor("_Color");
            _water.GetComponent<Renderer>().material.SetColor("_Color", color);
        }
    }

}
