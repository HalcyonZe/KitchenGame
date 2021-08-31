using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class Pan : BasicObj
{
    public enum PanState
    {
        Normal,
        Blanching,
        Move,
        HoldWater,
    }
    public PanState m_panState = PanState.Normal ;

    private Dictionary<GameObject, float> _foodDic = new Dictionary<GameObject, float>();
    private float PanY = 0;
    //private bool CanRotate = false;

    private GameObject _boiling, _water;
    private bool HasWater = false;
    public float WaterHeight = 0;

    private void Start()
    {
        _water = this.transform.GetChild(1).gameObject;
        _boiling = this.transform.GetChild(2).gameObject;
    }

    private void FixedUpdate()
    {
        JudgeFood();
        PanStateUpdate();
    }

    private void JudgeFood()
    {
        if (_foodDic.Count > 0)
        {
            for (int i = 0; i < _foodDic.Count; i++)
            {
                GameObject food = _foodDic.ElementAt(i).Key;
                if (food.transform.parent != this.transform)
                {
                    _foodDic.Remove(food);
                }          
                else if(food.transform.position.y < this.transform.position.y)
                {
                    _foodDic.Remove(food);
                    food.transform.parent = null;
                }
            }
        }
    }

    private void PanStateUpdate()
    {
        switch (m_panState)
        {
            case PanState.Blanching:
                Blanching();
                break;
            case PanState.Normal:
                //NormalState();
                break;
            case PanState.Move:
                PanMove();
                StopMove();
                break;
            case PanState.HoldWater:
                HoldWaterMove();
                StopMove();
                break;
        }
    }

    private void Blanching()
    {
        
        if (_foodDic.Count > 0)
        {
            for(int i=0;i<_foodDic.Count;i++)
            {
                GameObject food = _foodDic.ElementAt(i).Key;
                if (food.transform.parent != this.transform)
                {
                    _foodDic.Remove(food);
                }
                else
                {
                    if (_foodDic[food] < 5.0f)
                    {
                        _foodDic[food] += Time.fixedDeltaTime;
                    }
                    else
                    {
                        if (!food.GetComponent<Foods>().m_foodItem.handleScoreDic.ContainsKey("blanching"))
                        {
                            food.GetComponent<Foods>().m_foodItem.handleScoreDic.Add("blanching", 5);
                        }
                    }
                }
            }
        }
    }

    private void PanMove()
    {

        //鼠标移动
        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = screenPos.z;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = new Vector3(worldPos.x, PanY, worldPos.z);

        //if (CanRotate)
        //{
            //旋转
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward, -1.0f);
            HasWater = false;
            _water.SetActive(false);
            WaterHeight = 0;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward, 1.0f);
            HasWater = false;
            _water.SetActive(false);
            WaterHeight = 0;
        }
       // }
    }

    private void StopMove()
    {
        if (Input.GetMouseButtonDown(1))
        {

            Cursor.lockState = CursorLockMode.Locked;
            //CanRotate = false;

            PickObjs();

            GameController.Instance.PlayerPlay();
        }
    }

    private void NormalState()
    {
        if (HasWater)
        {
            _water.SetActive(true);
            _boiling.SetActive(false);
        }
    }

    private void HoldWaterMove()
    {
        //鼠标移动
        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = screenPos.z;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = new Vector3(worldPos.x, PanY, worldPos.z);



    }

    public override void PickObjs()
    {
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.transform.DOMove(MouseSFM.Instance.transform.GetChild(1).position, 0.1f).
                        OnComplete(() => {
                            MouseSFM.Instance.PickObj = this.gameObject;
                            this.transform.parent = MouseSFM.Instance.transform;
                        });
        MouseSFM.Instance.SwitchState(MouseState.HasPan);
        //m_panState = PanState.Normal;
        ChangeState(PanState.Normal);
    }

    public override void UseTools(GameObject Obj)
    {
        Obj.transform.DOMove(this.transform.GetChild(0).position, 0.1f).
            OnComplete(() => {
                Obj.transform.parent = this.transform;

                if (!_foodDic.ContainsKey(Obj))
                {
                    float cookTime = 0;
                    _foodDic.Add(Obj, cookTime);
                }
            });

    }

    public override void PutObjs()
    {
        GameObject obj = MouseSFM.Instance.PickObj;
        
        obj.transform.DOMove(this.transform.GetChild(0).position, 0.3f).
            OnComplete(() => {
                obj.GetComponent<Rigidbody>().isKinematic = false;
                obj.GetComponent<Collider>().enabled = true;
                obj.transform.parent = this.transform;

                if (!_foodDic.ContainsKey(obj))
                {
                    float cookTime = 0;
                    _foodDic.Add(obj, cookTime);
                }

                MouseSFM.Instance.PickObj = null;
            });
        MouseSFM.Instance.SwitchState(MouseState.Nothing);

    }

    public override void SendObjs(GameObject Obj)
    {
        MouseSFM.Instance.PickObj.transform.parent = null;

        GameController.Instance.PlayerPause();

        
        PanY = Obj.transform.position.y + 0.5f;

        transform.DOMove(new Vector3(Obj.transform.position.x, PanY, Obj.transform.position.z), 0.3f).
            OnComplete(() =>
            {
                this.transform.localEulerAngles = new Vector3(0, 90, 0);
                Cursor.lockState = CursorLockMode.None;
                //CanRotate = true;
                //m_panState = PanState.Move;
                ChangeState(PanState.Move);

            });
    }

    public void ChangeState(PanState panState)
    {
        m_panState = panState;
        if (HasWater)
        {
            switch (m_panState)
            {
                case PanState.Normal:
                    _water.SetActive(true);
                    _boiling.SetActive(false);
                    break;
                case PanState.Move:
                    _water.SetActive(true);
                    _boiling.SetActive(false);
                    break;
                case PanState.Blanching:
                    _water.SetActive(false);
                    _boiling.SetActive(true);
                    break;
            }
        }
    }

    public void SetPosY(Vector3 pos)
    {
        PanY = pos.y;

        MouseSFM.Instance.PickObj.transform.parent = null;
        GameController.Instance.PlayerPause();

        transform.DOMove(pos, 0.3f).
            OnComplete(() =>
            {
                this.transform.localEulerAngles = new Vector3(0, 90, 0);
                Cursor.lockState = CursorLockMode.None;
                //m_panState = PanState.HoldWater;
                ChangeState(PanState.HoldWater);
            });
    }


    private void OnTriggerStay(Collider other)
    {
        if(m_panState==PanState.HoldWater && other.gameObject.tag=="Faucet")
        {
            HasWater = true;
            WaterHeight += Time.fixedDeltaTime;
            //Debug.Log(WaterHeight);
            float WH = Mathf.Clamp(WaterHeight/5,0,1);
            //Debug.Log(WH);
            float Wscale = Mathf.Lerp(2, 4, WH);
            _water.SetActive(true);
            _water.transform.localScale = new Vector3(Wscale, 1, Wscale);
            _water.transform.localPosition = new Vector3(0, Mathf.Lerp(0.1f, 0.2f, WH), 0);

            _boiling.transform.GetChild(0).localScale = _water.transform.localScale;
            _boiling.transform.GetChild(0).localPosition = _water.transform.localPosition;
        }
    }
}
