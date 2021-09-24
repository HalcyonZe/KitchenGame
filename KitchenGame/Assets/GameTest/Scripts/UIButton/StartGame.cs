using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public Button startBtn, quitBtn;

    private void Awake()
    {
        //startBtn = this.transform.GetChild(0).GetComponent<Button>();
        //quitBtn = this.transform.GetChild(2).GetComponent<Button>();
        startBtn.onClick.AddListener(StartButton);
        quitBtn.onClick.AddListener(QuitButton);
    }

    public void StartButton()
    {
        Debug.Log("hhh");
        SceneManager.LoadScene(2);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

}
