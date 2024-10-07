using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[System.Serializable]
public class Student
{
    public string ID;
    public string Name;
    public string NUID;
    public string Major;
    public int Membership;
}

[System.Serializable]
public class Line
{
    public string NUID;
    public string Time;
    public string WaitingNumber;
    public string WaitingRealTimeNumber;
    public string WaitingSpot;
    public int ReceiptConfirmation;
    public string WaitingFinishedTime;
    public string ID;
}

[System.Serializable]
public class ResGetUsers
{
    public List<Student> students;
    public List<Line> lines;
}


public class HTTPManager : MonoBehaviour
{
    public static HTTPManager instance;
    public UnityAction<List<Student>> onGetStudentInfo;
    public UnityAction<List<Line>> onGetLineInfo;

    public List<Student> studentList;
    
    private string _host = "http://localhost";
    private int _port = 3000;

    private void Awake()
    {
        instance = this;
    }

    public void RequestUsers()
    {
        StartCoroutine(RequestUsersImpl());
    }

    private IEnumerator RequestUsersImpl()
    {
        var url = string.Format("{0}:{1}{2}", _host, _port, "/api/data");
        Debug.Log(url);
        var www = new UnityWebRequest(url, "GET");
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("WEB Connected");
            var json = www.downloadHandler.text;

            // JSON 문자열을 ResGetUsers 객체로 변환
            var res_get_users = JsonUtility.FromJson<ResGetUsers>(json);

            if (res_get_users.students != null)
            {
                studentList = res_get_users.students; // 학생 리스트 저장
                onGetStudentInfo?.Invoke(studentList); // 이벤트 호출
            }
            if (res_get_users.lines != null)
            {
                List<Line> waitingUsers = res_get_users.lines.FindAll(line => line.ReceiptConfirmation == 0);
                onGetLineInfo?.Invoke(waitingUsers);
            }
        }
        else
        {
            Debug.Log("ERROR: " + www.error);
        }
    }

    public List<Student> GetStudents() // 학생 리스트 반환 메서드
    {
        return studentList;
    }
}
