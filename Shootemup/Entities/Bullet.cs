using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shootemup.Graphics;

namespace Shootemup.Entities
{
	class Bullet : AccelerationEntity
	{
		public event EventHandler<EventArgs> Destroying;
		public event EventHandler<EventArgs> Updating;

		public Bullet(Texture2D texture, float radius, Vector2 position, Vector2 velocity, Vector2 acceleration, float scale = 1.0F, float rotation = 0F, bool rotateTowardsDirection = false)
			: base(texture, radius, position, velocity, acceleration, scale, rotation, rotateTowardsDirection) { }

		public Bullet(Texture2D texture, Color color, float radius, Vector2 position, Vector2 velocity, Vector2 acceleration, float scale = 1.0F, float rotation = 0F, bool rotateTowardsDirection = false)
			: base(texture, radius, position, velocity, acceleration, scale, rotation, rotateTowardsDirection)
		{
			Sprite.Color = color;
		}

		public override void Update(float time)
		{
			if (Updating != null)
				Updating(this, new EventArgs());

			if (WantDestroy && Destroying != null)
				Destroying(this, new EventArgs());

			if (!WantDestroy)
				base.Update(time);
		}

		public override void Draw(SpriteBatch batch)
		{
			base.Draw(batch);
		}
	}
}
