using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using EditorLightUtilities;

[CustomEditor(typeof(Sunlight))]
public class SunlightEditor : Editor
{
    [MenuItem("GameObject/Light/Sunlight", false, 10)]
    static void CreateCustomGameObject(MenuCommand menuCommand)
    {
        // Create a custom game object
        GameObject sunlight = new GameObject("Sunlight");
        // Ensure it gets reparented if this was a context click (otherwise does nothing)
        GameObjectUtility.SetParentAndAlign(sunlight, menuCommand.context as GameObject);
        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(sunlight, "Create " + sunlight.name);
        Selection.activeObject = sunlight;
        sunlight.AddComponent<Sunlight>();
    }

    public Sunlight sunlight;
    SerializedProperty animationParameters;
    SerializedProperty useManager;
    SerializedProperty sunlightParameters;
    SerializedProperty drawGizmo;
    SerializedProperty gizmoSize;
    public List<Vector3> lightPivots;

    void OnEnable()
    {
        sunlight = (Sunlight)serializedObject.targetObject;
        useManager = serializedObject.FindProperty("useManager");
        sunlightParameters = serializedObject.FindProperty("sunlightParameters");
        drawGizmo = serializedObject.FindProperty("drawGizmo");
        gizmoSize = serializedObject.FindProperty("gizmoSize");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(useManager);
        EditorGUILayout.PropertyField(sunlightParameters, true);

        EditorGUI.BeginDisabledGroup(useManager.boolValue);
        EditorGUI.indentLevel = 0;
        LightUIUtilities.DrawSplitter();
        LightUIUtilities.DrawHeader("Visualization");
        EditorGUI.indentLevel = 1;

        EditorGUILayout.PropertyField(drawGizmo);
        EditorGUILayout.PropertyField(gizmoSize);
        EditorGUI.EndDisabledGroup();

        serializedObject.ApplyModifiedProperties();
    }
}
