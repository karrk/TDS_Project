# 📌 구현 기능
### 1. 충돌 레이어 분리
- 배경
  - 레퍼런스 게임 분석 시 바닥의 하나의 경로만 사용하지 않고, 총 3갈래 길(상,중,하) 경로를 활용하고 있는점을 확인하였으며 몬스터 간 충돌이 발생하지만, 같은 경로에 배치된 몬스터끼리만 충돌이 가능함을 확인
- 구현방법
  - 바닥 오브젝트 내에 서로 충돌되지 않는 콜라이더 오브젝트를 배치, 레이어를 설정 (Ground Top, Mid, Bot)
  - 몬스터 오브젝트 스폰 시 경로선정과 함께 몬스터별 레이어를 설정하는 방식으로 구현 (Monster Top, Mid, Bot)
  - 트럭 오브젝트의 경우 몬스터의 경로와 상관없이 상호작용을 진행해야하므로 지정한 모든 몬스터 레이어와 충돌을 허용
    
- 결과
  - 2D환경에서 같은 선상에 오브젝트를 배치하지만, 레퍼런스와 같이 2D콜라이더를 활용했음에도 충돌 레이어를 분리함으로써 자연스럽게 2.5D 와 같은 연출이 가능
  - 레이어 자체를 분리함으로써 별도의 충돌처리 로직이 필요없었으며, 수많은 몬스터들간 충돌 발생으로 인한 물리연산으로 인한 성능저하를 방지
### 2. 다중 오브젝트 풀
- 배경
 	- 레퍼런스 게임 분석 시, 가비지 컬렉팅을 유발하는 요소를 확인하였으며 이를 방지하기 위해 오브젝트 풀 구현을 목표로 함
 	- 오브젝트 풀로 적용될 오브젝트는 몬스터 뿐만 아니라 총알, 데미지 텍스트 와 같이 분류가 나눠질 수 있음을 예상하였으며, 이 후 다양한 요소가 추가될 수 있음을 인지하여 확장성 높은 구조 설계가 필요
- 구현 방법
  - 쉬운 접근, 활용을 허용하기 위해 이를 중개하는 오브젝트 풀 매니저를 제작하였으며 모든 오브젝트를 가져오거나 반환할때 오브젝트 풀로 관리할 수 있게 제작
  - 오브젝트 풀로 관리될 요소는 IPooling 인터페이스를 상속받아야 하는 구조로 제작되었으며, 해당 인터페이스는 각 오브젝트의 구체적인 Enum 타입 명시를 필요로 함
  - 오브젝트 풀 매니저 내에 Get , Return 메서드는 오브젝트의 각 Enum 타입을 확인하여 자신에 맞는 풀로부터 오브젝트를 호출, 반환하는 구조로 제작
  - 풀 내부의 컬렉션은 List를 활용하였으며, 오브젝트가 부족할 경우 해당 오브젝트가 빈번하게 사용되고 있다는 판단 하에 리스트의 Capacity를 2배로 동적으로 확장하도록 구현함
- 결과
  - 오브젝트 타입을 Enum으로 분류하고, 인터페이스 기반으로 접근함으로써 신규 타입 추가 시 최소한의 수정만으로도 대응 가능하도록 설계
  - 매니저를 통한 모든 오브젝트 풀의 접근 지점을 통일함으로, 추후 디버깅 및 리소스 사용 분석 시 효율적으로 추적 및 제어가 용이
  - List의 반복적인 내부 재할당을 방지함으로써 가비지 컬렉팅에 의해 일시적으로 발생하는 프레임 드롭 현상을 방지할 수 있음
### 3. 몬스터 리프팅
- 배경
  - 몬스터는 전부 플레이어 쪽으로 이동해야하는 목적이 있으며, 자신의 경로가 앞에 배치된 몬스터로 인해 막혀있을 경우 대상의 몬스터를 위로 넘어가는 기능구현이 필요
  - 레퍼런스 분석 시, 원거리 몬스터가 사거리범위 내 플레이어를 감지하고 제자리에서 공격하는 중에도 전방에 있는 몬스터를 넘어가는 기능이 동작함을 확인하였으며, 해당 기능은 스스로가 넘어가는 방식이 아닌 전방에 있는 몬스터가 뒤에 배치된 몬스터를 위로 끌리는 과정을 확인함
- 구현 방법
  - 전방 몬스터는 후방에 있는 몬스터를 감지하는 레이캐스트를 동작시키도록 제작하였으며, 직선형 레이캐스트로는 감지범위가 너무 협소할 수 있으므로 박스형태의 범위형 레이캐스트를 활용
  - 몬스터 개체는 각자 자신이 띄워 올려질 수 있는 상태변수를 배치하였으며, 해당 변수가 true 상태일 경우 띄워지지 않도록 설정해 무분별한 리프팅 기능을 제한하여 너무 높게 올라가는 현상을 방지
  - 띄워지는 동작 시 자신의 리프팅 상태변수를 코루틴을 활용해 일정 시간 후 원상태로 복구시키는 과정을 진행
- 결과
  - 상태변수를 활용하여 한번에 너무 많은 힘을 전달받지 않도록 제작하여, 자연스럽게 몬스터를 넘어갈 수 있도록 연출
  - 마찰로 인해 서로 조금씩 걸리는 현상을 확인하였으며 이는 Physic Material을 활용하여 마찰력을 최소한으로 줄여 이를 해결하였으며, 약간의 Bounce 값을 설정하여 게임의 컨셉에 맞는 통통 튀는 연출을 추가할 수 있었음
### 4. 스크립터블 오브젝트를 활용한 확장 설계
- 배경
  - 프로토타입 제작 후 다양한 몬스터 또는 총 탄환이 필요할 수 있으며, 이러한 확장성을 대비할 수 있는 구조가 필요
- 구현 방법
  - 같은 목적의 오브젝트이며, 구체적인 행동은 클래스 내에서 수정, 적용을 가능하게 하며 내부에서 다뤄야할 각 값들은 스크립터블 오브젝트를 통해 값을 전달할 수 있도록 제작
  - 각 오브젝트는 자신의 데이터(SO)를 참조하여 초기화 및 설정을 수행하며, 데이터 변경 시 프리팹 수정 없이도 손쉽게 밸런스 조정 및 테스트가 가능하도록 구성
- 결과
  - 클래스 내부 로직과 데이터 정의를 분리함으로써, 손 쉽게 컨텐츠를 조정할 수 있는 구조 확보
  - 새로운 몬스터, 총기를 추가할때 스크립터블 오브젝트만 추가 적용함으로 컨텐츠를 유연하게 확장이 가능
    
# 📌 Conventions

<details><summary> 🚀 Git convention</summary>

### 개요

- 프로젝트 진행 간 Git Repository 관리에 있어 통일성 유지를 위해 Git Convention을 지정하고 관리 가이드라인으로 적용한다.<br><br>

### 규칙

### `Branch`

- Master(main)

  - 정식 버전 빌드용 브랜치
  - 실수 및 무분별한 수정을 막기 위해 엑세스 제한

- Develop

  - 기능 개발/구현용 브랜치
  - 이 브랜치에서 기능작업(구현 용도)를 추가 분리한 후, 작업 완료시 해당 브랜치로 Merge를 진행한다.
    - Merge시 Pull Request를 활용하여 충돌 대비, 충돌 발생시 대처를 준비한다.

- 기능 구현 Branch

  - 생성 브랜치 명 : <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">Type/작업내용</span>

    - Type

      - <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">Feat</span> : 새로운 기능 추가
      - <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">Fix</span> : 기존 기능 및 버그 수정
      - <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">Test</span> : 기능 테스트 전용

    - ex > <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">Feat/CharacterAnimation
    - ex > <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">Fix/FlowerObject
    - ex > <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">Test/MonsterMove

### `Commit`

- Commit 주기

  - 충돌 및 버그 발생시 Reset 을 용이하게 하기 위해 작은 단위로 나눠 커밋을 진행한다.
    - ex> 이미지 리소스 추가, 스크립트 생성, 한 단위의 기능 구현 시
  - 커밋 메세지는 아래의 규칙을 따르며, 부가 설명이 필요한 경우 Description 란에 작성한다.
  - 만일, 작업 내용이 상당히 작은경우 Git Message 를 활용하여 작업 기록을 남긴다.

- Commit 메세지

  - <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">[Type] : 내용</span>
    - ex> [Fix]:Flower 오브젝트 배치시 위치 어긋나는 버그 수정
  - Type

    - <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">[Feat]</span>: 새로운 기능 추가
    - <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">[Fix]</span>: 버그 및 기능의 수정
    - <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">[Set]</span>: 프로젝트 설정, 유니티 자체 설정
    - <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">[Refactor]</span>: 기능에 영향을 주지 않는 범위의 코드 수정
    - <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">[Test]</span>: 테스트에 관련된 작업
    - <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">[Create]</span>: 프로젝트, 씬, 스크립트, 오브젝트(프리팹) 생성
    - <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">[Add]</span>: 리소스 및 에셋 등, 외적인 파일 추가

  - 타입 종류에 기재되지 않은 단순 작업은 타입을 정의하지 않는다.

    - ex>`불필요 파일 정리`

      <br>
      <br>
      <br>
      <br>

</details>

<details><summary>🚀 Code convention</summary>

### 개요

- 개발 진행 간 코드 작성 및 유지보수에 있어 통일성 유지로 가독성 향상, 작업환경의 효율성을 높이기 위함<br><br>

### 규칙

### `Naming`

- 애매한 변수명은 [변수명 짓기 사이트](https://var.gg/ko#google_vignette) 활용
- 코드 상에서 단어 사이에 띄어쓰기 목적의 언더바(\_)는 사용하지 않는다.<br><br>

### `클래스 및 파일 명`

- 파스칼 케이스를 적용
- 프리팹, 스크립트 외 같은 포맷의 파일이 여러개일 경우 용도별로 정렬하기 위해 <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">항목(type)*index*파일명(파스칼케이스)</span> 를 적용한다.

```csharp
ex >
public class PlayerMovement : MonoBehavior{ ... }
```

```csharp
ex >
BGM_00_Normal.mp3
BGM_01_Fast.mp3

SFX_00_Hit.mp3
SFX_01_Click.mp3

CharacterMovement.cs
```

  <br>

### `변수`

- <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">private</span> 은 언더바(\_) 후 카멜케이스를 적용
- <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">public</span> 은 파스칼 케이스 적용
  - 기본적으로 <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">public</span> 변수는 사용하지 않으며, 사용시 <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">프로퍼티</span>로 선언
- bool 타입은 상태를 나타내기 위해 <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">Is</span> 를 붙인다.
  - 상태를 저장/표기하기 위함이 아닌 경우 붙이지 않아도 무방
- 변수명이 길어 약칭을 사용할 경우 주석을 표기한다.
- 타입을 즉각 알 수 없는 경우 <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">var 타입은 사용을 지양
- 상수는 전부 대문자로 표기, 필요한경우 언더바를 사용

```csharp
public class Example
{
  // 상수 선언
  private const int INIT_POOL_COUNT = 5;

  // bool 상태
  private bool _isGrounded;

  // public 변수 선언
  public bool IsAlive { get; private set; }
  public int EventStep { get; private set; }

  // private 변수 선언
  private float _maxHp;
  private GameObject _target;

  private void Foo()
  {
		    // 코드 상으로 타입이 바로 판단되지 않는 경우 var 사용은 지양한다
      var hit = _target;
      var targetList = new List<GameObject>();
  }
}
```

  <br>

### `함수`

- 함수명 : 파스칼 케이스, 동사형으로 작성한다.
- 매개변수 : 카멜케이스를 적용하되 매개변수임을 표기하기 위해 m\_ 표기를 붙인다
- 함수 명 기준의 기능 단위로 기능 모듈화
- 함수 내에서 한 기능의 로직이 끝나면 개행으로 분리
- 함수 작성 시 기능 정보는 <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">&lt;summary&gt;</span> 주석을 활용한다
- 함수 명으로 필요 이상의 부가설명은 작성하지 않는다.

```csharp
ex>
public class Player : MonoBehaviour
{
  private void UseItem(CharacterController m_target)
  {

  }

  /// <summary>
  /// 아이템 획득 메서드
  /// </summary>
  public void GetItem(Item m_item)
  {
  }

  private void PlayerHpDown(float m_damage)
  {
      // 필요 이상의 부가설명의 예시
      // Player 클래스 내에서 Player에게 영향이 가해지는 기능의 함수는
      // 'Player'가 붙지 않아도 된다.
  }

  private void HpDown(float m_damage)
  {
      // 옳은 함수명
  }
}
```

  <br>

### `인터페이스`

- 인터페이스 작성 시 이름 앞에 알파벳 대문자 'I'추가

```csharp
public Interface IAction{ ... }
```

  <br>

### `Enum`

- 클래스, 구조체들과 혼동을 방지하기 위해 열거형 선언 이유를 명확히 작성한다.
- 클래스와의 구분을 위해 앞에 'E\_'를 붙인다.
- 포함되는 속성은 항상 파스칼 케이스로 작성한다.
- 순회를 위해 마지막 선언은 Size로 정의한다.

```csharp
public enum E_StateType
{
  None = -1,
  Move = 0,
  Attack,
  Size,
}
```

  <br>
</details>


### `[최종 등록일자]`

- 25년 04월 29일 / 작성자 : 장동진

<!--
? 스타일 템플릿
<span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">public</span>
<span style="color:#ff9c00; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">summary</span>
-->
