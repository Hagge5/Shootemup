using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Shootemup.Entities.Battle;
using Shootemup.Entities.EnemyStuff;
using Shootemup.Entities;

namespace Shootemup
{
	delegate EnemyManager GetNewEnemiesDelegate();

	class Levels
	{
		private List<GetNewEnemiesDelegate> _delegates { get; set; }

		public Levels()
		{
			_delegates = new List<GetNewEnemiesDelegate>();
		}

		public int Count
		{
			get { return _delegates.Count; }
		}

		public void Add(GetNewEnemiesDelegate enemypattern)
		{
			_delegates.Add(enemypattern);
		}

		public EnemyManager Start(int whichLevel)
		{
			if (whichLevel > Count - 1)
				throw new IndexOutOfRangeException("The value of whichLevel is too large: there is no corresponding level.");
			if (whichLevel < 0)
				throw new IndexOutOfRangeException("The value of whichlevel must be greater than or equal to 0.");

			return _delegates[whichLevel]();
		}

		public void Start(int whichLevel, BattleManager bm)
		{
			if (whichLevel > Count - 1)
				throw new IndexOutOfRangeException("The value of whichLevel is too large: there is no corresponding level.");
			if (whichLevel < 0)
				throw new IndexOutOfRangeException("The value of whichlevel must be greater than or equal to 0.");

			bm.EnemyManager = _delegates[whichLevel]();
		}
	}
}
