using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewWorldTile", menuName = "Tiles/NewWorldTile", order = 1)]
public class WorldTile_SO : ScriptableObject
{
    [SerializeField]
    string name;
    [SerializeField]
    int id;
    [SerializeField]
    int difficulty;
    [SerializeField]
    bool defaultImpassible;
    [SerializeField]
    Sprite tileSprite;
    public TileTypes tileType;
}

public enum TileTypes
{
    Empty = 0,
    Mountain = 1,
    Forest = 2,
    Field = 3
}
