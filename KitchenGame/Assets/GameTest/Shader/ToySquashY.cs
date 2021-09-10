using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToySquashY : MonoBehaviour
{
    void Start()
    {
        Material mat = GetComponent<Renderer>().material;


        float minY, maxY;
        CalculateMinMaxY(out minY, out maxY);
        mat.SetFloat("_BottomY", minY);
        mat.SetFloat("_TopY", maxY);
        //mat.SetFloat("_ObjectWorldPosY", transform.position.y);
    }

    /// <summary>
    /// ����ģ�� Y ���ֵ�������Сֵ����
    /// </summary>
    /// <param name="minY"></param>
    /// <param name="maxY"></param>
    void CalculateMinMaxY(out float minY, out float maxY)
    {
        Vector3[] vertices = GetComponent<MeshFilter>().mesh.vertices;
        minY = maxY = vertices[0].y;
        for (int i = 1; i < vertices.Length; i++)
        {
            Vector3 tmp = vertices[i];
            float y = tmp.y;
            if (y < minY)
                minY = y;
            if (y > maxY)
                maxY = y;
        }

        // ת������������ȥ
        //minY += transform.position.y;
        //maxY += transform.position.y;
    }
}
