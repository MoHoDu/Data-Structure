using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStructure.LinkedList;

namespace Tests.LinkedList
{
    // 턴제 전투 시스템 (원형 연결 리스트)
    public class TurnBasedCombat
    {
        public interface ICombatant
        {
            string Name { get; }
            float Speed { get; }
            bool IsAlive { get; }
            void TakeTurn();
        }

        private CircularLinkedList<ICombatant> turnOrder;

        public void InitializeBattle(List<ICombatant> combatants)
        {
            // 속도 기준 정렬
            combatants.Sort((a, b) => b.Speed.CompareTo(a.Speed));

            turnOrder = new CircularLinkedList<ICombatant>();
            foreach (var combatant in combatants)
            {
                turnOrder.Add(combatant);
            }
        }

        public IEnumerator BattleLoop()
        {
            while (turnOrder.Count > 1) // 최소 2명 이상
            {
                var current = turnOrder.Current;

                if (!current.IsAlive)
                {
                    turnOrder.RemoveCurrent();
                    continue;
                }

                current.TakeTurn();
                yield return new WaitForSeconds(1f); // 턴 딜레이

                turnOrder.MoveNext();
            }
        }
    }
}