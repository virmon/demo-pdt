using UnityEngine;
using System.Collections;

public class EnemyBoss : MonoBehaviour
{
    public GameObject bullet;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        while(true)
        {
            yield return new WaitForSeconds(8f);

            if (gameManager.noActionFlag == false)
            {
                Instantiate(bullet, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
                Instantiate(bullet, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
                Instantiate(bullet, transform.position, Quaternion.identity);
            }
        }
    }
}
