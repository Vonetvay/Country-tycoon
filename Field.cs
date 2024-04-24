using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gardening
{
    public class Field : MonoBehaviour
    {
        public List<FieldSkin> Skins = new List<FieldSkin>();

        public int HarvestingPrice = 0;


        public string ID
        {
            get
            {
                return _ID;
            }
        }


        [SerializeField] private string _ID = "";

        private FieldSkin _currentSkin = null;



        public void ApplyNewSkin(FieldSkin skin)
        {
            if (_currentSkin != null)
            {
                _currentSkin.Skin.SetActive(false);
            }
            _currentSkin = skin;
            if (_currentSkin != null)
            {
                _currentSkin.Skin.SetActive(true);
            }
        }
    }
}

