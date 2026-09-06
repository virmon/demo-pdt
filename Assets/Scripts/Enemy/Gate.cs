using UnityEngine;
using DG.Tweening;

public class Gate : MonoBehaviour
{
    private bool inPlayerFlag = false;
    Transform playerPos;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        SearchPlayer();
    }

    private void SearchPlayer()
    {
        float distanceX = Mathf.Abs(playerPos.position.x - transform.position.x);
        if (distanceX <= 1 && inPlayerFlag == false)
        {
            inPlayerFlag = true;
            playerPos.transform.DOMoveX(-2, 0.1f).SetRelative().SetLink(playerPos.gameObject);
        }
        else
        {
            inPlayerFlag = false;
        }
    }
}
