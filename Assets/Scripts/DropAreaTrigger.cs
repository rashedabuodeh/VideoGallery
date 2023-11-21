using UnityEngine;
using UnityEngine.UI;

public class DropAreaTrigger : MonoBehaviour
{
    #region Private Members 
    private Image image;
    private Color OnDropColor;
    #endregion

    #region Unity CallBacks 
    private void Awake()
    {
        image = GetComponent<Image>();
        OnDropColor = Color.gray;
        OnDropColor.a = 0.5f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Video"))
        {
            image.color = OnDropColor;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Video"))
        {
            image.color = Color.white;
        }
    }
    #endregion

}
