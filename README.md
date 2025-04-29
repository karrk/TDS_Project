# Conventions

<br>
<div style="background-color:#a3c2fe; color:black; padding:10px; font-size:25px; font-weight:bold; border-radius:8px;text-align:center;"> Git convention </div><br>

## ����

- ������Ʈ ���� �� Git Repository ������ �־� ���ϼ� ������ ���� Git Convention�� �����ϰ� ���� ���̵�������� �����Ѵ�.<br><br>

## ��Ģ

### `Branch`

- Master(main)

  - ���� ���� ����� �귣ġ
  - �Ǽ� �� ���к��� ������ ���� ���� ������ ����

- Develop

  - ��� ����/������ �귣ġ
  - �� �귣ġ���� ����۾�(���� �뵵)�� �߰� �и��� ��, �۾� �Ϸ�� �ش� �귣ġ�� Merge�� �����Ѵ�.
    - Merge�� Pull Request�� Ȱ���Ͽ� �浹 ���, �浹 �߻��� ��ó�� �غ��Ѵ�.

- ��� ���� Branch

  - ���� �귣ġ �� : <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">Type/�۾�����</span>

    - Type

      - <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">Feat</span> : ���ο� ��� �߰�
      - <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">Fix</span> : ���� ��� �� ���� ����
      - <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">Test</span> : ��� �׽�Ʈ ����

    - ex > <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">Feat/CharacterAnimation
    - ex > <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">Fix/FlowerObject
    - ex > <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">Test/MonsterMove

### `Commit`

- Commit �ֱ�

  - �浹 �� ���� �߻��� Reset �� �����ϰ� �ϱ� ���� ���� ������ ���� Ŀ���� �����Ѵ�.
    - ex> �̹��� ���ҽ� �߰�, ��ũ��Ʈ ����, �� ������ ��� ���� ��
  - Ŀ�� �޼����� �Ʒ��� ��Ģ�� ������, �ΰ� ������ �ʿ��� ��� Description ���� �ۼ��Ѵ�.
  - ����, �۾� ������ ����� ������� Git Message �� Ȱ���Ͽ� �۾� ����� �����.

- Commit �޼���

  - <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">[Type] : ����</span>
    - ex> [Fix]:Flower ������Ʈ ��ġ�� ��ġ ��߳��� ���� ����
  - Type

    - <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">[Feat]</span>: ���ο� ��� �߰�
    - <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">[Fix]</span>: ���� �� ����� ����
    - <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">[Set]</span>: ������Ʈ ����, ����Ƽ ��ü ����
    - <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">[Refactor]</span>: ��ɿ� ������ ���� �ʴ� ������ �ڵ� ����
    - <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">[Test]</span>: �׽�Ʈ�� ���õ� �۾�
    - <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">[Create]</span>: ������Ʈ, ��, ��ũ��Ʈ, ������Ʈ(������) ����
    - <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">[Add]</span>: ���ҽ� �� ���� ��, ������ ���� �߰�

  - Ÿ�� ������ ������� ���� �ܼ� �۾��� Ÿ���� �������� �ʴ´�.

    - ex>`���ʿ� ���� ����`

      <br>
      <br>
      <br>
      <br>
    <div style="background-color:#a3c2fe; color:black; padding:10px; font-size:25px; font-weight:bold; border-radius:8px;text-align:center;"> Code convention </div><br>

## ����

- ���� ���� �� �ڵ� �ۼ� �� ���������� �־� ���ϼ� ������ ������ ���, �۾�ȯ���� ȿ������ ���̱� ����<br><br>

## ��Ģ

### `Naming`

- �ָ��� �������� [������ ���� ����Ʈ](https://var.gg/ko#google_vignette) Ȱ��
- �ڵ� �󿡼� �ܾ� ���̿� ���� ������ �����(\_)�� ������� �ʴ´�.<br><br>

### `Ŭ���� �� ���� ��`

- �Ľ�Į ���̽��� ����
- ������, ��ũ��Ʈ �� ���� ������ ������ �������� ��� �뵵���� �����ϱ� ���� <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">�׸�(type)*index*���ϸ�(�Ľ�Į���̽�)</span> �� �����Ѵ�.

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

### `����`

- <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">private</span> �� �����(\_) �� ī�����̽��� ����
- <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">public</span> �� �Ľ�Į ���̽� ����
  - �⺻������ <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">public</span> ������ ������� ������, ���� <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">������Ƽ</span>�� ����
- bool Ÿ���� ���¸� ��Ÿ���� ���� <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">Is</span> �� ���δ�.
  - ���¸� ����/ǥ���ϱ� ������ �ƴ� ��� ������ �ʾƵ� ����
- �������� ��� ��Ī�� ����� ��� �ּ��� ǥ���Ѵ�.
- Ÿ���� �ﰢ �� �� ���� ��� <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">var Ÿ���� ����� ����
- ����� ���� �빮�ڷ� ǥ��, �ʿ��Ѱ�� ����ٸ� ���

```csharp
public class Example
{
  // ��� ����
  private const int INIT_POOL_COUNT = 5;

  // bool ����
  private bool _isGrounded;

  // public ���� ����
  public bool IsAlive { get; private set; }
  public int EventStep { get; private set; }

  // private ���� ����
  private float _maxHp;
  private GameObject _target;

  private void Foo()
  {
		    // �ڵ� ������ Ÿ���� �ٷ� �Ǵܵ��� �ʴ� ��� var ����� �����Ѵ�
      var hit = _target;
      var targetList = new List<GameObject>();
  }
}
```

  <br>

### `�Լ�`

- �Լ��� : �Ľ�Į ���̽�, ���������� �ۼ��Ѵ�.
- �Ű����� : ī�����̽��� �����ϵ� �Ű��������� ǥ���ϱ� ���� m\_ ǥ�⸦ ���δ�
- �Լ� �� ������ ��� ������ ��� ���ȭ
- �Լ� ������ �� ����� ������ ������ �������� �и�
- �Լ� �ۼ� �� ��� ������ <span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">&lt;summary&gt;</span> �ּ��� Ȱ���Ѵ�
- �Լ� ������ �ʿ� �̻��� �ΰ������� �ۼ����� �ʴ´�.

```csharp
ex>
public class Player : MonoBehaviour
{
  private void UseItem(CharacterController m_target)
  {

  }

  /// <summary>
  /// ������ ȹ�� �޼���
  /// </summary>
  public void GetItem(Item m_item)
  {
  }

  private void PlayerHpDown(float m_damage)
  {
      // �ʿ� �̻��� �ΰ������� ����
      // Player Ŭ���� ������ Player���� ������ �������� ����� �Լ���
      // 'Player'�� ���� �ʾƵ� �ȴ�.
  }

  private void HpDown(float m_damage)
  {
      // ���� �Լ���
  }
}
```

  <br>

### `�������̽�`

- �������̽� �ۼ� �� �̸� �տ� ���ĺ� �빮�� 'I'�߰�

```csharp
public Interface IAction{ ... }
```

  <br>

### `Enum`

- Ŭ����, ����ü��� ȥ���� �����ϱ� ���� ������ ���� ������ ��Ȯ�� �ۼ��Ѵ�.
- Ŭ�������� ������ ���� �տ� 'E\_'�� ���δ�.
- ���ԵǴ� �Ӽ��� �׻� �Ľ�Į ���̽��� �ۼ��Ѵ�.
- ��ȸ�� ���� ������ ������ Size�� �����Ѵ�.

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

### `[���� �������]`

- 25�� 04�� 29�� / �ۼ��� : �嵿��

<!--
? ��Ÿ�� ���ø�
<span style="color:#ff5c5c; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">public</span>
<span style="color:#ff9c00; background-color:#2d2d2d; padding:2px 4px; border-radius:4px;">summary</span>
-->
