using System.Collections.Generic;
using UnityEngine;

public class LinkObjController : MonoBehaviour
{
    private LinkedList<FixedObject> _fixedObjs;

    private void Awake()
    {
        _fixedObjs = new LinkedList<FixedObject>();
        RegistObjects();
    }

    /// <summary>
    /// 자식 객체 중 FixedObject 오브젝트를 확인해 연결 관계를 형성합니다.
    /// </summary>
    private void RegistObjects()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent<FixedObject>(out FixedObject obj))
            {
                _fixedObjs.AddLast(obj);
            }
        }
    }

    /// <summary>
    /// FixedObject 의 파괴 신호를 전달받습니다.
    /// 파괴 오브젝트의 이후의 오브젝트들에게 고정 메서드를 호출시킵니다.
    /// </summary>
    public void SendDestroySignal(FixedObject m_obj)
    {
        LinkedListNode<FixedObject> node = _fixedObjs.First;
        LinkedListNode<FixedObject> tempNode;

        while (true)
        {
            if (node == null) break;

            if (node.Value == m_obj) // 파괴되는 노드를 찾았다면
            {
                tempNode = node.Next; // 임시 노드를 해당 노드의 다음노드로 설정

                while (true) // 해당 노드 이후의 오브젝트를 고정시키기 위함
                {
                    if (tempNode == null) break; // 임시노드가 없다면 중단

                    tempNode.Value.Drop(); // 대상 노드 오브젝트를 떨어지게

                    tempNode = tempNode.Next; // 다음 노드로 전환
                }

                _fixedObjs.Remove(node); // 파괴 대상 파괴진행
                return; // 로직 종료
            }

            node = node.Next;
        }
    }
}
