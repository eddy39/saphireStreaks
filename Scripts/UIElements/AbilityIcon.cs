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
        // Load Texture based on color
		switch (AbilityColor)
		{
			case Gem.Color.Blue:
				GemTexture.Texture = GD.Load<Texture>("res://Assets/UI/GemBlue.png");
				break;
			case Gem.Color.Yellow:
				GemTexture.Texture = GD.Load<Texture>("res://Assets/UI/GemYellow.png");
				break;
			case Gem.Color.Red:
				GemTexture.Texture = GD.Load<Texture>("res://Assets/UI/GemRed.png");
				break;
			case Gem.Color.Purple:
				GemTexture.Texture = GD.Load<Texture>("res://Assets/UI/GemPurple.png");
				break;
		}
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
