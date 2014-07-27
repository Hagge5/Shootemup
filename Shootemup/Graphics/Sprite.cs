using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shootemup.Utilities;

namespace Shootemup.Graphics
{
	/// <summary>
	/// A 2D sprite class used for graphics.
	/// </summary>
	class Sprite : Drawable
	{
		public Texture2D Texture			{ get; set; }
		public Vector2 Position				{ get; set; }
		public Vector2 Origin				{ get; set; }
		public Color Color					{ get; set; }
		public float Rotation				{ get; set; }
		public float Scale					{ get; set; }
		public SpriteEffects Effects		{ get; set; }
		public float LayerDepth				{ get; set; }
		public Rectangle Source				{ get; set; }

		private static int RoundAlphaColorValue(int v)
		{
			if (v < 0)
				return 0;
			else if (v > 255)
				return 255;
			else
				return v;
		}

		public int Alpha
		{
			get { return Color.A; }
			set { Color = new Color(Color.R, Color.G, Color.B, RoundAlphaColorValue(value)); }
		}

		public bool Invisible
		{
			get { return (Color.A == 0); }
			set { if (value) Color = new Color(Color.R, Color.G, Color.B, 0); }
		}

		private const float DefaultRotation = 0F;
		private const float DefaultScale = 1.0F;
		private const float DefaultLayerDepth = 0.5F;
		private static readonly Vector2 DefaultPosition = new Vector2(0, 0);
		private static readonly Vector2 DefaultOrigin = new Vector2(0, 0);
		private static readonly Color DefaultColor = new Color(255, 255, 255, 255);
		private static readonly SpriteEffects DefaultSpriteEffects = new SpriteEffects();

		public Sprite(Texture2D texture, Vector2 position, Vector2 origin, float rotation, float scale, Color color)
			: base()
		{
			Texture = texture;
			Position = position;
			Origin = origin;
			Color = color;
			Rotation = rotation;
			Scale = scale;

			Effects = DefaultSpriteEffects;
			LayerDepth = DefaultLayerDepth;
			Source = texture.Bounds;
		}

		public Sprite(Texture2D texture, Vector2 position, Vector2 origin, float rotation, float scale)
			: base()
		{
			Texture = texture;
			Position = position;
			Origin = origin;
			Rotation = rotation;
			Scale = scale;

			Color = DefaultColor;
			Effects = DefaultSpriteEffects;
			LayerDepth = DefaultLayerDepth;
			Source = texture.Bounds;
		}

		public Sprite(Texture2D texture, Vector2 position, Vector2 origin)
			: base()
		{
			Texture = texture;
			Position = position;
			Origin = origin;

			Color = DefaultColor;
			Rotation = DefaultRotation;
			Scale = DefaultScale;
			Effects = DefaultSpriteEffects;
			LayerDepth = DefaultLayerDepth;
			Source = texture.Bounds;
		}

		public Sprite(Texture2D texture, Vector2 position)
			: base()
		{
			Texture = texture;
			Position = position;

			Origin = DefaultOrigin;
			Color = DefaultColor;
			Rotation = DefaultRotation;
			Scale = DefaultScale;
			Effects = DefaultSpriteEffects;
			LayerDepth = DefaultLayerDepth;
			Source = texture.Bounds;
		}

		public Sprite(Texture2D texture)
			: base()
		{
			Texture = texture;

			Position = DefaultPosition;
			Origin = DefaultOrigin;
			Color = DefaultColor;
			Rotation = DefaultRotation;
			Scale = DefaultScale;
			Effects = DefaultSpriteEffects;
			LayerDepth = DefaultLayerDepth;
			Source = texture.Bounds;
		}

		/// <summary>
		/// Draws the sprite in its current state.
		/// </summary>
		public override void Draw(SpriteBatch batch)
		{
			batch.Draw(Texture, Position, Source, Color, Rotation, Origin, Scale, Effects, LayerDepth);
		}

		/// <summary>
		/// Update has no implementation in Sprite, although subclasses of Sprite may very well override it to
		/// achive things like animations.
		/// </summary>
		public override void Update(float time) { }

		/// <summary>
		/// Sets the origin of the Sprite to its very center.
		/// </summary>
		public void CenterOrigin()
		{
			Origin = new Vector2((float)Texture.Width * 0.5F, (float)Texture.Height * 0.5F);
		}


	}
}
