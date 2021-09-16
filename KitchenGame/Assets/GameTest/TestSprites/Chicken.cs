using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Dish
{
    public override void ListInit()
    {
        _dishItem = new DishItem();
        _dishItem.dishname = "»ÆìË¼¦";

        FoodItem foodItem1 = new FoodItem();
        foodItem1.foodName = "¼¦";
        foodItem1.handleScoreDic.Add("cut", 10);
        foodItem1.handleScoreDic.Add("blanching", 5);
        foodItem1.handleScoreDic.Add("fried", 5);
        foodItem1.handleScoreDic.Add("sugar", 5);
        foodItem1.handleScoreDic.Add("pepper", 5);
        foodItem1.handleScoreDic.Add("salt", 5);
        foodItem1.handleScoreDic.Add("ÁÏ¾Æ", 5);
        _dishItem.foodScoreDic.Add(foodItem1, 5);

        FoodItem foodItem2 = new FoodItem();
        foodItem2.foodName = "Ä¢¹½";
        foodItem2.handleScoreDic.Add("cut", 5);
        _dishItem.foodScoreDic.Add(foodItem2, 10);



    }


    public override int ScoreCalc(List<FoodItem> foodList)
    {
        CheckDish(foodList);
        //Debug.Log(Score);
        return Score;
    }
}
