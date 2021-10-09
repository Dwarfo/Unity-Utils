using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RPG.Dialogue
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 0)]
    public class Dialogue : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField]
        List<DialogueNode> nodes = new List<DialogueNode>();
        Dictionary<string, DialogueNode> nodeLookup = new Dictionary<string, DialogueNode>();
        [SerializeField]
        Vector2 newNodeOffseet = new Vector2(250,0);
        
        public IEnumerable<DialogueNode> GetAllNodes()
        {
            return nodes;
        }

        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode)
        {
            if(parentNode.GetChildren().Count == 0)
            {
                yield break;
            }

            foreach(string childId in parentNode.GetChildren())
            {
                if(nodeLookup.ContainsKey(childId))
                {
                    yield return nodeLookup[childId];   
                }
            }
        }


        private void Awake()
        {
#if UNITY_EDITOR
#endif
        }

        private void OnValidate()
        {   
            nodeLookup.Clear();
            foreach(DialogueNode node in GetAllNodes())
            {
                nodeLookup[node.name] = node;
            }
        }

        public DialogueNode GetRootNode()
        {
            return nodes[0];
        }

        public void CreateNode(DialogueNode parentNode)
        {
            DialogueNode newNode = MakeNode(parentNode);
            Undo.RegisterCreatedObjectUndo(newNode, "CreatedDialogue Node");
            Undo.RecordObject(this, "Added Dialogue node");
            nodes.Add(newNode);

            nodeLookup[newNode.name] = newNode;
        }

        public DialogueNode MakeNode(DialogueNode parentNode)
        {
            DialogueNode newNode = CreateInstance<DialogueNode>();
            newNode.name = System.Guid.NewGuid().ToString();

            if(parentNode != null)
            {
                parentNode.AddChild(newNode.name);
                newNode.SetParentSpeaking(!parentNode.IsPlayerSpeaking());
                newNode.SetPosition(parentNode.GetRect().position + newNodeOffseet);
            }

            return newNode;
        }

        public void DeleteNode(DialogueNode nodeToDelete)
        {
            Undo.RecordObject(this, "Deleted Dialogue node");
            nodes.Remove(nodeToDelete);
            OnValidate();
            ClearUnrootedDialogues(nodeToDelete);
            Undo.DestroyObjectImmediate(nodeToDelete);
        }

        private void ClearUnrootedDialogues(DialogueNode nodeToDelete)
        {
            foreach(DialogueNode node in GetAllNodes())
            {
                node.RemoveChild(nodeToDelete.name);
            }
        }

        public void OnBeforeSerialize()
        {
            if(nodes.Count == 0)
            {
                DialogueNode newNode = MakeNode(null);
                nodes.Add(newNode);

            nodeLookup[newNode.name] = newNode;
            }

            if(AssetDatabase.GetAssetPath(this) != "")
            {
                foreach(DialogueNode node in GetAllNodes())
                {
                    if(AssetDatabase.GetAssetPath(node) == "")
                    {
                        AssetDatabase.AddObjectToAsset(node, this);
                    }
                }
            }
        }

        public void OnAfterDeserialize()
        {

        }
    }
}