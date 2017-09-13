public class Tile {

    private Hero hero;
    private int row;
    private int col;

    public Tile(Hero hero, int row, int col)
    {
        this.hero = hero;
        this.row = row;
        this.col = col;
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

    public int Level
    {
        get
        {
            return hero.Level;
        }        
    }

    public HeroType charType
    {
        get
        {
            return hero.heroType;
        }        
    }

    public int Row
    {
        get
        {
            return row;
        }

        set
        {
            row = value;
        }
    }

    public int Col
    {
        get
        {
            return col;
        }

        set
        {
            col = value;
        }
    }
}
