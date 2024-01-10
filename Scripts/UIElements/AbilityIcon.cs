using Godot;
using System;

public class AbilityIcon : TextureRect
{
    [Export]
    public Gem.Color AbilityColor;
    // Nodes
    public TextureRect GemTexture;
	private Label Label;
    public override void _Ready()
    {
		this.Label = GetNode<Label>("Label");
		GameState.GemsUpdatedNotifier += (_) => {
			this.Label.Text = GameState.GemCount[AbilityColor].ToString();
		};

        // Get Gem
        GemTexture = GetNode<TextureRect>("Gem");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
