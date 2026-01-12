using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
public class CodeTestStringSort : MonoBehaviour
{
     string[] inputtext = new string[] { "sun", "game", "play","sudder" };
    public TextMeshProUGUI answerText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  public void StringSort(int n)
    {
        inputtext = inputtext.OrderBy(s => s[n]).ThenBy(s=> s).ToArray();
        answerText.text = string.Join(", ", inputtext);                                               
    }
}
