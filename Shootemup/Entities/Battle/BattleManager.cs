using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shootemup.Entities;
using Shootemup.Utilities;
using Shootemup.Entities.EnemyStuff;
using Shootemup.Entities.PlayerStuff;
using Shootemup.BulletGroups;
using Shootemup.Graphics;

namespace Shootemup.Entities.Battle
{
	class BattleManager : Drawable
	{
		public event EventHandler Ending;
		public event EventHandler Losing;
		public event EventHandler Winning;

		public Player Player { get; private set; }
		public EnemyManager EnemyManager { get; set; }

		public static readonly RectangleF CameraBounds = new RectangleF(0, 0, 800, 400);
		public static readonly RectangleF PlayerBounds = new RectangleF(0, 150, 800, 450);
		public static readonly RectangleF BulletBounds  = new RectangleF(-200, -200, 1400, 1000);

		public BattleManager(Boss boss, Player player)
			: base()
		{
			EnemyManager = new EnemyManager(boss, BulletBounds);
			Player = player;
		}

		public BattleManager(EnemyManager enemyManager, Player player)
			: base()
		{
			EnemyManager = enemyManager;
			Player = player;
		}

		public override void Update(float time)
		{

			foreach (var bg in EnemyManager.BulletGroups)
				foreach (var b in bg.Bullets)
					if (Player.Collision(b) && Losing != null)
					{
						if (Ending != null)
							Ending(this, new EventArgs());
						if (Losing != null)
							Losing(this, new EventArgs());
					}

			foreach (var b in Player.Bullets.Bullets)
				if (b.Collision(EnemyManager.Boss))
				{
					EnemyManager.Boss.Damage(1);
					b.WantDestroy = true;
				}

			EnemyManager.Update(time);
			Player.Update(time);

			if (Player.Position.Y < PlayerBounds.Top)
				Player.Position = new Vector2(Player.Position.X, PlayerBounds.Top);
			else if (Player.Position.Y > PlayerBounds.Bottom)
				Player.Position = new Vector2(Player.Position.X, PlayerBounds.Bottom);
			if (Player.Position.X < PlayerBounds.Left)
				Player.Position = new Vector2(PlayerBounds.Left, Player.Position.Y);
			else if (Player.Position.X > PlayerBounds.Right)
				Player.Position = new Vector2(PlayerBounds.Right, Player.Position.Y);



			if (EnemyManager.Boss.Defeated && Winning != null)
			{
				Ending(this, new EventArgs());
				Winning(this, new EventArgs());
			}
		}

		public override void Draw(SpriteBatch batch)
		{
			EnemyManager.Draw(batch);
			Player.Draw(batch);
		}
	}
}