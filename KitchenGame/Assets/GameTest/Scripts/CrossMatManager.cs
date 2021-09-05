using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossMatManager : Singleton<CrossMatManager>
{
    public Dictionary<string, Material> CrossMatDic;
    [System.Serializable]
    public struct CrossMat
    {
        public string foodName;
        public Material foodCrossMat;
    }
    public CrossMat[] crossMats;
    
    protected override void Awake()
    {
        base.Awake();

        CrossMatDic = new Dictionary<string, Material>();
        for(int i = 0; i < crossMats.Length; i++)
        {
            CrossMatDic.Add(crossMats[i].foodName,crossMats[i].foodCrossMat);
        }
    }

    public Material GetMaterial(string foodName)
    {
        if(CrossMatDic.ContainsKey(foodName))
        {
            return CrossMatDic[foodName];
        }
        else
        {
            return null;
        }
    }

}
