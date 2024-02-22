using Godot;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

public partial class global_handler : Node2D
{
	public enum MonsterStates
	{
		NotPresent, // used for unique states in some rooms
		BecameHappy, // unique, starts happy timer
		Appeased, // starts happy timer countdown
		BecameAngry, // unique, starts angry timer
		Irritated, // timer is counting
		Homicidal, // timer has been met
		Dead // used for unique states in some rooms
	}

	public int currentAntennaeOrientation = -1;
	public int currentChannel = -1;

	public MonsterStates currentMonsterState = MonsterStates.NotPresent;

	[Export]
	public AudioStream whitenoise;
	[Export]
	public AudioStream violence;
	[Export]
	public AudioStream gross;
	[Export]
	public AudioStream disquieting;
	[Export]
	public AudioStream loud;
	[Export]
	public AudioStream creaking;
	[Export]
	public AudioStream risingTensionMusic;
	[Export]
	public AudioStream angered;
	public AudioStreamPlayer2D currentTVStatic;
	public AudioStreamPlayer2D currentTVChannelNoises;
	public AudioStreamPlayer2D currentAmbientNoises;
	public AudioStreamPlayer2D currentTenseMusic;
	public AudioStreamPlayer2D currentMonsterNoises;

	public enum LocationOfNoises
	{
		Front,
		DistantFront,
		Left,
		DistantLeft,
		Right,
		DistantRight,
		Back,
		DistantBack
	}

	public bool haveShownTutorial = false;

	public string currentSceneBeingShown = "Main_Menu";
	public string sceneToReturnToAfterSettings = "Main_Menu";
	public bool goingToReturnFromSettings = false;

	public bool isCarryingBowl = false;
	public bool isCarryingBadFood = false;
	public bool isCarryingSludge = false;
	

	public Stopwatch timeSinceMonsterWasAppeased = new Stopwatch();
	public Stopwatch timeSinceMonsterWasAngered = new Stopwatch();

	/// <summary>
	/// Setting for visibility of interactable buttons
	/// </summary>
	public int ButtonVisibilitySetting = 0;

	public Node CommentInstance;

	public int channelMonsterWants = new Random().Next(1,5);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print($"Monster wants: {channelMonsterWants}");
		currentTVStatic = new AudioStreamPlayer2D();
		currentTVChannelNoises = new AudioStreamPlayer2D();
		currentAmbientNoises = new AudioStreamPlayer2D();
		currentTenseMusic = new AudioStreamPlayer2D();
		currentMonsterNoises = new AudioStreamPlayer2D();
		AddChild(currentTVStatic);
		AddChild(currentTVChannelNoises);
		AddChild(currentAmbientNoises);
		AddChild(currentTenseMusic);
		AddChild(currentMonsterNoises);
		ProcessChangeScene(currentSceneBeingShown);//Main_Menu
		HideAcquirables();
	}
	public void HideAcquirables()
	{
		GetNode<TextureRect>("Canvas/PickUpAbles/Bowl").Modulate = new Color(1,1,1,0);
		GetNode<TextureRect>("Canvas/PickUpAbles/gross-food").Modulate = new Color(1,1,1,0);
		GetNode<TextureRect>("Canvas/PickUpAbles/toxic-goo").Modulate = new Color(1,1,1,0);
	}
	public void ShowBowl()
	{
		GetNode<TextureRect>("Canvas/PickUpAbles/Bowl").Modulate = new Color(1,1,1,1);
		isCarryingBowl = true;
	}
	public void ShowFood()
	{
		GetNode<TextureRect>("Canvas/PickUpAbles/gross-food").Modulate = new Color(1,1,1,1);
		isCarryingBadFood = true;
	}
	public void ShowGoo()
	{
		GetNode<TextureRect>("Canvas/PickUpAbles/toxic-goo").Modulate = new Color(1,1,1,1);
		isCarryingSludge = true;
	}

	public override void _Process(double delta)
	{
		if (currentMonsterState == MonsterStates.BecameAngry)
		{
			GD.Print("Monster became angry, starting timer");
			//start angry timer (and stop happy timer)
			timeSinceMonsterWasAppeased.Stop();
			timeSinceMonsterWasAngered.Restart();
			//generate a new channel preference (as long as it's not the current one)
			int previousMonsterChannel = channelMonsterWants;
			while (channelMonsterWants == previousMonsterChannel)
			{
				channelMonsterWants = new Random().Next(1,5);
			}
			//set new monster state
			currentMonsterState = MonsterStates.Irritated;
			//start scary music
			currentTenseMusic.Stream = risingTensionMusic;
			currentTenseMusic.Play();
			currentAmbientNoises.Stream = angered;
			currentAmbientNoises.Play();
			return;
		}
		if (currentMonsterState == MonsterStates.BecameHappy)
		{
			GD.Print("Monster became happy, starting timer");
			timeSinceMonsterWasAngered.Stop();
			timeSinceMonsterWasAppeased.Restart();
			currentMonsterState = MonsterStates.Appeased;
			currentTenseMusic.Stop();
			return;
		}
		if (currentMonsterState == MonsterStates.Homicidal)
		{
			//Screech
			DisplayComment("OH *#$^@#",1);
			//don't infinite loop back into here, set to empty state
			currentMonsterState = MonsterStates.NotPresent;
			//play death scene
			GD.Print("disabling buttons");
			DisableAllButtons();
			GD.Print("queuing the freeing of the scene of house");
			//should only have 1 child, the scene_of_house
			var temp = GetChildCount();
			GD.Print($"There are {temp} children to consider removing");
			int indexOfChildWithMostChildren = 0;
			int maxChildrenSeen = 0;
			for (int i = 0; i < temp; i++)
			{
				var bluh = GetChild(i);
				GD.Print($"Looking at child node: {bluh.Name}");
				var whatever = bluh.GetChildCount();
				if (maxChildrenSeen < whatever)
				{
					GD.Print($"child set new record: {whatever}");
					maxChildrenSeen = whatever;
					indexOfChildWithMostChildren = i;
				}
				else
				{
					GD.Print($"child fail, only had {whatever} children");

				}
			}
			var nodeToRemove = GetChild(indexOfChildWithMostChildren);
			nodeToRemove.QueueFree();
			GD.Print("starting death scene");
			ProcessChangeScene("Death");
			return;
		}
		if (currentMonsterState == MonsterStates.Irritated)
		{
			//check if we've exceeded the timer
			if (timeSinceMonsterWasAngered.ElapsedMilliseconds > 15000)
			{				
				GD.Print($"{timeSinceMonsterWasAngered.ElapsedMilliseconds} elapsed, monster will kill");
				currentMonsterState = MonsterStates.Homicidal;
			}
			return;
		}
		if (currentMonsterState == MonsterStates.Appeased)
		{
			//check if we've exceeded the timer
			if (timeSinceMonsterWasAppeased.ElapsedMilliseconds > 20000)
			{				
				GD.Print($"{timeSinceMonsterWasAppeased.ElapsedMilliseconds} elapsed, monster will become angry");
				currentMonsterState = MonsterStates.BecameAngry;
			}
			return;
		}
	}

	public void ProcessChangeScene(string sceneToLoad)
	{
		HandlePageSpecificLogic(sceneToLoad);
		
		var sceneInstance = ResourceLoader.Load<PackedScene>($"res://Scenes/{currentSceneBeingShown}.tscn").Instantiate();
		GD.Print($"Was told to load {currentSceneBeingShown} scene, parent node: {sceneInstance.Name}");

		// Find all of the child button nodes, set their buttons to signal this class
		LinkSceneTransitionButtonsToHandler(sceneInstance);

		// Find all of the child button nodes, set their buttons to signal this class
		LinkInteractableButtonsToHandler(sceneInstance);

		//allow the acquirable buttons to make comments
		LinkAcquirableButtonsToHandler(sceneInstance);

		GetTree().CurrentScene.AddChild(sceneInstance);
	}


    private void HandlePageSpecificLogic(string sceneToLoad)
    {
		GD.Print($"scene to be rendered, {sceneToLoad}.");
		if (sceneToLoad == "Settings")
		{
			//save off current before we change what will be shown
			sceneToReturnToAfterSettings = currentSceneBeingShown;
			//setup for returning from settings
			goingToReturnFromSettings = true;
		}
		
		if (sceneToLoad != "Settings" && goingToReturnFromSettings)
		{
			//we were just in settings. Intead of loading the given scene, load the previous scene
			currentSceneBeingShown = sceneToReturnToAfterSettings;
			goingToReturnFromSettings = false;
		}
		else
		{
			//we're in any other scenario. set scene to load to the given scene
			currentSceneBeingShown = sceneToLoad;
		}

		//stuff that should happen for most all pages, unrelated to above shenanigans
		if (sceneToLoad != "Settings")
		{
			//we're not Settings, so make sure things aren't paused (in case we just left Settings, which pauses stuff)
			//unpause music
			currentTVStatic.StreamPaused = false;
			currentTVChannelNoises.StreamPaused = false;
			currentAmbientNoises.StreamPaused = false;
			currentTenseMusic.StreamPaused = false;
			//if the monster is able to act, unpause its timers
			if (currentMonsterState == MonsterStates.Irritated
				&& !timeSinceMonsterWasAngered.IsRunning)
			{
				timeSinceMonsterWasAngered.Start();
			}
			if (currentMonsterState == MonsterStates.Appeased
				&& !timeSinceMonsterWasAppeased.IsRunning)
			{
				timeSinceMonsterWasAppeased.Start();
			}
		}
    }

    private void LinkSceneTransitionButtonsToHandler(Node currentSceneInstance)
	{
		var clickableChildren = currentSceneInstance.FindChildren("*Clickable*");
		//GD.Print($"found {clickableChildren.Count} buttons to change");
		foreach (var child in clickableChildren)
		{
			var childScript = child as SceneTransitionButton;
			//GD.Print($"Setting {child}'s ChangeScene button to this function");
			childScript.ChangeScene += (newSceneName) => ProcessChangeScene(newSceneName);
		}
	}
	private void LinkAcquirableButtonsToHandler(Node currentSceneInstance)
	{
		var acquirableChildren = currentSceneInstance.FindChildren("*Acquirable*");
		GD.Print($"Found {acquirableChildren.Count} acquirable options");
		//GD.Print($"found {commentingChildren.Count} buttons to change");
		foreach (var child in acquirableChildren)
		{
			var childScript = child as AcquiringButton;
			//GD.Print($"Setting {child}' Commentting button to display function");
			childScript.MakeComment += (commentToShow) => DisplayComment(commentToShow);
		}
		//GetTree().CurrentScene.AddChild(currentSceneInstance);
	}

	public void DisableAllButtons()
	{
		var toplevelchildren = GetTree().CurrentScene.GetChildren();
		foreach (var child in toplevelchildren)
		{
			var clickableChildren = child.FindChildren("*Clickable*");
			GD.Print($"Found {clickableChildren.Count} clickables");
			foreach (var cchild in clickableChildren)
			{
				
				var button = cchild as TextureButton;
				button.Disabled = true;
				//var childScript = cchild as SceneTransitionButton;
				//childScript.ChangeScene -= (newSceneName) => ProcessChangeScene(newSceneName);
			}
			clickableChildren = child.FindChildren("*Commentable*");
			GD.Print($"Found {clickableChildren.Count} commentables");
			foreach (var cchild in clickableChildren)
			{
				var button = cchild as TextureButton;
				button.Disabled = true;
				//var childScript = cchild as InteractableButton;
				//GD.Print($"Setting {child}'s ChangeScene button to this function");
				//childScript.MakeComment -= (newSceneName) => DisplayComment(newSceneName);
			}
		}
	}

	public async void DisplayComment(string commentToShow, float timeToShow = 2.0f)
	{
		GD.Print("Displaying Comment");
		//create the comment shading box thing
		CommentInstance = ResourceLoader.Load<PackedScene>($"res://Scenes/CommentWindow.tscn").Instantiate();
		//get the richtextlabel in it and set its text
		var label = CommentInstance.GetNode<RichTextLabel>("RichTextLabel");
		//label.Text.Replace("Test.", commentToShow);
		label.Text = $"[center]{commentToShow}[/center]";
		//Show it
		GetTree().CurrentScene.AddChild(CommentInstance);
		//Get ready to reseive timout signal
		GD.Print("creating 2 second timer");
		await ToSignal(GetTree().CreateTimer(timeToShow), SceneTreeTimer.SignalName.Timeout);
		GD.Print("Removing Comment");
		CommentInstance.QueueFree();
	}


	private void LinkInteractableButtonsToHandler(Node currentSceneInstance)
	{
		var commentingChildren = currentSceneInstance.FindChildren("*Commentable*");
		GD.Print($"Found {commentingChildren.Count} commentable options");
		//GD.Print($"found {commentingChildren.Count} buttons to change");
		foreach (var child in commentingChildren)
		{
			var childScript = child as InteractableButton;
			//GD.Print($"Setting {child}' Commentting button to display function");
			childScript.MakeComment += (commentToShow) => DisplayComment(commentToShow);
		}
		//GetTree().CurrentScene.AddChild(currentSceneInstance);
	}
}
