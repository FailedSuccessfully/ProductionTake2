using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
[Serializable]
public class PlayerStats : ScriptableObject
{
    [SerializeField]
    public float maxSpeed, acceleration, deceleration, jumpForce, dashForce, boostForce, dashCooldown, dashDuration, wallGrind, downForce;
    [SerializeField]
    public int jumpNum, playerGravity;
}


// Custom Drawer
[CustomPropertyDrawer(typeof(PlayerStats))]
public class StatsDrawer : PropertyDrawer {
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
    [CustomEditor(typeof(Player), true)]
    public class PlayerEditor : Editor
    {
    }
