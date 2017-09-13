using UnityEngine;
using System.Collections;

public class CharacterSpriteHolder : GenericSingleton<CharacterSpriteHolder>
{
    [Header("Warrior")]
    public Sprite recruit;
    public Sprite warrior;
    public Sprite knight;
    public Sprite paladin;

    [Header("Mage")]
    public Sprite apprentice;
    public Sprite enchanter;
    public Sprite mage;
    public Sprite archMage;

    [Header("Blockage")]
    public Sprite yellowBlock;
    public Sprite brownBlock;
}
