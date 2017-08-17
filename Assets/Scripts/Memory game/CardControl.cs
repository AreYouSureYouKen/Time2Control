using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CardControl : MonoBehaviour {
    MemoryCard _firstSelected;
    MemoryCard _secondSelected;

    [SerializeField]
    private GameObject _memoryCardPanel;
    [SerializeField]
    private Text _notificationText;
    [SerializeField]
    private GameObject _StartPanel;
    [SerializeField]
    private GameObject _twentyTryPanel;
    [SerializeField]
    private string _twentyTryText;
    [SerializeField]
    private PhaseControl _phaseControl;

    [SerializeField]
    private List<MemoryCard> allCards;

    [SerializeField]
    private List<GameObject> _cardPositions;
    private List<GameObject> _cardPositionsBackup = new List<GameObject>();

    private int attempts = 0;
    private bool changed = false;

    [ContextMenu("Set Up")]
    public void SetUp()
    {
        attempts = 0;
        if (_cardPositionsBackup.Count == 0) _cardPositionsBackup.AddRange(_cardPositions);
        _cardPositions.Clear();
        _cardPositions.AddRange(_cardPositionsBackup);
        _memoryCardPanel.SetActive(true);
        _StartPanel.SetActive(true);
        _twentyTryPanel.SetActive(false);

            foreach (MemoryCard mc in allCards)
            {
                int cardindex = Random.Range(0, (_cardPositions.Count - 1));
                mc.transform.position = _cardPositions[cardindex].transform.position;
                _cardPositions.RemoveAt(cardindex);

            }



    }

    private void SetSpecificColor()
    {
        foreach (MemoryCard mc in allCards)
        {
            mc.SetSpecificColor();
        }
    }

    public void clickedCard(MemoryCard card)
    {
        if(_firstSelected == null)
        {
            _firstSelected = card;
        }
        else
        {
            _secondSelected = card;
            attempts++;
            if (_secondSelected.isPair(_firstSelected))
            {
                StartCoroutine(RemoveFromField()); 
            }
            else
            {
                StartCoroutine(RotateBack());
            }
        }
        if(attempts >= 20 && !changed)
        {
            SetSpecificColor();
            changed = true;
            _memoryCardPanel.SetActive(true);
            _StartPanel.SetActive(false);
            _twentyTryPanel.SetActive(true);
        }
    }

    private IEnumerator RotateBack()
    {
        MemoryCard card1 = _firstSelected;
        MemoryCard card2 = _secondSelected;
        _firstSelected = null;
        _secondSelected = null;
        yield return new WaitForSeconds(2f);
        card1.RotateToBack();
        card2.RotateToBack();
        
    }

    private IEnumerator RemoveFromField()
    {
        MemoryCard card1 = _firstSelected;
        MemoryCard card2 = _secondSelected;
        allCards.Remove(_firstSelected);
        allCards.Remove(_secondSelected);
        _firstSelected = null;
        _secondSelected = null;
        yield return new WaitForSeconds(2f);
        card1.gameObject.SetActive(false);
        card2.gameObject.SetActive(false);
        if (allCards.Count == 0) Completed();
    }

    private void Completed()
    {
        _phaseControl.UpdatePhase();
    }
}
