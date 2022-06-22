using UnityEngine;

public class SingletonObject<T> : MonoBehaviour where T : SingletonObject<T> {
    public static T INSTANCE; // 자기 자신을 담는 변수

    protected virtual void Awake() { // 0 프레임에 시작
        DontDestroyOnLoad(this.gameObject); // 새로운 씬으로 변경이 되어도 제거 되지 않도록 한다. 
        if (INSTANCE != null) { // SingletonObject가 이미 만들어 저 있을 경우 자기 자신을 파괴
            Destroy(this.gameObject);
        } else {  // 만들어 져있지 않을 경우 자기 자신을 할당한다.
            INSTANCE = GetComponent<T>();
        }
    }
}