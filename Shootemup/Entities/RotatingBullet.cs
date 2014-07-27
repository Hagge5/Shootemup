using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shootemup.Graphics;

namespace Shootemup.Entities
{
	class RotatingBullet : Bullet
	{
		public float AngularVelocity { get; set; }

		public RotatingBullet(Texture2D texture, float radius, Vector2 position, Vector2 velocity, Vector2 acceleration, float angularVelocity, float scale = 1.0F, float rotation = 0F)
			: base(texture, radius, position, velocity, acceleration, scale, rotation, false)
		{
			AngularVelocity = angularVelocity;
		}

		public override void Update(float time)
		{
			Sprite.Rotation += AngularVelocity * (float) time;
			base.Update(time);
		}
	}
}
