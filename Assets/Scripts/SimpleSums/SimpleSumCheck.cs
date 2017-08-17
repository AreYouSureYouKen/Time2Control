using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimpleSumCheck : MonoBehaviour {
    [SerializeField]
    private int _correctAnswer;
    [SerializeField]
    private InputField _answerField;
	
    public bool checkAnswer()
    {
        int given;
        
        if(int.TryParse(_answerField.text, out given))
        if (given.Equals(_correctAnswer))
        {
            Debug.Log("answer is correct!");
            return true;
        }
        Debug.Log("Answer is " + given + " but should be " + _correctAnswer + " at "+ this.gameObject.name);
        _answerField.text = "";
        
        return false;
    }

    public void Reset()
    {
        _answerField.text = "";
    }
}
