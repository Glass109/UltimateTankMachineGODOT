using Godot;
using System;

public partial class OverlayOscuro : ColorRect
{
	signal_bus Signal_Bus;
	ShaderMaterial _material;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Signal_Bus = GetNode<signal_bus>("/root/SignalBus");
		Signal_Bus.TakeDamage += OnTakeDamage;
		Signal_Bus.AddHealth += OnAddHealth;
		Signal_Bus.Reset += OnReset;
		_material = (ShaderMaterial) this.Material;
		
		
	}
	public void OnTakeDamage(){ChangeState();}
	public void OnAddHealth(){ChangeState();}
	
	public void ChangeState(){
		GD.Print(Signal_Bus.vida);
		switch(Signal_Bus.vida){
			case 3:
				_material.SetShaderParameter("BORDER_SIZE",0.3);
				_material.SetShaderParameter("UPDATE_INTERVAL",0.8);
				break;
			case 2:
				_material.SetShaderParameter("BORDER_SIZE",0.3);
				_material.SetShaderParameter("UPDATE_INTERVAL",0.8);
				break;
			case 1:
				_material.SetShaderParameter("BORDER_SIZE",0.2);
				_material.SetShaderParameter("UPDATE_INTERVAL",0.5);
				break;
			case 0:
				_material.SetShaderParameter("BORDER_SIZE",0.1);
				_material.SetShaderParameter("UPDATE_INTERVAL",0.2);
				break;
		}
	}
	public void OnReset(){
		_material.SetShaderParameter("BORDER_SIZE",0.4);
	}
	
}
