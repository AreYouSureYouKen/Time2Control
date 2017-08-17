using UnityEngine;
using System.Collections;

public class AlignThisAlinea : MonoBehaviour {
    [SerializeField]
    private float _paddingWidth = 2;

    [SerializeField]
    private float _paddingHeight = 10;

    [SerializeField]
    private float _paddingRight = 0;
    [SerializeField]
    private GameObject _contentParent;

    [SerializeField]
    private bool _debug;

    [SerializeField]
    private Transform _canvas;

    private float _currentHeight = 0;

    private float _currentWidth = 0;

    private float _widthToConstrain;

    public IEnumerator Align()
    {
        yield return new WaitForEndOfFrame();
        AlignThis();
    }

    [ContextMenu("Align this Alinea")]
    public void AlignThis()
    {
        _widthToConstrain = _contentParent.GetComponent<RectTransform>().rect.width;
        _currentWidth = _paddingWidth;
        _currentHeight = -_paddingHeight;

        for(int i=0; i < this.transform.childCount; i++)
        {

            GameObject go = this.transform.GetChild(i).gameObject;

            TextCollider tc = go.GetComponent<TextCollider>();
            if(tc != null)
            {
                tc.ExtendCollider();
                tc.SetCanvas(_canvas);
                Rect wordRect = tc.GetRect();
                _currentWidth += (wordRect.width * 0.25f);
                if (_debug) Debug.Log("Found width is " + wordRect.width + " And height " + wordRect.height);
                if ((_currentWidth + _paddingRight + (wordRect.width * 0.25f)) > _widthToConstrain) { _currentWidth = (wordRect.width * 0.25f) + _paddingWidth; _currentHeight -= (wordRect.height * 0.25f) + _paddingHeight; }
                if(_debug)Debug.Log("setting width to " + _currentWidth +  " And height to " + _currentHeight);
                tc.GetComponent<RectTransform>().anchoredPosition = new Vector2(_currentWidth,_currentHeight);
                _currentWidth += (wordRect.width * 0.25f) + _paddingWidth;
            }
        }
    }
}
