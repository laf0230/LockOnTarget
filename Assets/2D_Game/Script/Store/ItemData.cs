using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Item")]
public class ItemData: ScriptableObject
{
    public Sprite ItemImage;
    public int Amount = 0;
    public int Price;
    public SpecialEffect specialEffect;
}

public class SpecialEffect
{

}
