using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

public class GenericNodeEditor : EditorWindow
{
        //[MenuItem("Window/Dialogue Editor")]
        public static void ShowEditorWindow()
        {
            
        }

        [OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceId, int line)
        {
        
        
            return false;
        }

        private void OnSelectionChange()
        {
            
        }

        private void OnGUI()
        {

        }
}
