using Godot;
using System;

public partial class Settings : Node2D
{
	public override void _Ready()
	{
		var handlerNode = GetTree().CurrentScene as global_handler;
		//pause timers
		handlerNode.timeSinceMonsterWasAppeased.Stop();
		handlerNode.timeSinceMonsterWasAngered.Stop();
		//pause music
		handlerNode.currentTVStatic.StreamPaused = true;
		handlerNode.currentTVChannelNoises.StreamPaused = true;
		handlerNode.currentAmbientNoises.StreamPaused = true;
		//currentTenseMusic.StreamPaused = true;
	}
}
