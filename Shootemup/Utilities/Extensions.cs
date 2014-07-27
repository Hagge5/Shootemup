using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Shootemup.Utilities
{
	public static class Extensions
	{
		public static float Angle(this Vector2 v2)
		{
			return (float) Math.Atan2(v2.Y, v2.X);
		}

		public static double NextDouble(this Random r, double min, double max)
		{
			return min + r.NextDouble() * (max - min);
		}

		public static float NextFloat(this Random r)
		{
			return (float) (r.NextDouble());
		}

		public static float NextFloat(this Random r, float min, float max)
		{
			return (float)(r.NextDouble(min, max));
		}
	}
}
