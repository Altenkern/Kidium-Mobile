using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraCommand)), CanEditMultipleObjects]
public class CommandEditor : Editor
{
    public SerializedProperty
          commandType_Prop,
          vectorStartOfMove_Prop,
          vectorEndOfMove_Prop,
          speed_Prop,
          percentOfZoom_Prop;

    void OnEnable()
    {
        // Setup the SerializedProperties
        commandType_Prop = serializedObject.FindProperty("_commandType");
        vectorStartOfMove_Prop = serializedObject.FindProperty("_vectorStartOfMove");
        vectorEndOfMove_Prop = serializedObject.FindProperty("_vectorEndOfMove");
        speed_Prop = serializedObject.FindProperty("_speed");
        percentOfZoom_Prop = serializedObject.FindProperty("_percentOfZoom");
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(commandType_Prop);

        CameraCommand.CameraCommandTypes st = (CameraCommand.CameraCommandTypes)commandType_Prop.enumValueIndex;

        switch (st)
        {
            case CameraCommand.CameraCommandTypes.move:
                EditorGUILayout.PropertyField(vectorStartOfMove_Prop, new GUIContent("vectorStartOfMove"));
                EditorGUILayout.PropertyField(vectorEndOfMove_Prop, new GUIContent("vectorEndOfMove"));
                EditorGUILayout.PropertyField(speed_Prop, new GUIContent("speed"));
                break;

            case CameraCommand.CameraCommandTypes.zoom:
                EditorGUILayout.PropertyField(percentOfZoom_Prop, new GUIContent("percentOfZoom"));
                EditorGUILayout.PropertyField(speed_Prop, new GUIContent("speed"));
                break;

            //case CameraCommand.CameraCommandTypes.C:
            //    EditorGUILayout.PropertyField(controllable_Prop, new GUIContent("controllable"));
            //    EditorGUILayout.IntSlider(valForC_Prop, 0, 100, new GUIContent("valForC"));
            //    break;

        }


        serializedObject.ApplyModifiedProperties();
    }
}
