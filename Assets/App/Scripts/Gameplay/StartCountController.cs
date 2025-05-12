using System.Collections;
using UnityEngine;

public class StartCountController : MonoBehaviour
{
    [SerializeField] private StartCountView view;
    [SerializeField] private string[] words;

    public IEnumerator StartCount(float duration)
    {
        float timeForOneWord = duration / words.Length;

        foreach (string word in words)
        {
            view.SetText(word);
            yield return new WaitForSeconds(timeForOneWord);
        }
        
        view.SetText("");
    }
}
