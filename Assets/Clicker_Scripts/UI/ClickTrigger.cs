using UnityEngine;

public class ClickTrigger : MonoBehaviour
{
    public void EnterTrigger() {
        Player.INSTANCE.ClickAct(); // Player ClickAct() 호출한다.
    }
}
