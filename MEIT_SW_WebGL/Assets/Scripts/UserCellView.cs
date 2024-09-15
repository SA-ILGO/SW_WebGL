using TMPro;
using UnityEngine;

public class UserCellView : MonoBehaviour
{
    public TMP_Text userId;
    public TMP_Text userMajor;
    public TMP_Text userName;
    public TMP_Text userTime;

    // User Ÿ���� ����ϵ��� ����
    public void Init(User user)
    {
        // User Ŭ������ �Ӽ� ������ �����ͼ� �ؽ�Ʈ �ʵ忡 ����
        userId.text = user.ID;
        userMajor.text = user.Major;
        userName.text = user.Name;
        userTime.text = user.Time;
    }
}
