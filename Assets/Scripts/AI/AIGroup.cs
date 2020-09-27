using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;

namespace AI
{
    public class AIGroup : MonoBehaviour
    {
        [Serializable]
        public class OnUnitTakeDamage : UnityEvent<DamageInfo>
        {

        }

        /// <summary>
        /// 当有单位受到伤害时
        /// </summary>
        public OnUnitTakeDamage onUnitTakeDamage { get => _onUnitTakeDamage; }

        [SerializeField]
        private OnUnitTakeDamage _onUnitTakeDamage = new OnUnitTakeDamage();

        private List<BaseAI> _aiList;
    }
}
