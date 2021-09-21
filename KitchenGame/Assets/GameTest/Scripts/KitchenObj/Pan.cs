using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class Pan : BasicObj
{
    #region 枚举
    public enum PanState
    {
        Normal,
        Cooking,
        Move,
        HoldWater,
    }
    public PanState m_panState = PanState.Normal ;
    #endregion

    #region 属性
    protected Dictionary<GameObject, float> _foodDic = new Dictionary<GameObject, float>();
    private float PanY = 0;

    protected int Hasliquid = 0;
    //protected bool HasWater = false;
    public float WaterHeight = -2f;
    public float foodDist = 0.1f;
    #endregion

    #region 组件
    protected GameObject _boiling, _water, _smoke;
    private AudioSource m_audioSource;
    private ParticleSystem ps1, ps2;
    #endregion


    #region 高度属性
    [Header("高度信息")]
    public float MinHeight;
    public float MaxHeight;
    public float MinScale;
    public float MaxScale;
    #endregion

    private void Start()
    {
        _water = this.transform.GetChild(1).gameObject;
        _boiling = this.transform.GetChild(2).GetChild(0).gameObject;
        _smoke = this.transform.GetChild(2).GetChild(1).gameObject;
        ps1 = transform.GetChild(3).GetComponent<ParticleSystem>();
        ps2 = transform.GetChild(4).GetComponent<ParticleSystem>();

        m_audioSource = this.GetComponent<AudioSource>();

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
                    i--;
                }          
                else if((this.transform.position.y- food.transform.position.y)> foodDist)
                {
                    food.GetComponent<Foods>().m_foodState = Foods.FoodState.normal;
                    _foodDic.Remove(food);
                    food.transform.parent = null;
                    i--;
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
                PanMove();
                StopMove();
                break;
        }
    }

    public void ChangeState(PanState panState)
    {
        m_panState = panState;
        if (Hasliquid>0)
        {
            _water.SetActive(true);
            _boiling.SetActive(false);
            _smoke.SetActive(false);

            if (m_panState == PanState.Cooking)
            {
                m_audioSource.Play();
                _smoke.SetActive(true);
                if(Hasliquid==1)
                {
                    _water.SetActive(false);
                    _boiling.SetActive(true);
                }
            }
            /*switch (m_panState)
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
                    m_audioSource.Play();
                    break;
            }*/
        }
        if(m_panState!= PanState.Cooking)
        {
            m_audioSource.Stop();
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

    public virtual void Cooking()
    {
        if (_foodDic.Count > 0) {

            if (Hasliquid == 1)
            {
                for (int i = 0; i < _foodDic.Count; i++)
                {
                    GameObject food = _foodDic.ElementAt(i).Key;
                    food.GetComponent<Foods>().m_foodState = Foods.FoodState.boil;
                }
            }
            else if (Hasliquid == 2)
            {
                for (int i = 0; i < _foodDic.Count; i++)
                {
                    GameObject food = _foodDic.ElementAt(i).Key;
                    food.GetComponent<Foods>().m_foodState = Foods.FoodState.fry;
                }
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

        if (m_panState == PanState.Move)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.forward, -1.0f);

                //Hasliquid = 0;
                //_water.SetActive(false);
                //WaterHeight = 0;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.forward, 1.0f);

                //Hasliquid = 0;
                //_water.SetActive(false);
                //WaterHeight = 0;
            }
            if (Hasliquid > 0)
            {
                Color _color = _water.GetComponent<Renderer>().material.GetColor("_Color");
                if (transform.localRotation.x > 0.4f)
                {
                    ps1.gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", _color);
                    ps1.Play();
                    Hasliquid = 0;
                    _water.SetActive(false);
                    WaterHeight = 0;
                }
                if (transform.localRotation.x < -0.4f)
                {
                    ps2.gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", _color);
                    ps2.Play();
                    Hasliquid = 0;
                    _water.SetActive(false);
                    WaterHeight = 0;
                }

            }
            
        }
    }

    protected void StopMove()
    {
        if (Input.GetMouseButtonDown(1))
        {

            Cursor.lockState = CursorLockMode.Locked;

            PickObjs();

            GameController.Instance.PlayerPlay();
        }
    }

    public override void PickObjs()
    {
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.transform.DOMove(MouseSFM.Instance.transform.GetChild(1).position, 0.1f).
                        OnComplete(() => {                           
                            this.transform.parent = MouseSFM.Instance.transform;
                            MouseSFM.Instance.PickObj = this.gameObject;
                            MouseSFM.Instance.SwitchState(MouseState.HasPan);
                        });
        
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
        
        if(obj.TryGetComponent<dough>(out dough dough))
        {
            return;
        }

        obj.transform.DOMove(this.transform.GetChild(0).position, 0.3f).
            OnComplete(() => {
                if (Hasliquid == 1)
                {
                    AudioController.Instance.SetAudioPlay("PutInWater");
                }
                else if(Hasliquid == 2)
                {
                    AudioController.Instance.SetAudioPlay("Fried");
                }
                else
                {
                    AudioController.Instance.SetAudioPlay("Put");
                }

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

    public void SendObjs(GameObject Obj)
    {
        MouseSFM.Instance.PickObj.transform.parent = null;

        GameController.Instance.PlayerPause();

        
        PanY = Obj.transform.position.y + 0.5f;

        transform.DOMove(new Vector3(Obj.transform.position.x, PanY, Obj.transform.position.z), 0.3f).
            OnComplete(() =>
            {
                this.transform.localEulerAngles = new Vector3(0, MouseSFM.Instance.transform.eulerAngles.y+180, 0);
                Cursor.lockState = CursorLockMode.None;
                ChangeState(PanState.Move);

            });
    }

    
    public void SetPosY(Vector3 pos)
    {
        PanY = pos.y;

        MouseSFM.Instance.PickObj.transform.parent = null;
        MouseSFM.Instance.PickObj = null;
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
            //HasWater = true;
            Hasliquid = 1;
            _water.GetComponent<Renderer>().material.SetColor("_Color", new Color(1,1,1));
            WaterHeight += Time.fixedDeltaTime;

            SetWaterHeight();

        }
    }

    private Color materialColor;
    private void OnParticleCollision(GameObject other)
    {
        Hasliquid = 3;
        if (other.tag == "oil")
        {
            Hasliquid = 2;
        }
        
        #region 颜色混合
        Color color;
        Color curColor = _water.GetComponent<Renderer>().material.GetColor("_Color");
        if (materialColor != other.GetComponent<Renderer>().material.GetColor("_BaseColor"))
        {
            materialColor = other.GetComponent<Renderer>().material.GetColor("_BaseColor");
            color = curColor * materialColor;
        }
        else
        {
            color = curColor;
        }
        _water.GetComponent<Renderer>().material.SetColor("_Color", color);

        #endregion
        
        #region 颜色替换
        /*Color new_color = other.GetComponent<Renderer>().material.GetColor("_BaseColor");
        _water.GetComponent<Renderer>().material.SetColor("_Color", new_color);*/
        #endregion


        WaterHeight += Time.fixedDeltaTime;

        SetWaterHeight();


    }

    protected virtual void SetWaterHeight()
    {
        float WH = Mathf.Clamp(WaterHeight / 8, 0, 1);
        float Wscale = Mathf.Lerp(MinScale, MaxScale, WH);
        float Wposy = Mathf.Lerp(MinHeight, MaxHeight, WH);

        //float Wscale = Mathf.Lerp(0.1f, 4, WH);
        _boiling.SetActive(false);
        _water.SetActive(true);
        _water.transform.localScale = new Vector3(Wscale, 1, Wscale);
        _water.transform.localPosition = new Vector3(0, Wposy, 0);
        //_water.transform.localPosition = new Vector3(0, Mathf.Lerp(0.1f, 0.2f, WH), 0);

        _boiling.transform.localScale = _water.transform.localScale;
        _boiling.transform.localPosition = _water.transform.localPosition;
    }

}
