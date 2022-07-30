using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INode
{
    string Id {get;}
    string Name {get;set;}
    Rect RectPos {get;}
    IEnumerable<string> Children {get;}
    int ChildrenCount {get;}
    bool ChildrenContain(string nodeId);
    void AddChild(string childName);
    void RemoveChild(string childId);
    void SetPosition(Vector2 newPos);

}
