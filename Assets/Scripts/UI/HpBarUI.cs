
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HpBarUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject _content;
        [SerializeField]
        private Image _bar;

        private Unit _unit;

        public void Init(Unit unit)
        {
            _unit = unit;
        }

        void Update()
        {
            if(_unit == null)
            {
                Destroy(gameObject);
            }
            else
            {
                Vector3 viewPos = Camera.main.WorldToViewportPoint(_unit.HpBarPoint.position);

                if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
                {
                    _content.SetActive(false);
                }
                else
                {
                    Camera uiCamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
                    transform.position = uiCamera.ViewportToWorldPoint(viewPos);
                    _bar.fillAmount = _unit.Hp * 1f / _unit.MaxHp;

                    _content.SetActive(true);
                }
            }
        }


    }

}

