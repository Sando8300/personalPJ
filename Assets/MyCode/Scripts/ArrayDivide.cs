using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class ArrayDivide : MonoBehaviour
{


    public TextMeshProUGUI text;
    public List<int> intlist = new List<int> { 1, 20, 13, 1, 513, 12, 18, 80, 97, 10 };
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    
    public void Divide(int inputint)
    {
        List<int> answer = intlist.FindAll(i => i % inputint == 0);
        answer.Sort();
        if (answer.Count == 0)
            text.text = "-1";
        else
        {
            text.text = $"나누어 떨어지는 요소 : {string.Join(", ",answer)}";
        }
    }

    public int[] ints = new int[10];
    public int[] answer;
    
    public  void EvenDivideCalu()
    {
        answer = ints.Select(_=> Random.Range(1, 101)).ToArray();
        

        text.text = string.Join(", ", answer.Where(i => i % 2 == 0).ToArray().OrderBy(i => i));
    }
}
