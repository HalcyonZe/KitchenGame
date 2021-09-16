using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextBtn : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = this.GetComponent<Button>();
        _button.onClick.AddListener(NextButton);
    }

    private void NextButton()
    {        

        ScoreController.Instance.SetCurDish();
        this.transform.parent.gameObject.SetActive(false);
    }

}
