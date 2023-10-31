using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TargetMovement))]
public class TargetMovementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draws the default inspector content

        TargetMovement targetScript = (TargetMovement)target;

        if (GUILayout.Button("Toggle God Mode"))
        {
            targetScript.godMode = !targetScript.godMode;
        }
    }
}
