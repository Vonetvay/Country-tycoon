using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gardening 
{
    [System.Serializable]
    public class FieldSkin
    {
        [SerializeField] private AgriCulture _type;
        [SerializeField] private FieldState _variant;
        [SerializeField] private GameObject _skin;

        public AgriCulture Type
        {
            get { return _type; }
        }
        public FieldState Variant
        {
            get { return _variant; }
        }

        public GameObject Skin
        {
            get { return _skin; }
        }
    }
}

