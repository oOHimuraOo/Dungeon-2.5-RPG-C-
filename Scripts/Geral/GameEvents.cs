using System;

public class GameEvents
{
    public static event Action OnStartGame;
    public static event Action OnEndGame;
    public static event Action<int> OnNewEnemyCount;
    public static event Action OnEnemyDied;
    public static event Action OnPlayerVictory;
    public static event Action<RewardResouce> OnReward;
    public static event Action OnEnemyHitted;

    public static void RaiseStartGame() => OnStartGame?.Invoke();
    public static void RaiseEndGame() => OnEndGame?.Invoke();
    public static void RaiseNewEnemyCount(int count) => OnNewEnemyCount?.Invoke(count);
    public static void RaiseEnemyDied() => OnEnemyDied?.Invoke();
    public static void RaisePlayerVictory() => OnPlayerVictory?.Invoke();
    public static void RaiseReward(RewardResouce reward) => OnReward?.Invoke(reward);
    public static void RaiseEnemyHitted() => OnEnemyHitted?.Invoke();
}