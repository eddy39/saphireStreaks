using Godot;
using System;

public class UI : CanvasLayer
{
    // Nodes 
    public HBoxContainer HBoxGems;
    // vars
    public PackedScene AbilityIconScene;
    public override void _Ready()
    {
        // Get HBoxGems
        HBoxGems = (HBoxContainer) FindNode("HBoxGems");
        // Get AbilityIconScene
        AbilityIconScene = (PackedScene) ResourceLoader.Load("res://Scenes/UIElements/AbilityIcon.tscn");
        // Connect to GameState
        GameState.GemsUpdatedNotifier += GetGems;
        // Get Gems
        GetGems(Gem.Color.Blue); // Color gets ignored
    }
    // Get Gems
    public void GetGems(Gem.Color _)
    {
        // Iterate through gamestate gems
        for (int i = 0; i < GameState.Gems.Length; i++)
        {
            // Check if gem is true
            if (GameState.Gems[i])
            {
                // Check if gem is not already in UI
                if (HBoxGems.GetChildCount() < GameState.Gems.Length)
                {
                    // Create AbilityIcon
                    AbilityIcon abilityIcon = (AbilityIcon) AbilityIconScene.Instance();
                    // Set AbilityIcon AbilityColor
                    abilityIcon.AbilityColor = (Gem.Color) i;
                    // Add AbilityIcon to HBoxGems
                    HBoxGems.AddChild(abilityIcon);
                }
            }
        }
    }
}
