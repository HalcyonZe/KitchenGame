using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    public Image StopImage;

    private void FixedUpdate()
    {
        GameStop();
    }

    public void GameStop()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPause();
            Cursor.lockState = CursorLockMode.None;
            StopImage.gameObject.SetActive(true);
        }
    }

    public void PlayerPause()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().CanMove = false;
        GameObject.Find("Main Camera").GetComponent<CameraController>().CanCamera = false;
        //GameObject.Find("Main Camera").GetComponent<MouseControl>().CanClick = false;
        MouseSFM.Instance.CanClick = false;
        UIController.Instance.ToNothing();
    }

    public void PlayerPlay()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().CanMove = true;
        GameObject.Find("Main Camera").GetComponent<CameraController>().CanCamera = true;
        //GameObject.Find("Main Camera").GetComponent<MouseControl>().CanClick = true;
        MouseSFM.Instance.CanClick = true;
        UIController.Instance.ShowMouse();
    }

}
