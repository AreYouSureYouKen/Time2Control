using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SumBlock : MonoBehaviour {

    [SerializeField]
    private List<SimpleSumCheck> _sumAnswers;

    public int CheckAnswers()
    {
        int isCorrect = 0;
        foreach (SimpleSumCheck ssc in _sumAnswers)
        {
            if (ssc.checkAnswer()) isCorrect++;
        }
        return isCorrect;
    }

    public void ResetAnswers()
    {
        foreach(SimpleSumCheck ssc in _sumAnswers)
        {
            ssc.Reset();
        }
    }
}
