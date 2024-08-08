# 2DArcadeGame
![Intro](https://github.com/user-attachments/assets/d42b4f4c-2de5-4d58-b2ff-cd0d786a7369)

블루프린트로 만든 2D 아케이드 게임
</BR>

목차
---
- [간단한 소개](#간단한-소개)
- [플레이 영상](#플레이-영상)
- [기능 구현](#기능-구현)
  * [플레이어 이동 구현](#플레이어-이동-구현)

## 간단한 소개
블루프린트로만 제작한 단일 스테이지 아케이드 게임입니다.</BR></BR>

플레이어는 캐릭터를 움직여 스테이지 끝에 있는 문에 도착하면 스테이지가 클리어되며 시작지점으로 돌아갑니다.</BR>
몬스터는 총 4종류가 있으며 각 몬스터마다 특징이 있습니다.(예를 들면 선제 공격을 하거나 원거리 공격을 하는 몬스터가 있습니다.)</BR>
플레이어는 총을 쏴서 몬스터를 처치할 수 있으며 기본샷은 1의 데미지를 주고 차지샷은 3의 데미지를 줍니다.</BR>
플레이어의 체력이 다 떨어지거나 용암에 빠진다면 세이브 포인트에서부터 시작하게됩니다.
</BR></BR>


## 플레이 영상
[![Video Label](http://img.youtube.com/vi/9Cq64qgYQcM/0.jpg)](https://youtu.be/9Cq64qgYQcM)
</BR>
👀Link: https://youtu.be/9Cq64qgYQcM</BR>
이미지나 주소 클릭하시면 영상을 보실 수 있습니다. </BR>

## 기능 구현

### [플레이어 이동 구현]
언리얼 엔진5에서 제공하는 Enhanced Input System을 이용하였습니다. </BR>
플레이어는 점프, 이동, 슈팅 총 3가지 행동을 할 수 있으며 행동들을 Input Action을 통해 만들었습니다. </BR></BR>


![InputAction](https://github.com/user-attachments/assets/e97e085c-95f8-4926-a930-2825fa0cbc17)
<div align="center"><strong>생성된 3가지 Input Action</strong></div></BR>

생성한 Input Action들을 Input Mapping Context에 등록하여 3종류의 행동을 키 입력에 연동시킵니다.</BR>
각 행동들은 Input Action에서 설정한 값의 타입에 따라 키를 설정할 수 있습니다.</BR></BR>

![IMC](https://github.com/user-attachments/assets/2663473e-cbc4-40d0-9435-5a668242eaf4)
<div align="center"><strong>Input Mapping Context에 연동된 Input Action</strong></div></BR>

특히 이동관련 키 세팅은 한 개의 키를 기준으로 모디파이어를 두어 쉽게 설정할 수 있습니다.</BR>
오른쪽으로 이동하는 D키를 기준으로 한다면 A키는 반대인 왼쪽으로 움직이므로 모디파이어에서 Negative를 설정해줍니다.</br>
좌우가 아닌 상하로 움직이는 경우 모디파이어를 스위즐 입력 측 값을 YXZ로 두어 X축과 Y축을 바꿔 좌우를 상하로 움직이도록 설정합니다.</BR></BR>

![InputSetting](https://github.com/user-attachments/assets/534ee593-99d4-4186-8ba1-2d1f5aedc3b5)
<div align="center"><strong>BP_Player에서 BeginPlay노드에 컨트롤러 설정하기</strong></div></BR>

Input Mapping Context(IMC)에 등록된 행동들은 몬스터들이 아닌 플레이어만 사용할 행동들이므로 BP_Player의 부모인 BP_BaseCharacter에 설정할 필요가 없습니다.</br>
BP_Player의 BeginPlay노드에 PlayerController에 Enhanced Input System을 등록하여 IMC에 등록된 키 입력을 바탕으로 컨트롤 할 수 있게 해줍니다.</BR>

![IA_MOVE](https://github.com/user-attachments/assets/cb7adb38-00b1-4e8c-bff8-77eed9be0b4b)
<div align="center"><strong>이동 관련 키 입력이 된 경우</strong></div></BR>

W, A, S, D가 입력된 경우 IMC에서 해당 키가 연동된 행동을 찾아 이벤트를 발생시킵니다.</BR>
해당 행동은 IA_Move의 Input Action이므로 IA_Move 이벤트 노드에서 입력값을 처리해줍니다.</br>
Add Movement Input노드를 통해 캐릭터를 앞으로 움직이게 하며 IMC에서 D를 기준으로 모디파이어를 설정해줬기 때문에 X축에 1값만 줘도 입력된 방향으로 나아갑니다.</BR></BR>

![rotation](https://github.com/user-attachments/assets/7168132e-820c-4cbb-a981-ee36ec8aaf58)
<div align="center"><strong>좌우 키 방향에 따른 캐릭터 방향 전환</strong></div></BR>

X축의 값이 1 이상이라면 캐릭터가 움직이는 중 이므로 캐릭터가 올바른 방향을 바라본 채 앞으로 나아가게끔 해야합니다.</br>
캐릭터의 Sprite는 오른쪽을 바라보고 있으므로 D키를 누른다면 방향 전환이 필요 없습니다.</BR>
A키를 누른다면 Sprite와 반대인 왼쪽을 바라봐야하므로 Make Rotator 노드를 통해 Z축을 180도 회전하여 왼쪽을 바라보도록 합니다.</br>

---

### [애니메이션 연동]
애니메이션을 사용하기 위해서는 Flipbook들을 Animation Source에 설정을 해줘야합니다.</br>

![AnimationSource](https://github.com/user-attachments/assets/446ebfcb-6347-4931-8d26-1e3796b97674)
<div align="center"><strong>Animation Source에 설정된 플립북들</strong></div></BR>

Animation Source는 사용할 모든 플립북의 모음으로 Animation Blueprint에서 해당 플립북을 손쉽게 쓸 수 있도록 해줍니다.</br>

![ABP](https://github.com/user-attachments/assets/4f25bc92-b765-4b76-9087-86c58bceea58)
<div align="center"><strong>등록된 플립북을 Animation Blueprint에서 사용하는 모습</strong></div></BR>

Animation Blueprint에서는 캐릭터가 어떤 상황에서 어떤 플립북을 재생할지 설정할 수 있습니다.</br>

![RunToIdle](https://github.com/user-attachments/assets/4ed7e17b-3a29-4dee-bba4-c1a15b1f4f5e)
<div align="center"><strong>Run 애니메이션에서 Idle로 돌아가는 조건</strong></div></BR>

현재 애니메이션에서 다른 애니메이션을 재생하는 조건을 설정해주고 해당 조건을 충족하면 다른 애니메이션을 재생합니다.</br>
여기서 Owning Player은 BP_Player 타입의 변수로 Animation Blueprint가 Init할 때 Get Owning Actor 노드를 통해 가져왔습니다.</br>
캐릭터의 속도가 0보다 크지 않다면 더이상 앞으로 가고 있지 않으므로 Run 애니메이션에서 Idle 애니메이션으로 바꿔서 재생합니다.</br></br>

![OwningActor](https://github.com/user-attachments/assets/3e019e88-0b1d-4713-b909-e32fc40b555b)
<div align="center"><strong>BP_Player에서 사용할 애니메이션 클래스 설정</strong></div></BR>

Animation Blueprint에서 Owning Player을 갖고 오기 위해서는 BP_Player에 해당 애니메이션 클래스를 사용하겠다고 지정해줘야합니다.</br>
Animation Component의 디테일 탭에서 Paper ZD의 Anim Instance Class에 위에서 만든 Animation Blueprint를 설정하여 Ownint Player를 참조할 수 있도록 합니다.</br>

---

### [발사체 구현]
발사체는 플레이어도 쏠 수 있고 특정 몬스터 또한 파이어볼을 뱉으므로 모든 발사체의 부모클래스인 BaseProjectile를 생성합니다.</br>
BP_BaseProjectile은 Actor타입으로 해당 Actor가 발사체 처럼 행동하기 위해서는 ProjectileMovement 컴포넌트가 필요합니다.</br>
ProjectileMovement 컴포넌트는 발사체가 몇의 속도로 날라가고 중력의 영향을 얼마나 받는지 설정하여 Actor가 발사체처럼 작동하도록 합니다.</br>
ProjectileMovement가 작동하기 위해서는 Collision이 필요하므로 구형 발사체를 사용하기 위해 Sphere Collision 컴포넌트를 추가했습니다.</br></br>

![BaseProjectile](https://github.com/user-attachments/assets/4056739c-5e67-4384-b6b1-5909803cf539)
<div align="center"><strong>BaseProjectile에 설정된 컴포넌트와 발사체 설정</strong></div></BR>

플레이어와 몬스터는 발사체에 맞으면 데미지를 받아야하므로 Sphere Collision에 Pawn타입의 객체가 발사체에 부딪힌다면 이벤트를 발생시켜야합니다.</br>
Begin Overlap 노드를 이용하여 충돌할 시 누구와 충돌했는지 알기 위해 BP_BaseCharacter로 캐스팅하고 발사체를 발사한 객체와 맞은 객체가 다르다면 데미지를 적용합니다.</br></br>

![ProjectileDamage](https://github.com/user-attachments/assets/06f2d0fd-135a-40fb-bdd5-f620a20a874d)
<div align="center"><strong>발사체가 Pawn타입의 객체와 부딪힐 시 데미지 주기</strong></div></BR>

여기서 문제점은 발사체에 맞은 대상은 Begin Overlap 노드를 통해 누군지 아는데 발사체를 발사한 주체를 어떻게 아는지 알 수가 없습니다.</br>
해결 방안은 발사체를 생성할 때 사용하는 SpawnActor노드에 Owner 입력칸에 Self(발사체를 발사한 주체)를 넣어 해당 정보를 얻을 수 있게합니다.</br></br>

![SpawnActor](https://github.com/user-attachments/assets/eea74962-e809-4b92-b2ae-b50553fa449b)
<div align="center"><strong>누가 발사체를 쐈는지 Owner에 설정하기</strong></div></BR>

이후 데미지를 준 발사체는 Destroy Actor노드를 통해 사라지게 만들어 맞은 객체를 관통하여 앞으로 나아가지 않도록 합니다.</br>

플레이어 발사체는 총 3발만 발사할 수 있고 발사체가 화면밖으로 나가거나 파괴될 때마다 1발 씩 충전됩니다.</br>
또한 슈팅 버튼을 몇 초간 누르면 차지 샷이 나가 더욱 강력한 데미지를 입힐 수 있지만 차지 샷은 강력한 만큼 3발을 소모합니다.</br></br>

![ScreenProjectile](https://github.com/user-attachments/assets/ebb1e9f7-dbe5-49e3-96e5-56a0de3a0671)
<div align="center"><strong>발사체가 화면밖으로 나가는 경우</strong></div></BR>

플레이어만 사용하는 발사체이므로 BaseProjectile을 상속받는 PlayerProjectile을 생성합니다.</br>
PlayerProjectile에서는 발사체가 화면 밖으로 나가는 경우 Destory노드를 통해 파괴되도록 합니다.</br>
Player Controller를 통해 Convert World Location To Screen노드를 가져와 발사체가 현재 화면상 어느 위치에 있는지 확인하고 
화면의 X축보다 크거나 작으면 화면 밖으로 나간 것이므로 해당 발사체를 파괴합니다.</br></br>

![Restore](https://github.com/user-attachments/assets/536a5a80-bf43-4306-90c5-f9d03d39f450)
<div align="center"><strong>발사체가 파괴될 경우</strong></div></BR>

발사체가 파괴될 경우 플레이어는 발사한 탄에 따라 샷 에너지를 회복합니다.</br>
일반 탄을 쐈을 경우 1발, 차지 샷을 쐈을 경우 3발을 회복해야합니다.</br></br>

![ShotEnergyRestore](https://github.com/user-attachments/assets/c5457540-5cb6-49f1-870b-fe56b5fc6a65)
<div align="center"><strong>샷 에너지 회복하기</strong></div></BR>

발사체는 파괴될 때 자신의 클래스(기본 샷, 차지샷)를 Restore Shot Energy 함수노드에 입력값으로 넣고
클래스 이름을 Key값으로 하고, 회복할 샷 에너지 양인 int형을 Value값으로 갖는 맵에서 Find함수를 통해 샷 에너지를 몇을 회복할지 결정합니다.</br>
샷 에너지는 최대 3발로 3발이 넘지 않도록 MIN노드를 이용하여 조정했습니다.</BR>

---

### [체력 설정]
몬스터와 플레이어 캐릭터는 Actor로 일정 체력을 갖고 있으며, 공격에 맞을 시 데미지에 따라 체력이 닳습니다.</BR>
Actor Component를 통해 체력 시스템을 만들고 몬스터와 플레이어 캐릭터에게 컴포넌트를 부착하는 형식으로 시스템을 구축했습니다.</BR></br>

![BPC_Vitality](https://github.com/user-attachments/assets/707d8a91-6570-42af-bfc3-ed4bd38b0d11)
<div align="center"><strong>게임 시작 시 초기 체력 세팅하기</strong></div></BR>

Actor Component인 BPC_Vitality는 게임 시작 시 초기 체력을 세팅하고 공격받을 시 체력을 깍는 함수를 갖고 있습니다.</br>
초기 체력 세팅은 컴포넌트에 MaxHealth와 CurrentHealth를 변수를 두어 현재 체력이 최대 체력으로 세팅되도록 합니다.</br></br>

![4](https://github.com/user-attachments/assets/a5e4d3d8-a514-4b18-a1fc-1dd6a5d33fd3)
</br>
만든 BPC_Vitality는 BaseCharacter에 컴포넌트를 추가하여 BaseCharacter가 상속받는 모든 Actor가 체력 시스템을 갖습니다.</br>
각각의 Actor는 디테일 탭에서 MaxHealth를 세팅하여 최대 체력을 다르게 세팅할 수 있습니다.</br></br>

![AnyDamage](https://github.com/user-attachments/assets/b72c70a3-a968-4aa8-8f4d-61ca78442bda)
<div align="center"><strong>데미지를 체력에 반영하기</strong></div></BR>

AnyDamage는 언리얼엔진에서 제공하는 함수로 피격판정을 받을 경우 활성화 되는 이벤트 노드입니다.</br>
BPC_Vitality에서 IsDefeated변수를 통해 Actor의 생사를 확인하고 죽지 않았다면 BPC_Vitality에 있는 Receive Damage 함수를 호출합니다.</br>
Receive Damage함수는 받은 데미지 수치를 입력으로 받아 현재 체력에서 데미지를 빼며, Clamp함수를 통해 체력이 0 미만으로 떨어지지 않게 합니다.</br>

만약 데미지 계산이후 현재 체력이 0이하라면 해당 Actor는 죽은 것이므로 IsDefeated변수를 True로 바꿔 다른 시스템이 알 수 있게합니다.</br></br>

![ReceiveDamage](https://github.com/user-attachments/assets/b736b062-6dca-47b2-b670-b82958f8ef25)
<div align="center"><strong>Receive Damage함수</strong></div></BR>

---

### [플레이어 피격 시]
플레이어는 몬스터가 쏘는 불덩이 뿐만 아니라 몸체끼리 부딪혀도 데미지를 받습니다.</br>
몬스터 측에서 데미지 계산을 할 시 피격 대상이 누군지 매번 알아야하고 피격 대상의 hp를 깍아야하므로 비효율적이라 생각해 BP_Player에서 계산하도록 했습니다.</br></br>

![overlap](https://github.com/user-attachments/assets/25369f18-20b0-41f6-b9d7-e3ddeeb3ab1e)
<div align="center"><strong>Overlap이벤트 발생 시</strong></div></BR>

몬스터가 가진 캡슐 컴포넌트와 Player가 가진 캡슐 컴포넌트가 겹쳐질 시 Begin Overlap이벤트가 발생합니다.</br>
플레이어가 부딪힌 대상이 몬스터인지 확인하기 위해 Begin Overlap에서 상대측 Actor가 BaseEnemy기반으로 만들어진 Actor인지 체크합니다.</br></br>

특정 몬스터는 선제 공격 특성을 지녀 어그로 범위를 갖고 있습니다. 어그로 범위 또한 충돌 컴포넌트를 사용하므로 어그로 범위 안에 들어간 것만으로도 Overlap이벤트를 발생시키고 데미지를 받게됩니다.</br>
문제점을 해결하기 위해 Overlap노드에서 출력되는 Other Comp와 몬스터가 가진 캡슐 컴포넌트가 같은지 판별합니다.</br>
Other Comp는 CollisionCylinder을 반환하며, 이는 해당 Actor가 가지고 있는 콜리전을 의미합니다.</br>
부딪힌 몬스터의 캡슐 컴포넌트와 Overlap됐을 때만 데미지가 적용되도록 Branch노드를 사용하고 몬스터의 캡슐 컴포넌트가 맞다면 Apply Damage를 자기자신한테 적용시킵니다.</br></br>

![super](https://github.com/user-attachments/assets/8ed9e263-c049-457c-b94b-e7b47d40676f)
<div align="center"><strong>피격 시 무적 이벤트 발동</strong></div></BR>

피격 후에도 반복적으로 몬스터를 접촉해 빠르게 플레이어의 체력을 소진하는 것을 방지하기 위해 피격 이후 몇 초간 무적 시간을 추가했습니다.</br>
플레이어의 캡슐 컴포넌트를 가져와 콜리전 채널에서 Pawn을 Ignore하도록 바꿔 충돌을 무시하도록 하고,
자신이 무적상태인지 아닌지 확인하기 위해 Sprite가 깜박이는 이벤트를 추가했습니다.</br></br>

![clear](https://github.com/user-attachments/assets/e12b2bf2-16dc-4ded-9e0a-8e576696007c)
<div align="center"><strong>일정 시간 후 무적 이벤트 종료</strong></div></BR>

무적 시간 동안 Delay를 주고 이후 Sprite관련 이벤트를 종료시키고 플레이어의 캡슐 컴포넌트의 콜리전 채널을 다시 원상복귀 시켜 몬스터와 충돌이 가능하도록 했습니다.</br>
무적 일 시 플레이어의 Sprite를 깜빡인 후, 무적이 꺼진다면 Sprite가 꺼진 채로 돌아올지 켜진 채로 돌아올지 예측이 힘드므로 Set Visibility노드를 통해 Sprite가 보이도록 했습니다.</br></br>

몬스터와 플레이어 캐릭터가 계속 겹쳐있을 시, Sprite가 비정상적으로 깜빡이던 버그가 생겼습니다.</br>
해당 버그는 Sprite관련 이벤트 노드를 SetCollisionResponsetoChannel노드 뒤에 배치하여 생겼습니다.</br>
Sprite 이벤트는 종료되지 않았지만 무적 시간이 풀려 다시 Overlap이벤트가 발생해 Sprite 이벤트가 종료되지 않은 채 다시 호출되어 생긴 버그였습니다.</br>
따라서 두 노드의 순서를 바꿔 버그를 고쳤습니다.</br></br>

![damaged](https://github.com/user-attachments/assets/9db6e0bb-a992-40f5-96c1-3d9dad38f728)
<div align="center"><strong>몬스터에게 피격 시 튕겨나가는 플레이어</strong></div></BR>
또한 피격 시 플레이어가 몬스터에게 가만히 있는 것이 아니라 부딪힌 몬스터가 있는 방향의 반대로 튕겨나가도록 했습니다.</br>
몬스터가 플레이어의 오른쪽에서 부딪혔으면 왼쪽으로 튕겨나가며, 왼쪽에서 부딪히면 오른쪽으로 튕겨나갑니다.</br>
이를 위해서 현재 플레이어의 위치와 플레이어와 부딪힌 Actor의 위치를 알아야합니다.</br></br>

![knockback](https://github.com/user-attachments/assets/cf46e8af-8582-492a-97b5-55e3c022270f)
<div align="center"><strong>튕겨나갈 방향 구하기</strong></div></BR>

부딪힌 몬스터는 Overlap이벤트에서 BaseEnemy인지 체크하고 맞다면 해당 몬스터를 Apply Damage노드의 Damage Causer에 입력값으로 줬습니다.</br>
Apply Damage노드가 활성화 되면 언리얼엔진에서 제공하는 AnyDamage이벤트가 발생하며 해당 이벤트는 Damage Causer를 출력합니다.</br>
이렇게하여 몬스터의 위치와 플레이어의 위치를 구하고 두 위치를 알 수 있으므로 몬스터가 플레이어를 기준으로 어느 방향에 있는지 Find Look at Rotation노드를 통해 확인 할 수 있습니다.</br>
몬스터가 플레이어의 오른쪽에 있으면 X값이 양수가 나오고, 왼쪽에 있으면 X값이 음수가 나오므로, 플레이어가 반대 방향으로 튕길 수 있도록 Select Float노드를 작성했습니다.</br></br>

![damaged1](https://github.com/user-attachments/assets/d29ad391-f64f-4682-8bbd-871d9494142d)
<div align="center"><strong>튕겨나가는 애니메이션 설정하기</strong></div></BR>
구한 방향에 얼마만큼 튕겨질지 수치를 정해 Lauch Character 노드를 통해 Character가 튕겨지도록 합니다.</br>
플레이어가 튕겨질 시 키보드의 입력에 영향을 받지 않도록 XYZ축을 Override하여 강제적으로 튕겨진 수치만큼 캐릭터가 움직이도록 했습니다.</br>
이후 JumptoNode노드를 사용하여 Animation 컴포넌트를 통해 현재 상태에서 JumpStun상태의 애니메이션으로 건너뛰어 재생하도록 했습니다.</br>
이를 통하여 플레이어는 피격한 몬스터의 위치에서 반대로 튕겨나가는 위치를 갖고 해당 상태에 알맞은 애니메이션이 재생됩니다.</br></br>

![stun](https://github.com/user-attachments/assets/0d66393e-7198-4001-be04-28d2af241b88)
<div align="center"><strong>기절 상태 구현하기</strong></div></BR>

플레이어가 피격되어 튕겨나갈 때는 기절 상태를 부여하여 튕겨나가는 동안 이동과 공격을 방지하고자 Boolean타입으로 상태를 저장했습니다.</br>
일정 시간후 스턴 상태가 풀리고 초반에 설정한 Lateral Friction또한 기본 값으로 되돌려 움직임이 정상적으로 회복되도록 했습니다.</br>

---

### [차지샷 구현]
차지샷은 일정 시간 동안 공격 버튼을 누르면 3발을 소모하는 대신 데미지가 더 높은 샷을 발사하는 기능입니다.</br>
몇 초 동안 공격키를 눌렀는지 알기 위해서는 키 설정을 별도로 해줘야합니다.</br></br>

![Released](https://github.com/user-attachments/assets/c8756a31-28d7-48ba-883a-14a3affa4cc5)
<div align="center"><strong>트리거 설정하기</strong></div></BR>

트리거를 해제됨(Released)로 설정할 시 입력이 지속되는 동안 트리거가 Ongoing을 반환하며 Ongoing을 통해 입력이 지속될 동안 어떤 행동을 할지 정할 수 있습니다.</br>
게임에서는 공격 버튼을 계속 누를 시 플레이어가 흰색으로 깜빡이며 점차 푸르게 되어 차지가 완료됐다는 것을 시각적으로 확인할 수 있고, 특유의 차징 사운드를 플레이시켜 공격키를 계속 누르고 있다는 것을 알 수 있게 했습니다.</br></br>

![IA_Shoot](https://github.com/user-attachments/assets/aacceeba-9f39-45d4-a6ae-b2b622ccc937)
<div align="center"><strong>플레이어 공격 시스템</strong></div></BR>

플레이어가 맞고 있거나 슬라이딩 중에는 공격을 하지 못하며 공격키를 짧게 누를 시 기본 샷 투사체가 나가며, 공격키를 계속 누르고 있을 시 위에서 설명한 내용을 Handle Charge Flash 함수에서 실행합니다.</br>
IA_Shoot에서 주어지는 Elapsed Seconds을 통해 Try ChargeShot 함수에서 특정 시간보다 짧다면 기본 샷이 나가고 길다면 차지 샷이 나가도록 Branch노드를 이용하여 구현했습니다.</br></br>

![shot](https://github.com/user-attachments/assets/86f22f6d-6775-441e-b8a7-bf70cc712ea6)
<div align="center"><strong>샷에 따른 샷 에너지 관리</strong></div></BR>

플레이어는 3개의 샷 에너지를 가지고 있어 무한정으로 샷을 쏠 수 없습니다.</br>
샷을 발사할 준비가 완료되면 발사할 투사체가 몇 개의 샷 에너지를 필요로 한지 확인하고 현재 샷 에너지에서 사용할 에너지 양을 뺀 후 발사체를 월드에 생성합니다.</br>
차지샷은 3개의 샷 에너지를 모두 소모하고, 일반 샷은 1개의 에너지만을 필요로 합니다.</br>
발사된 투사체는 파괴되면 발사체 클래스가 가지고 있는 Restore Shot Energy 함수를 호출해 샷 에너지를 회복하게 됩니다.</br>

---

### [벽 점프 구현]
플레이어가 벽에 있을 경우 2가지 행동을 할 수 있습니다.</br>
첫 번째는 벽을 차고 반대편으로 점프하는 것이고, 두 번째는 벽에 기대서 천천히 내려가는 것입니다.</br>
그러기 위해서는 플레이어 앞에 현재 벽이 있는지 확인할 필요가 있습니다.</br></br>

![CheckWall](https://github.com/user-attachments/assets/f5e3f781-3af6-43dd-b236-dcc81cc96fe6)
<div align="center"><strong>벽의 유무 확인하기</strong></div></BR>

플레이어의 위치를 알아내고, 앞 방향 벡터를 구해 플레이어의 일정 범위 내에 벽이 있는지 확인합니다.</br>
벽은 게임 환경으로 World Static 오브젝트 타입이므로 확인할 오브젝트 타입으로 World Static을 세팅합니다.</br>
Line Trace For Objects 노드는 플레이어의 앞 방향의 일정 범위 내에 World Static 타입 물체가 있을 경우 True를 반환합니다.</br>
여기서는 World Static타입인 벽이 Line Trace안에 들어오면 True를 반환하게 되고 없으면 False를 반환하게 됩니다.</br></br>

![WallSlide](https://github.com/user-attachments/assets/4b573cbe-c14a-42bc-9550-c6fc0f20c915)
<div align="center"><strong>벽에 기댈 경우</strong></div></BR>

플레이어가 벽을 탐지하는 범위를 벽에 붙을 정도로 설정해주고 플레이어가 벽에 붙어 있다면 일정 속도로 내려가도록 해줍니다.</br>
X와 Y축 방향으로는 움직일 필요가 없으므로 Character Movement에서 그대로 사용하고 Z축의 경우 MAX 노드를 사용하여 떨어지는 속도를 조절합니다.</BR>
MAX노드를 사용한 이유는 평지에서 벽에 기댄 경우 무조건 아래로 천천히 내려가도록 하지 않기 위해 사용했습니다.</BR>
평지에서 이동하다가 벽에 기댄 경우 Character Movement에서 Z축의 속도는 0이므로 MAX노드를 통해 0이 나오므로 아래로 내려가지 않습니다.</BR></br>

![condition](https://github.com/user-attachments/assets/199e28b5-ac67-4a40-92cf-8a21a03955a6)
<div align="center"><strong>벽 슬라이딩이 나오는 상황</strong></div></BR>

MAX노드를 통해 평지에서 벽에 기댄다고 아래로 내려가지 않게 하였지만 애니메이션의 경우 상황이 다릅니다.</BR>
평지에서 벽에 부딪혔다고 벽 슬라이딩 애니메이션이 나오면 좋지 않은 플레이 경험을 주므로 벽 슬라이딩을 하는 조건을 지정해줬습니다.</BR>
첫 번째로는 플레이어가 일정 속도 이상으로 Z축 방향으로 나아가는 중이여야 합니다. 즉, 일정 속도 이상으로 추락중이여야 벽을 탐지할 수 있게 했습니다.</BR>
그리고 벽 방향으로 이동 키를 지속적으로 눌러야 앞에 있는 벽을 탐지하여 벽에서 천천히 아래로 슬라이딩하도록 했습니다.</BR>
벽을 탐지하지 못하는 경우 아래로 추락하게 됩니다.</BR></br>

벽에 슬라이딩 중인 경우 공중에 체공하고 있는 중이기 때문에 엔진 상에서는 점프 중으로 인식하고 있습니다.</br>
문제점은 점프 중인 경우 엔진에서 제공하는 Jump 노드가 작동되지 않습니다. 체공 상태에서 한 번 더 점프하기 위해서는 Launch Character 노드를 이용하여 강제적으로 캐릭터의 위치를 이동시켜줍니다.</br></br>

![WallJump](https://github.com/user-attachments/assets/4cf53660-573f-4eb7-95db-3416be3ee1fa)
<div align="center"><strong>벽 점프 구현하기</strong></div></BR>

벽에서 박차가 나가기 때문에 반대방향으로 나가도록 X축에 -1을 곱해주고 점프할 높이만큼 Lauch Velocity Z에 값을 입력합니다.</BR>
여기서 추락 및 측면 마찰로 인해 캐릭터가 마찰력에 의해 점프력이 크게 저하되므로 캐릭터의 위치를 이동시키기 전에 추락 및 측면 마찰을 0으로 세팅합니다.</BR>
벽 점프가 완료되면 마찰력을 초기 세팅으로 되돌려 정상적인 움직임이 가능하도록 했습니다.</BR>

---

### [체크포인트 구현]

몬스터가 죽을 시 이벤트를 발생 시켜 Destroy노드를 이용해 몬스터를 게임 상에서 없애면 되지만, 플레이어의 경우 부활시켜서 체크포인트 지점에서 플레이할 수 있도록 해야합니다.</br>
BaseCharacter에서 AnyDamage 노드를 통해 입은 데미지만큼 체력을 빼줬으며 체력이 0 이하로 내려가면 isDefeated 변수를 True로 하고 Defeated 이벤트를 발생시킵니다.</br>
BP_Player에서는 BeginPlay 노드에 Defeated 이벤트를 바인딩하여 이벤트가 발생하면 플레이어 소생절차가 시작됩니다.</br></br>

![Respawn](https://github.com/user-attachments/assets/3e916e02-f085-48e3-a405-45a7fa13b168)
<div align="center"><strong>새로운 플레이어 소환하기</strong></div></BR>

플레이어 리스폰 시 기존 캐릭터를 그대로 이용하고 체력을 다시 채우는 방법과 기존 캐릭터를 삭제하고 새 캐릭터로 바꾸는 2가지 방법이 있었습니다.</br>
2가지 방안 중 스탯 관련 버그를 배제하기 위해 기존 캐릭터는 삭제하고 새 캐릭터로 대체하는 방안을 채택했습니다.</br>
Spawn Actor노드를 통해 BP_Player의 Actor가 스타트 지점에서 소환되도록 했으며, Player Controller를 주어 플레이어가 제어할 수 있도록 했습니다.</br>
체력이 0으로 떨어진 기존 Actor는 Destroy 처리를 하지 않으면 맵 상에서 존재하게 되므로 새 플레이어를 소환 후 기존 플레이어를 삭제했습니다.</br></br>

시작점에 가까이에서 플레이어가 죽을 시 부활 후 기존 플레이어를 볼 수 있다는 가능성과 기존 플레이어에 카메라가 세팅되어 있다는 문제점이 생겼습니다.</br>
첫 번째 문제점은 죽은 플레이어의 Collision을 No Collision으로 설정해 어떤 물체와도 충돌하지 않게 해 게임 맵에서 추락하도록 했습니다.</br>
카메라 문제는 Detach Component를 통해 기존 플레이어에서 카메라가 부착된 Spring Arm을 떼냈습니다.</br>
이렇게하면 플레이어가 죽을 시 카메라가 죽은 장소에 고정되고 플레이어는 아래로 떨어지게 됩니다. 플레이어가 죽었다는 효과를 확실히 주기 위해 장면을 비추는 카메라를 어둡게 해주고 부활하면 다시 카메라가 새 캐릭터를 비추는 동시에 밝아지도록 했습니다.</br></br>

![Death](https://github.com/user-attachments/assets/95b075b2-2d20-4bbe-9efd-894809e82ec7)
<div align="center"><strong>플레이어 사망 시 카메라 설정</strong></div></BR>

이렇게 하여 플레이어가 죽었을 시 카메라 효과와 재소환하는 기능을 구현했습니다.</br>

![CheckPoint](https://github.com/user-attachments/assets/7f421904-2ebb-406e-ad58-332f43eb20d0)
<div align="center"><strong>체크 포인트 구현</strong></div></BR>

체크포인트는 간단한 충돌 이벤트가 있는 Actor로 자신의 Collision에 플레이어가 부딪힌다면 체크포인트의 위치를 저장하면 됩니다.</br>
우선 부딪힌 대상이 BP_Player 타입의 Actor인지 캐스팅하여 체크하고 체크포인트가 여러번 활성화 되는 것을 방지하기 위해 Do Once노드를 사용했습니다.</br>
이후 GameMode인 GM_Action에 자기의 위치를 GM_Action의 변수인 ActiveCheckpoint에 저장합니다.</br>

![CheckpointRespawn](https://github.com/user-attachments/assets/76388696-128d-4c58-8f27-2e3d7f374a35)
<div align="center"><strong>체크 포인트에서 부활하기</strong></div></BR>

게임모드는 ActiveCheckpoint의 유무에 따라 플레이어 리스폰 위치를 결정합니다.</br>
플레이어가 죽어 BaseCharacter에서 Defeated 이벤트가 발생하면 BP_Player에 바인딩 된 Defeated 이벤트가 활성화되고, Defeated는 게임 모드인 GM_Action의 함수 Respawn를 호출하게 됩니다.</br>
GM_Action은 활성화 된 체크포인트의 유무에 따라 부활 장소를 지정하여 새로운 BP_Player 액터를 소환하게 됩니다.</br>

---

### [몬스터 구현]

꽃게 몬스터는 제일 간단하고 쉬운 몬스터로 화면상에서 좌우로 움직이는 몬스터입니다.</br>
몬스터에게 행동을 주기 위해 AI Controller 타입의 블루프린트를 생성하고 AI Controller가 꽃게 몬스터의 제어권을 가질 때 매 틱마다 앞으로 나아가게했습니다.</br>
BP_Crab에서는 WalkFoward 이벤트를 생성하여 현재 Actor의 앞 방향 벡터를 구해 Add Movement Input 노드로 앞으로 가게 했습니다.</br>
AI Controller는 매 틱마다 BP_Crab에 있는 WalkFoward 이벤트를 호출하게 되어 꽃게가 앞으로 나아가게 합니다.</br></br>

![AIC_Crab](https://github.com/user-attachments/assets/ee60fcb5-b10a-4944-ba9f-fa4fb39dfd02)
<div align="center"><strong>AI Controller 예시</strong></div></BR>

꽃게 몬스터의 행동 포인트는 벽이나 절벽을 만나면 가던 방향을 멈추고 되돌아오는 것입니다.</br>
이를 위해서는 플레이어가 벽을 탐지하듯이 꽃게가 벽과 절벽을 탐지가 가능해야합니다. 플레이어의 벽 점프 구현시 라인 트레이스를 사용했으나 꽃게에서는 콜리전 박스를 이용하여 구현해봤습니다.</br></br>

![CrabCollision](https://github.com/user-attachments/assets/67b665bd-74bb-4c8d-92d6-722fa3903208)
<div align="center"><strong>벽 및 절벽 식별을 위한 콜리전</strong></div></BR>

그림과 같이 콜리전 박스를 꽃게에 할당하고 해당 박스에 게임 환경인 벽이나 절벽인 WorldStatic타입 물체가 감지되면 꽃게가 좌우대칭 시키도록 했습니다.</br>
각 콜리전이 WorldStatic타입 물체와 만나면 Overlap이벤트가 발생하며 Overlap 이벤트는 Turn Character 함수를 호출합니다.</br>
Turn Character 함수는 현재 꽃게의 Rotation을 갖고 와 Z축으로 180도를 회전시켜 Set Actor Rotation 노드를 통해 꽃게를 회전시켜줬습니다.</br>

도마뱀 몬스터는 제자리에서 원거리 공격을 날리는 몬스터입니다.</br>
AI Controller가 도마뱀 몬스터를 제어하면 BP_Lizard에 있는 Shoot Projectile함수를 호출합니다.</br>
도마뱀이 공격을 하는 횟수는 Tick 이벤트를 통해서 하지 않고 일정 시간 마다 공격을 하도록 Set Timer by Event 노드를 사용했습니다.</br>
해당 노드는 개발자가 정해준 시간마다 Shoot Projectile함수를 호출하여 도마뱀이 일정시간마다 파이어볼을 날리도록 합니다.</br></br>

![AIC_Lizard](https://github.com/user-attachments/assets/9700110c-ecb2-401b-869a-61205e5ea6e2)
<div align="center"><strong>도마뱀 몬스터의 AI Controller</strong></div></BR>

박쥐 몬스터는 정해진 두 좌표를 날라다니는 몬스터입니다.</br>
두 지점을 World상에서 우선 좌표를 구하고 값을 저장하여 해당 좌표를 왔다갔다하며 날라다니는 형태입니다.</br></br>

![Bat](https://github.com/user-attachments/assets/acef40c3-602d-4613-acc6-dfc0896d8375)
<div align="center"><strong>박쥐에 있는 두 지점</strong></div></BR>

두 지점의 좌표가 구해졌으면 박쥐가 어느 한 지점에 도착하면 다시 다른 지점에 돌아가도록 구현을 해줘야합니다.</br>
TimeLine 노드를 사용하여 A 지점에서 B 지점으로 1초안에 가도록 정해주고 해당 이동이 끝난다면 TimeLine 노드의 Reverse from End에 있는 Move Backward 이벤트를 통해 다시 B지점에서 A지점으로 갈 수 있게 했습니다.</br>
한 가지 문제점은 TimeLine이 언제 끝나는지 알아야하므로 TimeLine 노드의 Finished 출력을 통해 종료가 되면 다른 이벤트를 호출할 수 있도록 FlipFlop 노드를 사용했습니다.</br>
따라서 박쥐가 Move Forward를 통해 A 지점에서 B 지점으로 도착했으면 TimeLine의 Finished가 활성화되어 Flip Flop노드가 Forward가 아닌 이벤트인 Move Backward 이벤트를 호출하여 다시 되돌아가게 됩니다.</br></br>

![AIC_Bat](https://github.com/user-attachments/assets/21cd5316-af1d-40c8-894e-d24516d77670)
<div align="center"><strong>박쥐 움직임 구현하기</strong></div></BR>

눈깔 몬스터는 플레이어가 해당 몬스터의 일정 범위안에 들어오면 선제 공격을 가하는 몬스터입니다.</br>
어그로 범위를 설정해주기 위해 구형 콜리전을 눈깔 몬스터에게 추가해주고 해당 콜리전에 Overlap이벤트를 추가합니다.</br>
어그로 범위를 담당하는 콜리전은 충돌 시 충돌 대상이 BP_Player 타입 Actor인지 확인하고, 맞다면 변수로 저장합니다.</br>
AI Controller에서는 변수로 저장한 BP_Player가 존재하는지 확인하고 존재한다면 눈깔 몬스터와 플레이어의 방향을 구하여 플레이어를 향하여 몬스터가 움직이도록 합니다.</br>

![AIC_Eye](https://github.com/user-attachments/assets/ad40a01c-55f6-4319-8103-8e93809cc19f)
<div align="center"><strong>눈깔 몬스터의 AI Controller</strong></div></BR>
