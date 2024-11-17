using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesHW
{
    public class EliteMonster : Monster
    {
        private int _revivalAmount;

        public EliteMonster(int monsterHp, int monsterPower, string name, int monsterX, int monsterY)
            : base(monsterHp, monsterPower, name, monsterX, monsterY)
        {
            _revivalAmount = new Random().Next(1, 3);
        }

        private void Revive()
        {
            monsterHp = 100;
            _revivalAmount--;
            Console.WriteLine("This elite monster came back to life! press enter to continue the fight.");
            Console.ReadKey();
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            if (monsterHp <= 0 && _revivalAmount > 0)
                Revive();
        }
    }
}
