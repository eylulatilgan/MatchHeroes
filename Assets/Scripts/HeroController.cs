using UnityEngine;
using System.Collections;
using System;

public class HeroController : MonoBehaviour {
     
    private Hero hero;
    private bool snappedOnBoard;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    public void CreateHero(Hero hero)
    {
        this.hero = hero;
        snappedOnBoard = false;  
        InitSprite();
    }

    public void LevelUp() {
        if(Hero.Level < Constants.levelSize)
        {
            hero.Level++;
            InitSprite();
        }            
    }

    private void InitSprite()
    {
        if(hero.heroType == HeroType.Mage)
        {
            switch (hero.Level)
            {
                case (int)MageType.Apprentice:
                    spriteRenderer.sprite = CharacterSpriteHolder.Instance.apprentice;
                    break;
                case (int)MageType.Enchanter:
                    spriteRenderer.sprite = CharacterSpriteHolder.Instance.enchanter;
                    break;
                case (int)MageType.Mage:
                    spriteRenderer.sprite = CharacterSpriteHolder.Instance.mage;
                    break;                
                case (int)MageType.ArchMage:
                    spriteRenderer.sprite = CharacterSpriteHolder.Instance.archMage;
                    break;
            }
        }

        else if(hero.heroType == HeroType.Warrior)
        {
            switch (hero.Level)
            {
                case (int)WarriorType.Recruit:
                    spriteRenderer.sprite = CharacterSpriteHolder.Instance.recruit;
                    break;
                case (int)WarriorType.Warrior:
                    spriteRenderer.sprite = CharacterSpriteHolder.Instance.warrior;
                    break;
                case (int)WarriorType.Knight:
                    spriteRenderer.sprite = CharacterSpriteHolder.Instance.knight;
                    break;
                case (int)WarriorType.Paladin:
                    spriteRenderer.sprite = CharacterSpriteHolder.Instance.paladin;
                    break;
            }
        }  
    }

    public Hero Hero
    {
        get
        {
            return hero;
        }

        set
        {
            hero = value;
        }
    }

    public bool SnappedOnBoard
    {
        get
        {
            return snappedOnBoard;
        }

        set
        {
            snappedOnBoard = value;
        }
    }
}
