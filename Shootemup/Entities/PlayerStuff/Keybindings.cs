using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Shootemup.Entities.PlayerStuff
{
	abstract class Keybindings
	{
		public abstract bool IsFocusPressed();
		public abstract bool IsShootPressed();
		public abstract Vector2 GetMovementSpeed(float speedSize);


		public Keybindings() {}
	}
}
