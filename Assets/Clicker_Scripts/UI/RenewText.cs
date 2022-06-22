using UnityEngine;
using UnityEngine.UI;

public class RenewText : MonoBehaviour
{
    Text _text;
    private void Awake() {
        _text = this.gameObject.GetComponent<Text>(); // _text 변수에 할당
    }
    public void Renew(string str) {
        _text.text = str; // Text를 str로 갱신
    }
}
