using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimpleSumControl : MonoBehaviour {
    [SerializeField]
    private float _waitTime = 60;
    [SerializeField]
    private int _numberOfIncreases = 6;
    [SerializeField]
    private PhaseControl _phaseControl;
    private int _timesIncreased = 2;

    private int _activeBlock = 1;

    [SerializeField]
    private SumBlock _sumblock1;
    [SerializeField]
    private SumBlock _sumblock2;
    [SerializeField]
    private GameObject _notificationPanel;
    [SerializeField]
    private Text _notificationText;
    [SerializeField]
    private string _notificationString = "Tijd om jouw geheugen weer een nieuwe boost te geven! Dit kun je doen door snel een aantal simpele sommen te maken.";
    [SerializeField]
    private string _lateString = "Helaas, je was niet snel genoeg. Probeer het nog een keer!";

    private Coroutine Waiter;

    public void PressedOK()
    {
        Waiter = StartCoroutine(WaitTime(_waitTime * _timesIncreased));
        _notificationPanel.SetActive(false);
    }

    [ContextMenu("Set up")]
    public void SetUp()
    {
        _activeBlock = 1;
        _timesIncreased = 2;
        _sumblock2.gameObject.SetActive(false);
        _sumblock1.gameObject.SetActive(true);
        _sumblock1.ResetAnswers();
        _notificationText.text = _notificationString;
        _notificationPanel.SetActive(true);
    }


    private IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);
        switch (_activeBlock)
        {
            case 1:
                    _activeBlock = 2;
                    _timesIncreased++;
                    _sumblock1.gameObject.SetActive(false);
                    _sumblock2.gameObject.SetActive(true);
                    _sumblock2.ResetAnswers();
                _notificationText.text = _lateString;
                _notificationPanel.SetActive(true);
                break;
            case 2:
                    _activeBlock = 1;
                    _timesIncreased++;
                    _sumblock2.gameObject.SetActive(false);
                    _sumblock1.gameObject.SetActive(true);
                    _sumblock1.ResetAnswers();
                _notificationText.text = _lateString;
                _notificationPanel.SetActive(true);
                break;
        }
    }

    public void CheckAnswers()
    {
        StopCoroutine(Waiter);
        if(_timesIncreased == _numberOfIncreases)
        {
            _phaseControl.UpdatePhase();
        }
        int correctanswers = 0;
        switch (_activeBlock)
        {
            case 1:
                correctanswers = _sumblock1.CheckAnswers();
                if (correctanswers >= 43)
                {
                    _phaseControl.UpdatePhase();
                }
                else
                {
                    _activeBlock = 2;
                    _timesIncreased++;
                    _sumblock1.gameObject.SetActive(false);
                    _sumblock2.gameObject.SetActive(true);
                    _sumblock2.ResetAnswers();
                    _notificationText.text = "Helaas je had er " + correctanswers + " goed van de 45. Probeer het nog een keer! ";
                    _notificationPanel.SetActive(true);
                }
                break;
            case 2:
                correctanswers = _sumblock2.CheckAnswers();
                if (correctanswers >= 43)
                {
                    _phaseControl.UpdatePhase();
                }
                else
                {
                    _activeBlock = 1;
                    _timesIncreased++;
                    _sumblock2.gameObject.SetActive(false);
                    _sumblock1.gameObject.SetActive(true);
                    _sumblock1.ResetAnswers();
                    _notificationText.text = "Helaas je had er " + correctanswers + " goed van de 45. Probeer het nog een keer! ";
                    _notificationPanel.SetActive(true);
                }
                break;
        }
        
    }
}
