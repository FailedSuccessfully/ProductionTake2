using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats", order = 2)]
[Serializable]
public class EnemyStats : ScriptableObject
{
    [SerializeField]
    public float moveSpd;
}


// Custom Drawer
[CustomPropertyDrawer(typeof(EnemyStats))]
public class EStatsDrawer : PropertyDrawer {
    // Cached scriptable object editor
    private Editor editor = null;
 
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Draw label
        EditorGUI.PropertyField(position, property, label, true);
        // Draw foldout arrow
        if (property.objectReferenceValue != null)
        {
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none);
        }
 
        // Draw foldout properties
        if (property.isExpanded)
        {
            // Make child fields be indented
            EditorGUI.indentLevel++;
         
            // Draw object properties
            if (!editor)
                Editor.CreateCachedEditor(property.objectReferenceValue, null, ref editor);
            editor.OnInspectorGUI();
         
            // Set indent back to what it was
            EditorGUI.indentLevel--;
        }
    }
}
     
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Enemy), true)]
    public class EnemyEditor : Editor
    {
    }
