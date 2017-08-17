using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class NOTStroopTest : MonoBehaviour
{
    public enum State { None, Color, Name, Solved };
    public State StartState = State.Color;

    private Coroutine timerco;
    private float timer = 0f;


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
    private GameObject _mainPanel;
    [SerializeField]
    private GameObject _stroopTest;
    [SerializeField]
    private RectTransform _strooptestColorPos;
    [SerializeField]
    private RectTransform _strooptestNamePos;
    [SerializeField]
    private GameObject _startPanel;
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
    private string _notificationCompleted;
    [SerializeField]
    private List<StroopColor> _colorWords;
    [SerializeField]
    private List<StroopColor> _nameWords;
    [SerializeField]
    private Text Timer;

    [SerializeField]
    private float _colorTime = 30;
    [SerializeField]
    private float _nameTime = 45;


    private State _currentState;
    private int _currentWordNr = 0;
    private StroopColor _currentWord;

    public State CurrentState
    {
        get { return _currentState; }
        set
        {
            _currentState = value;
            switch (_currentState)
            {
                case State.None:
                    _currentState = State.None;
                    _mainPanel.SetActive(false);
                    _startPanel.SetActive(true);
                    _colorButtonsPanel.SetActive(false);
                    _nameButtonsPanel.SetActive(false);
                    break;
                case State.Color:
                    _currentState = State.Color;
                    _mainPanel.SetActive(true);
                    _stroopTest.GetComponent<RectTransform>().anchoredPosition = _strooptestColorPos.anchoredPosition;
                    _startPanel.SetActive(false);
                    _colorButtonsPanel.SetActive(true);
                    _nameButtonsPanel.SetActive(false);
                    if(timerco != null)
                    StopCoroutine(timerco);
                    SetWordsColor();
                    _notificationText.text = _notificationColor;
                    Timer.text = "Seconden: "+timer;
                    _notificationPanel.SetActive(true);
                    break;
                case State.Name:
                    _currentState = State.Name;
                    _mainPanel.SetActive(true);
                    _stroopTest.GetComponent<RectTransform>().anchoredPosition = _strooptestNamePos.anchoredPosition;
                    _startPanel.SetActive(false);
                    _colorButtonsPanel.SetActive(false);
                    _nameButtonsPanel.SetActive(true);
                    if (timerco != null)
                        StopCoroutine(timerco);
                    SetWordsName();
                    _notificationText.text = _notificationName;
                    _notificationPanel.SetActive(true);
                    break;
                case State.Solved:
                    _currentState = State.Solved;
                    if (timerco != null)
                        StopCoroutine(timerco);
                    _mainPanel.SetActive(true);
                    _startPanel.SetActive(false);
                    _colorButtonsPanel.SetActive(false);
                    _nameButtonsPanel.SetActive(false);
                    _notificationText.text = string.Format(_notificationCompleted, timer) ;
                    _notificationPanel.SetActive(true);
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
        foreach (StroopColor sc in _nameWords)
        {
            sc._textObject.text = sc._textWord;
            sc._textObject.color = Color.grey;
        }
        _currentWordNr = 0;
        SetNextNameWord();
    }

    public void SetUp()
    {
        timer = 0f;
        CurrentState = State.None;
    }

    private void SetNextColorWord()
    {
        if (_currentWordNr == _colorWords.Count)
        {
            StopCoroutine(colorco);
            StopCoroutine(timerco);
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
        if (_currentWordNr == _nameWords.Count)
        {

            StopCoroutine(nameco);
            StopCoroutine(timerco);
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
        switch (_currentState)
        {
            case State.None:
                CurrentState = State.Color;
                break;
            case State.Color:
                colorco = StartCoroutine(WaitTime(_colorTime));
                timerco = StartCoroutine(timerTick());
                _notificationPanel.SetActive(false);
                break;

            case State.Name:
                nameco = StartCoroutine(WaitTime(_nameTime));
                timerco = StartCoroutine(timerTick());
                _notificationPanel.SetActive(false);
                break;
            case State.Solved:
                SetUp();
                break;
        }
    }

    private IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);
        CurrentState = _currentState;
    }

    private IEnumerator timerTick()
    {
        yield return new WaitForSeconds(1f);
        timer += 1f;
        Timer.text = "Seconden: "+ timer;
        timerco = StartCoroutine(timerTick());
    }


    // Use this for initialization
    void Start()
    {
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
        if (_currentWord._textWord.ToLower().Equals(word))
        {
            SetNextNameWord();
        }
        else
        {
            SetWordsName();
        }
    }
}
