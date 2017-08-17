using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WoordZoeker : MonoBehaviour {
    private int _wordsClicked = 0;
    [SerializeField]
    private PhaseControl _phaseControl;
    [SerializeField]
    private AlignThisAlinea _scrollText;
    [SerializeField]
    private GameObject _notificationPanel;

    public void SetUp()
    {
        StartCoroutine(_scrollText.Align());
        _notificationPanel.SetActive(true);
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed");
            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.up);
            if(hit.collider != null)
            {
                hit.collider.gameObject.SetActive(false);
                _wordsClicked++;
                if(_wordsClicked == 15)
                {
                    _phaseControl.UpdatePhase();
                }
                Debug.Log("Clicked" + hit.collider.gameObject.name);
            }
        }
	}
}
