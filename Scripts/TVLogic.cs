using Godot;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

public partial class TVLogic : Node2D
{


	[Export]
	public AudioStream happyGrunt;

	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		var handlerNode = GetTree().CurrentScene as global_handler;

		GD.Print($"Changed static to new volume: -20");
		handlerNode.currentTVStatic.VolumeDb = -20f;

		
		
		if (!handlerNode.haveShownTutorial)
		{
			var channelDeets = ReturnOnlyStatic(null);
			GD.Print($"Changed static to new volume: {channelDeets.staticVolume}");
			handlerNode.currentTVStatic.VolumeDb = channelDeets.staticVolume;
			GD.Print($"Changed TV to new volume: {channelDeets.channelVolume}");
			handlerNode.currentTVChannelNoises.VolumeDb = channelDeets.channelVolume;
			GD.Print($"1 static is playing?: {handlerNode.currentTVStatic.Playing}");
			GD.Print($"1 tv is playing?: {handlerNode.currentTVChannelNoises.Playing}");
		
			//set antennae image to something
			UpdateAntennae();
			//spawn in tv knob
			UpdateKnob();
			handlerNode.DisplayComment("<Tutorial> Click the Circular Knob to change the channel.", 3.5f);
			await ToSignal(GetTree().CreateTimer(3.5f), SceneTreeTimer.SignalName.Timeout);
			handlerNode.DisplayComment("<Tutorial> Click the Antennae to improve Connection.", 3.5f);
			await ToSignal(GetTree().CreateTimer(3.5f), SceneTreeTimer.SignalName.Timeout);
			handlerNode.haveShownTutorial = true;
		}
		else
		{
			GetNode<TextureRect>("KnobImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/Knob-{handlerNode.currentChannel}.png");
			GetNode<TextureRect>("AntennaeImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/antennae-orientation-{handlerNode.currentAntennaeOrientation}.png");
		}
		GD.Print($"3 static is playing?: {handlerNode.currentTVStatic.Playing}");
		GD.Print($"3 tv is playing?: {handlerNode.currentTVChannelNoises.Playing}");
	}

	private ChannelComboDetails GetCurrentChannelInterferenceCombo()
	{
		var handlerNode = GetTree().CurrentScene as global_handler;
		//channel 0 is only static
		if (handlerNode.currentChannel == 0)
		{
			//  set static sound to -20 (loudest), static opacity to 0% (most interference), channel audio to -50 (quietest), and channel opacity to 100%
			return ReturnOnlyStatic(null);
		}
		//channel 1 is violence, 
		if (handlerNode.currentChannel == 1)
		{
			if (handlerNode.currentAntennaeOrientation == 0)
			{
				return ReturnWorstInterference(handlerNode.violence);
			}
			if (handlerNode.currentAntennaeOrientation == 1)
			{
				return ReturnLightInterference(handlerNode.violence);
			}
			if (handlerNode.currentAntennaeOrientation == 2)
			{
				return ReturnNoInterference(handlerNode.violence);
			}
		}
		//channel 2 is gross
		if (handlerNode.currentChannel == 2)
		{
			if (handlerNode.currentAntennaeOrientation == 0)
			{
				return ReturnNoInterference(handlerNode.gross);
			}
			if (handlerNode.currentAntennaeOrientation == 1)
			{
				return ReturnWorstInterference(handlerNode.gross);
			}
			if (handlerNode.currentAntennaeOrientation == 2)
			{
				return ReturnLightInterference(handlerNode.gross);
			}
		}
		//channel 3 is Disquieting
		if (handlerNode.currentChannel == 3)
		{
			if (handlerNode.currentAntennaeOrientation == 0)
			{
				return ReturnLightInterference(handlerNode.disquieting);
			}
			if (handlerNode.currentAntennaeOrientation == 1)
			{
				return ReturnNoInterference(handlerNode.disquieting);
			}
			if (handlerNode.currentAntennaeOrientation == 2)
			{
				return ReturnWorstInterference(handlerNode.disquieting);
			}
		}
		//channel 4 is loud
		if (handlerNode.currentChannel == 4)
		{
			if (handlerNode.currentAntennaeOrientation == 0)
			{
				return ReturnNoInterference(handlerNode.loud);
			}
			if (handlerNode.currentAntennaeOrientation == 1)
			{
				return ReturnWorstInterference(handlerNode.loud);
			}
			if (handlerNode.currentAntennaeOrientation == 2)
			{
				return ReturnLightInterference(handlerNode.loud);
			}
		}
		return new ChannelComboDetails(0,0,0, null);
	}

	private ChannelComboDetails ReturnOnlyStatic(AudioStream audioForChannel)
	{
		return new ChannelComboDetails(-15,1,-50,audioForChannel);
	}

	private ChannelComboDetails ReturnWorstInterference(AudioStream audioForChannel)
	{
		return new ChannelComboDetails(-25,.9f,-20,audioForChannel);
	}

	private ChannelComboDetails ReturnLightInterference(AudioStream audioForChannel)
	{
		return new ChannelComboDetails(-35,.5f,-10,audioForChannel);
	}

	private ChannelComboDetails ReturnNoInterference(AudioStream audioForChannel)
	{
		return new ChannelComboDetails(-50,.1f,0,audioForChannel);
	}

	

	private async void UpdateAntennae()
	{
		var handlerNode = GetTree().CurrentScene as global_handler;
		handlerNode.currentAntennaeOrientation++;
		handlerNode.currentAntennaeOrientation = handlerNode.currentAntennaeOrientation % 3;
		GD.Print($"Loading antennae orientation {handlerNode.currentAntennaeOrientation}");
		GetNode<TextureRect>("AntennaeImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/antennae-orientation-{handlerNode.currentAntennaeOrientation}.png");
		//change static image to use
		GetNode<AnimationPlayer>("StaticAnimation").Play($"Static{handlerNode.currentAntennaeOrientation}");
		//depending on the current channel, change opacity of StaticImage, and volume of static
		GD.Print($"1.1 static is playing?: {handlerNode.currentTVStatic.Playing}");
		GD.Print($"1.1 tv is playing?: {handlerNode.currentTVChannelNoises.Playing}");
		var audioDeets = UpdateNoises();
		GD.Print($"1.2 static is playing?: {handlerNode.currentTVStatic.Playing}");
		GD.Print($"1.2 tv is playing?: {handlerNode.currentTVChannelNoises.Playing}");
		
		if (handlerNode.currentChannel == handlerNode.channelMonsterWants && audioDeets.channelVolume == 0 && handlerNode.currentMonsterState != global_handler.MonsterStates.Dead)
		{
			handlerNode.currentMonsterState = global_handler.MonsterStates.BecameHappy;
			handlerNode.DisplayComment("Phew......",3);
			await ToSignal(GetTree().CreateTimer(3f), SceneTreeTimer.SignalName.Timeout);
			handlerNode.ProcessChangeScene("S8");
			QueueFree();
		}
	}

	private void UpdateKnob()
	{
		var handlerNode = GetTree().CurrentScene as global_handler;
		GD.Print($"1.3 static is playing?: {handlerNode.currentTVStatic.Playing}");
		GD.Print($"1.3 tv is playing?: {handlerNode.currentTVChannelNoises.Playing}");
		GD.Print($"1.3 updating knob: what is it playing? {handlerNode.currentTVChannelNoises.Stream}");
		//click noise?

		handlerNode.currentChannel++;
		handlerNode.currentChannel = handlerNode.currentChannel % 5;
		GD.Print($"Loading channel {handlerNode.currentChannel}");
		GetNode<TextureRect>("KnobImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/Knob-{handlerNode.currentChannel}.png");
		//change channel image to use
		GetNode<AnimationPlayer>("ChannelAnimation").Play($"Channel{handlerNode.currentChannel}");
		//depending on current antennae orientation, change opacity of ChannelImage, volume of channel
		var audioDeets = UpdateNoises();
		handlerNode.currentTVChannelNoises.Stream = audioDeets.audioToPlay;
		handlerNode.currentTVChannelNoises.Play();

		GD.Print($"1.4 static is playing?: {handlerNode.currentTVStatic.Playing}");
		GD.Print($"1.4 tv is playing?: {handlerNode.currentTVChannelNoises.Playing}");
		if (handlerNode.currentChannel == handlerNode.channelMonsterWants && handlerNode.currentMonsterState != global_handler.MonsterStates.Dead)
		{
			//play happy noises
			handlerNode.currentAmbientNoises.Stream = happyGrunt;
			handlerNode.currentAmbientNoises.Play();
			//if interference already what it needs to be, change it
			if (audioDeets.channelVolume == 0)
			{
				UpdateAntennae();
			}
		}
		GD.Print($"1.5 static is playing?: {handlerNode.currentTVStatic.Playing}");
		GD.Print($"1.5 tv is playing?: {handlerNode.currentTVChannelNoises.Playing}");
		GD.Print($"1.5 updating knob: what is it playing? {handlerNode.currentTVChannelNoises.Stream}");
	}

    private ChannelComboDetails UpdateNoises()
    {
		var handlerNode = GetTree().CurrentScene as global_handler;
		//get values
        ChannelComboDetails currentChannelInterferenceDetails = GetCurrentChannelInterferenceCombo();
		//set values
		GetNode<TextureRect>("StaticImage").Modulate = new Color(1,1,1,currentChannelInterferenceDetails.staticOpacity);
			GD.Print($"Changed static to new volume: {currentChannelInterferenceDetails.staticVolume}");
		handlerNode.currentTVStatic.VolumeDb = currentChannelInterferenceDetails.staticVolume;
		GD.Print($"Changing TV to new volume: {currentChannelInterferenceDetails.channelVolume}");
		handlerNode.currentTVChannelNoises.VolumeDb = currentChannelInterferenceDetails.channelVolume;
		return currentChannelInterferenceDetails;
    }

}

public class ChannelComboDetails
{
	public int staticVolume;
	public float staticOpacity;
	public int channelVolume;
	public AudioStream audioToPlay;
	public ChannelComboDetails(int svol, float sopa, int cvol, AudioStream channelAudio)
	{
		staticVolume = svol;
		staticOpacity = sopa;
		channelVolume = cvol;
		audioToPlay = channelAudio;
	}
}
