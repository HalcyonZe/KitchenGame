using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stop : MonoBehaviour
{
    private Button homeBtn, playBtn;

    private void Awake()
    {
        homeBtn = this.transform.GetChild(2).GetComponent<Button>();
        playBtn = this.transform.GetChild(1).GetComponent<Button>();
        homeBtn.onClick.AddListener(HomeButton);
        playBtn.onClick.AddListener(PlayButton);
    }

    private void HomeButton()
    {
        SceneManager.LoadScene(0);
    }

    private void PlayButton()
    {
        TimeController.Instance.TimeIn();
        GameController.Instance.PlayerPlay();
        Cursor.lockState = CursorLockMode.Locked;
        this.gameObject.SetActive(false);
    }

}
