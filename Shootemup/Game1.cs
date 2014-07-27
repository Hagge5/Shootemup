using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Shootemup.Graphics;
using Shootemup.Utilities;
using Shootemup.Entities;
using Shootemup.Entities.EnemyStuff;
using Shootemup.Entities.PlayerStuff;
using Shootemup.Entities.Battle;
using Shootemup.BulletGroups;
using IndependentResolutionRendering;

namespace Shootemup
{
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		//==================================================
		GraphicsDeviceManager _graphics;
		SpriteBatch _spriteBatch;
		Texture2D _pTexture;
		Texture2D _nonagon;
		Texture2D _triangle;
		Texture2D _heart;
		Texture2D _backgroundT;
		Texture2D _pentagon;
		Texture2D _winScreen;
		Texture2D _loseScreen;
		Sprite _background;
		Random _rng;

		BattleManager _bm;
		Player _player;

		bool _wantFadeOut = false;

		int _curLvl = 0;
		Levels _lvls;

		//==================================================
		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Resolution r = new Resolution(_graphics, Color.Black);
			r.SetVirtualResolution(800, 600);
			r.SetResolution(800, 600, false);
			Window.AllowUserResizing = true;
			IsFixedTimeStep = false;
			Window.ClientSizeChanged += (a, b) =>
			{
				var w = a as GameWindow;
				r.SetResolution(w.ClientBounds.Width, w.ClientBounds.Height, false);
			};
			Content.RootDirectory = "Content";
		}

		private void InitLevels()
		{
			_lvls = new Levels();
			Vector2 bossPos = new Vector2(400, 100);
			
			_lvls.Add( () => { return new EnemyManager(new Boss(_triangle, bossPos, 50), BattleManager.BulletBounds); });

			_lvls.Add(() =>
			{
				float dt = 0.3F;
				int howmany = 10;

				var e = new EnemyManager(new Boss(_triangle, bossPos, 500), BattleManager.BulletBounds);
				int i = 0;
				float origin = 0;

				e.Spawners.Add(new BulletSpawner(a => {
					origin = (float) (_rng.NextDouble() * 2 * Math.PI);
					return new StandardBullet(e.Boss.Position, new Vector2(0, 0), Utils.VectorFromSize(400, origin + 0 * 2 * Math.PI / howmany), StandardBullet.DefaultAngularVelocity, 0.1F); 
				}, e.ColoredBulletGroups[0], dt));

				for (i = 1; i < howmany; i++)
				{
					var ir = i;
					e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(e.Boss.Position, new Vector2(0, 0), Utils.VectorFromSize(400, origin + ir * 2 * Math.PI / howmany), StandardBullet.DefaultAngularVelocity, 0.1F); }, e.ColoredBulletGroups[0], dt));
				}
				return e;
			});

			_lvls.Add(() =>
			{
				var e = new EnemyManager(new Boss(_triangle, bossPos, 400), BattleManager.BulletBounds);
				e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(e.Boss.Position, new Vector2(0, 0), Utils.VectorFromSize(400, _rng.NextFloat(0, (float)Math.PI)), StandardBullet.DefaultAngularVelocity, 0.1F); }, e.ColoredBulletGroups[0], 0.075F));
				return e;
			});

			

			_lvls.Add(() =>
			{
				float dt = 0.14F;
				int howmany = 5;

				var e = new EnemyManager(new Boss(_triangle, bossPos, 600), BattleManager.BulletBounds);
				for (int i = 0; i < howmany; i++)
				{
					var ir = i;
					e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(e.Boss.Position, Utils.VectorFromSize(85, Math.PI / 2 * -a.LifeTime + ir * 2 * Math.PI / howmany), StandardBullet.DefaultAngularVelocity, 0.1F); }, e.ColoredBulletGroups[0], dt));
				}
				return e;
			});

			_lvls.Add(() =>
			{
				float dt = 0.14F;
				float dt2 = 0.5F;
				int howmany = 4;

				var e = new EnemyManager(new Boss(_triangle, bossPos, 600), BattleManager.BulletBounds);
				for (int i = 0; i < howmany; i++)
				{
					var ir = i;
					e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(e.Boss.Position, Utils.VectorFromSize(85, Math.PI / 2 * -a.LifeTime + ir * 2 * Math.PI / howmany), StandardBullet.DefaultAngularVelocity, 0.1F); }, e.ColoredBulletGroups[0], dt));
				}
				e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(new Vector2(-50, (float)(_rng.NextDouble() * 600)), new Vector2(40, 0), StandardBullet.DefaultAngularVelocity, 0.08F); }, e.ColoredBulletGroups[0], dt2));
				e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(new Vector2(850, (float)(_rng.NextDouble() * 600)), new Vector2(-40, 0), StandardBullet.DefaultAngularVelocity, 0.08F); }, e.ColoredBulletGroups[0], dt2));
				return e;
			});

			_lvls.Add(() =>
			{
				var e = new EnemyManager(new Boss(_triangle, bossPos, 500), BattleManager.BulletBounds);
				e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(e.Boss.Position, new Vector2(0, 0), Utils.VectorFromSize(400, (Math.PI / 4) * a.LifeTime), StandardBullet.DefaultAngularVelocity, 0.1F); }, e.ColoredBulletGroups[0], 0.15F));
				e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(e.Boss.Position, new Vector2(0, 0), Utils.VectorFromSize(400, (Math.PI / 4) * a.LifeTime + Math.PI), StandardBullet.DefaultAngularVelocity, 0.1F); }, e.ColoredBulletGroups[0], 0.15F));
				return e;
			});

			_lvls.Add(() =>
			{
				var e = new EnemyManager(new Boss(_triangle, bossPos, 500), BattleManager.BulletBounds);
				e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(e.Boss.Position, new Vector2(0, 0), Utils.VectorFromSize(400, (Math.PI / 4) * a.LifeTime), StandardBullet.DefaultAngularVelocity, 0.1F); }, e.ColoredBulletGroups[0], 0.15F));
				e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(e.Boss.Position, new Vector2(0, 0), Utils.VectorFromSize(400, (Math.PI / 4) * a.LifeTime + Math.PI), StandardBullet.DefaultAngularVelocity, 0.1F); }, e.ColoredBulletGroups[0], 0.15F));
				e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(e.Boss.Position, new Vector2(0, 0), Utils.VectorFromSize(400, (Math.PI / 4) * -a.LifeTime), StandardBullet.DefaultAngularVelocity, 0.1F); }, e.ColoredBulletGroups[0], 0.15F));
				e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(e.Boss.Position, new Vector2(0, 0), Utils.VectorFromSize(400, (Math.PI / 4) * -a.LifeTime + Math.PI), StandardBullet.DefaultAngularVelocity, 0.1F); }, e.ColoredBulletGroups[0], 0.15F));
				return e;
			});

			_lvls.Add(() =>
			{
				var e = new EnemyManager(new Boss(_triangle, bossPos, 350), BattleManager.BulletBounds);
				e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(e.Boss.Position, new Vector2(0, 0), Utils.VectorFromSize(400, (Math.PI / 4) * a.LifeTime), StandardBullet.DefaultAngularVelocity, 0.1F); }, e.ColoredBulletGroups[0], 0.15F));
				e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(e.Boss.Position, new Vector2(0, 0), Utils.VectorFromSize(400, (Math.PI / 4) * a.LifeTime + Math.PI), StandardBullet.DefaultAngularVelocity, 0.1F); }, e.ColoredBulletGroups[0], 0.15F));
				e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(e.Boss.Position, new Vector2(0, 0), Utils.VectorFromSize(400, (Math.PI / 4) * -a.LifeTime), StandardBullet.DefaultAngularVelocity, 0.1F); }, e.ColoredBulletGroups[0], 0.15F));
				e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(e.Boss.Position, new Vector2(0, 0), Utils.VectorFromSize(400, (Math.PI / 4) * -a.LifeTime + Math.PI), StandardBullet.DefaultAngularVelocity, 0.1F); }, e.ColoredBulletGroups[0], 0.15F));
				e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(e.Boss.Position, new Vector2(0, 0), Utils.VectorFromSize(400, (Math.PI / 4) * a.LifeTime + Math.PI * 1.5), StandardBullet.DefaultAngularVelocity, 0.1F); }, e.ColoredBulletGroups[0], 0.15F));
				e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(e.Boss.Position, new Vector2(0, 0), Utils.VectorFromSize(400, (Math.PI / 4) * a.LifeTime + Math.PI * 0.5), StandardBullet.DefaultAngularVelocity, 0.1F); }, e.ColoredBulletGroups[0], 0.15F));
				e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(e.Boss.Position, new Vector2(0, 0), Utils.VectorFromSize(400, (Math.PI / 4) * -a.LifeTime + Math.PI * 1.5), StandardBullet.DefaultAngularVelocity, 0.1F); }, e.ColoredBulletGroups[0], 0.15F));
				e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(e.Boss.Position, new Vector2(0, 0), Utils.VectorFromSize(400, (Math.PI / 4) * -a.LifeTime + Math.PI * 0.5), StandardBullet.DefaultAngularVelocity, 0.1F); }, e.ColoredBulletGroups[0], 0.15F));
				return e;
			});

			_lvls.Add(() =>
			{
				float dt = 0.14F;
				int howmany = 8;

				var e = new EnemyManager(new Boss(_triangle, bossPos, 700), BattleManager.BulletBounds);
				for (int i = 0; i < howmany; i++)
				{
					var ir = i;
					e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(e.Boss.Position, Utils.VectorFromSize(85, (Math.PI / 2) * a.LifeTime + ir * 2 * Math.PI / howmany), StandardBullet.DefaultAngularVelocity, 0.1F); }, e.ColoredBulletGroups[0], dt));
				}
				return e;
			});

			_lvls.Add(() =>
			{
				float dt = 0.14F;
				int howmany = 8;

				var e = new EnemyManager(new Boss(_triangle, bossPos, 400), BattleManager.BulletBounds);
				for (int i = 0; i < howmany; i++)
				{
					var ir = i;
					e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(e.Boss.Position, Utils.VectorFromSize(400, (Math.PI / 2) * a.LifeTime + ir * 2 * Math.PI / howmany), StandardBullet.DefaultAngularVelocity, 0.1F); }, e.ColoredBulletGroups[0], dt));
				}
				return e;
			});

			_lvls.Add(() =>
			{
				var e = new EnemyManager(new Boss(_triangle, bossPos, 500), BattleManager.BulletBounds);

				float dt1 = 0.21F;
				e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(e.Boss.Position, new Vector2(0, 0), Utils.VectorFromSize(400, (Math.PI / 5) * a.LifeTime), StandardBullet.DefaultAngularVelocity, 0.1F); }, e.ColoredBulletGroups[0], dt1));
				e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(e.Boss.Position, new Vector2(0, 0), Utils.VectorFromSize(400, (Math.PI / 5) * a.LifeTime + Math.PI), StandardBullet.DefaultAngularVelocity, 0.1F); }, e.ColoredBulletGroups[0], dt1));
				e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(e.Boss.Position, new Vector2(0, 0), Utils.VectorFromSize(400, (Math.PI / 5) * -a.LifeTime), StandardBullet.DefaultAngularVelocity, 0.1F); }, e.ColoredBulletGroups[0], dt1));
				e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(e.Boss.Position, new Vector2(0, 0), Utils.VectorFromSize(400, (Math.PI / 5) * -a.LifeTime + Math.PI), StandardBullet.DefaultAngularVelocity, 0.1F); }, e.ColoredBulletGroups[0], dt1));

				float dt2 = 0.14F;
				int howmany = 8;
				for (int i = 0; i < howmany; i++)
				{
					var ir = i;
					e.Spawners.Add(new BulletSpawner(a => { return new StandardBullet(e.Boss.Position, Utils.VectorFromSize(85, (Math.PI / 2) * a.LifeTime + ir * 2 * Math.PI / howmany), StandardBullet.DefaultAngularVelocity, 0.1F); }, e.ColoredBulletGroups[0], dt2));
				}

				return e;
			});
		}

		protected override void Initialize()
		{
			base.Initialize();
			StandardBullet.Texture = _nonagon;

			new Effects();
			new MouseAutoHider(this, 6);
			_rng = new Random();

			_background = new Sprite(_backgroundT, new Vector2(1, 1), new Vector2(0, 0), 0, (float) 1/3);
			_background.Color = new Color(230, 90, 190, 130);

			var binds = new KeyboardKeybindings(Keys.Left, Keys.Right, Keys.Up, Keys.Down, Keys.Z, Keys.X);
			_player = new StandardPlayer(_pentagon, _heart, binds);
			new EndScreen(_winScreen, _loseScreen, 0.33F, 0.7F, binds);

			InitLevels();
			_bm = new BattleManager(_lvls.Start(_curLvl), _player);

			_bm.Ending += ((a, b) => {
				foreach (var bg in _bm.EnemyManager.BulletGroups)
					foreach (var bu in bg.Bullets)
					{
						Vector2 bupos = bu.Position;
						Color c = bu.Sprite.Color;
						FadingSprite f = new FadingSprite(bu.Sprite.Texture, bu.Sprite.Position, bu.Sprite.Origin, bu.Sprite.Rotation, bu.Sprite.Scale, c, 500F / (255F * 1000F));
						f.LayerDepth = 0.1F;
						Effects.Instance.Add(f, 0.5F + 0.1F);
					}
			});

			_bm.Losing += ((a, b) => {
				EndScreen.Instance.Fade(false, true);
				float oldbLife = _bm.EnemyManager.Boss.CurrentLifePercent;
				_bm.EnemyManager = _lvls.Start(_curLvl);
				_bm.EnemyManager.Boss.BeginCharge(oldbLife);
			});

			_bm.Winning += ((a, b) => {
				if (_curLvl != 0)
					EndScreen.Instance.Fade(true, true);
				if (_curLvl + 1 <= _lvls.Count - 1)
					_curLvl++;
				_bm.EnemyManager = _lvls.Start(_curLvl);
				_bm.EnemyManager.Boss.BeginCharge();
			});
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_backgroundT = Content.Load<Texture2D>("background");
			_heart = Content.Load<Texture2D>("heart");
			_triangle = Content.Load<Texture2D>("triangle");
			_pentagon = Content.Load<Texture2D>("pentagon");
			_pTexture = Content.Load<Texture2D>("goat");
			_nonagon = Content.Load<Texture2D>("nonagon500WB");
			_winScreen = Content.Load<Texture2D>("winscreen");
			_loseScreen = Content.Load<Texture2D>("losescreen");
		}

		protected override void UnloadContent() { }

		protected override void Update(GameTime gameTime)
		{
			if (EndScreen.Instance.Visisble)
			{
				if (_wantFadeOut)
					EndScreen.Instance.FadeCurrent(false);
			}
			else
			{
				_bm.Update((float) gameTime.ElapsedGameTime.TotalSeconds);
				Effects.Instance.Update((float) gameTime.ElapsedGameTime.TotalSeconds);
			}

			EndScreen.Instance.Update((float) gameTime.ElapsedGameTime.TotalSeconds);
			MouseAutoHider.Instance.Update((float) gameTime.ElapsedGameTime.TotalSeconds);
			_wantFadeOut = false;

			Window.Title = "Please Notice Me Triangle-Senpai! FPS: " + Convert.ToString((int)(1 / (float) gameTime.ElapsedGameTime.TotalSeconds));

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			Resolution.Instance.BeginDraw();
			_spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, null, null, null, Resolution.Instance.TransformationMatrix);
			Effects.Instance.Draw(_spriteBatch);
			_background.Draw(_spriteBatch);
			_bm.Draw(_spriteBatch);
			EndScreen.Instance.Draw(_spriteBatch);
			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
