using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructure.LinkedList
{
    /// <summary>
    /// 이중 연결 리스트 - 플레이어 인벤토리, 스킬 쿨다운 큐에 활용
    /// </summary>
    public class DoublyLinkedList<T> : IEnumerable<T>
    {
        public class Node
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

        private Node head;
        private Node tail;
        private int count;

        public int Count => count;
        public bool IsEmpty => head == null;
        public Node First => head;
        public Node Last => tail;

        // O(1) - 맨 앞 삽입
        public Node AddFirst(T data)
        {
            var newNode = new Node(data);

            if (IsEmpty)
            {
                head = tail = newNode;
            }
            else
            {
                newNode.Next = head;
                head.Prev = newNode;
                head = newNode;
            }
            count++;
            return newNode;
        }

        // O(1) - 맨 뒤 삽입
        public Node AddLast(T data)
        {
            var newNode = new Node(data);

            if (IsEmpty)
            {
                head = tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }
            count++;
            return newNode;
        }

        // O(1) - 특정 노드 뒤에 삽입
        public Node AddAfter(Node node, T data)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            var newNode = new Node(data);
            newNode.Next = node.Next;
            newNode.Prev = node;

            if (node.Next != null)
                node.Next.Prev = newNode;
            else
                tail = newNode; // node가 tail이었던 경우

            node.Next = newNode;
            count++;
            return newNode;
        }

        // O(1) - 특정 노드 삭제 (노드 참조 있을 때)
        public void Remove(Node node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if (node.Prev != null)
                node.Prev.Next = node.Next;
            else
                head = node.Next; // node가 head인 경우

            if (node.Next != null)
                node.Next.Prev = node.Prev;
            else
                tail = node.Prev; // node가 tail인 경우

            count--;
        }

        // O(n) - 값으로 첫 번째 노드 찾기
        public Node Find(T data)
        {
            var current = head;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Data, data))
                    return current;
                current = current.Next;
            }
            return null;
        }

        // 양방향 순회 가능
        public IEnumerable<T> Reverse()
        {
            var current = tail;
            while (current != null)
            {
                yield return current.Data;
                current = current.Prev;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}