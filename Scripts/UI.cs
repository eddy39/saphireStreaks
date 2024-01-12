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

    // 
    private Tween afterImageCDTween;
    public override void _Ready()
    {
        // Get HBoxGems
        HBoxGems = (HBoxContainer)FindNode("HBoxGems");
        dialogueBox = (DialogueBox)FindNode("DialogueBox");
        // Get AbilityIconScene
        AbilityIconScene = (PackedScene)ResourceLoader.Load("res://Scenes/UIElements/AbilityIcon.tscn");
        // Connect to GameState
        GameState.GemsUpdatedNotifier += GetGems;
        GameState.LevelsChanged += InitializeGems;

        // Preloads the gem icons
        InitializeGems();
    }
    public override void _ExitTree()
    {
        base._ExitTree();
        // Connect to GameState
        GameState.GemsUpdatedNotifier -= GetGems;
        GameState.LevelsChanged -= InitializeGems;
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
                PackedScene abilityIconScene = GD.Load<PackedScene>("res://Scenes/UIElements/AbilityIcon.tscn");;
                // Load Texture based on color
                /* switch (i)
                {
                    case (int)Gem.Color.Blue:
                        
                        break;
                    case (int)Gem.Color.Yellow:
                        abilityIconScene = GD.Load<PackedScene>("res://Scenes/UIElements/AbilityIcon.tscn");
                        break;
                    case (int)Gem.Color.Red:
                        abilityIconScene = GD.Load<PackedScene>("res://Scenes/UIElements/AbilityIcon.tscn");
                        break;
                    case (int)Gem.Color.Purple:
                        abilityIconScene = GD.Load<PackedScene>("res://Scenes/UIElements/AbilityIcon.tscn");
                        break;
                } */
                AbilityIcon abilityIcon = abilityIconScene.Instance<AbilityIcon>();
                // Set AbilityIcon AbilityColor
                abilityIcon.AbilityColor = (Gem.Color)i;

                // Add AbilityIcon to HBoxGems
                HBoxGems.AddChild(abilityIcon);
                AbilityIcons[i] = abilityIcon;

                abilityIcon.Visible = GameState.Gems[i];
            }
        }
        // Setups
        // Set Red and Purple greyed out
        DeactivateRedPurple();
    }

    public void GetGems(Gem.Color color)
    {
        AbilityIcons[(int)color].Visible = true;
    }
    //
    public void StartAfterImageCooldown(float cooldown)
    {
        // create AfterImageCooldownTween
        if (afterImageCDTween == null)
        {    
            afterImageCDTween = new Tween();
            // add AfterImageCooldownTween to UI
            AddChild(afterImageCDTween);
            // Connect to AfterImageCooldownTween
            afterImageCDTween.Connect("tween_all_completed", this, nameof(StopAfterImageCooldown));
        }
        // interpolate AfterImageCooldownTween
        afterImageCDTween.InterpolateMethod(
            AbilityIcons[(int)Gem.Color.Blue], "UpdateGemShader", 1f, 0f, cooldown,
            Tween.TransitionType.Linear, Tween.EaseType.InOut);
        // start AfterImageCooldownTween
        afterImageCDTween.Start();
        
        // Set Red and Purple not greyed out
        AbilityIcons[(int)Gem.Color.Red].GemMaterial.SetShaderParam("gray_level", 0f);
        AbilityIcons[(int)Gem.Color.Purple].GemMaterial.SetShaderParam("gray_level", 0f);
        
    }
    public void StopAfterImageCooldown()
    {
        // stop AfterImageCooldownTween
        afterImageCDTween.StopAll();
        // reset AfterImageCooldown
        AbilityIcons[(int)Gem.Color.Blue].GemMaterial.SetShaderParam("gray_level", 0f);
        // Set Red and Purple greyed out
        DeactivateRedPurple();
    }
    public void DeactivateRedPurple()
    {
        // Set Red and Purple greyed out
        AbilityIcons[(int)Gem.Color.Red].GemMaterial.SetShaderParam("gray_level", 1f);
        AbilityIcons[(int)Gem.Color.Purple].GemMaterial.SetShaderParam("gray_level", 1f);
    }
}