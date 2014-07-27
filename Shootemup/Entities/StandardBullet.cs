using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shootemup.Graphics;

namespace Shootemup.Entities
{
	class StandardBullet : RotatingBullet
	{
		public static Texture2D Texture { get; set; }
		public static float RadiusPerScale = 240;
		public const float DefaultAngularVelocity = 6.14F / 2F;
		public const float BaseScale = 0.5F * 500 / 548;

		public StandardBullet(Vector2 position, Vector2 velocity, float angularVelocity = DefaultAngularVelocity, float scale = 1.0F, float rotation = 0F)
			: base(Texture, RadiusPerScale * scale * BaseScale, position, velocity, new Vector2(0, 0), angularVelocity, scale * BaseScale, rotation)
		{
		}

		public StandardBullet(Color color, Vector2 position, Vector2 velocity, float angularVelocity = DefaultAngularVelocity, float scale = 1.0F, float rotation = 0F)
			: base(Texture, RadiusPerScale * scale * BaseScale, position, velocity, new Vector2(0, 0), angularVelocity, scale * BaseScale, rotation)
		{
			Sprite.Color = color;
		}

		public StandardBullet(Vector2 position, Vector2 velocity, Vector2 acceleration, float angularVelocity = DefaultAngularVelocity, float scale = 1.0F, float rotation = 0F)
			: base(Texture, RadiusPerScale * scale * BaseScale, position, velocity, acceleration, angularVelocity, scale * BaseScale, rotation)
		{
		}

		public StandardBullet(Color color, Vector2 position, Vector2 velocity, Vector2 acceleration, float angularVelocity = DefaultAngularVelocity, float scale = 1.0F, float rotation = 0F)
			: base(Texture, RadiusPerScale * scale * BaseScale, position, velocity, acceleration, angularVelocity, scale * BaseScale, rotation)
		{
			Sprite.Color = color;
		}
	}
}
