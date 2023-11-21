using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IEndDragHandler
{
    #region Private Members 
    private Vector3 initialPos;
    private Vector2 mouseOffset;
    private bool reached;
    [SerializeField] private float mouseOffsetInX;
    #endregion

    #region Unity CallBacks 
    private void Awake()
    {
        initialPos = transform.position;
        reached = false;
        mouseOffset = Vector3.zero;
        mouseOffset.x = mouseOffsetInX;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DropArea"))
            reached = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DropArea"))
            reached = false;
    }

    #endregion

    #region Private Methods

    [ContextMenu("SetOffset")]
    private void SetOffset()
    {
        mouseOffset.x = mouseOffsetInX;
    }
    #endregion

    #region public Methods

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + mouseOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (reached)
        {
            // should send the video index and open the 2nd app
        }
        else
            transform.position = initialPos;

    }

    #endregion
}
