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
				Handle = "Action 1",
				Target = InputControlType.Action1,
				Source = new UnityKeyCodeSource(KeyCode.Space)
			},
			new InputControlMapping ()
			{
				Handle = "Action 2",
				Target = InputControlType.Action2,
				Source = new UnityKeyCodeSource(KeyCode.P)
			},
			new InputControlMapping ()
			{
				Handle = "Action 3",
				Target = InputControlType.Action3,
				Source = new UnityKeyCodeSource(KeyCode.C)
			},
			new InputControlMapping ()
			{
				Handle = "Action 4",
				Target = InputControlType.Action4,
				Source = new UnityKeyCodeSource(KeyCode.P)
			},
			new InputControlMapping ()
			{
				Handle = "Menu",
				Target = InputControlType.Menu,
				Source = new UnityKeyCodeSource(KeyCode.Escape)
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
