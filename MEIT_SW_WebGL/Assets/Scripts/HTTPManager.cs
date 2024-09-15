using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[System.Serializable]
public class User
{
    //public string Alphabet;
    public string ID;
    public string Major;
    public string Name;
    public string Time;
    //public bool Receipt;
}

[System.Serializable]
public class ResGetUsers
{
    public int status_code;
    public List<User> users;
}

public class HTTPManager : MonoBehaviour
{
    public static HTTPManager instance;
    public UnityAction<List<User>> onGetUsers;

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
        var url = string.Format("{0}:{1}{2}", _host, _port, "/users/getUsers");
        Debug.Log(url);
        var www = new UnityWebRequest(url, "GET");
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            var json = www.downloadHandler.text;

            // JSON 문자열을 ResGetUsers 객체로 변환
            var res_get_users = JsonUtility.FromJson<ResGetUsers>(json);

            if (res_get_users.status_code == 200)
            {
                onGetUsers?.Invoke(res_get_users.users);
            }
        }
        else
        {
            Debug.Log("ERROR: " + www.error);
        }
    }
}
