using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MoneyUI : MonoBehaviour
    {
        [SerializeField]
        private Text _moneyText;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(PlayerController.Instance != null)
            {
                _moneyText.text = Data.Instance.Money.ToString();
            }
        }
    }

}
