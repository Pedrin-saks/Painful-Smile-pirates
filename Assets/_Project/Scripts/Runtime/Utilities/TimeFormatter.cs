using UnityEngine;

public static class TimeFormatter
{
    public static string FormatTime(float seconds)
    {
        if (seconds < 60)
        {
            return $"{Mathf.FloorToInt(seconds)}s";
        }
        else
        {
            int minutes = Mathf.FloorToInt(seconds / 60);
            int remainingSeconds = Mathf.FloorToInt(seconds % 60);
            return $"{minutes}min {remainingSeconds}s";
        }
    }
}
