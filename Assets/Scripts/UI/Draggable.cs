using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IEndDragHandler
{
    #region Private Members 
    private Vector3 _initialPos;
    private Vector2 _mouseOffset;
    private bool _reached;
    [SerializeField] private float _mouseOffsetInX; // to make sure that the mouse is dragging it exactly from the middle 
    [SerializeField] private int _index;
    #endregion

    #region Unity CallBacks 
    private void Awake()
    {
        _initialPos = transform.position;
        _reached = false;
        _mouseOffset = Vector3.zero;
        _mouseOffset.x = _mouseOffsetInX;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DropArea"))
            _reached = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DropArea"))
            _reached = false;
    }

    #endregion

    #region Private Methods

    [ContextMenu("SetOffset")]
    private void SetOffset()
    {
        _mouseOffset.x = _mouseOffsetInX;
    }
    #endregion

    #region public Methods

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + _mouseOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_reached)
        {
            Destroy(gameObject);
            // send the video index and open the 2nd app
            AppManager.Instance.OnDropVideo(_index);
        }
        else
            transform.position = _initialPos;

    }

    #endregion
}
