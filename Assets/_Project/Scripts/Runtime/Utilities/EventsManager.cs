using System;

public static class EventsManager
{
    public static Action OnFinishGame;
    public static Action OnEnemyDie;
    public static Action OnPlayerDead;

    public static void OnFinishGameTrigger()
    {
        OnFinishGame?.Invoke();
    }

    public static void OnEnemyDieTrigger()
    {
        OnEnemyDie?.Invoke();
    }

    public static void OnPlayerDeadTrigger()
    {
        OnPlayerDead?.Invoke();
    }
}
