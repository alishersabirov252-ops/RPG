using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiManager : MonoBehaviour
{
    [SerializeField] private  CanvasGroup menuBar;
    private bool isMenuActive;
    [SerializeField] private  CanvasGroup statsMenu;
    [SerializeField] private CanvasGroup skillsMenu;
    [SerializeField] private CanvasGroup questMenu;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private Sprite closeSprite;
    [SerializeField] private Image menuToggleImage;



    public void ToggleMenu(CanvasGroup target)
    {
        SetMenuState(statsMenu, false);
        SetMenuState(skillsMenu, false);
        SetMenuState(questMenu, false);

        SetMenuState(target, true);
        



    }
    

    public void ToggleMainMenu()
    {
        isMenuActive = !isMenuActive;
        SetMenuState(menuBar, isMenuActive);
        menuToggleImage.sprite = isMenuActive ? closeSprite : openSprite;

        SetMenuState(statsMenu, false);
        SetMenuState(skillsMenu, false);
        SetMenuState(questMenu, false);


        EventSystem.current.SetSelectedGameObject(null);


    }
    
    private void SetMenuState(CanvasGroup group, bool isActive)
    {
        group.alpha = isActive ? 1 : 0;
        group.interactable = isActive;
        group.blocksRaycasts = isActive;

    }
}
