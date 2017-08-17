using UnityEngine;
using System.Collections;

[System.Serializable]
public class MemoryCard : MonoBehaviour {

    [SerializeField]
    private MemoryCard _other;
    [SerializeField]
    private float _time = 1.5f;

    [SerializeField]
    CardControl _cardControl;

    [SerializeField]
    Transform BackRotation;
    [SerializeField]
    Transform FrontRotation;

    [SerializeField]
    private GameObject _backSide;

    [SerializeField]
    private Color _startColor = Color.white;

    [SerializeField]
    private Color _memoryColor;


    private void OnMouseDown()
    {
        _cardControl.clickedCard(this);
        RotateToText();
    }

    public void SetStartColor()
    {
        _backSide.GetComponent<Renderer>().material.color = _startColor;
    }

    public void SetSpecificColor()
    {
        _backSide.GetComponent<Renderer>().material.color = _memoryColor;
    }


    [ContextMenu("Rotate to front")]
    public void RotateToText()
    {
        StartCoroutine(RotateThis(FrontRotation.rotation, _time));
    }
    [ContextMenu("Rotate to back")]
    public void RotateToBack()
    {
        StartCoroutine(RotateThis(BackRotation.rotation, _time));
    }

    private IEnumerator RotateThis(Quaternion targetRotation, float time)
    {
        Quaternion startRot = this.transform.rotation;
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRot, targetRotation, (elapsedTime / time));
            yield return new WaitForEndOfFrame();
        }
    }

    public bool isPair(MemoryCard other)
    {
        return _other.Equals(other);
    }

}
