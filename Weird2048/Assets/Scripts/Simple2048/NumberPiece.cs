using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
namespace Simple2048
{
    public class NumberPiece : MonoBehaviour
    {
        public Image Bg;
        public TextMeshProUGUI ValueDisplay;
        private int value;
        public void SetValue(int _value)
        {
            value = _value;
            ValueDisplay.SetText(value.ToString());
        }
        public int GetValue() => value;

    }
}
