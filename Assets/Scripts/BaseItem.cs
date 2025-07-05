using Knight;
using UnityEngine;

public interface BaseItem
{
    ItemManager Inventory { get; set; }
    GameObject obj { get; set; }
    string itmeName { get; set; }
    Sprite icon { get; set; }

    void Get();
    void Use();
}