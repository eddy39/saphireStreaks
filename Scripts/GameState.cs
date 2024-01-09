using System;
using System.Collections.Generic;
using System.Linq;

public static class GameState
{
    // Singleton for Game states like keycard and which doors are opened

    // might be a better way to do this, but if it works, it works :P
    // 0: blue
    // 1: yellow
    // 2: red
    // 3: purple
    public static bool[] Gems { get; private set; } = new bool[4] { false, false, false, false };
    public static event Action<Gem.Color> GemsUpdatedNotifier;
    public static event Action<Boolean> DoorOpened;
    public static void AddGem(Gem.Color color)
    {
        Gems[(int)color] = true;
        GemsUpdatedNotifier?.Invoke(color);
    }

    public static void RemoveGem(Gem.Color color)
    {
        Gems[(int)color] = false;
        GemsUpdatedNotifier?.Invoke(color);
    }

    public static void OpenDoor()
    {
        DoorOpened?.Invoke(true);
    }
}