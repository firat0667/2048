using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class NumberController : MonoBehaviour
{
    [SerializeField]
    private NumberColorData _numberColorData;
    [SerializeField]
    private TextMeshPro _text;
    [SerializeField]
    private SpriteRenderer _renderer;

    public int Number {
        get => _number;
    set {
            int valuePrint = (int)Mathf.Pow(2, value);
            _text.text = valuePrint.ToString();
            // _text.settext(valuePrint.ToString());
            _renderer.color = _numberColorData.NumberColor[value-1];
            _number = value;

        }

    }
    private int _number = 1;
    private void Start()
    {
        Number = _number;
    }
}
