using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MeshMaker)), CanEditMultipleObjects]
public class MeshMakerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MeshMaker maker = (MeshMaker)target;

        GUILayout.Label("Mode ", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("View"))
        {
            maker.mode = MeshMakerMode.View;
        }
        if (GUILayout.Button("Move"))
        {
            maker.mode = MeshMakerMode.Move;
        }
        GUILayout.EndHorizontal();

        serializedObject.Update();
        if (GUILayout.Button("Make Mesh"))
        {
            maker.MakeMesh();
        }

        DrawDefaultInspector();
    }

    protected virtual void OnSceneGUI()
    {
        MeshMaker maker = (MeshMaker)target;

        for (int i = 0; i < maker.verticies.Count; i++)
        {
            if (maker.mode == MeshMakerMode.Move)
            {
                Vector3 edit = maker.verticies[i] + maker.transform.position;
                edit = Handles.PositionHandle(edit, Quaternion.identity);
                maker.verticies[i] = edit - maker.transform.position;
            }
            Handles.Label(maker.verticies[i] + maker.transform.position, new GUIContent(i.ToString()));
        }
    }
}