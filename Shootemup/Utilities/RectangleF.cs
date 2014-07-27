using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Shootemup.Utilities
{
	/// <summary>
	/// Represents an axis-aligned bounding-box used primarily used for collision detection.
	/// </summary>
	class RectangleF
	{
		private float _top;
		private float _left;
		private float _width;
		private float _height;

		public float Width
		{
			get { return _width; }
			set { _width = value; }
		}
		public float Height
		{
			get { return _height; }
			set { _height = value; }
		}

		public float Top
		{
			get { return _top; }
			set { _top = value; }
		}
		public float Left
		{
			get { return _left; }
			set { _left = value; }
		}
		public float Right
		{
			get { return Left + Width; }
			set { Left = value - Width; }
		}
		public float Bottom
		{
			get { return Top + Height; }
			set { Top = value - Height; }
		}

		public Vector2 Center
		{
			get { return new Vector2(Left + Width * 0.5F, Top + Height * 0.5F); }
			set { Left = value.X - Width * 0.5F; Top = value.Y - Height * 0.5F; }
		}
		public Vector2 Size
		{
			get { return new Vector2(Width, Height); }
			set { Width = value.X; Height = value.Y; }
		}

		public float Area
		{
			get { return Width * Height; }
		}

		public RectangleF()
		{
			_left = 0;
			_top = 0;
			_width = 0;
			_height = 0;
		}

		public RectangleF(float left, float top, float width, float height)
		{
			_left = left;
			_top = top;
			_width = width;
			_height = height;
		}

		public RectangleF(Vector2 topLeft, Vector2 size)
		{
			_left = topLeft.X;
			_top = topLeft.Y;
			_width = size.X;
			_height = size.Y;
		}

		public RectangleF(Vector2 center, float sidelength)
		{
			float halfwidth = sidelength * 0.5F;

			_width = sidelength;
			_height = sidelength;
			_left = center.X - halfwidth;
			_top = center.Y - halfwidth;
		}

		/// <summary>
		/// Checks whether two RectangleF's intersects each other in any way.
		/// </summary>
		/// <param name="other">The other RectangleF.</param>
		/// <returns>True if they intersect, false if not.</returns>
		public bool Intersects(RectangleF other)
		{
			return (!(
				Right < other.Left ||
				Left > other.Right ||
				Top > other.Bottom ||
				Bottom < other.Top));
		}

		/// <summary>
		/// Checks wether a 2D point (a Vector2) is inside of this RectangleF.
		/// </summary>
		/// <param name="point">The point which is hypothetically inside.</param>
		/// <returns>True if the point is inside of this RectangleF, false if not.</returns>
		public bool Contains(Vector2 point)
		{
			return !(
				Right < point.X	||
				Left > point.X		||
				Top > point.Y		||
				Bottom < point.Y);
		}

		/// <summary>
		/// Checks wether a 2D point (consisting of an x-coordinate and a y-coordinate) is inside of this RectangleF.
		/// </summary>
		/// <param name="x">The x coordinate of the 2D point.</param>
		/// <param name="y">The y coordinate of the 2D point.</param>
		/// <returns>True if the point is inside of this RectangleF, false if not.</returns>
		public bool Contains(float x, float y)
		{
			return Contains(new Vector2(x, y));
		}

	}
}
