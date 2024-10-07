using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System;

public class TestMain : MonoBehaviour
{
    public static TestMain instance;

    public GameObject userCellviewPrefab;
    public RectTransform UserInfo;

    public GameObject student;
    private Vector3 studentPosition = new Vector3(-2, 0.4f, -5.72f);
    private Quaternion studentRotation = Quaternion.Euler(0, 0, 0);

    public float spacing;

    public GameObject studentList;
    public GameObject studentInfoList;

    private void Awake()
    {
        instance = this;
        #if UNITY_WEBGL && !UNITY_EDITOR
                WebGLInput.captureAllKeyboardInput = false;
        #endif
    }


    private void Start()
    {
        HTTPManager.instance.onGetLineInfo = (users) =>
        {
            int index = 0; // 사용자 인덱스

            if (studentList.transform.childCount > 0) 
            {
                foreach (Transform child in studentList.transform)
                {
                    Destroy(child.gameObject);
                }
                for (int i = studentInfoList.transform.childCount - 1; i > 0; i--)
                {
                    Destroy(studentInfoList.transform.GetChild(i).gameObject);
                }
            }
            

            foreach (var user in users)
            {
                // 사용자 셀뷰 생성
                var go = Instantiate(userCellviewPrefab, UserInfo);
                var cellview = go.GetComponent<UserCellView>();

                var studentData = HTTPManager.instance.GetStudents()?.FirstOrDefault(s => s.NUID == user.NUID);
                cellview.SelectUser(studentData, user);

                RectTransform rectTransform = go.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(225f, -19f);

                go.SetActive(false); 

                // student 오브젝트 생성 및 위치 설정
                var waitingUser = Instantiate(student, studentList.transform);

                waitingUser.transform.position = studentPosition + new Vector3(index * -1.5f, 0, 0);
                waitingUser.transform.rotation = studentRotation;
                waitingUser.transform.GetChild(0).GetComponent<TextMeshPro>().text = user.ID;

                index++;
            }
        };

        HTTPManager.instance.RequestUsers();
    }

}
