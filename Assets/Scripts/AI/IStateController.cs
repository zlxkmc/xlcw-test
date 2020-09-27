using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    public interface IStateController
    {
        IState CurState { get; }
    }
}
