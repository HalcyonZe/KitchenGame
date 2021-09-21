using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Plates : BasicObj
{
    public List<GameObject> Foods = new List<GameObject>();
    private GameObject _water;

    private void Awake()
    {
        _water = transform.GetChild(1).gameObject;
    }

    private void FixedUpdate()
    {
        PlateUpdate();
    }
    private void PlateUpdate()
    {
        if (Foods.Count > 0)
        {
            for(int i=0;i<Foods.Count;i++)
            {
                if ( (this.transform.position.y-Foods[i].transform.position.y)>=0.1f)
                {
                    Foods[i].transform.parent = null;
                    Foods.Remove(Foods[i]);
                    i--;
                }
                else if(Foods[i].transform.parent != this.transform)
                {
                    Foods.Remove(Foods[i]);
                    i--;
                }
            }
        }
    }

    public override void PickObjs()
    {
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.transform.DOMove(MouseSFM.Instance.transform.GetChild(1).position, 0.1f).
                        OnComplete(() => {

                            this.transform.parent = MouseSFM.Instance.transform;
                            MouseSFM.Instance.SwitchState(MouseState.HasPlate);
                        });
        MouseSFM.Instance.PickObj = this.gameObject;
        
    }

    public override void UseTools(GameObject Obj)
    {
        Obj.transform.DOMove(this.transform.GetChild(0).position, 0.1f).
            OnComplete(()=> {
                AudioController.Instance.SetAudioPlay("Put");
                Obj.layer = LayerMask.NameToLayer("Foods");
                Obj.transform.parent = this.transform;
                if (!Foods.Contains(Obj))
                {
                    Foods.Add(Obj);
                }
            });
                
    }

    public override void PutObjs()
    {
        Vector3 pos = new Vector3();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit))
        {
            pos = hit.point;
        }


        GameObject obj = MouseSFM.Instance.PickObj;
        obj.transform.parent = this.transform;
        obj.transform.DOMove(/*this.transform.GetChild(0).position*/pos+new Vector3(0,0.1f,0), 0.3f).
            OnComplete(() => {
                AudioController.Instance.SetAudioPlay("Put");
                obj.GetComponent<Rigidbody>().isKinematic = false;
                obj.GetComponent<Collider>().enabled = true;
                Foods.Add(obj);
                obj = null;          
            });
        MouseSFM.Instance.SwitchState(MouseState.Nothing);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer==LayerMask.NameToLayer("Foods") || collision.gameObject.layer == LayerMask.NameToLayer("CutFoods"))
        {
            if(!Foods.Contains(collision.gameObject))
            {
                collision.transform.parent = this.transform;
                Foods.Add(collision.gameObject);
            }
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        //_water = transform.GetChild(1).gameObject;
        Color new_color = other.GetComponent<Renderer>().material.GetColor("_BaseColor");
        _water.SetActive(true);
        _water.GetComponent<Renderer>().material.SetColor("_Color", new_color);
        
    }

    public void SetTrash(GameObject Obj)
    {
        Vector3 pos = Obj.transform.GetChild(0).position;
        _water.SetActive(false);
        if (Foods.Count > 0)
        {
            for (int i = 0; i < Foods.Count; i++)
            {
                Foods[i].transform.parent = null;
                Foods[i].transform.DOMove(pos,0.1f);
                //Foods.Remove(Foods[i]);
            }
            Foods.Clear();
        }
    }

    public void GetSoup()
    {
        GameObject obj = MouseSFM.Instance.PickObj;
        obj.transform.parent = this.transform;
        obj.transform.DOMove(this.transform.GetChild(0).position, 0.3f).
            OnComplete(() => {
                AudioController.Instance.SetAudioPlay("Put");
                obj.GetComponent<Rigidbody>().isKinematic = false;
                obj.GetComponent<Collider>().enabled = true;
                Foods.Add(obj);
                obj = null;
            });
        MouseSFM.Instance.SwitchState(MouseState.Nothing);
    }

}
