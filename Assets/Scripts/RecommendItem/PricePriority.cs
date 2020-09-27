using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 价格优先
/// </summary>
public class PricePriority : IRecommendItem
{
    public List<Item> GetRecommendItems()
    {
        int money = Data.Instance.Money;

        // 找到买的起的装备
        List<Item> items = Data.Instance.Items.FindAll((item) =>
        {
            return item.Price <= money;
        });

        List<Item> result = new List<Item>();

        if (items.Count > 0)
        {
            // 按价格从大到小排列
            //items.Sort((item1, item2) => { return -item1.Price.CompareTo(item2.Price); });
            Sort(items, 0, items.Count - 1, (item1, item2) => { return -item1.Price.CompareTo(item2.Price); });

            /*
            StringBuilder s = new StringBuilder();
            foreach (var item in items)
            {
                s.AppendLine(item.Price.ToString());
            }

            Debug.Log(s);
            */

            // 取前五个
            for (int i = 0; i < items.Count && i <= 5; i++)
            {
                result.Add(items[i]);
            }
        }

        return result;
    }


    /// <summary>
    /// 快速排序，升序
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="comparison"></param>
    private void Sort<T>(List<T> list, int left, int right, Comparison<T> comparison)
    {
        if(list.Count == 0 || left >= right)
        {
            return;
        }

        int l = left;
        int r = right;
        T k = list[l];

        while (l < r)
        {
            for (; l < r; r--)
            {
                if(comparison(list[r], k) <= 0)
                {
                    list[l++] = list[r];
                    break;
                }
            }

            for (; l < r; l++)
            {
                if (comparison(list[l], k) > 0)
                {
                    list[r--] = list[l];
                    break;
                }
            }
        }

        list[l] = k;

        Sort<T>(list, left, l - 1, comparison);
        Sort<T>(list, l + 1, right, comparison);
    }
}

