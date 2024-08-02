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
캐릭터의 속도가 0보다 크지 않다면 더이상 앞으로 가고 있지 않으므로 Run 애니메이션에서 Idle 애니메이션으로 바꿔서 재생합니다.</br>

![OwningActor](https://github.com/user-attachments/assets/3e019e88-0b1d-4713-b909-e32fc40b555b)
<div align="center"><strong>BP_Player에서 사용할 애니메이션 클래스 설정</strong></div></BR>

Animation Blueprint에서 Owning Player을 갖고 오기 위해서는 BP_Player에 해당 애니메이션 클래스를 사용하겠다고 지정해줘야합니다.</br>
Animation Component의 디테일 탭에서 Paper ZD의 Anim Instance Class에 위에서 만든 Animation Blueprint를 설정하여 Ownint Player를 참조할 수 있도록 합니다.</br>
