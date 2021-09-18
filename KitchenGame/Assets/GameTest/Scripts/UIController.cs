using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{
    #region ×é¼þ
    public Sprite point, click;
    public Image Objs;
    private Text ObjName;
    private Image image;

    private GameObject tip;
    private Text toolName,UiNumber;
    private int useNumber=0;
    #endregion

    private void Start()
    {
        image = this.transform.GetChild(0).GetComponent<Image>();

        tip = this.transform.GetChild(1).GetChild(2).gameObject;
        toolName = tip.transform.GetChild(1).GetComponent<Text>();
        UiNumber = tip.transform.GetChild(2).GetComponent<Text>();

        ObjName = Objs.transform.GetChild(0).GetComponent<Text>();
    }

    public void ToPoint()
    {
        image.sprite = point;
    }

    public void ToClick()
    {
        image.sprite = click;
    }

    public void ToNothing()
    {
        image.GetComponent<CanvasRenderer>().SetAlpha(0);
    }

    public void ShowMouse()
    {
        image.GetComponent<CanvasRenderer>().SetAlpha(1);
    }

    public void OpenTip(string name,int num)
    {
        tip.SetActive(true);
        toolName.text = name;
        useNumber += num;
        UiNumber.text = (useNumber).ToString();
    }

    public void CloseTip()
    {
        tip.SetActive(false);
        toolName.text = name;
        useNumber = 0;
        UiNumber.text = useNumber.ToString();
    }

    public void ShowObjName(string name)
    {
        Objs.gameObject.SetActive(true);
        ObjName.text = name;
    }

    public void CloseObjName()
    {
        Objs.gameObject.SetActive(false);
    }

}
