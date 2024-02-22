using Godot;
using System;

public partial class S14 : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var handlerNode = GetTree().CurrentScene as global_handler;
		if (!handlerNode.isCarryingSludge)
		{
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/S14-untouched.png");
		}
		else
		{
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/S14-touched.png");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
