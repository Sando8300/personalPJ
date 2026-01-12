
using TMPro;
using UnityEngine;


public class CodeTest : MonoBehaviour
{
    private TextMeshProUGUI textPn;
    int year = 0;
    public TMP_InputField input;

    private void Awake()
    {
        textPn = GetComponent<TextMeshProUGUI>();
    }



    void Start()
    {
           
    }

    public void LeapYearCheck()
    {
        int inputYear = int.Parse(input.text);
        bool isLeap = (inputYear % 400 != 0) && (inputYear % 100 == 0 || inputYear % 4 != 0) ? false : true;
        input.text = isLeap ? "LeapYear" : "NormalYear";



    }
}
