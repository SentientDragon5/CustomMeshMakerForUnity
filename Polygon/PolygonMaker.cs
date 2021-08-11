using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class PolygonMaker : MonoBehaviour
{
    public MeshMakerMode mode;

    public List<Vector3> verticies = new List<Vector3>();
    public List<int> triangles = new List<int>();

    [ContextMenu("Make Mesh")]
    public void MakeMesh()
    {
        GetComponent<MeshFilter>().mesh = Polygon(verticies);
    }

    public static Mesh Polygon(List<Vector3> verticies)
    {
        Mesh mesh;
        mesh = new Mesh();
        mesh.name = "Custom " + verticies.Count.ToString() + " sided Polygon";

        verticies = OrderClockwise(verticies, Average(verticies));

        verticies = AverageAdded(verticies);
        mesh.vertices = verticies.ToArray();

        mesh.triangles = Triangles(verticies.Count).ToArray();

        mesh.uv = UV(verticies).ToArray();

        verticies.RemoveAt(0);//remove average

        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        return mesh;


        #region Dependancies
        Vector3 Average(List<Vector3> vector3s)
        {
            int count = 0;
            Vector3 sum = Vector3.zero;
            foreach (Vector3 vert in vector3s)
            {
                sum += vert;
                count++;
            }
            Vector3 average = sum / (float)count;
            return average;
        }
        List<Vector3> AverageAdded(List<Vector3> vector3s)
        {
            List<Vector3> averageFirst = new List<Vector3>();
            averageFirst.Add(Average(vector3s));
            averageFirst.AddRange(vector3s);
            return averageFirst;
        }
        List<Vector3> OrderClockwise(List<Vector3> vector3s, Vector3 center)
        {
            //https://stackoverflow.com/questions/6880899/sort-a-set-of-3-d-points-in-clockwise-counter-clockwise-order
            //https://en.wikipedia.org/wiki/Atan2
            vector3s = vector3s.OrderBy(i => Mathf.Atan2(center.z - i.z, center.x - i.x)).ToList();
            vector3s.Reverse();
            return vector3s;
        }
        

        List<int> Triangles(int sides)
        {
            //center
            //this
            //next

            List<int> tris = new List<int>();
            for (int i = 0; i < sides; i++)
            {
                tris.Add(0);
                tris.Add(i);
                if (i + 1 >= sides)
                {
                    tris.Add(1);
                }
                else
                {
                    tris.Add(i + 1);
                }
            }
            return tris;
        }

        List<Vector2> UV(List<Vector3> verticies)
        {
            List<Vector2> uvs = new List<Vector2>();
            foreach (Vector3 vert in verticies)
            {
                uvs.Add(new Vector2(vert.x, vert.z));
            }
            return uvs;
        }
        #endregion
    }
}
