using Godot;
using System;

public class UI : CanvasLayer
{
    // Nodes 
    public HBoxContainer HBoxGems;
    public DialogueBox dialogueBox;
    // vars
    public PackedScene AbilityIconScene;
    private AbilityIcon[] AbilityIcons = new AbilityIcon[4] { null, null, null, null };
    public override void _Ready()
    {
        // Get HBoxGems
        HBoxGems = (HBoxContainer)FindNode("HBoxGems");
        dialogueBox = (DialogueBox)FindNode("DialogueBox");
        // Get AbilityIconScene
        AbilityIconScene = (PackedScene)ResourceLoader.Load("res://Scenes/UIElements/AbilityIcon_Blue.tscn");
        // Connect to GameState
        GameState.GemsUpdatedNotifier += GetGems;
        GameState.LevelsChanged += InitializeGems;

        // Preloads the gem icons
        InitializeGems();
    }

    private void InitializeGems()
    {
        foreach (var child in HBoxGems.GetChildren())
        {
            HBoxGems.RemoveChild(child as Node);
        }

        for (int i = 0; i < GameState.Gems.Length; i++)
        {
            // Check if gem is not already in UI
            if (HBoxGems.GetChildCount() < GameState.Gems.Length)
            {
                // Create AbilityIcon
                // AbilityIcon abilityIcon = (AbilityIcon)AbilityIconScene.Instance();
                PackedScene abilityIconScene = default;
                // Load Texture based on color
                switch (i)
                {
                    case (int)Gem.Color.Blue:
                        abilityIconScene = GD.Load<PackedScene>("res://Scenes/UIElements/AbilityIcon_Blue.tscn");
                        break;
                    case (int)Gem.Color.Yellow:
                        abilityIconScene = GD.Load<PackedScene>("res://Scenes/UIElements/AbilityIcon_Yellow.tscn");
                        break;
                    case (int)Gem.Color.Red:
                        abilityIconScene = GD.Load<PackedScene>("res://Scenes/UIElements/AbilityIcon_Red.tscn");
                        break;
                    case (int)Gem.Color.Purple:
                        abilityIconScene = GD.Load<PackedScene>("res://Scenes/UIElements/AbilityIcon_Purple.tscn");
                        break;
                }
                var abilityIcon = abilityIconScene.Instance<AbilityIcon>();
                // Set AbilityIcon AbilityColor
                abilityIcon.AbilityColor = (Gem.Color)i;

                // Add AbilityIcon to HBoxGems
                HBoxGems.AddChild(abilityIcon);
                AbilityIcons[i] = abilityIcon;

                abilityIcon.Visible = GameState.Gems[i];
            }
        }
    }

    public void GetGems(Gem.Color color)
    {
        GD.Print("2");
        AbilityIcons[(int)color].Visible = GameState.GemCount[color] != 0;
    }
}
