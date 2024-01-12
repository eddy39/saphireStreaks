using Godot;
using System;

public class AbilityIcon : TextureRect
{
    [Export]
    public Gem.Color AbilityColor;
    // Nodes
    public TextureRect GemTexture;
	public ShaderMaterial GemMaterial;
	private Label KeyLabel;
    public override void _Ready()
    {
		this.KeyLabel = (Label) FindNode("KeyLabel");
		
        // Get Gem
        GemTexture = GetNode<TextureRect>("Gem");
		GemTexture.Material =(ShaderMaterial) ResourceLoader.Load("res://Scenes/UIElements/GreyCooldownMaterial.tres").Duplicate();
		
		GemMaterial = (ShaderMaterial) GemTexture.Material;
        // Load Texture based on color
		switch (AbilityColor)
		{
			case Gem.Color.Blue:
				GemTexture.Texture = ResourceLoader.Load<Texture>("res://Assets/UI/Blue_Gem.png");
				KeyLabel.Text = "E";
				break;
			case Gem.Color.Yellow:
				GemTexture.Texture = ResourceLoader.Load<Texture>("res://Assets/UI/Yellow_Gem.png");
				KeyLabel.Text = "R";
				break;
			case Gem.Color.Red:
				GemTexture.Texture = ResourceLoader.Load<Texture>("res://Assets/UI/Red_Gem.png");
				KeyLabel.Text = "X";
				break;
			case Gem.Color.Purple:
				GemTexture.Texture = ResourceLoader.Load<Texture>("res://Assets/UI/Purple_Gem.png");
				KeyLabel.Text = "E";
				break;
		}
    }
	
	public void UpdateGemShader(float value)
	{
		GemMaterial.SetShaderParam("gray_level", value);
	}
}
