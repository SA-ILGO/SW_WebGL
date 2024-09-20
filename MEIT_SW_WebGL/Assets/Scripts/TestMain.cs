using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestMain : MonoBehaviour
{
    public static TestMain instance;

    public GameObject userCellviewPrefab;
    public RectTransform UserInfo;

    public GameObject student;
    private Vector3 studentPosition = new Vector3(-2, 0.4f, -5.72f);
    private Quaternion studentRotation = Quaternion.Euler(0, -30, 0);

    public float spacing;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        HTTPManager.instance.onGetUsers = (users) =>
        {
            int index = 0; // ����� �ε���
            foreach (var user in users)
            {
                // ����� ���� ����
                var go = Instantiate(userCellviewPrefab, UserInfo);
                var cellview = go.GetComponent<UserCellView>();
                cellview.Init(user);

                RectTransform rectTransform = go.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(225f, -19f);

                go.SetActive(false); 

                // student ������Ʈ ���� �� ��ġ ����
                var waiting = Instantiate(student);

                waiting.transform.position = studentPosition + new Vector3(index * -1, 0, 0);
                waiting.transform.rotation = studentRotation;
                waiting.transform.GetChild(0).GetComponent<TextMeshPro>().text = user.ID;

                index++;
            }
        };

        HTTPManager.instance.RequestUsers();
    }
}
