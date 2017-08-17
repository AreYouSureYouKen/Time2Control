using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class ClickableTextSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject _clickableText;

    [SerializeField]
    private bool _constrainToWidth;

    [SerializeField]
    private float _paddingWidth = 2;

    [SerializeField]
    private float _paddingHeight = 10;

    [SerializeField]
    private float _paddingRight = 20;

    [SerializeField]
    private GameObject _contentParent;

    [SerializeField]
    private Transform _canvas;

    [SerializeField]
    private GameObject _target;

    private float _currentHeight;

    private float _currentWidth;

    private float _widthToConstrain;
    public IEnumerator SpawnText(string text)
    {
        // get the width to constrain it on from the parent object.
        _widthToConstrain = _contentParent.GetComponent<RectTransform>().rect.width;
        List<TextCollider> wordsList = new List<TextCollider>();
        //split given text in to words
        string[] words = text.Split(' ');
        _currentHeight = -_paddingHeight;
        // create new clickable text object for each word.
        for (int i = 0; i < words.Length; i++)
        {
            GameObject newWord = GameObject.Instantiate(_clickableText);
            TextCollider wordText = newWord.GetComponent<TextCollider>();
            wordText.transform.SetParent(_contentParent.transform, false);
            wordText.SetCanvas(_canvas);
            wordText.GetText().text = words[i];
            wordsList.Add(wordText);
        }
        // wait for the frame to end so that all bounds have been set correctly, otherwise all Rects return 0
        yield return new WaitForEndOfFrame();
        // space the objects and check for width constrain
        foreach (TextCollider word in wordsList)
        {
            word.ExtendCollider();
            Rect wordRect = word.GetRect();
            _currentWidth += (wordRect.width * 0.5f) + _paddingWidth;
            if (_constrainToWidth && (_currentWidth + _paddingRight + (wordRect.width * 0.5)) > _widthToConstrain) { _currentWidth = (wordRect.width * 0.5f) + _paddingWidth; _currentHeight -= (wordRect.height * 0.5f) + _paddingHeight; }
            word.transform.localPosition = new Vector3(_currentWidth, _currentHeight, 0);
            _currentWidth += (wordRect.width * 0.5f);
            if (word.GetText().text.Contains("\n")) { word.GetText().text.Replace("\n", ""); _currentWidth = 0; _currentHeight -= (wordRect.height * 0.5f) + _paddingHeight; }
        }
    }
}