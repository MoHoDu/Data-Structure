using System;
using System.Collections.Generic;
using UnityEngine;
using DataStructure.LinkedList;

namespace Tests.LinkedList
{
    // 대화 시스템 (단일 연결 리스트)
    public class DialogueSystem
    {
        [Serializable]
        public class DialogueNode
        {
            public string speaker;
            public string text;
            public float displayTime;
            public AudioClip voiceClip;
        }

        private SinglyLinkedList<DialogueNode> dialogueChain;
        private IEnumerator<DialogueNode> currentDialogue;

        public void LoadDialogue(DialogueNode[] nodes)
        {
            dialogueChain = new SinglyLinkedList<DialogueNode>();
            foreach (var node in nodes)
            {
                dialogueChain.AddLast(node);
            }
            currentDialogue = dialogueChain.GetEnumerator();
        }

        public DialogueNode GetNextDialogue()
        {
            if (currentDialogue != null && currentDialogue.MoveNext())
                return currentDialogue.Current;
            return null;
        }
    }
}