using System;
using System.Collections.Generic;
using UnityEngine;
using DataStructure.LinkedList;

namespace Tests.LinkedList
{
    // 무기 전환 시스템 (원형 연결 리스트)
    public class WeaponSwitchSystem : MonoBehaviour
    {
        [Serializable]
        public class Weapon
        {
            public string name;
            public GameObject prefab;
            public int ammo;
            public float fireRate;
            public Sprite icon;
        }

        private CircularLinkedList<Weapon> weapons;
        private Weapon currentWeapon;

        void Start()
        {
            weapons = new CircularLinkedList<Weapon>();
            // 무기 추가
            weapons.Add(new Weapon { name = "Pistol" });
            weapons.Add(new Weapon { name = "Shotgun" });
            weapons.Add(new Weapon { name = "Rifle" });
        }

        void Update()
        {
            // 마우스 휠로 무기 전환
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0)
            {
                currentWeapon = weapons.MoveNext();
                EquipWeapon(currentWeapon);
            }
            else if (scroll < 0)
            {
                currentWeapon = weapons.MovePrev();
                EquipWeapon(currentWeapon);
            }

            // 숫자 키로 직접 선택
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (weapons.MoveTo(new Weapon { name = "Pistol" }))
                    EquipWeapon(weapons.Current);
            }
        }

        void EquipWeapon(Weapon weapon)
        {
            Debug.Log($"Equipped: {weapon.name}");
            // 실제 무기 전환 로직
        }
    }
}