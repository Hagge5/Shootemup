using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shootemup.Graphics
{
	class Effects : Drawable
	{
		class Effect
		{
			public Drawable Value { get; set; }
			public float SecondsRemaining { get; set; }

			public Effect(Drawable obj, float time)
			{
				Value = obj;
				SecondsRemaining = time;
			}
		}

		private static Effects _instance = null;
		public static Effects Instance
		{
			private set { _instance = value; }
			get
			{
				if (_instance == null)
					throw new NullReferenceException("Accessing the instance of the singleton class requires initzialisation via the constructor.");
				return _instance;
			}
		}

		private LinkedList<Effect> _list;

		public Effects()
		{
			_list = new LinkedList<Effect>();
			Effects.Instance = this;
		}

		public void Add(Drawable sprite, float time)
		{
			_list.AddFirst(new Effect(sprite, time));
		}

		private bool FindUnwanted()
		{
			foreach (var e in _list)
			{
				if (e.SecondsRemaining <= 0)
				{
					_list.Remove(e);
					return true;
				}
			}

			return false;
		}

		public override void Update(float time)
		{
			foreach (var e in _list)
			{
				e.SecondsRemaining -= time;
				e.Value.Update(time);
			}

			while (FindUnwanted()) ;
		}

		public override void Draw(SpriteBatch batch)
		{
			foreach (var e in _list)
				e.Value.Draw(batch);
		}
	}
}
