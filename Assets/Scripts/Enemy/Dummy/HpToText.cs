using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HpToText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text dummyText;


    // Update is called once per frame
    void Update()
    {
        dummyText.text = GetComponent<Dummy>().dummyHp.ToString("0");
    }
}
