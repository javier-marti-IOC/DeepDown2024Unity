using TMPro;
using UnityEngine;

/**
 * Component per canviar el color del text d'un botó i reproduïr un só quan es fa mouseover o es clica
 */
public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color hoverColor;

    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponentInChildren<TMP_Text>();
        _text.color = defaultColor;
    }

    // ALERTA! no es criden automàticament quan s'utilitza només el nou InputSystem
    public void OnMouseEnter()
    {
        _text.color = hoverColor;
        AudioManager.Instance.PlayHoverClip();
    }

    public void OnMouseExit()
    {
        _text.color = defaultColor;
    }

    public void OnMouseDown()
    {
        AudioManager.Instance.PlaySelectClip();
    }
}