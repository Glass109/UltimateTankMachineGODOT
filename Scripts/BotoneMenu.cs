using Godot;
using System;

public partial class BotoneMenu : VBoxContainer
{
	Button _start, _options, _exit;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_start = GetNode<Button>("Start");
		_options = GetNode<Button>("Options");
		_exit = GetNode<Button>("Exit");
		_exit.Pressed += Salir;
		_start.Pressed += Empezar;
		_start.Pressed += Opciones;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
	void Empezar(){
		GetTree().ChangeSceneToFile("res://Escenas/NivelTest.tscn");
	}
	void Opciones(){}
	void Salir(){
		GetTree().Quit();
	}
}
