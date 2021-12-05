using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Scenario))]
public class SceneryEditor : Editor
{
	SerializedObject[] objects;
	int[] IDs;
	public override void OnInspectorGUI()
    {
		bool doMakeVisibleList = GUILayout.Toggle(true, new GUIContent("Show list"));

		objects = new SerializedObject[serializedObject.FindProperty("_commands").arraySize];
		IDs = new int[objects.Length];

		var list = serializedObject.FindProperty("_commands");


		/////// Sort do not work
		int temp = 0;
		SerializedObject tempObj = null;

		for (int write = 0; write < IDs.Length; write++)
		{
			for (int sort = 0; sort < IDs.Length - 1; sort++)
			{
				if (IDs[sort] > IDs[sort + 1])
				{
					temp = IDs[sort + 1];
					IDs[sort + 1] = IDs[sort];
					IDs[sort] = temp;

					tempObj = objects[sort + 1];
					objects[sort + 1] = objects[sort];
					objects[sort] = tempObj;

					list.MoveArrayElement(sort, sort + 1);
				}
			}
		}
		//////////////////////////////////
		
		for (int i = 0; i < objects.Length; i++)
        {
			objects[i] = new UnityEditor.SerializedObject(serializedObject.FindProperty("_commands").GetArrayElementAtIndex(i).objectReferenceValue);
			IDs[i] = objects[i].FindProperty("idOfExecution").intValue;
		}

		serializedObject.Update();
		CustomEditorList.Show(list, objects, true, doMakeVisibleList);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("_currentCommand"));
		//EditorGUILayout.PropertyField(serializedObject.FindProperty("colorPoints"));
		//EditorGUILayout.PropertyField(serializedObject.FindProperty("objects"));
		serializedObject.ApplyModifiedProperties();

		foreach (SerializedObject item in objects)
        {
			item.ApplyModifiedProperties();
        }
	}
}
