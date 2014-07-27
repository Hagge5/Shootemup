using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shootemup.Utilities;

namespace Shootemup.Graphics
{
	delegate Vector2i ChangeFrameDelegate(AnimatedSprite a);

	class AnimatedSprite : Sprite
	{
		private static readonly ChangeFrameDelegate DefaultFrameChanger = (a) =>
		{
			Vector2i current = a.Frame;
			if (current.X + 1 > a.MaxFrame.X)
			{
				if (current.Y + 1 > a.MaxFrame.Y)
					return new Vector2i(0, 0);
				return new Vector2i(0, current.Y + 1);
			}
			return new Vector2i(current.X + 1, current.Y);
		};

		private Vector2i _frameSize { get; set; }
		private Vector2i _currentFrame { get; set; }
		private ChangeFrameDelegate _frameChanger { get; set; }

		private float _timePerFrame { get; set; }
		private float _timeBuffer { get; set; }

		public Vector2i Frame
		{
			get { return _currentFrame; }
			set 
			{ 
				Source = new Rectangle(value.X * _frameSize.X, value.Y * _frameSize.Y, _frameSize.X, _frameSize.Y);
				_currentFrame = value;
			}
		}

		public Vector2i MaxFrame
		{
			get { return new Vector2i((int)(Texture.Width / _frameSize.X) - 1, (int)(Texture.Height / _frameSize.Y) - 1); }
		}

		public override void Draw(SpriteBatch batch)
		{
			base.Draw(batch);
		}

		public override void Update(float time)
		{
			_timeBuffer -= time;
			if (_timeBuffer <= 0)
			{
				Frame = _frameChanger(this);
				_timeBuffer = _timePerFrame;
			}

			base.Update(time);
		}

		//============================================
		//BUNCH OF CONSTRUCTORS BELOW
		//============================================

		public AnimatedSprite(Texture2D texture, Vector2 position, Vector2 origin, float rotation, float scale, Color color, float frameTime, Vector2i frameSize)
			: base(texture, position, origin, rotation, scale, color)
		{
			_frameSize = frameSize;
			_timePerFrame = frameTime;

			if (Texture.Width % frameSize.X != 0 || Texture.Height % frameSize.Y != 0)
				throw new ArgumentException("The frame size is not divisible by the texture size.");
			_frameChanger = DefaultFrameChanger;
			_timeBuffer = _timePerFrame;
			Frame = new Vector2i(0, 0);
		}

		public AnimatedSprite(Texture2D texture, Vector2 position, Vector2 origin, float rotation, float scale, float frameTime, Vector2i frameSize)
			: base(texture, position, origin, rotation, scale)
		{
			_frameSize = frameSize;
			_timePerFrame = frameTime;

			if (Texture.Width % frameSize.X != 0 || Texture.Height % frameSize.Y != 0)
				throw new ArgumentException("The frame size is not divisible by the texture size.");
			_frameChanger = DefaultFrameChanger;
			_timeBuffer = _timePerFrame;
			Frame = new Vector2i(0, 0);
		}

		public AnimatedSprite(Texture2D texture, Vector2 position, Vector2 origin, float frameTime, Vector2i frameSize)
			: base(texture, position, origin)
		{
			_frameSize = frameSize;
			_timePerFrame = frameTime;

			if (Texture.Width % frameSize.X != 0 || Texture.Height % frameSize.Y != 0)
				throw new ArgumentException("The frame size is not divisible by the texture size.");
			_frameChanger = DefaultFrameChanger;
			_timeBuffer = _timePerFrame;
			Frame = new Vector2i(0, 0);
		}

		public AnimatedSprite(Texture2D texture, Vector2 position, float frameTime, Vector2i frameSize)
			: base(texture, position)
		{
			_frameSize = frameSize;
			_timePerFrame = frameTime;

			if (Texture.Width % frameSize.X != 0 || Texture.Height % frameSize.Y != 0)
				throw new ArgumentException("The frame size is not divisible by the texture size.");
			_frameChanger = DefaultFrameChanger;
			_timeBuffer = _timePerFrame;
			Frame = new Vector2i(0, 0);
		}

		public AnimatedSprite(Texture2D texture, float frameTime, Vector2i frameSize)
			: base(texture)
		{
			_frameSize = frameSize;
			_timePerFrame = frameTime;

			if (Texture.Width % frameSize.X != 0 || Texture.Height % frameSize.Y != 0)
				throw new ArgumentException("The frame size is not divisible by the texture size.");
			_frameChanger = DefaultFrameChanger;
			_timeBuffer = _timePerFrame;
			Frame = new Vector2i(0, 0);
		}

	}
}
