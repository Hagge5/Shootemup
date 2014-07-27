using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shootemup.Utilities;
using Shootemup.Entities.PlayerStuff;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shootemup.Graphics
{
	class EndScreen : Drawable
	{
		private static EndScreen _instance = null;
		public static EndScreen Instance
		{
			private set { _instance = value; }
			get
			{
				if (_instance == null)
					throw new NullReferenceException("Accessing the instance of the singleton class requires initzialisation via the constructor.");
				return _instance;
			}
		}

		private Sprite _win;
		private Sprite _lose;
		private bool _winning;
		private bool _fadingIn;
		private float _alphapersecond;
		private float _alphaBuffer;
		private Keybindings _keybinds;
		private bool _shootPressedLastUpdate = false;

		private int _alpha
		{
			get { return _win.Alpha; } //Which alpha doesn't matter, they'll be the same
			set { _win.Alpha = value; _lose.Alpha = value; }
		}

		private float _alphaf
		{
			get { return _alphaBuffer; }
			set
			{
				if (value > 255F)
					_alphaBuffer = 255;
				else if (value < 0)
					_alphaBuffer = 0;
				else
					_alphaBuffer = value; 
				_alpha = (int)(_alphaf); 
			}
		}

		public EndScreen(Texture2D winTexture, Texture2D loseTexture, float scale, float fadeseconds, Keybindings keybindings)
		{
			Instance = this;
			_win = new Sprite(winTexture);
			_win.Scale = scale;
			_lose = new Sprite(loseTexture);
			_lose.Scale = scale;
			_alpha = 0;
			_winning = false;
			_alphapersecond = 255F / fadeseconds;
			_fadingIn = false;
			_keybinds = keybindings;
		}

		public override void Draw(SpriteBatch batch)
		{
			if (_winning && _win.Alpha > 0)
				_win.Draw(batch);
			else if (!_winning && _lose.Alpha > 0)
				_lose.Draw(batch);
		}

		public override void Update(float time)
		{

			if (_fadingIn && !_shootPressedLastUpdate && _keybinds.IsShootPressed())
				FadeCurrent(false);
			_shootPressedLastUpdate = _keybinds.IsShootPressed();

			if (!_fadingIn && _alpha == 0) return;
			if (_fadingIn && _alpha == 255) return;

			float fadeModifier;
			if (_fadingIn) fadeModifier = 1;
			else fadeModifier = -1;

			_alphaf += fadeModifier * time * _alphapersecond;
		}

		public bool Visisble
		{
			get { return (_alpha > 0); }
		}

		public void Fade(bool winning, bool fadein)
		{
			_winning = winning;
			_fadingIn = fadein;
		}

		public void FadeCurrent(bool fadein)
		{
			_fadingIn = fadein;
		}

	}
}
