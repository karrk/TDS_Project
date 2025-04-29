# 📌 Conventions

## 🚀 Git convention

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
      
## 🚀 Code convention

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

---

### `[최종 등록일자]`

- 25년 04월 29일 / 작성자 : 장동진

<!--
? 스타일 템플릿
<span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">public</span>
<span style="color:#ff9c00; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">summary</span>
-->
