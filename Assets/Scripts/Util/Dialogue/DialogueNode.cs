using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

namespace RPG.Dialogue
{
[Serializable]
    public class DialogueNode : ScriptableObject
    {
        [SerializeField]
        bool isPlayerSpeaking = false;
        [SerializeField]
        private string uniqueId;
        [SerializeField]
        private string text;
        [SerializeField]
        private List<string> children = new List<string>();
        [SerializeField]
        private Rect rect = new Rect(0,0,200,120);

        public Rect GetRect()
        {
            return rect;
        }

        public string GetText()
        {
            return text;
        }

        public List<string> GetChildren()
        {
            return children;
        }

        public bool IsPlayerSpeaking()
        {
            return isPlayerSpeaking;
        }

#if UNITY_EDITOR
        public void SetPosition(Vector2 newPos)
        {
            Undo.RecordObject(this, "Updated position");
            this.rect.position = newPos;
            EditorUtility.SetDirty(this);
        }

        public void SetText(string newText)
        {   
            if(newText != this.text)
            {
                Undo.RecordObject(this, "Updated text");
                this.text = newText;
                EditorUtility.SetDirty(this);
            }

        }

        public void AddChild(string childId)
        {
            Undo.RecordObject(this, "Added child");
            children.Add(childId);
            EditorUtility.SetDirty(this);
        }

        public void RemoveChild(string childId)
        {
            Undo.RecordObject(this, "Removed child");
            children.Remove(childId);
            EditorUtility.SetDirty(this);
        }

        public void SetParentSpeaking(bool newIsPlayerSpeaking)
        {
            Undo.RecordObject(this, "Change dialog speaker");
            this.isPlayerSpeaking = newIsPlayerSpeaking;
            EditorUtility.SetDirty(this);
        }
#endif
    }
}