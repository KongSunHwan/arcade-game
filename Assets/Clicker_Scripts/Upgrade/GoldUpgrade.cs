using UnityEngine;

public abstract class GoldUpgrade : MonoBehaviour, IUpgradeAble
{
    protected Gold price; // 업그레이드 비용
    protected Gold increase; // 업그레이드 증가량

    public RenewText priceText; // 비용 Text 
    public RenewText increaseText; // 증가량 Text

    public abstract void TryUpgrade(); // abstract로 반드시 자식 클레스에서 인터페이스 구현 하도록한다.

    protected abstract void SuccessUpgrade();
    protected abstract void FailUpgrade();

    protected virtual void RenewText() {
        priceText.Renew(price.GetGold().ToString()); // priceText를 갱신한다.
        increaseText.Renew(increase.GetGold().ToString()); // increaseText를 갱신하다.
    }
}
