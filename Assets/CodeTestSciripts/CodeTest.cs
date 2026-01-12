using TMPro;
using UnityEngine;
using System.Text;
using System.Linq;




public class EvenOrOdd : MonoBehaviour
{
    public TextMeshProUGUI textInput;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StudyMethod(string text)
    {
        int result = 0;
        char[] toInt = text.ToCharArray();
        StringBuilder sb = new StringBuilder("");
        result = toInt.Select(i => i - '0').Sum();
        sb.Append(result % 2 == 0 ? "¦" : "Ȧ");



        textInput.text = $"{result} {sb.ToString()}";
    }
    void Start()
    {
        StudyMethod(textInput.text);
    }

}