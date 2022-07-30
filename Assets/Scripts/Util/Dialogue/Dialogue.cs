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
        
        public IEnumerable<INode> GetAllNodes()
        {
            return nodes;
        }

        public IEnumerable<INode> GetAllChildren(INode parentNode)
        {
            if(parentNode.ChildrenCount == 0)
            {
                yield break;
            }

            foreach(string childId in parentNode.Children)
            {
                if(nodeLookup.ContainsKey(childId))
                {
                    yield return nodeLookup[childId];   
                }
            }
        }

        private void Awake()
        {

        }

        private void OnValidate()
        {   
            nodeLookup.Clear();
            foreach(DialogueNode node in GetAllNodes())
            {
                nodeLookup[node.Name] = node;
            }
        }

        public INode GetRootNode()
        {
            return nodes[0];
        }

        public void CreateNode(INode parentNode)
        {
            DialogueNode newNode = MakeNode(parentNode);
            Undo.RegisterCreatedObjectUndo((Object)newNode, "CreatedDialogue Node");
            Undo.RecordObject(this, "Added Dialogue node");
            nodes.Add(newNode);

            nodeLookup[newNode.Name] = newNode;
        }

        public void DeleteNode(INode nodeToDelete)
        {
            Undo.RecordObject(this, "Deleted Dialogue node");
            nodes.Remove((DialogueNode)nodeToDelete);
            OnValidate();
            ClearUnrootedDialogues((DialogueNode)nodeToDelete);
            Undo.DestroyObjectImmediate((Object)nodeToDelete);
        }

        private DialogueNode MakeNode(INode parentNode)
        {
            DialogueNode newNode = CreateInstance<DialogueNode>();

            newNode.Name = System.Guid.NewGuid().ToString();
            newNode.SetPosition(new Vector2(240, 50));

            if(parentNode != null)
            {
                DialogueNode deabstractedParent = DeabstractNode(parentNode);
                deabstractedParent.AddChild(newNode.Name);
                newNode.SetSpeaker(deabstractedParent.SpeakerVal == DialogueNode.Speaker.Player ? DialogueNode.Speaker.AI : DialogueNode.Speaker.Player);
                newNode.SetPosition(deabstractedParent.RectPos.position + newNodeOffseet);
            }

            return (DialogueNode)newNode;
        }

        private void ClearUnrootedDialogues(DialogueNode nodeToDelete)
        {
            foreach(DialogueNode node in GetAllNodes())
            {
                node.RemoveChild(nodeToDelete.Name);
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

        public void ChangeNodeText(INode node, string text)
        {
            DialogueNode mutableNode = DeabstractNode(node);
            mutableNode.SetText(text);
        }

        public string GetNodeText(INode node)
        {
            DialogueNode mutableNode = DeabstractNode(node);
            return mutableNode.Text;
        }

        public string CheckNodeSpeaker(INode node)
        {
            DialogueNode checkedNode = DeabstractNode(node);
            return checkedNode.SpeakerVal == DialogueNode.Speaker.Player ? "player" : "ai";
        }

        private DialogueNode DeabstractNode(INode node)
        {
            return (DialogueNode)node;
        }
    }
}