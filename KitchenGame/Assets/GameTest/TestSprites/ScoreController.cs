using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreController : Singleton<ScoreController>
{
    public Image Complete;

    public int current_num = 0;
    public Dish current_Dish;
    public List<Dish> dishList;
    

    

    protected override void Awake()
    {
        base.Awake();
        DishInit();
    }


    public void DishInit()
    {
        current_Dish = dishList[current_num];
        current_Dish.ListInit();
    }


    public void DishCalc(List<FoodItem> foodList)
    {
        int score = current_Dish.ScoreCalc(foodList);
        current_Dish.Score = 30;
        Debug.Log(score);
        ShowScoreUI(score);
    }

    public void ShowScoreUI(int score)
    {
        GameController.Instance.PlayerPause();
        Cursor.lockState = CursorLockMode.None;

        Complete.gameObject.SetActive(true);
        Complete.transform.GetChild(1).GetComponent<Text>().text = current_Dish._dishItem.dishname;
        Complete.transform.GetChild(2).GetComponent<Text>().text = score.ToString()+"%";
        
    }

    public void SetCurDish()
    {
        current_num++;
        if(current_num<5)
        {
            current_Dish = dishList[current_num];
            current_Dish.ListInit();

            GameController.Instance.PlayerPlay();
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if(current_num==5)
        {
            Debug.Log("End");
            SceneManager.LoadScene(0);
        }

        
    }


}
