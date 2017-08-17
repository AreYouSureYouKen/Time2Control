using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class StroopTest : MonoBehaviour {
    public enum State { None,Color,Name,Solved };
    public State StartState = State.Color;
    private Coroutine colorco;
    private Coroutine nameco;

    [SerializeField]
    private Color blue;
    [SerializeField]
    private Color black;
    [SerializeField]
    private Color yellow;
    [SerializeField]
    private Color red;
    [SerializeField]
    private Color green;
    [SerializeField]
    private GameObject _colorButtonsPanel;
    [SerializeField]
    private GameObject _nameButtonsPanel;
    [SerializeField]
    private GameObject _notificationPanel;
    [SerializeField]
    private Text _notificationText;
    [SerializeField]
    private string _notificationColor;
    [SerializeField]
    private string _notificationName;
    [SerializeField]
    private List<StroopColor> _colorWords;
    [SerializeField]
    private List<StroopColor> _nameWords;
    [SerializeField]
    private PhaseControl _phaseControl;

    [SerializeField]
    private float _colorTime = 30;
    [SerializeField]
    private float _nameTime = 45;


    private State _currentState;
    private int _currentWordNr = 0;
    private StroopColor _currentWord;

    public State CurrentState {
        get { return _currentState; }
        set { _currentState = value;
        switch(_currentState)
            {
            case State.None:
                _currentState = State.None;
                _colorButtonsPanel.SetActive(false);
                _nameButtonsPanel.SetActive(false);
                break;
            case State.Color:
                _currentState = State.Color;
                _colorButtonsPanel.SetActive(true);
                _nameButtonsPanel.SetActive(false);
                SetWordsColor();
                _notificationText.text = _notificationColor;
                _notificationPanel.SetActive(true);
                break;
            case State.Name:
                _currentState = State.Name;
                _colorButtonsPanel.SetActive(false);
                _nameButtonsPanel.SetActive(true);
                SetWordsName();
                _notificationText.text = _notificationName;
                _notificationPanel.SetActive(true);
                break;
            case State.Solved:
                _currentState = State.Solved;
                _phaseControl.UpdatePhase();
                break;
            }
        }
    }

    [System.Serializable]
    private struct StroopColor
    {
        public Text _textObject;
        public string _textWord;
        public Color _wantedColor;
    }

    [ContextMenu("Set-up for Color")]
    public void SetWordsColor()
    {
        foreach (StroopColor sc in _colorWords)
        {
            sc._textObject.text = sc._textWord;
            sc._textObject.color = Color.grey;
        }
        _currentWordNr = 0;
        SetNextColorWord();
    }

    [ContextMenu("Set-up for Name")]
    public void SetWordsName()
    {
        foreach(StroopColor sc in _nameWords)
        {
            sc._textObject.text = sc._textWord;
            sc._textObject.color = Color.grey;
        }
        _currentWordNr = 0;
        SetNextNameWord();
    }

    public void SetUp()
    {
        CurrentState = State.Color;
    }

    private void SetNextColorWord()
    {
        if (_currentWordNr == _colorWords.Count)
        {
            StopCoroutine(colorco);
            CurrentState = State.Name;
        }
        else
        {
            _currentWord = _colorWords[_currentWordNr];
            _currentWord._textObject.text = _currentWord._textWord;
            _currentWord._textObject.color = _currentWord._wantedColor;
            _currentWordNr++;
        }
    }

    private void SetNextNameWord()
    {
        if(_currentWordNr == _nameWords.Count)
        {
            
            StopCoroutine(nameco);
            CurrentState = State.Solved;
        }
        else
        {
            _currentWord = _nameWords[_currentWordNr];
            _currentWord._textObject.text = _currentWord._textWord;
            _currentWord._textObject.color = _currentWord._wantedColor;
            _currentWordNr++;
        }
    }

    public void PressedOK()
    {
        switch(_currentState)
        {
            case State.Color:
                colorco = StartCoroutine(WaitTime(_colorTime));
                _notificationPanel.SetActive(false);
                break;

            case State.Name:
                nameco = StartCoroutine(WaitTime(_nameTime));
                _notificationPanel.SetActive(false);
                break;
        }
    }

    private IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);
        CurrentState = _currentState;
    }


	// Use this for initialization
	void Start () {
        CurrentState = StartState;
	}

    public void PressedColorBlue()
    {
        if (_currentWord._wantedColor.Equals(blue))
        {
            SetNextColorWord();
        }
        else
        {
            SetWordsColor();
        }
    }

    public void PressedColorYellow()
    {
        if (_currentWord._wantedColor.Equals(yellow))
        {
            SetNextColorWord();
        }
        else
        {
            SetWordsColor();
        }
    }

    public void PressedColorBlack()
    {
        if (_currentWord._wantedColor.Equals(black))
        {
            SetNextColorWord();
        }
        else
        {
            SetWordsColor();
        }
    }

    public void PressedColorRed()
    {
        if (_currentWord._wantedColor.Equals(red))
        {
            SetNextColorWord();
        }
        else
        {
            SetWordsColor();
        }
    }

    public void PressedColorGreen()
    {
        if (_currentWord._wantedColor.Equals(green))
        {
            SetNextColorWord();
        }
        else
        {
            SetWordsColor();
        }
    }

    public void PressedNameWord(string word)
    {
        if(_currentWord._textWord.ToLower().Equals(word))
        {
            SetNextNameWord();
        }
        else
        {
            SetWordsName();
        }
    }
}
