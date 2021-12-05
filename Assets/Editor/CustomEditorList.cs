using UnityEditor;
using UnityEngine;

public static class CustomEditorList
{
	private static GUILayoutOption FloatWidth = GUILayout.Width(40f);
	public static void Show(SerializedProperty list, SerializedObject[] objects, bool showListSize = true, bool showListLabel = true)
	{

		if (showListLabel)
		{
			EditorGUILayout.PropertyField(list);
			EditorGUI.indentLevel += 1;
		}
		if (!showListLabel || list.isExpanded)
		{
			if (showListSize)
			{
				EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
			}
			for (int i = 0; i < list.arraySize; i++)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), GUIContent.none);
				EditorGUILayout.PropertyField(objects[i].FindProperty("idOfExecution"), GUIContent.none, FloatWidth);
				EditorGUILayout.EndHorizontal();
			}
		}
		if (showListLabel)
		{
			EditorGUI.indentLevel -= 1;
		}
	}
}
