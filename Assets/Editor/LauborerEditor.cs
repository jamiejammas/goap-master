using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NPC_Entities), true)]
[CanEditMultipleObjects]
public class LauborerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var labourer = (NPC_Entities)target;
        EditorGUILayout.BeginVertical();
        {
            EditorGUILayout.LabelField("AI Visibility",  new GUIStyle() { alignment = TextAnchor.MiddleCenter }, GUILayout.Width(EditorGUIUtility.currentViewWidth));
            EditorGUILayout.LabelField(labourer.currentGoal, EditorStyles.boldLabel, GUILayout.Width(EditorGUIUtility.currentViewWidth));
            EditorGUILayout.LabelField(labourer.currentPlan, EditorStyles.wordWrappedLabel, GUILayout.Width(EditorGUIUtility.currentViewWidth));
        }
        EditorGUILayout.EndVertical();


    }

}
