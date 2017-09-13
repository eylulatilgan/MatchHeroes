using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BoardManager : MonoBehaviour
{
    private HashSet<TileController> tileControllersToDestroy;
    private HashSet<TileController> tempTileCntrList;
    public static HashSet<GameObject> stonesOnBoard;

    [SerializeField]
    private TileController[] tileControllers;
    [SerializeField]
    private GameObject block;
    [SerializeField]
    private GameObject objectPooler;
    [SerializeField]
    private ObjectPool objectPool;

    private TileController currentController;    
    int streak = 0;
    int score = 0;
    int tileNumberOnBoard = 0;   

    void Awake()
    {
        tileControllersToDestroy = new HashSet<TileController>();
        tempTileCntrList = new HashSet<TileController>();
        stonesOnBoard = new HashSet<GameObject>();        
    }

    void OnEnable()
    {
        GameEvents.OnHeroPlaced += OnHeroPlaced;
        GameEvents.OnBoardInit += clearBoard;
        GameEvents.OnGameOver += clearBoard;
    }

    void OnDisable()
    {
        GameEvents.OnHeroPlaced -= OnHeroPlaced;
        GameEvents.OnBoardInit -= clearBoard;
        GameEvents.OnGameOver -= clearBoard;
    }
    
    public void clearBoard()
    {
        tileNumberOnBoard = 0;
        score = 0;

        for (int i = 0; i < tileControllers.Length; i++)
        {
            if (tileControllers[i].HeroObject != null)
            {
                tileControllers[i].HeroObject.SetActive(false);
                tileControllers[i].HeroObject.transform.SetParent(objectPooler.transform);
                tileControllers[i].HeroObject = null;
            }
               
            int row, col;
            initRowCol(tileControllers[i].name, out row, out col);
            tileControllers[i].Tile = new Tile(null, row, col);
            
        }
        initTiles();
    }    

    public void initTiles()
    {
        Hero randomHero;
        int row, col;
        for (int i = 0; i < 4; i++)
        {                       
            getRandomCoordinates(out row, out col);            
            instantiateBlocksRandomly(row, col, "Blockage");

            randomHero = RandomCharGenerator.GenerateChar();
            getRandomCoordinates(out row, out col);
            instantiateHeroesRandomly(row, col, "Hero", randomHero);
        }        
    }

    private void instantiateHeroesRandomly(int row, int col, string tag, Hero hero)
    {
        TileController tileController = tileControllers[row * Constants.tileSize + col];
        GameObject go = ObjectPool.Instance.GetPooledObject(tag);

        generateRandomHero(hero, tileController, go);

        instantiateStone(tileController, go);
    }

    private static void generateRandomHero(Hero hero, TileController tileController, GameObject go)
    {
        tileController.Tile.Hero = hero;
        go.GetComponent<HeroController>().CreateHero(hero);
    }

    private void instantiateBlocksRandomly(int row, int col, string tag)
    {
        TileController tileController = tileControllers[row * Constants.tileSize + col];
        GameObject go = ObjectPool.Instance.GetPooledObject(tag);

        instantiateStone(tileController, go);        
    }

    private void instantiateStone(TileController tileController, GameObject go)
    {
        Transform parent = tileController.gameObject.transform;
        go.layer = Constants.IgnoreRaycastLayer;
        tileController.HeroObject = go;
        
        go.SetActive(true);        
        go.transform.SetParent(parent);
        go.transform.localPosition = new Vector3(0, 0, 0);
        tileNumberOnBoard++;

    }

    private void getRandomCoordinates(out int row, out int col)
    {
        int randomRow;
        int randomCol;

        while (true)
        {
            randomRow = UnityEngine.Random.Range(0, 5);
            randomCol = UnityEngine.Random.Range(0, 5);

            if (tileControllerAt(randomRow, randomCol).HeroObject == null)
            {
                row = randomRow;
                col = randomCol;
                break;

            }
            
        }
        
    }

    void OnHeroPlaced(GameObject hitChar, string cellCoor)
    {
        //Initializing the indexes
        int row, col;
        initRowCol(cellCoor, out row, out col);

        HeroController heroCntr = hitChar.GetComponent<HeroController>();
        //Marking the TileControllerBoard        
        Tile tile = new Tile(heroCntr.Hero, row, col);
        currentController = tileControllers[row * Constants.tileSize + col];
        currentController.Tile = tile;
        currentController.HeroController = heroCntr;
        currentController.HeroObject = hitChar;
        //Check if game is over
        if(tileNumberOnBoard < Constants.sumOfTiles - Constants.blockageSize - 1)
        {
            tileNumberOnBoard++;
            Match();
        }
        else
        {
            GameEvents.TriggerGameOver();
        }            
    }

    private static void initRowCol(string cellCoor, out int row, out int col)
    {
        string[] coordinates = cellCoor.Split(',');
        row = int.Parse(coordinates[0]);
        col = int.Parse(coordinates[1]);
    }

    private TileController tileControllerAt(int row, int col)
    {
        if (row < 0 || row >= Constants.tileSize || col < 0 || col >= Constants.tileSize)
        {
            return null;
        }
        return tileControllers[row * Constants.tileSize + col];
    }

    private void Match()
    {        
        for (int i = 0; i < Constants.tileSize; i++)
        {
            for (int j = 0; j < Constants.tileSize; j++)
            {               
                check(i, j);
            }
        }

        if(tileControllersToDestroy.Count >= 3)
        {
            DestroyCharsOnTiles();
        }        
    }

    private void check(int i, int j)
    {
        Hero currentTileHero = tileControllerAt(i, j).Tile.Hero;
        streak = 0;

        //north
        if (i > 0)
        {
            Hero northTileHero = tileControllerAt(i - 1, j).Tile.Hero;
            if (isEqual(currentTileHero, northTileHero))
            {
                streak++;
                tempTileCntrList.Add(tileControllerAt(i - 1,j));                                    
            }
        }

        //south
        if (i < Constants.tileSize - 1)
        {
            Hero southTileHero = tileControllerAt(i + 1, j).Tile.Hero;
            if (isEqual(currentTileHero, southTileHero))
            {
               streak++;
                tempTileCntrList.Add(tileControllerAt(i + 1, j));
            }            
        }

        //west
        if (j > 0)
        {
            Hero westTileHero = tileControllerAt(i, j - 1).Tile.Hero;
            if (isEqual(currentTileHero, westTileHero))
            {
                streak++;
                tempTileCntrList.Add(tileControllerAt(i, j - 1));
            }
        }

        //east
        if (j < Constants.tileSize - 1)
        {
            Hero eastTileHero = tileControllerAt(i, j + 1).Tile.Hero;
            if (isEqual(currentTileHero, eastTileHero))
            {
                streak++;
                tempTileCntrList.Add(tileControllerAt(i, j + 1));
            }            
        }

        if (streak >= 2)
        {
            tempTileCntrList.Add(tileControllerAt(i, j));
            tileControllersToDestroy.UnionWith(tempTileCntrList);
        }
        else
        {
            tempTileCntrList.Clear();
        }
    }

    private bool isEqual(Hero currentTileHero, Hero secondTileHero)
    {
        if (currentTileHero != null && secondTileHero != null && currentTileHero.heroType == secondTileHero.heroType && currentTileHero.Level == secondTileHero.Level)
        {
            return true;
        }
        return false;
    }

    public void DestroyCharsOnTiles()
    {
        foreach (TileController tileController in tileControllersToDestroy)
        {
            int level = tileController.Tile.Level;
            if (currentController != tileController && level < 4)
            {               
                switch (level)
                {
                    case 1:
                        score += 50;
                        break;
                    case 2:
                        score += 100;
                        break;
                    case 3:
                        score += 200;
                        break;
                    case 4:
                        score += 1000;
                        break;
                    default:
                        break;
                }
                tileController.Tile.Hero = null;                
                tileController.HeroObject.transform.SetParent(objectPooler.transform);           
                tileController.HeroObject.SetActive(false);
                tileController.HeroObject = null;
                tileNumberOnBoard--;
            }                                
        }        
        currentController.HeroController.LevelUp();        
        tileControllersToDestroy.Clear();
        Match();

        GameEvents.TriggerOnScore(score);
    }  
} 
