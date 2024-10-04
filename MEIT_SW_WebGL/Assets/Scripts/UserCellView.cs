using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UserCellView : MonoBehaviour
{
    public TMP_Text userId;
    public TMP_Text userMajor;
    public TMP_Text userName;
    public TMP_Text userWaitingNumber;

    // User 타입을 사용하도록 수정
    public void Init(Student student, Line line)
    {
        // User 클래스의 속성 값들을 가져와서 텍스트 필드에 설정
        userId.text = student.ID;
        userMajor.text = student.Major;
        userName.text = student.Name;
        userWaitingNumber.text = line.WaitingNumber.ToString();
    }

    public void SelectUser(Student student, Line line)
    {
        if (line != null)
        {
            line.ID = student.ID;
            Init(student, line);
        }
    }
}
