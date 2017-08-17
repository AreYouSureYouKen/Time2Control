using UnityEngine;
using System.Collections;

public class ScrollText : MonoBehaviour {
    [SerializeField]
    private float scrollSpeed = 50f;

    [SerializeField]
    private float _cutoffvalue;
    [SerializeField]
    private float _speedupValue;
    private float timesIncreased = 0;

    private RectTransform rect;
	// Use this for initialization
	void Start () {
        rect = this.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        rect.anchoredPosition += Vector2.left * (Time.deltaTime * scrollSpeed+(_speedupValue * timesIncreased));
        if (rect.anchoredPosition.x < _cutoffvalue) { Debug.Log("resetting"); rect.anchoredPosition = new Vector2(_cutoffvalue * -1, 0); }
	}
}
