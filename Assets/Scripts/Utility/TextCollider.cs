using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ContentSizeFitter))]
public class TextCollider : MonoBehaviour, IDragHandler, IBeginDragHandler , IEndDragHandler{
    private Vector3 _beginPos = Vector3.zero;
    private Rect _returnRect;
    private bool moving;
    [SerializeField]
    private Transform _canvas;
    private Transform _parent;
    [SerializeField]
    private bool _acceptDrag = true;

    private Text _thisText;
	// Use this for initialization
	void Awake () {
	}

    [ContextMenu ("Extend bounds")]
    public void ExtendCollider()
    {
        _returnRect = this.GetComponent<Text>().GetComponent<RectTransform>().rect;
    }

    public Rect GetRect()
    {
        if (_returnRect == null) _returnRect = this.GetText().GetComponent<RectTransform>().rect;
        return _returnRect;
    }

    public void SetCanvas(Transform canv)
    {
        _canvas = canv;
    }

    public Text GetText()
    {
        if (_thisText == null) _thisText = this.GetComponent<Text>();
        return _thisText;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_acceptDrag)
        {
            this.transform.position = new Vector3(eventData.position.x, eventData.position.y, 0);
            GetText().raycastTarget = false;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_acceptDrag)
        {
            if (_beginPos.Equals(Vector3.zero)) _beginPos = this.transform.localPosition;
            this.transform.rotation = Quaternion.identity;
            if (_parent == null) _parent = this.transform.parent;
            this.transform.SetParent(_canvas, true);
        }
    }

    public void AutoComplete(GameObject newPos)
    {
        this.transform.position = newPos.transform.position;
        this.transform.rotation = newPos.transform.rotation;
        this.transform.SetParent(newPos.transform, true);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_acceptDrag)
        {
            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.up);
            GameObject endDragObject = null;
            if (hit.collider != null)
            {
                endDragObject = hit.transform.gameObject;
            }

            if (endDragObject == null || endDragObject.GetComponent<MindmapWordTarget>() == null || endDragObject.GetComponent<MindmapWordTarget>().isOccupied())
            {
                this.transform.SetParent(_parent, true);
                StartCoroutine(MoveToOrigin(_beginPos, 0.5f));
                GetText().raycastTarget = true;
            }
            else
            {
                this.transform.position = endDragObject.transform.position;
                this.transform.rotation = endDragObject.transform.rotation;
                this.transform.SetParent(endDragObject.transform, true);
                GetText().raycastTarget = true;
            }
        }
    }

    private IEnumerator MoveToOrigin(Vector3 newpos, float time)
    {
        float elapsedTime=0;
        Vector3 startingPos = transform.localPosition;
        while(elapsedTime <= time)
        {
            transform.localPosition = Vector3.Lerp(startingPos, newpos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localRotation = Quaternion.identity;
        transform.localPosition = _beginPos;
    }

    public void ResetToStart()
    {
        GetText().raycastTarget = true;
        this.transform.SetParent(_parent, true);
        StartCoroutine(MoveToOrigin(_beginPos, 0.5f));
    }
}
