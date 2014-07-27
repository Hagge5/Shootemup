using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shootemup.Utilities;
using Shootemup.Entities;
using Shootemup.Graphics;

namespace Shootemup.BulletGroups
{
	class BulletGroup : Drawable
	{
		private RectangleF _destroyBounds { get; set; }

		public LinkedList<Bullet> Bullets { get; set; }
		public int Count { get { return Bullets.Count; } }

		public BulletGroup(RectangleF destroyBounds)
		{
			_destroyBounds = destroyBounds;
			Bullets = new LinkedList<Bullet>();
		}

		protected bool IsInsideBounds(Entity e)
		{
			return _destroyBounds.Contains(e.Position);
		}

		protected void CheckForEscaped()
		{
			foreach (var b in Bullets)
				if (!IsInsideBounds(b))
					b.WantDestroy = true;
		}

		protected void RemoveSuicidalEntities()
		{
			var node = Bullets.First;
			while (node != null)
			{
				var next = node.Next;
				if(node.Value.WantDestroy)
					Bullets.Remove(node);
				node = next;
			}
		}

		public void AddBullet(Bullet b)
		{
			Bullets.AddLast(b);
		}

		public override void Update(float time)
		{
			foreach (var e in Bullets)
				e.Update(time);

			CheckForEscaped();
			RemoveSuicidalEntities();
		}

		public override void Draw(SpriteBatch batch)
		{
			foreach (var e in Bullets)
				e.Draw(batch);
		}
	}
}