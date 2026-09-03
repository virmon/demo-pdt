using UnityEngine;

public class UIObjFollower : MonoBehaviour
{
    private RectTransform myRectTfm;
    private Vector3 offset;
    private Camera mainCam;

    public Transform targetTfm;
    public float offsetPosX;
    public float offsetPosY;

    private void Start()
    {
        myRectTfm = GetComponent<RectTransform>();
        offset = new Vector3(offsetPosX, offsetPosY, 0);
        mainCam = Camera.main;
    }

    private void LateUpdate()
    {
        myRectTfm.position = RectTransformUtility.WorldToScreenPoint(mainCam, targetTfm.position + offset);
    }
}
