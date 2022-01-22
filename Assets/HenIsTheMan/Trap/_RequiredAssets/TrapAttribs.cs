using Genesis.Wisdom;
using UnityEngine;

namespace FoxHen {
	[CreateAssetMenu(
		fileName = nameof(TrapAttribs),
		menuName = StrHelper.scriptableObjsFolderPath + nameof(TrapAttribs)
	)]
	internal sealed class TrapAttribs: ScriptableObject {
		[SerializeField]
		internal LayerMask layerMask;

		[SerializeField]
		internal bool shldUseLifetime;

		[SerializeField]
		internal float lifetime;
	}
}