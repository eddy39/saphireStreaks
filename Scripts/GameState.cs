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
    public static Dictionary<Gem.Color, int> GemCount = new Dictionary<Gem.Color, int>
    {
        { Gem.Color.Blue, 0 }, { Gem.Color.Yellow, 0}, { Gem.Color.Red, 0 }, { Gem.Color.Purple, 0 }
    };
    
    public static Queue<Gem> RedGemQueue = new Queue<Gem>();

    public static event Action<Gem.Color> GemsUpdatedNotifier;

    public static void AddGem(Gem.Color color)
    {
        Gems[(int)color] = true;
        GemCount[color]++;

        GemsUpdatedNotifier?.Invoke(color);
    }

    public static void RemoveGem(Gem.Color color)
    {
        Gems[(int)color] = false;
        GemCount[color]--;

        GemsUpdatedNotifier?.Invoke(color);
    }

    
}