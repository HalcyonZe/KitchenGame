using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dongpo : Dish
{
    public override void ListInit()
    {
        _dishItem = new DishItem();
        _dishItem.dishname = "������";

        FoodItem foodItem1 = new FoodItem();
        foodItem1.foodName = "����";
        foodItem1.handleScoreDic.Add("blanching", 20);
        foodItem1.handleScoreDic.Add("boiledOneHour", 10);
        foodItem1.handleScoreDic.Add("boiledTwoHour", 10);
        foodItem1.handleScoreDic.Add("�Ͼ�", 5);
        _dishItem.foodScoreDic.Add(foodItem1, 5);

        FoodItem foodItem2 = new FoodItem();
        foodItem2.foodName = "��";
        foodItem2.handleScoreDic.Add("cut", 5);
        _dishItem.foodScoreDic.Add(foodItem2, 5);

        FoodItem foodItem4 = new FoodItem();
        foodItem4.foodName = "��";
        _dishItem.foodScoreDic.Add(foodItem4, 5);

        FoodItem foodItem5 = new FoodItem();
        foodItem5.foodName = "��Ƥ";
        _dishItem.foodScoreDic.Add(foodItem5, 5);


    }


    public override int ScoreCalc(List<FoodItem> foodList)
    {
        CheckDish(foodList);
        //Debug.Log(Score);
        return Score;
    }
}
