using UnityEngine;

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
            int index = 0; // 사용자 인덱스
            foreach (var user in users)
            {
                // 사용자 셀뷰 생성
                var go = Instantiate(userCellviewPrefab, UserInfo);
                var cellview = go.GetComponent<UserCellView>();
                cellview.Init(user);

                RectTransform rectTransform = go.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(261, 0);

                go.SetActive(true); // 셀뷰를 활성화

                // student 오브젝트 생성 및 위치 설정
                var waiting = Instantiate(student);

                waiting.transform.position = studentPosition + new Vector3(index * -1, 0, 0);
                waiting.transform.rotation = studentRotation;

                index++; // 인덱스 증가
            }
        };

        HTTPManager.instance.RequestUsers();
    }
}
