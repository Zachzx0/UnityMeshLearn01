using UnityEngine;
using System.Collections;


public class CreateMeshSphereTest : MonoBehaviour
{
    MeshFilter myMeshFilter;
    public float radius = 1;
    void Start()
    {
        myMeshFilter = GetComponent<MeshFilter>();

        Mesh ball = new Mesh();

        //顶点数组
        Vector3[] ballVertices = new Vector3[182];
        //三角形数组
        int[] ballTriangles = new int[1080];
        /*水平每18度、垂直每18度确定一个顶点，
        顶部和底部各一个顶点，一共是9x20+2=182个顶点。
        每一环与相邻的下一环为一组，之间画出40个三角形，一共8组。
        顶部和底部各与相邻环画20个三角形，总三角形数量40x8+20x2=360,
        三角形索引数量360x3=1080*/


        int verticeCount = 0;
        for (int vD = 18; vD < 180; vD += 18)
        {
            float circleHeight =
            radius * Mathf.Cos(vD * Mathf.Deg2Rad);
            float circleRadius =
            radius * Mathf.Sin(vD * Mathf.Deg2Rad);
            for (int hD = 0; hD < 360; hD += 18)
            {
                ballVertices[verticeCount] =
                new Vector3(
                circleRadius * Mathf.Cos(hD * Mathf.Deg2Rad),
                circleHeight,
                circleRadius * Mathf.Sin(hD * Mathf.Deg2Rad));
                verticeCount++;
            }
        }
        ballVertices[180] = new Vector3(0, radius, 0);
        ballVertices[181] = new Vector3(0, -radius, 0);
        ball.vertices = ballVertices;


        int triangleCount = 0;
        for (int j = 0; j < 8; j++)
        {
            for (int i = 0; i < 20; i++)
            {
                ballTriangles[triangleCount++] =
                j * 20 + i;
                ballTriangles[triangleCount++] =
                (j + 1) * 20 + (i == 19 ? 0 : i + 1);
                ballTriangles[triangleCount++] =
                (j + 1) * 20 + i;
                ballTriangles[triangleCount++] =
                j * 20 + i;
                ballTriangles[triangleCount++] =
                j * 20 + (i == 19 ? 0 : i + 1);
                ballTriangles[triangleCount++] =
                (j + 1) * 20 + (i == 19 ? 0 : i + 1);
            }
        }
        for (int i = 0; i < 20; i++)
        {
            ballTriangles[triangleCount++] =
            180;
            ballTriangles[triangleCount++] =
            (i == 19 ? 0 : i + 1);
            ballTriangles[triangleCount++] =
            i;
            ballTriangles[triangleCount++] =
            181;
            ballTriangles[triangleCount++] =
            160 + i;
            ballTriangles[triangleCount++] =
            160 + (i == 19 ? 0 : i + 1);
        }
        ball.triangles = ballTriangles;
        ball.RecalculateNormals();
        myMeshFilter.mesh = ball;
    }
}

