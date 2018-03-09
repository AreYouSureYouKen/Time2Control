using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MindMapControl : MonoBehaviour {
    [SerializeField]
    private PhaseControl _phaseControl;


    [SerializeField]
    private List<MindmapWordTarget> _targets;

    [SerializeField]
    private List<MindmapWordTarget> _firstTargets;
    [SerializeField]
    private List<MindmapWordTarget> _secondTargets;
    [SerializeField]
    private List<MindmapWordTarget> _thirdTargets;
    [SerializeField]
    private GameObject _startPanel;

    private int timesControlled = 0;

    [SerializeField]
    private AlignThisAlinea[] alineas;
    [SerializeField]

    [ContextMenu("Set Up")]
    void SetUp()
    {
        timesControlled = 0;
        _startPanel.SetActive(true);
        StartCoroutine(InitializeText());
    }


	// Use this for initialization
    [ContextMenu ("Spawn text")]
    public IEnumerator InitializeText()
    {
        yield return new WaitForEndOfFrame();
        foreach(AlignThisAlinea ata in alineas)
        {
            ata.AlignThis();
        }
    }

    public void controlAllTargets()
    {
        int occupied=0;
        int total=0;
        foreach(MindmapWordTarget mwt in _targets)
        {
            total++;
            if (mwt.isOccupied()) occupied++;
        }
        if ((total /2) > occupied) return;


        timesControlled++;
        bool allTrue = true;
        foreach (MindmapWordTarget mwt in _targets)
        {
            if(!mwt.CheckWords()) { allTrue = false; }
        }
        Debug.Log(allTrue);
        if (!allTrue)
        {
            if (timesControlled == 1)
            {
                foreach (MindmapWordTarget mwt in _firstTargets)
                {
                    mwt.AutoComplete();
                }
            }
            if (timesControlled == 2)
            {
                foreach (MindmapWordTarget mwt in _secondTargets)
                {
                    mwt.AutoComplete();
                }
            }
            if (timesControlled == 3)
            {
                foreach(MindmapWordTarget mwt in _thirdTargets)
                {
                    mwt.AutoComplete();
                }
            }
            { 
}
        }
        if(allTrue)
        {
            _phaseControl.UpdatePhase();
        }
    }
}
