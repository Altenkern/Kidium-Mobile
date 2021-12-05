using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Frukt)), CanEditMultipleObjects]
public class FruktEditor : Editor
{
    public SerializedProperty
             femine_Prop,
             fruktColor_Prop,
             sprites_Prop,
             fruktType_Prop,
             bonyImenPadezj_Prop,
             bonyRodPadezj_Prop,
             archiImenPadezj_Prop,
             archiRodPadezj_Prop,
             partAudio_Prop,
             shadowAudio_Prop,
             describeAudio_Prop,
             colorAudio_Prop,
             name_Prop,
             color_Prop,
             shadow_Prop,
             partSprite_Prop,
             colorSprite_Prop;

    void OnEnable()
    {
        // Setup the SerializedProperties
        femine_Prop          = serializedObject.FindProperty("_isFemineRod");
        fruktColor_Prop      = serializedObject.FindProperty("_fruktColor");
        sprites_Prop         = serializedObject.FindProperty("_sprites");
        fruktType_Prop       = serializedObject.FindProperty("_fruktType");
        bonyImenPadezj_Prop  = serializedObject.FindProperty("_bonyImenPadezj");
        bonyRodPadezj_Prop   = serializedObject.FindProperty("_bonyRodPadezj");
        archiImenPadezj_Prop = serializedObject.FindProperty("_archiImenPadezj");
        archiRodPadezj_Prop  = serializedObject.FindProperty("_archiRodPadezj");
        partAudio_Prop       = serializedObject.FindProperty("_partAudio");
        shadowAudio_Prop     = serializedObject.FindProperty("_shadowAudio");
        describeAudio_Prop   = serializedObject.FindProperty("_describeAudio");
        colorAudio_Prop      = serializedObject.FindProperty("_colorAudio");
        name_Prop            = serializedObject.FindProperty("_name");
        color_Prop           = serializedObject.FindProperty("_color");
        shadow_Prop          = serializedObject.FindProperty("_shadow");
        partSprite_Prop      = serializedObject.FindProperty("_partSprite");
        colorSprite_Prop     = serializedObject.FindProperty("_colorSprite"); 
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(fruktType_Prop);

        Frukt.FruktType st = (Frukt.FruktType)fruktType_Prop.enumValueIndex;

        EditorGUILayout.PropertyField(bonyImenPadezj_Prop, new GUIContent("bony-imen"));
        EditorGUILayout.PropertyField(bonyRodPadezj_Prop, new GUIContent("bony-rod"));
        EditorGUILayout.PropertyField(archiImenPadezj_Prop, new GUIContent("archi-imen"));
        EditorGUILayout.PropertyField(archiRodPadezj_Prop, new GUIContent("archi-rod"));
        EditorGUILayout.PropertyField(partAudio_Prop, new GUIContent("partAudio"));
        EditorGUILayout.PropertyField(shadowAudio_Prop, new GUIContent("shadowAudio"));
        EditorGUILayout.PropertyField(describeAudio_Prop, new GUIContent("describeAudio"));
        EditorGUILayout.PropertyField(colorAudio_Prop, new GUIContent("colorAudio"));
        EditorGUILayout.PropertyField(name_Prop, new GUIContent("name"));
        EditorGUILayout.PropertyField(femine_Prop, new GUIContent("nameF"));
        EditorGUILayout.PropertyField(shadow_Prop, new GUIContent("shadowSprite"));
        EditorGUILayout.PropertyField(partSprite_Prop, new GUIContent("partSprite"));
        EditorGUILayout.PropertyField(colorSprite_Prop, new GUIContent("colorSprite"));

        switch (st)
        {
            case Frukt.FruktType.fruct:
                EditorGUILayout.PropertyField(sprites_Prop, new GUIContent("sprites"));
                EditorGUILayout.PropertyField(fruktColor_Prop, new GUIContent("frukt-color_Dict"));
                break;

            case Frukt.FruktType.color:
                EditorGUILayout.PropertyField(color_Prop, new GUIContent("color"));
                break;

                //case CameraCommand.CameraCommandTypes.C:
                //    EditorGUILayout.PropertyField(controllable_Prop, new GUIContent("controllable"));
                //    EditorGUILayout.IntSlider(valForC_Prop, 0, 100, new GUIContent("valForC"));
                //    break;

        }


        serializedObject.ApplyModifiedProperties();
    }
}
