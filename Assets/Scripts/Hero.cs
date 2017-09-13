using System;

public class Hero {

    private HeroType type;
    private int level;    

    public HeroType heroType
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }

    public int Level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
        }
    }
}
