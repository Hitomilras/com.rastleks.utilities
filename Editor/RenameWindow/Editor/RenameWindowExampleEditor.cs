using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RenameWindowExample))]
public class RenameWindowExampleEditor : Editor
{

    private Rect renameButtonRect;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("exampleString"));

        if (GUILayout.Button("R", GUILayout.Width(22)))
            RenameWindow.RenameString(serializedObject.FindProperty("exampleString").stringValue, OnStringRenamed);

        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }

    private void OnStringRenamed(bool wasRenamed, string renamedSting)
    {
        serializedObject.Update();
        serializedObject.FindProperty("exampleString").stringValue = renamedSting;
        serializedObject.ApplyModifiedProperties();
    }

}
