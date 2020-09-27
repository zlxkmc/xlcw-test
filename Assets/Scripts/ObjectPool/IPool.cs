using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pool
{
    public interface IPool<T>
    {
        /// <summary>
        /// 申请
        /// </summary>
        /// <returns></returns>
        T Allocate();

        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        void Recycle(T obj);
    }


}
