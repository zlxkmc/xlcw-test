using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IRecommendItem
{
    /// <summary>
    /// 推荐的装备
    /// </summary>
    /// <returns></returns>
    List<Item> GetRecommendItems();
}

