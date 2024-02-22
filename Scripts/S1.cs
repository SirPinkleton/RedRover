using Godot;
using System;
using static global_handler;

public partial class S1 : Node2D
{
	public override void _Ready()
	{
		var handlerNode = GetTree().CurrentScene as global_handler;
		//If game has just started, stop the main menu music and start creaking sounds
		if (handlerNode.currentMonsterState == MonsterStates.NotPresent)
		{
			//end main menu music
			handlerNode.currentTenseMusic.Stop();
			handlerNode.currentAmbientNoises.Stream = handlerNode.creaking;
			handlerNode.currentAmbientNoises.Play();
		}
	}
}
