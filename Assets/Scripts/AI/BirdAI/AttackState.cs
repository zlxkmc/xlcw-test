using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AI.Bird
{
    public class AttackState : IState
    {
        private BirdAI _ai;
        private Unit _attactTarget;

        public AttackState(BirdAI ai, Unit attackTarget)
        {
            _ai = ai;
            _attactTarget = attackTarget;
        }

        public void OnStateEnd()
        {

        }

        public void OnStateStart()
        {

        }

        public void OnStateUpdate()
        {
            if(_attactTarget == null || Vector3.Distance(_ai.Unit.transform.position, _ai.Unit.SpawnPos) > _ai.ChaseRange)
            {
                _ai.CurState = new ReturnState(_ai);
            }
            else
            {
                if (_ai.Unit.IsInAttackRange(_attactTarget))
                {
                    _ai.Unit.NormalAttack(_attactTarget);
                }
                else
                {
                    _ai.Unit.MoveTo(_attactTarget.transform.position);
                }
            }
        }
    }
}
