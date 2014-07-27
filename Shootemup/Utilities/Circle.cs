using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;


namespace Shootemup.Utilities
{
	/// <summary>
	/// Represents a circle primarily used for collision detection.
	/// </summary>
	class Circle
	{
		public RectangleF SurrondingBox { get; private set; }

		private Vector2 _centerPosition { get; set; }
		public Vector2 CenterPosition
		{
			get { return _centerPosition; }
			set
			{
				_centerPosition = value;
				SurrondingBox.Center = value;
			}
		}

		private float _radius { get; set; }
		public float Radius
		{
			get { return _radius; }
			set
			{
				_radius = value;
				SurrondingBox.Width = _radius * 2;
				SurrondingBox.Height = _radius * 2;
			}
		}

		public float Circumference
		{
			get { return Radius * 2F * (float) Math.PI; }
			set { Radius = value / (float)(2 * Math.PI); }
		}

		public Circle(Vector2 centerPosition, float radius)
		{
			SurrondingBox = new RectangleF(); //The dimensions of this is calculated in the properties of this class
			Radius = radius;
			CenterPosition = centerPosition;
		}
		
		/// <summary>
		/// Checks wether a point is inside of the circle.
		/// </summary>
		/// <param name="point">The position which hypothetically is inside of the cirle.</param>
		/// <returns>True of the position is inside of the circle, false if not.</returns>
		public bool Contains(Vector2 point)
		{
			if (SurrondingBox.Contains(point))
				return (Radius >= Utils.DistanceBetweenPoints(CenterPosition, point));
			else
				return false;
		}

		/// <summary>
		/// Checks wether two BoundingCircles intersects each other.
		/// </summary>
		/// <param name="other">The other BoundingCircle, which hypothetically intersects with this one.</param>
		/// <returns>True if they intersect, false if not.</returns>
		public bool Intersects(Circle other)
		{
			if (SurrondingBox.Intersects(other.SurrondingBox))
				return (Radius + other.Radius >= Utils.DistanceBetweenPoints(CenterPosition, other.CenterPosition));
			else
				return false;
		}
	}
}
