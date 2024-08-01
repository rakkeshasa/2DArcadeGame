# 2DArcadeGame
![Intro](https://github.com/user-attachments/assets/d42b4f4c-2de5-4d58-b2ff-cd0d786a7369)

블루프린트로 만든 2D 아케이드 게임
</BR>

목차
---
- [간단한 소개](#간단한-소개)
- [플레이 영상](#플레이-영상)


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

