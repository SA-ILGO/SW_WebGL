using System.Collections;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject userInfo;
    public Camera mainCamera;

    private GameObject user;
    private int userNum = 1;

    private Vector3 mainCameraPosition = new Vector3(5, 6, -10);
    private Quaternion mainCameraRotation = Quaternion.Euler(30, -30, 0);
    private Vector3 studentCameraPosition = new Vector3(-0.8f, 1.41f, -7.33f);
    private Quaternion studentCameraRotation = Quaternion.Euler(17.7f, -24.54f, 0);

    public float transitionSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Click_BtnX()
    {
        userInfo.SetActive(false);
        if (user != null)
            user.SetActive(false);

        StartCoroutine(TransitionCamera(mainCameraPosition, mainCameraRotation, 56));
    }

    public void Click_BtnInfo()
    {
        userInfo.SetActive(true);
        user = userInfo.transform.GetChild(userNum).gameObject;
        user.SetActive(true);

        studentCameraPosition = new Vector3(-0.8f, 1.41f, -7.33f);
        StartCoroutine(TransitionCamera(studentCameraPosition, studentCameraRotation, 50));
    }

    public void Click_BtnL()
    {
        if (user != null)
            user.SetActive(false);

        if (userNum == userInfo.transform.childCount - 1)
        {
            userNum = 1;
            studentCameraPosition.x = -0.8f;
        }
        else
        {
            userNum++;
            studentCameraPosition.x--;
        }
        user = userInfo.transform.GetChild(userNum).gameObject;
        user.SetActive(true);

        StartCoroutine(TransitionCamera(studentCameraPosition, studentCameraRotation, 50));
       
    }

    public void Click_BtnR()
    {
        if (user != null)
            user.SetActive(false);

        if (userNum == 1)
        {
            userNum = userInfo.transform.childCount - 1;
            studentCameraPosition.x -= (userNum - 1);
        }
        else
        {
            userNum--;
            studentCameraPosition.x++;
        }
        user = userInfo.transform.GetChild(userNum).gameObject;
        user.SetActive(true);

        StartCoroutine(TransitionCamera(studentCameraPosition, studentCameraRotation, 50));
    }

    private IEnumerator TransitionCamera(Vector3 targetPosition, Quaternion targetRotation, float targetFOV)
    {
        float elapsedTime = 0;
        Vector3 startPosition = mainCamera.transform.position;
        Quaternion startRotation = mainCamera.transform.rotation;
        float startFOV = mainCamera.fieldOfView;

        while (elapsedTime < transitionSpeed)
        {
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / transitionSpeed));
            mainCamera.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, (elapsedTime / transitionSpeed));
            mainCamera.fieldOfView = Mathf.Lerp(startFOV, targetFOV, (elapsedTime / transitionSpeed));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = targetPosition;
        mainCamera.transform.rotation = targetRotation;
        mainCamera.fieldOfView = targetFOV;
    }
}
