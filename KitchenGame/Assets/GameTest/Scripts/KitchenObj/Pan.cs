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
        Cooking,
        Move,
        HoldWater,
    }
    public PanState m_panState = PanState.Normal ;

    protected Dictionary<GameObject, float> _foodDic = new Dictionary<GameObject, float>();
    private float PanY = 0;
    //private bool CanRotate = false;

    protected GameObject _boiling, _water;
    protected bool HasWater = false;
    public float WaterHeight = -2f;
    public float foodDist = 0.1f;

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

    protected void JudgeFood()
    {
        if (_foodDic.Count > 0)
        {
            for (int i = 0; i < _foodDic.Count; i++)
            {
                GameObject food = _foodDic.ElementAt(i).Key;
                if (food.transform.parent != this.transform)
                {
                    food.GetComponent<Foods>().m_foodState = Foods.FoodState.normal;
                    _foodDic.Remove(food);
                }          
                else if((this.transform.position.y- food.transform.position.y)> foodDist)
                {
                    food.GetComponent<Foods>().m_foodState = Foods.FoodState.normal;
                    _foodDic.Remove(food);
                    food.transform.parent = null;
                }
            }
        }
    }

    protected void PanStateUpdate()
    {
        switch (m_panState)
        {
            case PanState.Cooking:
                Cooking();
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

    public virtual void Cooking()
    {
        
        if (HasWater && _foodDic.Count > 0)
        {
            for(int i=0;i<_foodDic.Count;i++)
            {
                GameObject food = _foodDic.ElementAt(i).Key;
                food.GetComponent<Foods>().m_foodState = Foods.FoodState.Blanching;
            }
        }
    }

    protected void PanMove()
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

    protected void StopMove()
    {
        if (Input.GetMouseButtonDown(1))
        {

            Cursor.lockState = CursorLockMode.Locked;
            //CanRotate = false;

            PickObjs();

            GameController.Instance.PlayerPlay();
        }
    }

    protected void HoldWaterMove()
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
                            this.transform.parent = MouseSFM.Instance.transform;
                        });
        MouseSFM.Instance.PickObj = this.gameObject;
        MouseSFM.Instance.SwitchState(MouseState.HasPan);
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
                case PanState.Cooking:
                    _water.SetActive(false);
                    _boiling.SetActive(true);
                    break;
            }
        }
        if(m_panState!= PanState.Cooking)
        {
            if (_foodDic.Keys.Count >= 0)
            {
                for (int i = 0; i < _foodDic.Keys.Count; i++)
                {
                    GameObject food = _foodDic.ElementAt(i).Key;
                    food.GetComponent<Foods>().m_foodState = Foods.FoodState.normal;
                }
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

    public virtual void OnTriggerStay(Collider other)
    {
        if (m_panState == PanState.HoldWater && other.gameObject.tag == "Faucet")
        {
            HasWater = true;
            WaterHeight += Time.fixedDeltaTime;

            float WH = Mathf.Clamp(WaterHeight / 8, 0, 1);
            float Wscale = Mathf.Lerp(0.1f, 4, WH);
            _water.SetActive(true);
            _water.transform.localScale = new Vector3(Wscale, 1, Wscale);
            _water.transform.localPosition = new Vector3(0, Mathf.Lerp(0.1f, 0.2f, WH), 0);

            _boiling.transform.GetChild(0).localScale = _water.transform.localScale;
            _boiling.transform.GetChild(0).localPosition = _water.transform.localPosition;

        }
    }


}
