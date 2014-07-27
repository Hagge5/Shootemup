using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shootemup.Utilities
{
	static class Utils
	{
		public static float DistanceBetweenPoints(Vector2 p1, Vector2 p2)
		{
			return (float) Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
		}

		public static Vector2 VectorFromSize(float size, double rad)
		{
			return new Vector2((float) (size * Math.Cos(rad)),(float) (size * Math.Sin(rad)));
		}

		public static Texture2D GetColoredRect(Color color, int sizeX, int sizeY, GraphicsDevice graphicsDevice)
		{
			Texture2D r = new Texture2D(graphicsDevice, sizeX, sizeY);
			Color[] data = new Color[sizeX * sizeY];
			for (int i = 0; i < data.Length; ++i) data[i] = color;
			return r;
		}
	}
}
