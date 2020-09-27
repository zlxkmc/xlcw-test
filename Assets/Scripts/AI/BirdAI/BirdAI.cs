using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Bird
{
    public class BirdAI : BaseAI
    {
        /// <summary>
        /// 追逐范围
        /// </summary>
        public float ChaseRange { get => _chaseRange; set => _chaseRange = value; }

        /// <summary>
        /// 单位
        /// </summary>
        public new Monster Unit { get => (Monster)base.Unit; }


        [SerializeField]
        private float _chaseRange = 15;

        protected override void Start()
        {
            base.Start();
            CurState = new IdelState(this);
        }

    }
}
