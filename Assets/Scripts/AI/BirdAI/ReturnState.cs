using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AI.Bird
{
    public class ReturnState : IState
    {
        private BirdAI _ai;

        public ReturnState(BirdAI ai)
        {
            _ai = ai;
        }

        public void OnStateEnd()
        {
            
        }

        public void OnStateStart()
        {
            _ai.Unit.MoveTo(_ai.Unit.SpawnPos);
        }

        public void OnStateUpdate()
        {
            if(Vector3.Distance(_ai.Unit.transform.position, _ai.Unit.SpawnPos) < 0.1)
            {
                _ai.CurState = new IdelState(_ai);
            }
        }


    }
}
