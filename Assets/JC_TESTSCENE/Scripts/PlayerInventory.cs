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

        private List<ItemType> henItemTypeList = new List<ItemType>();
        private List<ItemType> foxItemTypeList = new List<ItemType>();

        private void Awake()
        {
            storedItem = ItemType.none;
            henItemTypeList = Enum.GetValues(typeof(ItemType)).Cast<ItemType>().ToList();
            foxItemTypeList = Enum.GetValues(typeof(ItemType)).Cast<ItemType>().ToList();
            henItemTypeList.Remove(ItemType.berries);
            foxItemTypeList.Remove(ItemType.chicken_shield);
            foxItemTypeList.Remove(ItemType.chicken_flash);
            foxItemTypeList.Remove(ItemType.chicken_feed);
        }

        public bool AddItem(ItemType item)
        {
            if(storedItem == ItemType.none)
            {
                storedItem = item;
                return true;
            }
            return false;
        }

        public bool AddRandomItem(bool type)
        {
            if (storedItem == ItemType.none)
            {
                //ifelse henfox
                if (type)
                {
                    storedItem = foxItemTypeList[UnityEngine.Random.Range((int)ItemType.none + 1, foxItemTypeList.Count)];
                }
                else
                {
                    storedItem = henItemTypeList[UnityEngine.Random.Range((int)ItemType.none + 1, henItemTypeList.Count)];
                }
                UseItem();
                return true;
            }
            return false;
        }

        public void UseItem()
        {
            Debug.Log(storedItem.ToString());
            activateItemDelegate?.Invoke(storedItem);
            storedItem = ItemType.none;
        }
    }
}