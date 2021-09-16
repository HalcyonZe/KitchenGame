using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackBtn : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = this.GetComponent<Button>();
        _button.onClick.AddListener(NextButton);
    }

    private void NextButton()
    {

        GameController.Instance.PlayerPlay();
        Cursor.lockState = CursorLockMode.Locked;
        this.transform.parent.gameObject.SetActive(false);
    }
}
