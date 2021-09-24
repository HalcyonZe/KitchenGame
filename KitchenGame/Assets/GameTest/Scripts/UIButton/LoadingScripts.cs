using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScripts : MonoBehaviour
{
    //public GameObject objProcessBar;
    public Text baifenbi;

    void Start()
    {
        StartCoroutine(StartLoading("Kitchen"));
    }
    IEnumerator StartLoading(string str)
    {
        float i = 0;
        AsyncOperation acOp = Application.LoadLevelAsync(str);
        acOp.allowSceneActivation = false;
        while (i <= 100)
        {
            i++;
            //objProcessBar.GetComponent<Slider>().value = i / 100;
            yield return new WaitForEndOfFrame();
            baifenbi.text = i.ToString() + "%";
        }
        acOp.allowSceneActivation = true;
    }
}
