using Godot;
using System;
public partial class signal_bus : Node
{
	[Signal]
	public delegate void DespausaEventHandler();
	[Signal]
	public delegate void PausaEventHandler();
	[Signal]
	public delegate void AddpointsEventHandler();
	[Signal]
	public delegate void TakeDamageEventHandler();	
	[Signal]
	public delegate void AddHealthEventHandler();
	[Signal]
	public delegate void GameOverEventHandler();
	[Signal]
	public delegate void ResetEventHandler();
	[Signal]
	public delegate void UpdateUI1EventHandler();
	[Signal]
	public delegate void UpdateUI2EventHandler();
	

	public int vida = 3;
	public int score;
	
    public override void _Ready()
    {
		this.GameOver += OnGameOver;
		this.TakeDamage += OnTakeDamage;
		this.AddHealth += OnAddHealth;
		this.Reset += OnReset;
    }

	public void OnGameOver(){

	}
	public void OnTakeDamage(){
		vida--;
		GD.Print("TOOK DAMAGE: " + (vida + 1 )+ "->" + vida);
		EmitSignal("UpdateUI1");

	}
	public void OnAddHealth(){
		vida++;
		if(vida > 3){vida = 3;}
		GD.Print("ADD HEALTH: " + (vida - 1 )+ "->" + vida);
		EmitSignal("UpdateUI2");

	}
	public void OnReset(){
		vida = 3;
	}
	
}