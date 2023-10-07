using Godot;
using System;

public partial class Niveltest : Node
{
	SMenuPausa menuPausa;
	signal_bus Signal_Bus;
	CanvasLayer _canlay;
	
	PackedScene pausaScene = (PackedScene)ResourceLoader.Load("res://Escenas/Pausa.tscn");
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		//Signal_Bus.EmitSignal("Reset");
		_canlay = GetNode<CanvasLayer>("GUI");
		Signal_Bus = GetNode<signal_bus>("/root/SignalBus");
		Signal_Bus.Connect(nameof(Signal_Bus.Despausa),Callable.From(OnDespausa));
		Signal_Bus.Connect(nameof(Signal_Bus.GameOver),Callable.From(OnGameover));
		GetTree().Paused = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	public override void _Input(InputEvent @event){		

		if(Input.IsActionPressed("Pausa") && !GetTree().Paused){
			GetTree().Paused = true;
			Signal_Bus.EmitSignal("Pausa");
		}
	}

	public void ChangePauseState(){
		GetTree().Paused = !GetTree().Paused;
	}
	public void OnGameover(){

	}
	public void OnDespausa(){
		
	}
}
