using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MindmapWordTarget : MonoBehaviour {
    [SerializeField]
    private List<string> _wordTargets;
    [SerializeField]
    private TextCollider _autoCompleteTarget;


    public bool CheckWordTarget(string word)
    {
        foreach(string w in _wordTargets)
        {
            if (w.ToLower().Equals(word.ToLower().Replace(".","").Replace(",","").Replace(":",""))) return true;
        }
        return false;
    }

    public bool isOccupied()
    {
        if (this.transform.childCount == 0) return false; else return true;
    }

    [ContextMenu("check words")]
    public bool CheckWords()
    {
        bool allTrue = true;
        if (this.gameObject.transform.childCount == 0)
        {
            allTrue = false;
        }
        else
        {
            foreach (TextCollider tc in transform.GetComponentsInChildren<TextCollider>())
            {
                string word = tc.GetText().text;
                bool correct = false;
                foreach (string w in _wordTargets)
                {
                    if (w.ToLower().Equals(word.ToLower().Replace(".", "").Replace(",", "").Replace(":", ""))) correct = true;
                }
                if (!correct)
                {
                    tc.ResetToStart();
                    allTrue = false;
                }
            }
        }
        return allTrue;
    }

    public void AutoComplete()
    {
        if(!CheckWords())
        _autoCompleteTarget.AutoComplete(this.gameObject);
    }
}
