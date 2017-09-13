using UnityEngine;
using System;

public class RandomCharGenerator
{
    public static Hero GenerateChar()
    {
        Hero newHero = new Hero();
        newHero.heroType = GetRandomHeroType();
        newHero.Level = GetRandomHeroLevel();        
        return newHero;
    }

    private static HeroType GetRandomHeroType()
    {
        HeroType heroType;
        Array heroTypes = Enum.GetValues(typeof(HeroType));
        
        heroType = (HeroType)heroTypes.GetValue(UnityEngine.Random.Range(0, heroTypes.Length));

        return heroType;
    }

    private static int GetRandomHeroLevel()
    {
        float randomRange = UnityEngine.Random.Range(0f, 1f);

        if(randomRange <= RandomizingParameters.level1SpawnRate)
        {
            return 1;
        }
        else if (randomRange <= RandomizingParameters.level2SpawnRate)
        {
            return 2;
        }
        else if (randomRange <= RandomizingParameters.level3SpawnRate)
        {
            return 3;
        }
        else
        {
            return 4;
        }
    }
}
