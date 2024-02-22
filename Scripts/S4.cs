using Godot;
using System;
using static global_handler;

public partial class S4 : Node2D
{
	public override void _Ready()
	{
		var handlerNode = GetTree().CurrentScene as global_handler;
		//If game has just started, turn on TV
		if (handlerNode.currentMonsterState == MonsterStates.NotPresent)
		{
			handlerNode.currentTVStatic.Stream = handlerNode.whitenoise;
			GD.Print($"Changing static to new volume: -30db");
			handlerNode.currentTVStatic.VolumeDb = -30f;
			handlerNode.currentTVStatic.Play();

			handlerNode.DisplayComment("Is that static...?");
		}
		if (handlerNode.currentTVChannelNoises.VolumeDb == -10)
		{
			GD.Print("Reducing TV volume further");
			//TV audio at max volume, set to sound more distant
			handlerNode.currentTVChannelNoises.VolumeDb -= 10;
		}
	}
}
