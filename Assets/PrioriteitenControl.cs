using UnityEngine;
using System.Collections.Generic;

public class PrioriteitenControl : MonoBehaviour {
    [SerializeField]
    private PhaseControl _phaseControl;
    [SerializeField]
    private GameObject _startPanel;
    [SerializeField]
    private GameObject _notificationPanel;
    [SerializeField]
    private GameObject _mainPanel;
    [SerializeField]
    private List<MindmapWordTarget> _targets;

	public void SetUp()
    {
        _mainPanel.SetActive(false);
        _notificationPanel.SetActive(false);
        _startPanel.SetActive(true);
    }

    public void PressedOK()
    {
        _startPanel.SetActive(false);
        _mainPanel.SetActive(true);
        _notificationPanel.SetActive(true);
    }

    public void CheckWords()
    {
        bool allTrue = true;
        foreach (MindmapWordTarget mwt in _targets)
        {
            if (!mwt.CheckWords()) { allTrue = false; }
        }
        if(allTrue)
        {
            _phaseControl.UpdatePhase();
        }
    }
}
