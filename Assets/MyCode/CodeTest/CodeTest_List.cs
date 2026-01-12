using System.Collections.Generic;
using System.Linq;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

public class CodeTest_List : MonoBehaviour
{
    public string answer;
    public TextMeshProUGUI text;
    public string str = "people";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Listtest();
    }

    // Update is called once per frame
   
    public void Listtest()
    {
   
        text.text = new string(str.Distinct().ToArray());
    }
}
