using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shootemup.Entities.EnemyStuff
{
	class Boss : Bullet
	{
		public float CurrentLife { get; set; }
		public float MaxLife { get; private set; }

		private float _chargeRate;
		private bool _invul;
		public bool Charging { get; private set; }

		public float CurrentLifePercent
		{
			get { return CurrentLife / MaxLife; }
			set { CurrentLife = MaxLife * value; }
		}

		private const float Scale = 0.25F;
		private const float HitboxRadius = 254 * Scale;
		private const float Rotation = 0;
		private static readonly Color StartColor = new Color(255, 20, 20);
		private static readonly Color EndColor = new Color(20, 255, 20);


		public Color Color { get { return Sprite.Color; } private set { Sprite.Color = value; } }
		public bool Defeated { get { return CurrentLife <= 0; } }

		public Boss(Texture2D texture, Vector2 position, float health)
			: base(texture, HitboxRadius, position, new Vector2(0, 0), new Vector2(0, 0), Scale, Rotation, false)
		{
			CurrentLife = health;
			MaxLife = health;
			Color = StartColor;
		}

		public Boss(Texture2D texture, Vector2 position, float health, float chargeFromHealthPercent)
			: base(texture, HitboxRadius, position, new Vector2(0, 0), new Vector2(0, 0), Scale, Rotation, false)
		{
			CurrentLife = health;
			MaxLife = health;
			Color = StartColor;
			BeginCharge(chargeFromHealthPercent);
		}

		public void BeginCharge(float fromWhatLifePercent = 0.0000001F, float time = 1)
		{
			if (Charging) throw new InvalidOperationException("The boss cannot charge while charging.");

			_invul = true;
			Charging = true;
			CurrentLife = MaxLife * fromWhatLifePercent;
			_chargeRate = (MaxLife - CurrentLife) * time;
			Update(0);
		}

		public override void Update(float time)
		{
			int r, g, b;
			float p = 1 - CurrentLife / MaxLife;
			r = (int)(StartColor.R + (EndColor.R - StartColor.R) * p);
			g = (int)(StartColor.G + (EndColor.G - StartColor.G) * p);
			b = (int)(StartColor.B + (EndColor.B - StartColor.B) * p);

			if (Charging)
			{
				CurrentLife += _chargeRate * (float)time;

				if (CurrentLife >= MaxLife)
				{
					Charging = false;
					_invul = false;
					CurrentLife = MaxLife;
				}
			}

			Color = new Color(r, g, b);
			base.Update(time);
		}

		public override void Draw(SpriteBatch batch)
		{
			base.Draw(batch);
		}

		public void Damage(float amount = 1F)
		{
			if (!_invul)
				CurrentLife -= amount;
		}
	}
}
