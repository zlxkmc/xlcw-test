using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AI
{
    public class BaseAI : MonoBehaviour, IStateController
    {
        /// <summary>
        /// 控制的单位
        /// </summary>
        public Unit Unit { get => _unit; }

        /// <summary>
        /// 管理这个ai的组
        /// </summary>
        public AIGroup AIGroup { get => _aiGroup; }

        public IState CurState {
            get
            {
                return _curState;
            }
            set
            {
                if(_curState != value)
                {
                    if (_curState != null)
                    {
                        _curState.OnStateEnd();
                    }
                    _curState = value;

                    if(_curState != null)
                    {
                        _curState.OnStateStart();
                    }
                }
            }
        }

        private IState _curState;

        private Unit _unit;
        private AIGroup _aiGroup;

        protected virtual void Awake()
        {
            _unit = GetComponent<Unit>();
            _aiGroup = GetComponentInParent<AIGroup>();
        }

        protected virtual void Start()
        {
            Unit.onTakeDamage.AddListener(AIGroup.onUnitTakeDamage.Invoke);
        }

        protected virtual void Update()
        {
            if(CurState != null)
            {
                CurState.OnStateUpdate();
            }
        }

        private void OnDestroy()
        {
            Unit.onTakeDamage.RemoveListener(AIGroup.onUnitTakeDamage.Invoke);
            if (CurState != null)
            {
                CurState.OnStateEnd();
            }
        }
    }
}
