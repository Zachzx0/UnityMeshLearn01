using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSphere : MonoBehaviour {

    //public GameObject go;
    public int angleTt = 1;
    public int angleFy = 1;
    public float r = 10;

    int _verticlesCount;
    int _triCount;

    int horizontalNum;
    int verticalNum;

   List<Vector3> _verticlesList;
    List<int> _trianglesList;
    List<Vector2> _uvs;

    const string matPath = "Materials/mat_earth";
    const string texturePath = "Textures/worldmap";

    void Start()
    {
        CreateEarth();
    }

    void CreateEarth()
    {

        horizontalNum = 360 / angleTt;
        verticalNum = 180 / angleFy;
        _verticlesList = new List<Vector3>();
        _trianglesList = new List<int>();
        _uvs = new List<Vector2>();

        GameObject newGO = Instantiate(new GameObject("Earth"),Vector3.zero,Quaternion.identity);
        newGO.AddComponent<MeshFilter>();
        newGO.AddComponent<MeshRenderer>();

        Material mat = Resources.Load<Material>(matPath);
        Texture texEarth = Resources.Load<Texture>(texturePath);

        newGO.GetComponent<MeshRenderer>().material = mat;

        Mesh mesh = newGO.GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        CreateObj(ref _verticlesList, ref _trianglesList);
        CreateUv(ref _uvs);
        Debug.Log("<color=red>vertListCount:" + _verticlesList.Count + "</color>");
        Debug.Log("<color=red>_trianglesListCount:" + _trianglesList.Count + "</color>");
        Debug.Log("<color=red>_uvsCount:" + _uvs.Count + "</color>");
        mesh.vertices = _verticlesList.ToArray();
        mesh.triangles = _trianglesList.ToArray();
        mesh.uv = _uvs.ToArray();


    }


    void CreateObj(ref List<Vector3> vList, ref List<int> tList)
    {
        for (int i =0; i <=360; i += angleFy)
        {
            for (int j = 0; j <=360; j += angleTt)
            {

                float x = r * Mathf.Sin(Deg2Rad(i)) * Mathf.Cos(Deg2Rad(j));
                float y = r * Mathf.Cos(Deg2Rad(i));
                float z = r * Mathf.Sin(Deg2Rad(i))* Mathf.Sin(Deg2Rad(j));
                Vector3 pos = new Vector3(x, y, z);
                vList.Add(pos);
                //Instantiate(go, pos, Quaternion.identity);
                //Debug.Log("pos:" + pos.ToString());
            }
        }

        for(int i = 0; i < verticalNum+1; i++)
        {
            for(int j = 0; j < horizontalNum+1; j++)
            {
                tList.Add(i * horizontalNum + j);
                tList.Add((i + 1) * horizontalNum + (j+1));
                tList.Add((i+1) * horizontalNum + j);
                tList.Add((i + 1) * horizontalNum +(j + 1));
                tList.Add(i * horizontalNum + j);
                tList.Add(i * horizontalNum + (j + 1));
            }
        }
    }

    void CreateUv(ref List<Vector2> uvs)
    {
        float verticalOffset = 1.0f / verticalNum;
        float horizontalOffset = 1.0f / horizontalNum;

        for(int i = 0; i < verticalNum *2 +1; i++)
        {
            for(int j = 0; j < horizontalNum+1; j++)
            {
                uvs.Add(new Vector2(j * horizontalOffset, i*verticalOffset));
            }
        }
    }

    float Deg2Rad(float angle)
    {
        return angle * Mathf.Deg2Rad;
    }
}
