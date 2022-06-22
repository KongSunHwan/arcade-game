    public class IncreaseClickGold : GoldUpgrade {

        private void Start() {
            price.SetGold(5); // 초기값
            increase.SetGold(10); // 초기값
            RenewText(); // 텍스트갱신
        }

        public override void TryUpgrade() {
            if(Player.INSTANCE.gold.GetGold() >= price.GetGold()) { // 플레이어가 가진 돈이 이상 일 경우 성공
                SuccessUpgrade();
            } else {
                FailUpgrade();
            }
        }

        protected override void SuccessUpgrade() { // 성공시
            Player.INSTANCE.gold.SetGold(Player.INSTANCE.gold.GetGold() - price.GetGold()); // 골드 소모
            Player.INSTANCE.increaseGold.SetGold(Player.INSTANCE.increaseGold.GetGold() + increase.GetGold()); // 골드 증가량 추가
            IncreaseFormula(); // 
            RenewText(); // 텍스트 갱신
        
        }

        protected override void FailUpgrade() { // 실패시
        }

        private void IncreaseFormula() { // 공식
            int p = price.GetGold() * 100 + 100; 
            int i = increase.GetGold() * 2 + 1; 
            price.SetGold(p); // 가격 갱신
            increase.SetGold(i); //증가량 갱신
        }
    }
