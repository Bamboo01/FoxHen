using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bamboo.Utility
{
    public static class GameObjectExtensions
    {
        public static void SetActiveDelayed(this GameObject go, bool a, float t)
        {
            CoroutineManager.Instance.StartCoroutine(_SetActiveDelayed(t, a));

            IEnumerator _SetActiveDelayed(float t, bool a)
            {
                yield return new WaitForSeconds(t);
                go.SetActive(a);
            }
        }

        public static T[] GetAllObjectsOfType<T>(this GameObject go, bool returnInactive)
        {
            var allGameObjects = go.scene.GetRootGameObjects();
            List<T> listOfObjects = new List<T>();
            for (int j = 0; j < allGameObjects.Length; j++)
            {
                var a = allGameObjects[j];
                listOfObjects.AddRange(a.GetComponentsInChildren<T>(returnInactive));
            }
            return listOfObjects.ToArray();
        }
    }
}