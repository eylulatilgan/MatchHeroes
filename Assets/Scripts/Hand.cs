public class Hand
{
    private Hero[] heroes;

    public Hero[] Heroes
    {
        get
        {
            return heroes;
        }

        set
        {
            heroes = value;
        }
    }

    public void SpawnHand(int k)
    {
        heroes = new Hero[k];

        for (int i = 0; i < k; i++)
        {
            heroes[i] = RandomCharGenerator.GenerateChar();
        }
    }


}
