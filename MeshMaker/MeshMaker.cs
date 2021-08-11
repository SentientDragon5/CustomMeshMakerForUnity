using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MeshMakerMode { View, Move};
public class MeshMaker : MonoBehaviour
{
    public MeshMakerMode mode;
    [Range(0,1)]
    public float hue;

    public List<Vector3> verticies = new List<Vector3>();
    public List<int> triangles = new List<int>();

    [ContextMenu("Make Mesh")]
    public void MakeMesh()
    {
        Mesh mesh;
        mesh = new Mesh();
        mesh.vertices = verticies.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = mesh;
    }
}
