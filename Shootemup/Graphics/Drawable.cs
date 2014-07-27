using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shootemup.Graphics
{
	abstract class Drawable : Updateable
	{
		public abstract override void Update(float time);

		/// <summary>
		/// Draws the drawable in its current state.
		/// </summary>
		public abstract void Draw(SpriteBatch batch);
	}
}
