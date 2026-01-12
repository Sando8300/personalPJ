using TMPro;
using UnityEngine;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;

public class CodeTest_Distinct : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI textUI;
    int[] array;
    int[] answer;
    List <int> listAnswer;
    int length;
    public void RemoveWIthDistinct()
    {
        length = Random.Range(5, 101);
        array = new int[length];
        answer = array.Select(_ => Random.Range(1, 11)).ToArray();
        
        answer = answer.Distinct().OrderBy(i => i).ToArray(); ;
        textUI.text = string.Join(", ", answer);
        
    }

    public void RemoveWithContains()
    {
        length = Random.Range(5, 101);
        array = new int[length];
        listAnswer = array.Select(_ => Random.Range(1, 11)).ToList();
        List<int> answer = new List<int> { };
        foreach(int i in listAnswer)
        {
           if(!answer.Contains(i))
            {
                answer.Add(i);
            }
        }
       answer.Sort();

        textUI.text = string.Join(", ",answer);
    }
}
