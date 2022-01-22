using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FoxHen
{
    public enum ItemType : uint
    {
        none = 0,
        bear_trap,
        snare_trap,
        glue_trap,
        magic_mushroom_trap,
        chicken_feed,
        chicken_shield,
        chicken_flash,
        berries,
        total
    }

    public class PlayerInventory : MonoBehaviour
    {
        public ItemType storedItem { get; private set; }
        public delegate void ActivateItem(ItemType item);
        public ActivateItem activateItemDelegate;

        private List<ItemType> itemTypeList;

        private void Awake()
        {
            storedItem = ItemType.none;
            itemTypeList = Enum.GetValues(typeof(ItemType)).Cast<ItemType>().ToList();
        }

        public void AddItem(ItemType item)
        {
            if(storedItem != ItemType.none)
            {
                storedItem = item;
            }
        }

        public void AddRandomItem()
        {
            if (storedItem != ItemType.none)
            {
                storedItem = itemTypeList[UnityEngine.Random.Range(0, itemTypeList.Count)];
            }
        }

        public void UseItem()
        {
            activateItemDelegate?.Invoke(storedItem);
        }
    }
}