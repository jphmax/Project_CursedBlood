using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraScript))]
public class CameraScriptEditor : Editor
{
    SerializedProperty transformsProperty;
    SerializedProperty targetGroupProperty;
    SerializedProperty[] radiusGroupProperty = new SerializedProperty[2];
    SerializedProperty[] distanceGroupProperty = new SerializedProperty[2];

    void OnEnable()
    {
        transformsProperty = serializedObject.FindProperty("transforms");
        targetGroupProperty = serializedObject.FindProperty("targetgroup");
        radiusGroupProperty[0] = serializedObject.FindProperty("minRadius");
        radiusGroupProperty[1] = serializedObject.FindProperty("maxRadius");
        distanceGroupProperty[0] = serializedObject.FindProperty("minDistance");
        distanceGroupProperty[1] = serializedObject.FindProperty("maxDistance");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        if (transformsProperty.arraySize != 2)
        {
            transformsProperty.arraySize = 2;
        }
        EditorGUILayout.PropertyField(transformsProperty.GetArrayElementAtIndex(0), new GUIContent("Object 1"));
        EditorGUILayout.PropertyField(transformsProperty.GetArrayElementAtIndex(1), new GUIContent("Object 2"));
        EditorGUILayout.PropertyField(radiusGroupProperty[0], new GUIContent("Minimum Radius"));
        EditorGUILayout.PropertyField(radiusGroupProperty[1], new GUIContent("Maximum Radius"));
        EditorGUILayout.PropertyField(distanceGroupProperty[0], new GUIContent("Minimum Distance"));
        EditorGUILayout.PropertyField(distanceGroupProperty[1], new GUIContent("Maximum Distance"));
        EditorGUILayout.PropertyField(targetGroupProperty, new GUIContent("Target Group"));

        serializedObject.ApplyModifiedProperties();
    }
}
