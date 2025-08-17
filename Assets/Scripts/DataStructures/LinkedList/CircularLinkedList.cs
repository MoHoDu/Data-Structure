using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructure.LinkedList
{
    /// <summary>
    /// 원형 연결 리스트 - 턴제 전투, 무기 전환, 순환 선택에 활용
    /// </summary>
    public class CircularLinkedList<T> : IEnumerable<T>
    {
        private class Node
        {
            public T Data { get; set; }
            public Node Next { get; set; }
            public Node Prev { get; set; }

            public Node(T data)
            {
                Data = data;
                Next = Prev = null;
            }
        }

        private Node current; // 현재 활성 노드
        private int count;

        public int Count => count;
        public bool IsEmpty => current == null;

        // Foreach 사용을 위한 Current 프로퍼티
        public T Current => current != null ? current.Data : default(T);

        // O(1) - 현재 위치 뒤에 삽입
        public void Add(T data)
        {
            var newNode = new Node(data);

            if (IsEmpty)
            {
                current = newNode;
                newNode.Next = newNode.Prev = newNode;
            }
            else
            {
                newNode.Next = current.Next;
                newNode.Prev = current;
                current.Next.Prev = newNode;
                current.Next = newNode;
            }
            count++;
        }

        // O(1) - 현재 노드 삭제하고 다음으로 이동
        public T RemoveCurrent()
        {
            if (IsEmpty)
                throw new InvalidOperationException("List is empty");

            T data = current.Data;

            if (count == 1)
            {
                current = null;
            }
            else
            {
                current.Prev.Next = current.Next;
                current.Next.Prev = current.Prev;
                current = current.Next;
            }

            count--;
            return data;
        }

        // O(1) - 다음 요소로 이동 (순환)
        public T MoveNext()
        {
            if (IsEmpty)
                throw new InvalidOperationException("List is empty");

            current = current.Next;
            return current.Data;
        }

        // O(1) - 이전 요소로 이동 (순환)
        public T MovePrev()
        {
            if (IsEmpty)
                throw new InvalidOperationException("List is empty");

            current = current.Prev;
            return current.Data;
        }

        // O(n) - 특정 데이터로 이동
        public bool MoveTo(T data)
        {
            if (IsEmpty) return false;

            var start = current;
            do
            {
                if (EqualityComparer<T>.Default.Equals(current.Data, data))
                    return true;
                current = current.Next;
            } while (current != start);

            return false;
        }

        // 한 바퀴 순회
        public IEnumerator<T> GetEnumerator()
        {
            if (IsEmpty) yield break;

            var start = current;
            do
            {
                yield return current.Data;
                current = current.Next;
            } while (current != start);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}