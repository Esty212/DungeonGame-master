using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesHW
{
    public class Monster
    {
        public int monsterHp;
        public int maxMonsterAttackPower;
        public string name;
        public int monsterX;
        public int monsterY;


        public Monster(int monsterHp, int monsterPower, string name, int monsterX, int monsterY)
        {
            this.monsterHp = monsterHp;
            this.maxMonsterAttackPower = monsterPower;
            this.name = name;
            this.monsterX = monsterX;
            this.monsterY = monsterY;
        }

        public bool HasEncounteredMonster(int x, int y)
        {
            return x == monsterX && y == monsterY;
        }

        public int MonsterAttack()
        {
            return new Random().Next(5, maxMonsterAttackPower);
        }

    }
}
