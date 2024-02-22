using Godot;
using System;

public partial class Death : Node2D
{
	[Export]
	public AudioStream deathScreech;

	public override async void _Ready()
	{
		var handlerNode = GetTree().CurrentScene as global_handler;
		handlerNode.currentAmbientNoises.Stream = deathScreech;
		handlerNode.currentAmbientNoises.Play();

		await ToSignal(GetTree().CreateTimer(.5f), SceneTreeTimer.SignalName.Timeout);
		GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/death-2.png");
		await ToSignal(GetTree().CreateTimer(.3f), SceneTreeTimer.SignalName.Timeout);
		GetNode<TextureRect>("BackgroundImage").Texture = null;

		try
		{
			handlerNode.timeSinceMonsterWasAppeased.Stop();
			handlerNode.timeSinceMonsterWasAngered.Stop();
			handlerNode.currentTVStatic.Stop();
			handlerNode.currentAmbientNoises.Stop();
			handlerNode.currentTenseMusic.Stop();
			handlerNode.currentTVChannelNoises.Stop();
		}
		catch (Exception e)
		{
			GD.Print($"caught an exception trying to stop noises, for some reason: {e}");
		}
		
		GD.Print("playing static for 3 seconds");
		GetNode<AnimationPlayer>("StaticAnimation").Play("staticdeath");
		await ToSignal(GetTree().CreateTimer(3), SceneTreeTimer.SignalName.Timeout);
		handlerNode.ProcessChangeScene("Main_Menu");
		GD.Print("ending death scene");
		QueueFree();
	}
}
