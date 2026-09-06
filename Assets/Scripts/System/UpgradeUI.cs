using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UpgradeUI : MonoBehaviour
{
    public GameObject startButton;
    public CanvasGroup canvas;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.noActionFlag = true;
        Invoke(nameof(Selected), 1f);
    }

    private void Selected()
    {
        EventSystem.current.SetSelectedGameObject(startButton);
    }

    public void OnShot()
    {
        gameManager.shotUpgradeLV++;
        Common();
    }

    public void OnUnit()
    {
        gameManager.unitUpgradeLV++;
        Common();
    }
    
    public void OnBomb()
    {
        gameManager.bombUpgradeLV++;
        Common();
    }

    private void Common()
    {
        gameManager.noActionFlag = false;

        canvas.DOFade(0, 0.5f).SetLink(gameObject);
        EventSystem.current.SetSelectedGameObject(null);
        Destroy(gameObject, 0.5f);
    }
}
