using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Shootemup.Utilities
{
	class MouseAutoHider : Updateable
	{
		private static MouseAutoHider _instance = null;
		public static MouseAutoHider Instance
		{
			private set { _instance = value; }
			get
			{
				if (_instance == null)
					throw new NullReferenceException("Accessing the instance of the singleton class requires initzialisation via the constructor.");
				return _instance;
			}
		}

		private float _hideTime { get; set; }
		private float _timeBuffer { get; set; }
		private MouseState _lastMouseState { get; set; }
		private Game _whichGame;
		public bool Enabled { get; set; }

		public MouseAutoHider(Game whichGame, float maximumIdleTime)
		{
			_timeBuffer = maximumIdleTime;
			_hideTime = maximumIdleTime;
			_whichGame = whichGame;

			_lastMouseState = Mouse.GetState();
			_whichGame.IsMouseVisible = true;
			Enabled = true;
			Instance = this;
		}

		public override void Update(float time)
		{
			if (!Enabled) return;

			if (_timeBuffer > 0)
				_timeBuffer -= time;
			else
				_whichGame.IsMouseVisible = false;

			MouseState currentMouseState = Mouse.GetState();
			if (currentMouseState != _lastMouseState)
			{
				_whichGame.IsMouseVisible = true;
				_timeBuffer = _hideTime;
			}

			_lastMouseState = currentMouseState;
		}

	}
}
