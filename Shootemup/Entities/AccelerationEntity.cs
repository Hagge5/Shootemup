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
	class AccelerationEntity : VelocityEntity
	{
		public Vector2 Acceleration { get; set; }

		public AccelerationEntity(Sprite sprite, float radius, bool rotateTowardsDirection = false)
			: base(sprite, radius, rotateTowardsDirection)
		{
			Acceleration = new Vector2(0, 0);
		}

		public AccelerationEntity(Sprite sprite, float radius, Vector2 velocity, bool rotateTowardsDirection = false)
			: base(sprite, radius, velocity, rotateTowardsDirection)
		{
			Acceleration = new Vector2(0, 0);
		}

		public AccelerationEntity(Sprite sprite, float radius, Vector2 velocity, Vector2 acceleration, bool rotateTowardsDirection = false)
			: base(sprite, radius, velocity, rotateTowardsDirection)
		{
			Acceleration = acceleration;
		}

		public AccelerationEntity(Texture2D texture, float radius, Vector2 position, float scale = 1.0F, float rotation = 0F, bool rotateTowardsDirection = false)
			: base(texture, radius, position, scale, rotation, rotateTowardsDirection)
		{
			Acceleration = new Vector2(0, 0);
		}

		public AccelerationEntity(Texture2D texture, float radius, Vector2 position, Vector2 velocity, Vector2 acceleration, float scale = 1.0F, float rotation = 0F, bool rotateTowardsDirection = false)
			: base(texture, radius, position, velocity, scale, rotation, rotateTowardsDirection)
		{
			Acceleration = acceleration;
		}

		public override void Update(float time)
		{
			Velocity += Acceleration * (float) time;
			base.Update(time);
		}
	}
}
