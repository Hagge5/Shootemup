using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Shootemup.Utilities;
using Shootemup.Graphics;


namespace Shootemup.Entities
{
	class VelocityEntity : HitboxEntity
	{
		public Vector2 Velocity { set; get; }
		protected bool RotateTowardsDirection { get; set; }

		public VelocityEntity(Sprite sprite, float radius, bool rotateTowardsDirection = false)
			: base(sprite, radius)
		{
			Velocity = new Vector2(0, 0);
			RotateTowardsDirection = rotateTowardsDirection;
		}

		public VelocityEntity(Sprite sprite, float radius, Vector2 velocity, bool rotateTowardsDirection = false)
			: base(sprite, radius)
		{
			Velocity = velocity;
			RotateTowardsDirection = rotateTowardsDirection;
		}

		public VelocityEntity(Texture2D texture, float radius, Vector2 position, float scale = 1.0F, float rotation = 0F, bool rotateTowardsDirection = false)
			: base(texture, position, radius, scale, rotation)
		{
			Velocity = new Vector2(0, 0);
			RotateTowardsDirection = rotateTowardsDirection;
		}

		public VelocityEntity(Texture2D texture, float radius, Vector2 position, Vector2 velocity, float scale = 1.0F, float rotation = 0F, bool rotateTowardsDirection = false)
			: base(texture, position, radius, scale, rotation)
		{
			Velocity = velocity;
			RotateTowardsDirection = rotateTowardsDirection;
		}

		public override void Update(float time) 
		{
			Position += Velocity * (float)time;

			if (RotateTowardsDirection)
				Sprite.Rotation = Velocity.Angle();

			base.Update(time);
		}
	}
}
