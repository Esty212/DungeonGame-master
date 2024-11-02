using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesHW
{
    public class ShieldedMonster : Monster
    {
        private int _monsterShieldPoints;

        public ShieldedMonster(int monsterHp, int monsterPower, string name, int monsterX, int monsterY, int shieldPoints) 
            : base(monsterHp, monsterPower, name, monsterX, monsterY)
        {
            _monsterShieldPoints = shieldPoints;
        }

        private bool IsShielded()
        {
            return _monsterShieldPoints > 0;
        }

        public override void TakeDamage(int damage)
        {
            if(IsShielded())
                _monsterShieldPoints--;
            else
            {
                monsterHp -= damage;
                if(monsterHp <= 0)
                {
                    Console.WriteLine(name + " has died! Press any key to continue");
                    Console.ReadKey();
                }
            }
        }
    }
}
