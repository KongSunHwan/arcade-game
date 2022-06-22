public struct Gold {
    private int currentGold; // 현재 골드

    public void SetGold(int value) { // 골드 설정
        currentGold = value;
    }
    public int GetGold() {
        return currentGold; // 골드 확인
    }
}