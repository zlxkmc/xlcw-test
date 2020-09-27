using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AI.Bird
{
    public class IdelState : IState
    {
        private BirdAI _ai;

        public IdelState(BirdAI ai)
        {
            _ai = ai;
        }

        public void OnStateEnd()
        {
            _ai.AIGroup?.onUnitTakeDamage.RemoveListener(OnGroupUnitTakeDamage);
            _ai.Unit.onTakeDamage.RemoveListener(OnTakeDamage);
        }

        public void OnStateStart()
        {
            _ai.AIGroup?.onUnitTakeDamage.AddListener(OnGroupUnitTakeDamage);
            _ai.Unit.onTakeDamage.AddListener(OnTakeDamage);
        }

        public void OnStateUpdate()
        {

        }

        private void OnTakeDamage(DamageInfo info)
        {
            Unit attacker = info.attacker;
            if (attacker != null)
            {
                _ai.CurState = new AttackState(_ai, attacker);
            }
        }

        private void OnGroupUnitTakeDamage(DamageInfo info)
        {
            Unit attacker = info.attacker;
            if(attacker != null)
            {
                _ai.CurState = new AttackState(_ai, attacker);
            }
        }
    }
}
