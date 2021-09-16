using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToFu : Dish
{
    public override void ListInit()
    {
        _dishItem = new DishItem();
        _dishItem.dishname = "ÂéÆÅ¶¹¸¯";

        FoodItem foodItem1 = new FoodItem();
        foodItem1.foodName = "¶¹¸¯";
        foodItem1.handleScoreDic.Add("cut", 5);
        foodItem1.handleScoreDic.Add("blanching", 5);
        foodItem1.handleScoreDic.Add("fried", 5);
        foodItem1.handleScoreDic.Add("bean", 5);
        foodItem1.handleScoreDic.Add("pepper", 5);
        foodItem1.handleScoreDic.Add("ÁÏ¾Æ", 5);
        _dishItem.foodScoreDic.Add(foodItem1, 5);



        FoodItem foodItem4 = new FoodItem();
        foodItem4.foodName = "½ª";
        foodItem4.handleScoreDic.Add("cut", 5);
        _dishItem.foodScoreDic.Add(foodItem4, 5);


        FoodItem foodItem6 = new FoodItem();
        foodItem6.foodName = "´Ð";
        _dishItem.foodScoreDic.Add(foodItem6, 5);

    }


    public override int ScoreCalc(List<FoodItem> foodList)
    {
        CheckDish(foodList);
        //Debug.Log(Score);
        return Score;
    }
}
