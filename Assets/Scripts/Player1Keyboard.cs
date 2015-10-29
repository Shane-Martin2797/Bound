using UnityEngine;
using System.Collections;
using InControl;

public class Player1Keyboard : UnityInputDeviceProfile
{

	public Player1Keyboard ()
	{
		Name = "Keyboard";
		
		ButtonMappings = new[]
		{
			new InputControlMapping ()
			{
				Handle = "A",
				Target = InputControlType.Action1,
				Source = new UnityKeyCodeSource(KeyCode.Space)
			},
			new InputControlMapping ()
			{
				Handle = "B",
				Target = InputControlType.Action2,
				Source = new UnityKeyCodeSource(KeyCode.X)
			},
			new InputControlMapping ()
			{
				Handle = "X",
				Target = InputControlType.Action3,
				Source = new UnityKeyCodeSource(KeyCode.Z)
			},
			new InputControlMapping ()
			{
				Handle = "Y",
				Target = InputControlType.Action4,
				Source = new UnityKeyCodeSource(KeyCode.LeftShift)
			},
			new InputControlMapping ()
			{
				Handle = "Menu",
				Target = InputControlType.Menu,
				Source = new UnityKeyCodeSource(KeyCode.Escape)
			},
			new InputControlMapping ()
			{
				Handle = "LB",
				Target = InputControlType.LeftBumper,
				Source = new UnityKeyCodeSource(KeyCode.Q)
			},
			new InputControlMapping ()
			{
				Handle = "RB",
				Target = InputControlType.RightBumper,
				Source = new UnityKeyCodeSource(KeyCode.P)
			},
			new InputControlMapping ()
			{
				Handle = "LT",
				Target = InputControlType.LeftTrigger,
				Source = new UnityKeyCodeSource(KeyCode.E)
			},
			new InputControlMapping ()
			{
				Handle = "RT",
				Target = InputControlType.RightTrigger,
				Source = new UnityKeyCodeSource(KeyCode.O)
			}
		};
		
		AnalogMappings = new[]
		{
			new InputControlMapping ()
			{
				Handle = "Left Stick X",
				Target = InputControlType.LeftStickX,
				Source = new UnityKeyCodeAxisSource(KeyCode.A, KeyCode.D)
			},
			new InputControlMapping ()
			{
				Handle = "Left Stick Y",
				Target = InputControlType.LeftStickY,
				Source = new UnityKeyCodeAxisSource(KeyCode.S, KeyCode.W)
			},
			new InputControlMapping ()
			{
				Handle = "Right Stick X",
				Target = InputControlType.RightStickX,
				Source = new UnityKeyCodeAxisSource(KeyCode.LeftArrow, KeyCode.RightArrow)
			},
			new InputControlMapping ()
			{
				Handle = "Right Stick Y",
				Target = InputControlType.RightStickY,
				Source = new UnityKeyCodeAxisSource(KeyCode.DownArrow, KeyCode.UpArrow)
			}
		};
	}
}
