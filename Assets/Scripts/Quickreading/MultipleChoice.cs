using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class MultipleChoice : MonoBehaviour {
    [SerializeField]
    private Text _question;
    [SerializeField]
    private Text _optionA;
    [SerializeField]
    private Text _optionB;
    [SerializeField]
    private Text _optionC;
    [SerializeField]
    private Text _optionD;
    [SerializeField]
    private GameObject _confirmPanel;
    [SerializeField]
    private GameObject _textToReadPanel;
    [SerializeField]
    private GameObject _questionsPanel;
    [SerializeField]
    private Text _optionText;
    [SerializeField]
    private GameObject _notificationPanel;
    [SerializeField]
    private Text _notificationText;
    [SerializeField]
    private GameObject _endNotificationPanel;
    [SerializeField]
    private Text _endNotificationText;
    [SerializeField]
    private string _beginNotification;
    [SerializeField]
    private string _correctEndNotification;
    [SerializeField]
    private string _badEndNotification;
    [SerializeField]
    private GameObject _wordsPerMinPanel;
    [SerializeField]
    private InputField _wordsPerMinField;
    private int _wordsPerMin;
    [SerializeField]
    private bool _multipleQuestions;
    [SerializeField]
    private int _numberofQuestions = 5;
    [SerializeField]
    private float _timeToWait = 75;
    [SerializeField]
    private int _numberOfWords = 725;
    [SerializeField]
    private PhaseControl _phaseControl;
    private Coroutine waiter;
    [SerializeField]
    private List<Toggle> _toggles;

    private int _correctAnswers = 0;
    private int _currentQuestion = 0;

    private Question _chosen;

    [SerializeField]
    private List<Question> _questions;
    private List<Question> questionBackup;

    [System.Serializable]
    private struct Question
    {
        public string _question;
        public string _answer;
        public string _optionB;
        public string _optionC;
        public string _optionD;


        public Question(string q,string a,string b,string c, string d)
        {
            _question = q;
            _answer = a;
            _optionB = b;
            _optionC = c;
            _optionD = d;
        }
    }

    public void SetUp()
    {
        startPhase();
        _wordsPerMinPanel.SetActive(true);
    }

    public void startPhase()
    {
        if (questionBackup == null) questionBackup = new List<Question>(_questions);
        _currentQuestion = 0;
        _correctAnswers = 0;
        _questions = new List<Question>(questionBackup);

        int option = Random.Range(0, (_questions.Count - 1));
        _chosen = _questions[option];
        _questions.RemoveAt(option);

        _question.text = _chosen._question;
        List<string> options = new List<string>();
        options.Add(_chosen._answer);
        options.Add(_chosen._optionB);
        options.Add(_chosen._optionC);
        options.Add(_chosen._optionD);

        option = Random.Range(0, (options.Count - 1));
        _optionA.text = options[option];
        options.RemoveAt(option);
        option = Random.Range(0, (options.Count - 1));
        _optionB.text = options[option];
        options.RemoveAt(option);
        option = Random.Range(0, (options.Count - 1));
        _optionC.text = options[option];
        options.RemoveAt(option);
        _optionD.text = options[0];
        options.RemoveAt(0);


        _currentQuestion++;
        UncheckToggles();
    }

    public void CheckAnswer()
    {
        
        if (_optionText.text.Equals(_chosen._answer))
        {
            _optionText.text = "Je gegeven antwoord is juist!";
            _correctAnswers++;
            if (_multipleQuestions && _currentQuestion < _numberofQuestions)
            {
                StartCoroutine(StartNewQuestions());
            }
            else
            {
                _confirmPanel.SetActive(false);
                _endNotificationPanel.SetActive(true);
                if(_correctAnswers >=4)
                {
                    _endNotificationText.text = string.Format(_correctEndNotification, _correctAnswers, _numberofQuestions);
                }
                else
                {
                    _endNotificationText.text = string.Format(_badEndNotification, _correctAnswers, _numberofQuestions);
                }
                
                StartCoroutine(ContinueOrReset());
            }
        }
        else
        {
            _optionText.text = "Helaas dit antwoord is niet correct.";
            if (_multipleQuestions && _currentQuestion < _numberofQuestions)
            {
                StartCoroutine(StartNewQuestions());

            }
            else
            {
                _confirmPanel.SetActive(false);
                _endNotificationPanel.SetActive(true);
                if (_correctAnswers >= 4)
                {
                    _endNotificationText.text = string.Format(_correctEndNotification, _correctAnswers, _numberofQuestions);
                }
                else
                {
                    _endNotificationText.text = string.Format(_badEndNotification, _correctAnswers, _numberofQuestions);
                }

                StartCoroutine(ContinueOrReset());
            }
        }

    }

    private void UncheckToggles()
    {
        foreach(Toggle tg in _toggles)
        {
            tg.isOn = false;
        }
    }

    public void PressedOK()
    {
        if (_currentQuestion > 0)
        { }
        _notificationPanel.SetActive(false);
        _questionsPanel.SetActive(false);
        waiter = StartCoroutine(WaitTime(_timeToWait));
    }

    public void PressedWPMOK()
    {
        if(int.TryParse(_wordsPerMinField.text,out _wordsPerMin))
        {
            _wordsPerMinPanel.SetActive(false);
            _timeToWait = ((_numberOfWords / _wordsPerMin) * 60) / 2;
            _notificationPanel.SetActive(true);
            _notificationText.text = _beginNotification;
            _textToReadPanel.SetActive(true);
        }
    }

    public void PressedContinue()
    {
        StopCoroutine(waiter);
        _notificationPanel.SetActive(false);
        _questionsPanel.SetActive(true);
        _textToReadPanel.SetActive(false);
    }

    public void ClickedOption(Text TextFromOption)
    {
        _confirmPanel.SetActive(true);
        _optionText.text = TextFromOption.text;
    }

    private IEnumerator StartNewQuestions()
    {
        yield return new WaitForSeconds(1.5f);
        UncheckToggles();
        _confirmPanel.SetActive(false);
        int option = Random.Range(0, (_questions.Count - 1));
        _chosen = _questions[option];
        _questions.RemoveAt(option);

        _question.text = _chosen._question;
        List<string> options = new List<string>();
        options.Add(_chosen._answer);
        options.Add(_chosen._optionB);
        options.Add(_chosen._optionC);
        options.Add(_chosen._optionD);

        option = Random.Range(0, (options.Count - 1));
        _optionA.text = options[option];
        options.RemoveAt(option);
        option = Random.Range(0, (options.Count - 1));
        _optionB.text = options[option];
        options.RemoveAt(option);
        option = Random.Range(0, (options.Count - 1));
        _optionC.text = options[option];
        options.RemoveAt(option);
        _optionD.text = options[0];
        options.RemoveAt(0);

        _currentQuestion++;
    }

    private IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);
        
        _notificationPanel.SetActive(true);
        PressedContinue();
        // do logic for restarting the text?
    }

    private IEnumerator ContinueOrReset()
    {
        yield return new WaitForSeconds(2f);

        if((_numberofQuestions-1) <= _correctAnswers)
        {
            _phaseControl.UpdatePhase();
        }
        else
        {
            startPhase();
            _notificationText.text = _beginNotification;
            _notificationPanel.SetActive(true);
            _endNotificationPanel.SetActive(false);
            _confirmPanel.SetActive(false);
            _questionsPanel.SetActive(false);
            _textToReadPanel.SetActive(true);
        }
    }
}
