using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shootemup.Graphics;

namespace Shootemup.Entities
{
	class Entity : Drawable
	{
		public Sprite Sprite { get; private set; }

		virtual public Vector2 Position
		{
			get { return Sprite.Position;	}
			set { Sprite.Position = value;	}
		}

		public bool WantDestroy { get; set; }
		public float LifeTime { get; private set; }

		public Entity(Sprite sprite)
			: base()
		{
			Sprite = sprite;
			Sprite.CenterOrigin();
			WantDestroy = false;
			LifeTime = 0;
		}

		public Entity(Texture2D texture, Vector2 position, float scale = 1.0F, float rotation = 0F)
			: base()
		{
			Sprite = new Sprite(texture, position, new Vector2(0, 0), rotation, scale);
			Sprite.CenterOrigin();
			WantDestroy = false;
			LifeTime = 0;
		}

		public override void Draw(SpriteBatch batch)
		{
			Sprite.Draw(batch);
		}

		public override void Update(float time)
		{
			LifeTime += time;
		}
	}
}
