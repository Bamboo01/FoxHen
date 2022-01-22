using Genesis.Wisdom;
using UnityEngine;

namespace FoxHen {
	[CreateAssetMenu(
		fileName = nameof(TrapAttribs),
		menuName = StrHelper.scriptableObjsFolderPath + nameof(TrapAttribs)
	)]
	internal sealed class TrapAttribs: ScriptableObject {
		[SerializeField]
		internal float maxHealth;

		[SerializeField]
		internal float currHealth;

		[SerializeField]
		internal float rangeDmgRange; //Dist rangeDmg will go

		[SerializeField]
		internal float rangeDmg;

		[SerializeField]
		internal float triggerRange; //Range in which the trap will be triggered

		[SerializeField]
		internal float triggerDmg;
	}
}