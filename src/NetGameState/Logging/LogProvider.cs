using BepInEx.Logging;

namespace NetGameState.Logging;

public static class LogProvider
{
    public static ManualLogSource? Log { get; set; } = null;
}