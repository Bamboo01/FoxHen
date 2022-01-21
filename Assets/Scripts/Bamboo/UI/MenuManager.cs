using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bamboo.Utility;

namespace Bamboo.UI
{
    public class MenuManager : Singleton<MenuManager>
    {
        [SerializeField] private List<Menu> Menus;
        private Dictionary<string, GameObject> NameToGameObject;

        protected override void OnAwake()
        {
            base.OnAwake();
            _persistent = false;
            Menus = new List<Menu>();
            var allGameObjects = gameObject.scene.GetRootGameObjects();
            for (int j = 0; j < allGameObjects.Length; j++)
            {
                var go = allGameObjects[j];
                Menus.AddRange(go.GetComponentsInChildren<Menu>(true));
            }

            NameToGameObject = new Dictionary<string, GameObject>();
            foreach (Menu a in Menus)
            {
                a.OnAwake();
                NameToGameObject.Add(a.name, a.gameObject);
            }
        }

        public void AddMenu(Menu menu)
        {
            Menus.Add(menu);
            Debug.Log("Added a menu: " + menu.MenuName);
        }


        public void OpenMenu(Menu menu) => OpenMenu(menu.name);
        public void OpenMenu(string menuName)
        {
            bool menuFound = false;
            foreach (Menu a in Menus)
            {
                if (a.name == menuName)
                {
                    menuFound = true;
                    a.Open();
                }
            }

            if (!menuFound)
            {
                Debug.LogWarning("Attempted to open a menu: " + menuName + ", which currently does not exist!");
            }
        }

        public void OnlyOpenThisMenu(Menu menu) => OnlyOpenThisMenu(menu.name);
        public void OnlyOpenThisMenu(string menuName)
        {
            bool menuFound = false;
            foreach (Menu a in Menus)
            {
                if (a.name == menuName)
                {
                    menuFound = true;
                    a.Open();
                }
                else
                {
                    if (a.ignoreOpenOnlyOneCall)
                    {
                        continue;
                    }
                    a.Close();
                }
            }

            if (!menuFound)
            {
                Debug.LogWarning("Attempted to open a menu: " + menuName + ", which currently does not exist!");
            }
        }

        public void CloseMenu(Menu menu) => CloseMenu(menu.name);
        public void CloseMenu(string menuName)
        {
            bool menuFound = false;
            foreach (Menu a in Menus)
            {
                if (a.name == menuName)
                {
                    menuFound = true;
                    a.Close();
                }
            }
            if (!menuFound)
            {
                Debug.LogWarning("Attempted to close a menu: " + menuName + ", which currently does not exist!");
            }
        }
        public void CloseAllMenus()
        {
            foreach (Menu a in Menus)
            {
                if (a.ignoreCloseAllCall)
                {
                    continue;
                }
                a.Close();
            }
        }

        public void ZeroScaleMenu(string menu)
        {
            foreach (Menu a in Menus)
            {
                if (a.name == menu)
                {
                    a.gameObject.transform.localScale = Vector3.zero;
                }
            }
        }
        public void UpScaleMenu(string menu)
        {
            foreach (Menu a in Menus)
            {
                if (a.name == menu)
                {
                    a.gameObject.transform.localScale = Vector3.one;
                }
            }
        }

        public GameObject getMenuGameObject(string menuName)
        {
            if(NameToGameObject.TryGetValue(menuName, out GameObject a))
            {
                Debug.LogWarning("Attempted to get gameobject of a menu: " + menuName + ", which currently does not exist!");
            }
            return a;
        }
    }
}