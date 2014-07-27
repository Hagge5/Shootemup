using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shootemup.Utilities;

namespace Shootemup.Graphics
{
	class FadingSprite : Sprite
	{
		private float _timePerAlpha { get; set; }
		private float _bufferTime { get; set; }
		private bool _fadeIn;
		private int _stopAt;

		private void InitTimes(float time)
		{
			_timePerAlpha = time;
			_bufferTime = time;
		}

		public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
		{
			base.Draw(batch);
		}

		public override void Update(float time)
		{
			_bufferTime -= time;

			while (_bufferTime <= 0)
			{
				if (Alpha == _stopAt) break;

				if (!_fadeIn)
					Alpha--;
				else
					Alpha++;

				_bufferTime += _timePerAlpha;
			}

			base.Update(time);
		}

		//============================================
		//BUNCH OF CONSTRUCTORS BELOW
		//============================================

		public FadingSprite(Texture2D texture, Vector2 position, Vector2 origin, float rotation, float scale, Color color, float secondPerFade, bool fadeIn = false, int stopAt = -1)
			: base(texture, position, origin, rotation, scale, color)
		{
			InitTimes(secondPerFade);
			_fadeIn = fadeIn;
			_stopAt = stopAt;

			if (_timePerAlpha <= 0)
				throw new InvalidOperationException("The secondPerFade argument is less than or equal to zero. This would cause an infinite loop in the Update(float time) method. Not allowed.");
		}

		public FadingSprite(Texture2D texture, Vector2 position, Vector2 origin, float rotation, float scale, float secondPerFade, bool fadeIn = false, int stopAt = -1)
			: base(texture, position, origin, rotation, scale)
		{
			InitTimes(secondPerFade);
			_fadeIn = fadeIn;
			_stopAt = stopAt;

			if (_timePerAlpha <= 0)
				throw new InvalidOperationException("The secondPerFade argument is less than or equal to zero. This would cause an infinite loop in the Update(float time) method. Not allowed.");
		}

		public FadingSprite(Texture2D texture, Vector2 position, Vector2 origin, float secondPerFade, bool fadeIn = false, int stopAt = -1)
			: base(texture, position, origin)
		{
			InitTimes(secondPerFade);
			_fadeIn = fadeIn;
			_stopAt = stopAt;

			if (_timePerAlpha <= 0)
				throw new InvalidOperationException("The secondPerFade argument is less than or equal to zero. This would cause an infinite loop in the Update(float time) method. Not allowed.");
		}

		public FadingSprite(Texture2D texture, Vector2 position, float secondPerFade, bool fadeIn = false, int stopAt = -1)
			: base(texture, position)
		{
			InitTimes(secondPerFade);
			_fadeIn = fadeIn;
			_stopAt = stopAt;

			if (_timePerAlpha <= 0)
				throw new InvalidOperationException("The secondPerFade argument is less than or equal to zero. This would cause an infinite loop in the Update(float time) method. Not allowed.");
		}

		public FadingSprite(Texture2D texture, float secondPerFade, bool fadeIn = false, int stopAt = -1)
			: base(texture)
		{
			InitTimes(secondPerFade);
			_fadeIn = fadeIn;
			_stopAt = stopAt;

			if (_timePerAlpha <= 0)
				throw new InvalidOperationException("The secondPerFade argument is less than or equal to zero. This would cause an infinite loop in the Update(float time) method. Not allowed.");
		}
	}
}
