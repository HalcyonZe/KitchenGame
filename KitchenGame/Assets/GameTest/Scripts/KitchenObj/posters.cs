using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class posters : BasicObj
{
    public Image image;
    public List<Text> texts;

    public enum dishState
    {
        tofu,
        chicken,
        yiduxu,
        dongpo,
        duck,
    }
    public dishState m_dishState;


    public override void PickObjs()
    {
        GameController.Instance.PlayerPause();
        Cursor.lockState = CursorLockMode.None;

        image.gameObject.SetActive(true);
        switch (m_dishState)
        {
                        
            case dishState.yiduxu:
                SetText(0);
                break;
            case dishState.tofu:
                SetText(1);
                break;
            case dishState.chicken:
                SetText(2);
                break;
            case dishState.dongpo:
                SetText(3);
                break;
            case dishState.duck:
                SetText(4);
                break;
        }

    }

    public void SetText(int t)
    {
        for(int i=0;i<texts.Count;i++)
        {
            if(i==t)
            {
                texts[i].gameObject.SetActive(true);
            }
            else
            {
                texts[i].gameObject.SetActive(false);
            }
        }
    }

}
