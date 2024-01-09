using Godot;
using System;

public class UI : CanvasLayer
{
    // Nodes 
    public HBoxContainer HBoxGems;
    // vars
    public PackedScene AbilityIconScene;
    private AbilityIcon[] AbilityIcons = new AbilityIcon[4] { null, null, null, null };
    public override void _Ready()
    {
        // Get HBoxGems
        HBoxGems = (HBoxContainer)FindNode("HBoxGems");
        // Get AbilityIconScene
        AbilityIconScene = (PackedScene)ResourceLoader.Load("res://Scenes/UIElements/AbilityIcon.tscn");
        // Connect to GameState
        GameState.GemsUpdatedNotifier += GetGems;
        
        // Preloads the gem icons
        InitializeGems();
    }

    private void InitializeGems()
    {
        for (int i = 0; i < GameState.Gems.Length; i++)
        {
            // Check if gem is not already in UI
            if (HBoxGems.GetChildCount() < GameState.Gems.Length)
            {
                // Create AbilityIcon
                AbilityIcon abilityIcon = (AbilityIcon)AbilityIconScene.Instance();
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
        AbilityIcons[(int)color].Visible = true;
    }
}
