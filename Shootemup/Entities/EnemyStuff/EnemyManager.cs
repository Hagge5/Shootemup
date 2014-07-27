using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shootemup.Utilities;
using Shootemup.Entities;
using Shootemup.BulletGroups;
using Shootemup.Graphics;

namespace Shootemup.Entities.EnemyStuff
{
	class EnemyManager : Drawable
	{
		public Boss Boss { get; set; }
		public List<BulletSpawner> Spawners { get; set; }
		public List<BulletGroup> ColoredBulletGroups { get; private set; }
		public List<BulletGroup> NormalBulletGroups { get; private set; }

		public Vector2 BossPosition { get { return Boss.Position; } set { Boss.Position = value; } }
		public List<BulletGroup> BulletGroups
		{
			get
			{
				var r = new List<BulletGroup>(ColoredBulletGroups.Count + NormalBulletGroups.Count);
				r.AddRange(ColoredBulletGroups);
				r.AddRange(NormalBulletGroups);
				return r;
			}
		}

		public EnemyManager(Boss boss, RectangleF firstGroupBounds)
			: base()
		{
			Boss = boss;
			Spawners = new List<BulletSpawner>();
			ColoredBulletGroups = new List<BulletGroup>();
			NormalBulletGroups = new List<BulletGroup>();
			ColoredBulletGroups.Add(new BulletGroup(firstGroupBounds));
			NormalBulletGroups.Add(new BulletGroup(firstGroupBounds));
		}

		public EnemyManager(Boss boss)
			: base()
		{
			Boss = boss;
			Spawners = new List<BulletSpawner>();
			ColoredBulletGroups = new List<BulletGroup>();
			NormalBulletGroups = new List<BulletGroup>();
		}

		public override void Draw(SpriteBatch batch)
		{
			Boss.Draw(batch);
			foreach (var g in BulletGroups)
				g.Draw(batch);
		}

		public override void Update(float time)
		{
			Boss.Update(time);

			if (!Boss.Charging)
			{
				foreach (var g in BulletGroups)
					g.Update(time);

				foreach (BulletSpawner s in Spawners)
					s.Update(time);

				foreach (var g in ColoredBulletGroups)
					foreach (Bullet b in g.Bullets)
						b.Sprite.Color = Boss.Color;
			}
		}

	}
}
