using Godot;
using System;

public partial class GameOver : Control
{
    Button B1,B2,B3;
    signal_bus Signal_Bus;
    AnimationPlayer animador;


    public override void _Ready()
    {
        B1 = GetNode<Button>("CanvasLayer/VBoxContainer/B1");
		B2 = GetNode<Button>("CanvasLayer/VBoxContainer/B2");
		B3 = GetNode<Button>("CanvasLayer/VBoxContainer/B3");
        B1.Pressed += Reiniciar;
		B2.Pressed += MainMenu;
		B3.Pressed += Quit;
        Signal_Bus = GetNode<signal_bus>("/root/SignalBus");    
        animador = GetNode<AnimationPlayer>("AnimationPlayer");
        Signal_Bus.Connect(nameof(Signal_Bus.GameOver),Callable.From(OnGameOver));
        animador.AnimationFinished += OnAnimationFinished;
    }

    public void OnGameOver(){
        Visible = true;
        animador.Play("Enter");
        GetTree().Paused = true;
    }
    
    public void Reiniciar(){
        GetTree().ReloadCurrentScene();
		Signal_Bus.EmitSignal("Reset");
		GetTree().Paused = false;
    }
    public void MainMenu(){
		GetTree().Paused = false;
		GetTree().ChangeSceneToFile("res://Escenas/MainMenu.tscn");
		Signal_Bus.EmitSignal("Reset");
	}
    public void Quit(){
		GetTree().Quit();
	}
    public void OnAnimationFinished(StringName animation_name){
        B1.GrabFocus();
    }
}
