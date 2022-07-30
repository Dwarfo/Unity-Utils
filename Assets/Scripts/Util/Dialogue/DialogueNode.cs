using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

namespace RPG.Dialogue
{
[Serializable]
    public class DialogueNode : ScriptableObject, INode
    {
        [SerializeField]
        private string uniqueId;
        [SerializeField]
        private string text;
        [SerializeField]
        private Speaker speaker;
        [SerializeField]
        private List<string> children = new List<string>();
        [SerializeField]
        private Rect rect = new Rect(0,0,200,120);

        public Rect GetRect()
        {
            return rect;
        }

        public IEnumerable<string> GetChildren()
        {
            return children;
        }

        public string Text{get {return text;}}
        public int ChildrenCount{ get {return children.Count;}}
        public Speaker SpeakerVal{ get {return speaker;}}
        public string Id { get {return uniqueId;}}
        public string Name { get {return name;} set {name = value;}}
        public Rect RectPos { get {return rect;}}
        public IEnumerable<string> Children { get {return children;}}

        public bool ChildrenContain(string nodeId)
        {
            return children.Contains(nodeId);
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
            if(!children.Contains(childId)){

            }
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

        public void SetSpeaker(Speaker speaker)
        {
            Undo.RecordObject(this, "Change dialog speaker");
            this.speaker = speaker;
            EditorUtility.SetDirty(this);
        }
#endif

        public enum Speaker
        {
            Player,
            AI
        }
    }
}