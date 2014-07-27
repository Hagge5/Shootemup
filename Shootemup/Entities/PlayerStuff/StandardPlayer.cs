using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shootemup.Entities.Battle;
using Shootemup.Utilities;

namespace Shootemup.Entities.PlayerStuff
{
	class StandardPlayer : Player
	{
		private const float BulletSpeed = 800;
		private const float BulletScale = 0.04F;
		private const float BulletsHowOften = 0.1F;
		private readonly Color BulletColor = new Color(230, 90, 200, 90);
		private readonly float BulletRadius = 240 * BulletScale;
		private const float Scale = 0.05F;
		private const float RadiusPerScale = 220;
		private const float VelocityFocused = 400;
		private const float VelocityUnfocused = 250;

		public StandardPlayer(Texture2D playerTexture, Texture2D bulletTexture, Keybindings keys)
			: base(playerTexture, new Color(40, 200, 40), Battle.BattleManager.PlayerBounds.Center, Scale * RadiusPerScale, keys, Battle.BattleManager.BulletBounds, VelocityFocused, 150, VelocityFocused * 0.03F, Scale)
		{
			BulletSpawners.Add(new BulletSpawner(a => { return new Bullet(bulletTexture, BulletColor, BulletRadius, Position, Utils.VectorFromSize(BulletSpeed, Math.PI * 1.5), new Vector2(0, 0), BulletScale, (float)Math.PI * 2.0F); }, Bullets, BulletsHowOften));
			BulletSpawners.Add(new BulletSpawner(a => { return new Bullet(bulletTexture, BulletColor, BulletRadius, Position, Utils.VectorFromSize(BulletSpeed, Math.PI * 1.55), new Vector2(0, 0), BulletScale, (float)Math.PI * 2.05F); }, Bullets, BulletsHowOften));
			BulletSpawners.Add(new BulletSpawner(a => { return new Bullet(bulletTexture, BulletColor, BulletRadius, Position, Utils.VectorFromSize(BulletSpeed, Math.PI * 1.45), new Vector2(0, 0), BulletScale, (float)Math.PI * 1.95F); }, Bullets, BulletsHowOften));
			BulletSpawners.Add(new BulletSpawner(a => { return new Bullet(bulletTexture, BulletColor, BulletRadius, Position, Utils.VectorFromSize(BulletSpeed, Math.PI * 1.40), new Vector2(0, 0), BulletScale, (float)Math.PI * 1.90F); }, Bullets, BulletsHowOften));
			BulletSpawners.Add(new BulletSpawner(a => { return new Bullet(bulletTexture, BulletColor, BulletRadius, Position, Utils.VectorFromSize(BulletSpeed, Math.PI * 1.60), new Vector2(0, 0), BulletScale, (float)Math.PI * 2.1F); }, Bullets, BulletsHowOften));
		}
	}
}
