using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shootemup.BulletGroups;
using Shootemup.Graphics;

namespace Shootemup.Entities
{
	delegate Bullet SpawnBullet(BulletSpawner fromWho);

	class BulletSpawner : Updateable
	{
		public int BulletsSpawned				{ get; private set; }
		public float LifeTime					{ get; private set; }
		protected BulletGroup SpawnTarget		{ get; set; }
		protected float HowOften				{ get; set; }
		private SpawnBullet _spawningDelegate	{ get; set; }
		private float bufferTime				{ get; set; }

		protected Bullet Spawn()
		{
			Bullet b = _spawningDelegate(this);
			SpawnTarget.AddBullet(b);
			return b;
		}

		public override void Update(float time)
		{
			if (HowOften <= 0) return;

			LifeTime += time;
			bufferTime += time;

			while (bufferTime > HowOften)
			{
				bufferTime -= HowOften;
				BulletsSpawned++;
				Spawn().Update(bufferTime);
			}
		}

		public BulletSpawner()
			: base()
		{
			SpawnTarget = new BulletGroup(new Utilities.RectangleF());
			HowOften = -1;
			LifeTime = 0;
			bufferTime = 0;
			BulletsSpawned = 0;
		}

		public BulletSpawner(SpawnBullet spawningDelegate, BulletGroup spawnTarget, float howOften)
			: base()
		{
			_spawningDelegate = spawningDelegate;
			SpawnTarget = spawnTarget;
			HowOften = howOften;
			LifeTime = 0;
			bufferTime = 0;
			BulletsSpawned = 0;
		}

	}
}
