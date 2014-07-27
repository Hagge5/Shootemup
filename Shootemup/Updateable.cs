using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Shootemup
{
	abstract class Updateable
	{
		/// <summary>
		/// Used for updating the method in a way decided by the implementation in a subclass.
		/// </summary>
		/// <param name="time">The delta-time elapsed in seconds.</param>
		public abstract void Update(float time);
	}
}
