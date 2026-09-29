public enum GameMode
{
    ClassicRun,
    PowerRun
}

public static class GameModeSettings
{
    public static GameMode CurrentMode { get; set; } = GameMode.ClassicRun;

    public static bool HasLives => CurrentMode == GameMode.PowerRun;
    public static bool HasPowerUps => CurrentMode == GameMode.PowerRun;
}
