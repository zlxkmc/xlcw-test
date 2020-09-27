using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RecommendItemList : MonoBehaviour
    {
        /// <summary>
        /// 装备ui容器
        /// </summary>
        [SerializeField]
        private Transform _content;

        /// <summary>
        /// 装备ui模板
        /// </summary>
        [SerializeField]
        private ItemUI _itemUI;

        /// <summary>
        /// 最大数量
        /// </summary>
        [Range(0,5)]
        [SerializeField]
        private int maxCount = 3;

        /// <summary>
        /// 推荐策略
        /// </summary>
        private IRecommendItem recommendItem;

        private List<ItemUI> _itemUIs = new List<ItemUI>();

        // Start is called before the first frame update
        void Start()
        {
            recommendItem = new PricePriority();

            StartCoroutine(UpdateRecommend());
        }

        IEnumerator UpdateRecommend()
        {
            while (true)
            {
                List<Item> items = recommendItem.GetRecommendItems();
                int index = 0;
                for(; index < items.Count && index < maxCount; index++)
                {
                    ItemUI itemUI = null;
                    if(index < _itemUIs.Count)
                    {
                        itemUI = _itemUIs[index];
                    }
                    else
                    {
                        itemUI = AddItemUI();
                    }

                    itemUI.SetItem(items[index]);
                    itemUI.Show();
                }

                // 多出来的隐藏掉
                for(; index < _itemUIs.Count; index++)
                {
                    _itemUIs[index].Hide();
                }

                yield return new WaitForSeconds(0.5f);
            }
        }

        private ItemUI AddItemUI()
        {
            GameObject go = Instantiate(_itemUI.gameObject, _content);
            ItemUI itemUI = go.GetComponent<ItemUI>();
            _itemUIs.Add(itemUI);

            return itemUI;
        }
    }
}
