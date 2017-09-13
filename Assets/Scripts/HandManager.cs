using System;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    private Hand hand;
    private int DefaultLayerInt = 0;

    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private GameObject objectPooler;
    private GameObject[] spawnedHeroes;

    int removedHands = 0;

    void Awake()
    {
        hand = new Hand();
        spawnedHeroes = new GameObject[spawnPoints.Length];
    }

    void OnEnable()
    {
        GameEvents.OnHandSpawn += SpawnHand;
        GameEvents.OnHeroPlaced += RemoveHand;
        GameEvents.OnResetGame += ResetHands;
    }

    void OnDisable()
    {
        GameEvents.OnHandSpawn -= SpawnHand;
        GameEvents.OnHeroPlaced -= RemoveHand;
        GameEvents.OnResetGame -= ResetHands;
    }

    private void SpawnHand()
    {
        removedHands = 0;
        hand.SpawnHand(spawnPoints.Length);

        
        for (int i = 0; i < hand.Heroes.Length; i++)
        {            
            spawnedHeroes[i] = SpawnHero(hand.Heroes[i]);
            spawnedHeroes[i].transform.position = spawnPoints[i].position;
            spawnedHeroes[i].transform.SetParent(spawnPoints[i].transform);
        }
    }

    private GameObject SpawnHero(Hero hero)
    {
        GameObject go = ObjectPool.Instance.GetPooledObject("Hero");
        go.layer = DefaultLayerInt;
        go.GetComponent<HeroController>().CreateHero(hero);
        go.SetActive(true);

        return go;
    }

    private void RemoveHand(GameObject hitChar, string cellCoor)
    {
        Hero removedHero = hitChar.GetComponent<HeroController>().Hero;

        for (int i = 0; i < hand.Heroes.Length; i++)
        {
            if(removedHero == hand.Heroes[i])
            {
                hand.Heroes[i] = null;
                spawnedHeroes[i] = null;
                removedHands++;
            }
        }

        RemoveHand();
    }

    private void RemoveHand()
    {
        if(removedHands == hand.Heroes.Length)
        {
            SpawnHand();            
        }
    }

    private void ResetHands()
    {
        for (int i = 0; i < spawnedHeroes.Length; i++)
        {
            if(spawnedHeroes[i] != null)
            {
                spawnedHeroes[i].SetActive(false);
                spawnedHeroes[i].transform.SetParent(objectPooler.transform);
            }
            spawnedHeroes[i] = null;
            hand.Heroes[i] = null;
        }
    }
}