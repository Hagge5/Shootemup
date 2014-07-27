using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shootemup.Utilities;

namespace Shootemup.Graphics
{
	class Text : Drawable
	{
		public SpriteFont Font			{ get; private set; }
		public string String			{ get; set; }
		public Vector2 Position			{ get; set; }
		public Vector2 Origin			{ get; set; }
		public Color Color				{ get; set; }
		public float Rotation			{ get; set; }
		public float Scale				{ get; set; }
		public SpriteEffects Effects	{ get; set; }
		public float LayerDepth			{ get; set; }

		public int Alpha
		{
			get { return Color.A; }
			set { Color = new Color(Color.R, Color.G, Color.B, value); }
		}

		public Text(SpriteFont font, string str)
		{
			Font = font;
			String = str;
			Position = new Vector2(0, 0);
			Origin = new Vector2(0, 0);
			Color = Color.White;
			Rotation = 0;
			Scale = 1;
			Effects = new SpriteEffects();
			LayerDepth = 0;
		}

		public Text(SpriteFont font, string str, Vector2 position)
		{
			Font = font;
			String = str;
			Position = position;
			Origin = new Vector2(0, 0);
			Color = Color.White;
			Rotation = 0;
			Scale = 1;
			Effects = new SpriteEffects();
			LayerDepth = 0;
		}

		public Text(SpriteFont font, string str, Vector2 position, Vector2 origin)
		{
			Font = font;
			String = str;
			Position = position;
			Origin = origin;
			Color = Color.White;
			Rotation = 0;
			Scale = 1;
			Effects = new SpriteEffects();
			LayerDepth = 0;
		}

		public Text(SpriteFont font, string str, Vector2 position, Vector2 origin, Color color)
		{
			Font = font;
			String = str;
			Position = position;
			Origin = origin;
			Color = color;
			Rotation = 0;
			Scale = 1;
			Effects = new SpriteEffects();
			LayerDepth = 0;
		}

		public Text(SpriteFont font, string str, Vector2 position, Vector2 origin, Color color, float rotation, float scale)
		{
			Font = font;
			String = str;
			Position = position;
			Origin = origin;
			Color = color;
			Rotation = rotation;
			Scale = scale;
			Effects = new SpriteEffects();
			LayerDepth = 0;
		}

		public override void Update(float time) { }

		public override void Draw(SpriteBatch batch)
		{
			batch.DrawString(Font, String, Position, Color, Rotation, Origin, Scale, Effects, LayerDepth);
		}
	}
}
