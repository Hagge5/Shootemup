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
	class HitboxEntity : Entity
	{
		public Circle Hitbox { get; private set; }

		override public Vector2 Position
		{
			get { return Sprite.Position; }
			set { Sprite.Position = value; }
		}

		public HitboxEntity(Sprite sprite, float radius, bool rotateTowardsDirection = false)
			: base(sprite)
		{
			Hitbox = new Circle(Position, radius);
		}

		public HitboxEntity(Texture2D texture, Vector2 position, float radius, float scale = 1.0F, float rotation = 0F, bool rotateTowardsDirection = false)
			: base(texture, position, scale, rotation)
		{
			Hitbox = new Circle(Position, radius);
		}

		public bool Collision(HitboxEntity other)
		{
			return Hitbox.Intersects(other.Hitbox);
		}

		public override void Update(float time)
		{
			base.Update(time);
			Hitbox.CenterPosition = Position;
		}

	}
}
