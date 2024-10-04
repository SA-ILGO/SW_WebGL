using System.Collections;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject userInfo;
    public GameObject L10button;
    public GameObject R10button;

    public Camera mainCamera;

    private GameObject user;
    private int userNum = 1;

    private Vector3 mainCameraPosition = new Vector3(-5.23000002f, 2.57999992f, -10.7399998f);
    private Vector3 mainCameraPositionStored = new Vector3(-5.23000002f, 2.57999992f, -10.7399998f);
    private Quaternion mainCameraRotation = Quaternion.Euler(13.457f, 0.051f, 0.217f);
    private Vector3 studentCameraPosition = new Vector3(-1.46000004f, 1.03999996f, -8.25f);
    private Vector3 studentCameraPositionStored = new Vector3(-1.46000004f, 1.03999996f, -8.25f);
    private Quaternion studentCameraRotation = Quaternion.Euler(11.04f, -0, 0);

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

        L10button.SetActive(true);
        R10button.SetActive(true);

        mainCameraPosition = mainCameraPositionStored;
        StartCoroutine(TransitionCamera(mainCameraPosition, mainCameraRotation, 56));
    }

    public void Click_BtnInfo()
    {
        userInfo.SetActive(true);
        user = userInfo.transform.GetChild(userNum).gameObject;
        user.SetActive(true);

        L10button.SetActive(false);
        R10button.SetActive(false);

        studentCameraPosition = studentCameraPositionStored;
        StartCoroutine(TransitionCamera(studentCameraPosition, studentCameraRotation, 50));
    }

    public void Click_BtnL()
    {
        if (user != null)
            user.SetActive(false);

        if (userNum == userInfo.transform.childCount - 1)
        {
            userNum = 1;
            studentCameraPosition.x = -1.46f;
        }
        else
        {
            userNum++;
            studentCameraPosition.x -= 1.5f;
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
            userNum = userInfo.transform.childCount-1; 
            studentCameraPosition.x = -1.46f - ((userNum-1) * 1.5f); 
        }
        else
        {
            userNum--; 
            studentCameraPosition.x += 1.5f; 
        }

        user = userInfo.transform.GetChild(userNum).gameObject;
        user.SetActive(true);

        StartCoroutine(TransitionCamera(studentCameraPosition, studentCameraRotation, 50));
    }

    public void Click_BtnL10()
    {
        mainCameraPosition.x -= 8f;

        if (mainCameraPosition.x < -54f) mainCameraPosition.x = -5.43f;
        StartCoroutine(TransitionCamera(mainCameraPosition, mainCameraRotation, 56));
    }

    public void Click_BtnR10()
    {
        mainCameraPosition.x += 8f;

        if (mainCameraPosition.x > -5f) mainCameraPosition.x = -53.43f;
        StartCoroutine(TransitionCamera(mainCameraPosition, mainCameraRotation, 56));
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
