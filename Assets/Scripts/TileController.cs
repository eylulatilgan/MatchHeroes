using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileController : MonoBehaviour
{
    private Tile tile;
    private HeroController heroController;
    private GameObject heroObj;

    public Tile Tile
    {
        get
        {
            return tile;
        }

        set
        {
            tile = value;
        }
    }

    public HeroController HeroController
    {
        get
        {
            return heroController;
        }

        set
        {
            heroController = value;
        }
    }

    public GameObject HeroObject
    {
        get
        {
            return heroObj;
        }

        set
        {
            heroObj = value;
        }
    }
}