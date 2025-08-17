using System;
using System.Collections.Generic;
using UnityEngine;
using DataStructure.LinkedList;

namespace Tests.LinkedList
{
    // 실행 취소 시스템 (이중 연결 리스트)
    public class UndoRedoSystem<T> where T : class
    {
        private DoublyLinkedList<T> history;
        private DoublyLinkedList<T>.Node currentState;
        private int maxHistorySize;

        public UndoRedoSystem(int maxSize = 50)
        {
            history = new DoublyLinkedList<T>();
            maxHistorySize = maxSize;
        }

        public void AddState(T state)
        {
            // 현재 위치 이후 모든 기록 삭제 (새 분기 생성)
            while (currentState != null && currentState.Next != null)
            {
                history.Remove(currentState.Next);
            }

            currentState = history.AddLast(state);

            // 최대 크기 제한
            while (history.Count > maxHistorySize)
            {
                history.Remove(history.First);
            }
        }

        public T Undo()
        {
            if (currentState?.Prev != null)
            {
                currentState = currentState.Prev;
                return currentState.Data;
            }
            return null;
        }

        public T Redo()
        {
            if (currentState?.Next != null)
            {
                currentState = currentState.Next;
                return currentState.Data;
            }
            return null;
        }
    }
}