using UnityEngine;
using UnityEngine.UI;

public class DropAreaTrigger : MonoBehaviour
{
    #region Private Members 
    private Image _image;
    private Color _OnDropColor;
    #endregion

    #region Unity CallBacks 
    private void Awake()
    {
        _image = GetComponent<Image>();
        _OnDropColor = Color.gray;
        _OnDropColor.a = 0.5f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Video"))
        {
            _image.color = _OnDropColor;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Video"))
        {
            _image.color = Color.white;
        }
    }
    #endregion

}
