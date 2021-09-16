using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class posters : BasicObj
{
    public Image image;
    public Text text1, text2, text3;

    public enum dishState
    {
        tofu,
        chicken,
        yiduxu,
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
                text1.gameObject.SetActive(true);
                text2.gameObject.SetActive(false);
                text3.gameObject.SetActive(false);
                break;
            case dishState.tofu:
                text1.gameObject.SetActive(false);
                text2.gameObject.SetActive(true);
                text3.gameObject.SetActive(false);
                break;
            case dishState.chicken:
                text1.gameObject.SetActive(false);
                text2.gameObject.SetActive(false);
                text3.gameObject.SetActive(true);
                break;
        }

    }
}
