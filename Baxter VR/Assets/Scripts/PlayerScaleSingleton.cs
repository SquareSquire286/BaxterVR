public enum PlayerScale
{
    Human,
    Giant,
    Dog,
    Flea
}

public static class PlayerScaleSingleton
{
    public static PlayerScale playerScale;

    public static void SetPlayerScale(PlayerScale newPlayerScale)
    {
        playerScale = newPlayerScale;
    }

    public static PlayerScale GetPlayerScale()
    {
        return playerScale;
    }
}