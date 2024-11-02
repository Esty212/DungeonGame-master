using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesHW
{
    public class RageMonster : Monster
    {
        private int _damageAmount;
        public RageMonster(int monsterHp, int monsterPower, string name, int monsterX, int monsterY) : base(monsterHp, monsterPower, name, monsterX, monsterY)
        {
            _damageAmount = 20;
        }

        public override int MonsterAttack()
        {
            int damageToPlayer = base.MonsterAttack();
            maxMonsterAttackPower += _damageAmount;
            return damageToPlayer;
        }
    }
}
