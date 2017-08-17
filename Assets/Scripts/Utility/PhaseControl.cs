using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class PhaseControl : MonoBehaviour {
    [SerializeField]
    private List<GameObject> Phases;
    private string logincode;
    private int _phase;
    private string _personName;
    [SerializeField]
    private InputField _nameField;
    [SerializeField]
    private InputField _trainerMentorField;
    private GameObject _currentPhaseObject = null;
    [SerializeField]
    private InputField code;
    [SerializeField]
    private GameObject loginObject;
    [SerializeField]
    private GameObject _wrongPasswordPanel;
    [SerializeField]
    private bool _bypass;
    [SerializeField]
    private GameObject _endPanel;
    [SerializeField]
    private Text _endName;
    [SerializeField]
    private string _endNameString;
    [SerializeField]
    private Text _endTimePhase1;
    [SerializeField]
    private string _endTimePhase1String;
    [SerializeField]
    private Text _endTimePhase2;
    [SerializeField]
    private string _endTimePhase2String;
    [SerializeField]
    private Text _endTimePhase3;
    [SerializeField]
    private string _endTimePhase3String;
    [SerializeField]
    private Text _endTimePhase4;
    [SerializeField]
    private string _endTimePhase4String;
    [SerializeField]
    private Text _endTimePhase5;
    [SerializeField]
    private string _endTimePhase5String;
    [SerializeField]
    private Text _endTimePhase6;
    [SerializeField]
    private string _endTimePhase6String;
    [SerializeField]
    private Text _endTimePhase7;
    [SerializeField]
    private string _endTimePhase7String;
    [SerializeField]
    private Text _endTimeTotal;
    [SerializeField]
    private string _endTimeTotalText;

    private float _startTime;

    public void TryLogin()
    {
        if (_bypass)
        {
            _phase = 0;
            loginObject.SetActive(false);
            StartPhase(_phase);
        }
        else
        {
            string loginstring = "http://www.time2control.nl/Time2Game/login.php?login=" + code.text;
            StartCoroutine(MakeCall(loginstring, true,false,false));
        }
    }

    public void UpdatePhase()
    {
        if (_bypass)
        {
            _phase++;
            StartPhase(_phase);
        }
        else
        {
            _phase++;
            float endTime = Time.time;
            float timeTaken = Mathf.Round(endTime - _startTime);
            string PhaseCode = "http://www.time2control.nl/Time2Game/login.php?logincode=" + logincode + "&savePhase=" + _phase+"&timePhase="+timeTaken+"&name="+_nameField.text;
            StartCoroutine(MakeCall(PhaseCode, false,true,false));
        }
    }

    public void saveName()
    {
        string nameCode = "http://www.time2control.nl/Time2Game/login.php?saveName=" + _nameField.text + "&nameLogin=" + code.text+"&mentorName="+_trainerMentorField.text;
        StartCoroutine(MakeCall(nameCode, false, false, false));
    }

    public void GetEndPhase()
    {
        string endPhaseCode = "http://www.time2control.nl/Time2Game/login.php?completePhase=" + code.text;
        StartCoroutine(MakeCall(endPhaseCode, false, false, true));
    }

    private IEnumerator MakeCall(string url,bool isLogin,bool isUpdate,bool isEndPhase)
    {
        WWW www = new WWW(url);
        yield return www;
        GetReturnCall(www,isLogin,isUpdate,isEndPhase);
    }

    private void GetReturnCall(WWW result,bool isLogin, bool isUpdate,bool isEndPhase)
    {
        if (isLogin)
        {
            string resultstring = result.text;
            string[] splitted = resultstring.Split(';');

            if (splitted.Length == 2)
            {
                loginObject.SetActive(false);
                Debug.Log("logged in, phase is " + splitted[1]);
                logincode = splitted[0];
                if(int.TryParse(splitted[1],out _phase))
                {
                    StartPhase(_phase);
                }
                else
                {
                    Debug.Log("something went wrong.");
                }
            }
            else
            {
                _wrongPasswordPanel.SetActive(true);
            }
        }
        else if(isUpdate)
        {
            StartPhase(_phase);
        }
        else if(isEndPhase)
        {
            EndPhase(result.text);
        }
        // else there is no return value so ignore this.
    }

    private void EndPhase(string resultstring)
    {
        string[] splitted = resultstring.Split(';');

        if (splitted.Length == 10)
        {
            string name;
            int time1;
            int time2;
            int time3;
            int time4;
            int time5;
            int time6;
            int time7;
            name = splitted[1];
            int.TryParse(splitted[3], out time1);
            int.TryParse(splitted[4], out time2);
            int.TryParse(splitted[5], out time3);
            int.TryParse(splitted[6], out time4);
            int.TryParse(splitted[7], out time5);
            int.TryParse(splitted[8], out time6);
            int.TryParse(splitted[9], out time7);
            int total = time1 + time2 + time3 + time4 + time5 + time6 + time7;
            _endName.text = string.Format(_endNameString, name);
            _endTimePhase1.text = string.Format(_endTimePhase1String, time1.ToString());
            _endTimePhase2.text = string.Format(_endTimePhase2String, time2.ToString());
            _endTimePhase3.text = string.Format(_endTimePhase3String, time3.ToString());
            _endTimePhase4.text = string.Format(_endTimePhase4String, time4.ToString());
            _endTimePhase5.text = string.Format(_endTimePhase5String, time5.ToString());
            _endTimePhase6.text = string.Format(_endTimePhase6String, time6.ToString());
            _endTimePhase7.text = string.Format(_endTimePhase7String, time7.ToString());
            _endTimeTotal.text = string.Format(_endTimeTotalText, total.ToString());
            Debug.Log("Name is " + name + " Times are " + time1 + " ; " + time2 + " ; " + time3 + " ; " + time4 + " ; " + time5 + " ; " + time6 + " ; " + time7);
        }
    }

    private void StartPhase(int currentPhase)
    {
        if((currentPhase) >= Phases.Count)
        {
            GetEndPhase();
            _endPanel.SetActive(true);

        }
        else
        {
            if(_currentPhaseObject != null)
            {
                _currentPhaseObject.SetActive(false);
            }
            _currentPhaseObject = Phases[currentPhase];
            _currentPhaseObject.SetActive(true);
            _currentPhaseObject.SendMessage("SetUp");
            _startTime = Time.time;
        }
    }
}