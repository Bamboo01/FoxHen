using System;
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
        speed,
        total
    }

    public class PlayerInventory : MonoBehaviour
    {
        public ItemType storedItem { get; private set; }
        public delegate void ActivateItem(ItemType item);
        public ActivateItem activateItemDelegate;

        private List<ItemType> henItemTypeList;
        private List<ItemType> foxItemTypeList;

        private void Awake()
        {
            storedItem = ItemType.none;
            henItemTypeList = Enum.GetValues(typeof(ItemType)).Cast<ItemType>().ToList();
            henItemTypeList.Remove(ItemType.berries);
            foxItemTypeList.Remove(ItemType.chicken_shield);
            foxItemTypeList.Remove(ItemType.chicken_flash);
            foxItemTypeList.Remove(ItemType.chicken_feed);
        }

        public bool AddItem(ItemType item)
        {
            if(storedItem != ItemType.none)
            {
                storedItem = item;
                return true;
            }
            return false;
        }

        public bool AddRandomItem()
        {
            if (storedItem != ItemType.none)
            {
                //ifelse henfox
                storedItem = henItemTypeList[UnityEngine.Random.Range((int)ItemType.none, henItemTypeList.Count)];
                storedItem = foxItemTypeList[UnityEngine.Random.Range((int)ItemType.none, foxItemTypeList.Count)];
                UseItem();
                return true;
            }
            return false;
        }

        public void UseItem()
        {
            activateItemDelegate?.Invoke(storedItem);
            storedItem = ItemType.none;
        }
    }
}