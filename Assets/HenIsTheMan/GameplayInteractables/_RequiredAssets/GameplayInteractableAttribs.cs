using Genesis.Wisdom;
using UnityEngine;

namespace FoxHen {
	[CreateAssetMenu(
		fileName = nameof(GameplayInteractableAttribs),
		menuName = StrHelper.scriptableObjsFolderPath + nameof(GameplayInteractableAttribs)
	)]
	internal sealed class GameplayInteractableAttribs: ScriptableObject {
		[SerializeField]
		internal LayerMask layerMask;

		[SerializeField]
		internal bool shldLifetimeDecreaseOverTime;

		[SerializeField]
		internal float maxLifetime;

		internal float currLifetime;
	}
}