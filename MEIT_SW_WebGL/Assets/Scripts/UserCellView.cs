using TMPro;
using UnityEngine;

public class UserCellView : MonoBehaviour
{
    public TMP_Text userId;
    public TMP_Text userMajor;
    public TMP_Text userName;
    public TMP_Text userTime;

    // User 타입을 사용하도록 수정
    public void Init(User user)
    {
        // User 클래스의 속성 값들을 가져와서 텍스트 필드에 설정
        userId.text = user.ID;
        userMajor.text = user.Major;
        userName.text = user.Name;
        userTime.text = user.Time;
    }
}
