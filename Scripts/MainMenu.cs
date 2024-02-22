using Godot;
using System;

public partial class MainMenu : Node2D
{
	[Export]
	public AudioStream MainMenuMusic;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var handlerNode = GetTree().CurrentScene as global_handler;
		if (!handlerNode.currentTenseMusic.Playing)
		{
			handlerNode.currentTenseMusic.Stream = MainMenuMusic;
			handlerNode.currentTenseMusic.Play();
		}
		handlerNode.currentMonsterState = global_handler.MonsterStates.NotPresent;
		handlerNode.HideAcquirables();
		handlerNode.isCarryingBadFood = false;
		handlerNode.isCarryingBowl = false;
		handlerNode.isCarryingSludge = false;
			
	}
}
