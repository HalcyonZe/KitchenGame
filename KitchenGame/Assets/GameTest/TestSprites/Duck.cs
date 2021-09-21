using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : Dish
{
    public override void ListInit()
    {
        _dishItem = new DishItem();
        _dishItem.dishname = "±±æ©øæ—º";

        FoodItem foodItem1 = new FoodItem();
        foodItem1.foodName = "øæ—º";
        foodItem1.handleScoreDic.Add("bake", 10);
        _dishItem.foodScoreDic.Add(foodItem1, 0);

        FoodItem foodItem2 = new FoodItem();
        foodItem2.foodName = "Ω¥”Õ";
        _dishItem.foodScoreDic.Add(foodItem2, 5);

        FoodItem foodItem3 = new FoodItem();
        foodItem3.foodName = "∫ƒ”Õ";
        _dishItem.foodScoreDic.Add(foodItem3, 5);

        FoodItem foodItem4 = new FoodItem();
        foodItem4.foodName = "∑‰√€";
        _dishItem.foodScoreDic.Add(foodItem4, 5);

        FoodItem foodItem5 = new FoodItem();
        foodItem5.foodName = "Ã«";
        _dishItem.foodScoreDic.Add(foodItem5, 5);

        FoodItem foodItem6 = new FoodItem();
        foodItem6.foodName = "√ÊÕ≈";
        _dishItem.foodScoreDic.Add(foodItem6, 10);

    }


    public override int ScoreCalc(List<FoodItem> foodList)
    {
        CheckDish(foodList);
        //Debug.Log(Score);
        return Score;
    }
}
