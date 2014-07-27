using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Shootemup.Utilities;
using Shootemup.Graphics;
using Shootemup.BulletGroups;

namespace Shootemup.Entities.PlayerStuff
{
	class Player : HitboxEntity
	{
		private const float DefaultScale = 1.0F;
		private const float DefaultRotation = 0F;

		public BulletGroup Bullets						{ get; set; }
		public List<BulletSpawner> BulletSpawners		{ get; set; }
		public Keybindings Bindings						{ get; private set; }
		private float _angularVelocity					{ get; set; }
		private float _movementVelocityUnfocusedSize	{ get; set; } //The total velocity when moving. Does not indicate direction
		private float _movementVelocityFocusedSize		{ get; set; }

		public Vector2 Velocity
		{
			get { return Bindings.GetMovementSpeed(_movementVelocityCurrentSize); }
		}

		private float _movementVelocityCurrentSize
		{
			get
			{
				if (Bindings.IsFocusPressed())
					return _movementVelocityFocusedSize;
				else
					return _movementVelocityUnfocusedSize;
			}
		}

		private float GetMovementSpeedModifier()
		{
			if (Bindings.IsFocusPressed())
				return _movementVelocityFocusedSize / _movementVelocityUnfocusedSize;
			else
				return 1;
		}

		public override void Update(float time)
		{
			//Changing positions depending on user input
			Vector2 currveloc = Velocity;
			Position += currveloc * (float) time;

			if (currveloc.X > 0)
				Sprite.Rotation += _angularVelocity * (float)time * GetMovementSpeedModifier();
			else if (currveloc.X < 0)
				Sprite.Rotation -= _angularVelocity * (float)time * GetMovementSpeedModifier();
			/*else if (currveloc.Y > 0)
				Sprite.Rotation += AngleVelocity * (float)time * getMovementSpeedModifier();
			else if (currveloc.Y < 0)
				Sprite.Rotation -= AngleVelocity * (float)time * getMovementSpeedModifier();*/

			//Shooting
			if (Bindings.IsShootPressed())
				foreach (var b in BulletSpawners)
					b.Update(time);
			
			
			//Updating the rest
			Bullets.Update(time);
			base.Update(time);
		}

		public override void Draw(SpriteBatch batch)
		{
			Bullets.Draw(batch);
			base.Draw(batch);
		}

		public Player(Texture2D texture, Color color, Vector2 position, float radius, Keybindings bindings, RectangleF bulletDestroyBounds, float movementVelocityUnfocused, float movementVelocityFocused, float angularVelocity, float scale = DefaultScale, float rotation = DefaultRotation)
			: base(texture, position, radius, scale, rotation, false)
		{
			Bullets = new BulletGroup(bulletDestroyBounds);
			BulletSpawners = new List<BulletSpawner>();
			_angularVelocity = angularVelocity;
			Bindings = bindings;
			_movementVelocityUnfocusedSize = movementVelocityUnfocused;
			_movementVelocityFocusedSize = movementVelocityFocused;
			Sprite.Color = color;
		}


	}
}
