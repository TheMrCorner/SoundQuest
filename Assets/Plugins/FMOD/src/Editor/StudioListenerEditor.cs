﻿using UnityEditor;

namespace FMODUnity
{
    [CustomEditor(typeof(StudioListener))]
    [CanEditMultipleObjects]
    public class StudioListenerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginDisabledGroup(true);
            var index = serializedObject.FindProperty("ListenerNumber");
            EditorGUILayout.IntSlider(index, 0, FMOD.CONSTANTS.MAX_LISTENERS - 1, "Listener Index");
            EditorGUI.EndDisabledGroup();

            /*EditorGUI.BeginChangeCheck();
            var mask = serializedObject.FindProperty("occlusionMask");
            int temp = EditorGUILayout.MaskField("Occlusion Mask", UnityEditorInternal.InternalEditorUtility.LayerMaskToConcatenatedLayersMask(mask.intValue), UnityEditorInternal.InternalEditorUtility.layers);
            mask.intValue = UnityEditorInternal.InternalEditorUtility.ConcatenatedLayersMaskToLayerMask(temp);

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }*/

        }
    }
}