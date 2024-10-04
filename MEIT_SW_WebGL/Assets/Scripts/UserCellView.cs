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

    // User Ÿ���� ����ϵ��� ����
    public void Init(Student student, Line line)
    {
        // User Ŭ������ �Ӽ� ������ �����ͼ� �ؽ�Ʈ �ʵ忡 ����
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
