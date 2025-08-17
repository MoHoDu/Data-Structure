using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataStructure.LinkedList
{
    /// <summary>
    /// 단일 연결 리스트 - 대화 시스템, 퀘스트 체인에 활용
    /// </summary>
    public class SinglyLinkedList<T> : IEnumerable<T>
    {
        private class Node
        {
            public T Data { get; set; }
            public Node Next { get; set; }

            public Node(T data)
            {
                Data = data;
                Next = null;
            }
        }

        private Node head;
        private Node tail;
        private int count;

        public int Count => count;
        public bool IsEmpty => head == null;

        // O(1) - 맨 앞 삽입
        public void AddFirst(T data)
        {
            var newNode = new Node(data);

            if (IsEmpty)
            {
                head = tail = newNode;
            }
            else
            {
                newNode.Next = head;
                head = newNode;
            }
            count++;
        }

        // O(1) - 맨 뒤 삽입 (tail 포인터 활용)
        public void AddLast(T data)
        {
            var newNode = new Node(data);

            if (IsEmpty)
            {
                head = tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                tail = newNode;
            }
            count++;
        }

        // O(1) - 맨 앞 삭제
        public T RemoveFirst()
        {
            if (IsEmpty)
                throw new InvalidOperationException("List is empty");

            T data = head.Data;
            head = head.Next;

            if (head == null)
                tail = null;

            count--;
            return data;
        }

        // O(n) - 맨 뒤 삭제
        public T RemoveLast()
        {
            if (IsEmpty)
                throw new InvalidOperationException("List is empty");

            T data = tail.Data;
            Node current = head;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Next.Data, data))
                {
                    current.Next = null;
                    tail = current;
                    count--;
                    return data;
                }

                current = current.Next;
            }

            throw new InvalidOperationException("Last node not found");
        }

        // O(n) - 값으로 검색
        public bool Contains(T data)
        {
            var current = head;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Data, data))
                    return true;
                current = current.Next;
            }
            return false;
        }

        // Iterator 구현 - foreach 지원
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