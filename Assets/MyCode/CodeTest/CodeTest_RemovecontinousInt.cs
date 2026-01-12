using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;



public class CodeTest_RemovecontinousInt : MonoBehaviour
{


    public TextMeshProUGUI textUI;

    public void RemoveWIthContinous()
    {
        int length = Random.Range(1, 6);
        int[] array = new int[length];
        array = array.Select(_ => Random.Range(1, 11)).ToArray();
        List<int> answer = new List<int>();
        if (array.Length == 1) return;
        for (int i = 0; i < array.Length; i++)
        {
            if (i == 0)
            {
                answer.Add(array[i]);
            }
            else
                if((array[i] !=array[i-1]))
                answer.Add(array[i]);
        }
        textUI.text = string.Join(", ", answer);

    }

    public void RemoveWIthContinousNext()
    {
        int length = Random.Range(1, 6);
        int[] array = new int[length];
        array = array.Select(_ => Random.Range(1, 11)).ToArray();
        List<int> answer = array.ToList();        
        if (array.Length == 1) return;
        for (int i = array.Length-1; i > 0; i--)
        {
         
                if (array[i] == array[i-1])
                answer.RemoveAt(i);
        }
        textUI.text = string.Join(", ", answer);

    }

    public void RemoveWithContinousNG()
    {
        int length = Random.Range(1, 6);
        int[] array = new int[length];
        array = array.Select(_ => Random.Range(1, 11)).ToArray();
        
        List<int> answer = array.Where((val, idx) => idx ==0 || val != array[idx-1]).ToList();
        Debug.Log(string.Join(",", array));
  
        textUI.text = string.Join(", ", answer);
    }
}
