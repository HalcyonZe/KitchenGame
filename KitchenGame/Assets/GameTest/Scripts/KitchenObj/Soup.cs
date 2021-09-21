using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soup : MonoBehaviour
{
    public Dictionary<string, int> MatDict = new Dictionary<string, int>();
    public bool TMJsauce = false;

    public void AddMat(string mat)
    {
        if (!MatDict.ContainsKey(mat))
        {
            MatDict.Add(mat, 5);
        }
    }

    public void MakeSoup()
    {
        if(HasMat("╨дсм")&& HasMat("лг"))
        {
            TMJsauce = true;
        }
    }

    private bool HasMat(string mat)
    {
        if(MatDict.ContainsKey(mat))
        {
            return true;
        }
        return false;
    }

}
