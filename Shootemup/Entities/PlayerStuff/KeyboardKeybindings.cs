using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Shootemup.Entities.PlayerStuff
{
	class KeyboardKeybindings : Keybindings
	{
		private Keys _leftKey	{ get; set; }
		private Keys _rightKey	{ get; set; }
		private Keys _upKey		{ get; set; }
		private Keys _downKey	{ get; set; }
		private Keys _shootKey	{ get; set; }
		private Keys _focusKey	{ get; set; }

		private const float InverseSquareRootOfTwo = 0.7071067811865475F; //(1 / Math.Sqrt(2))

		public bool LeftPressed
		{
			get { return Keyboard.GetState().IsKeyDown(_leftKey); }
		}
		public bool RightPressed
		{
			get { return Keyboard.GetState().IsKeyDown(_rightKey); }
		}
		public bool UpPressed
		{
			get { return Keyboard.GetState().IsKeyDown(_upKey); }
		}
		public bool DownPressed
		{
			get { return Keyboard.GetState().IsKeyDown(_downKey); }
		}
		public bool ShootPressed
		{
			get { return Keyboard.GetState().IsKeyDown(_shootKey); }
		}
		public bool FocusPressed
		{
			get { return Keyboard.GetState().IsKeyDown(_focusKey); }
		}

		public override bool IsShootPressed()
		{
			return ShootPressed;
		}

		public override bool IsFocusPressed()
		{
			return FocusPressed;
		}

		public override Vector2 GetMovementSpeed(float speedSize)
		{
			if (UpPressed)
				if (RightPressed)
					return new Vector2(speedSize * InverseSquareRootOfTwo, -speedSize * InverseSquareRootOfTwo);
				else if (LeftPressed)
					return new Vector2(-speedSize * InverseSquareRootOfTwo, -speedSize * InverseSquareRootOfTwo);
				else
					return new Vector2(0, -speedSize);
			else if (DownPressed)
				if (RightPressed)
					return new Vector2(speedSize * InverseSquareRootOfTwo, speedSize * InverseSquareRootOfTwo);
				else if (LeftPressed)
					return new Vector2(-speedSize * InverseSquareRootOfTwo, speedSize * InverseSquareRootOfTwo);
				else
					return new Vector2(0, speedSize);
			else if (RightPressed)
				return new Vector2(speedSize, 0);
			else if (LeftPressed)
				return new Vector2(-speedSize, 0);
			else
				return new Vector2(0, 0);
		}

		public KeyboardKeybindings(Keys leftKey, Keys rightKey, Keys upKey, Keys downKey, Keys focusKey, Keys shootKey)
			: base()
		{
			_leftKey = leftKey;
			_rightKey = rightKey;
			_upKey = upKey;
			_downKey = downKey;
			_shootKey = shootKey;
			_focusKey = focusKey;
		}

	}
}
