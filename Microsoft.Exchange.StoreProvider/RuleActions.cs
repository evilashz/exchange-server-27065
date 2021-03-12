using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x02000241 RID: 577
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class RuleActions
	{
		// Token: 0x06000A16 RID: 2582 RVA: 0x00030F6C File Offset: 0x0002F16C
		internal unsafe static RuleAction[] Unmarshal(IntPtr Handle)
		{
			_Actions* ptr = (_Actions*)Handle.ToPointer();
			if (1U != ptr->ulVersion)
			{
				return null;
			}
			RuleAction[] array = new RuleAction[ptr->cActions];
			_Action* lpAction = ptr->lpAction;
			for (uint num = 0U; num < ptr->cActions; num += 1U)
			{
				array[(int)((UIntPtr)num)] = RuleAction.Unmarshal(lpAction + (ulong)num * (ulong)((long)sizeof(_Action)) / (ulong)sizeof(_Action));
			}
			return array;
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00030FCC File Offset: 0x0002F1CC
		internal static int GetBytesToMarshal(params RuleAction[] actions)
		{
			int num = _Actions.SizeOf + 7 & -8;
			if (actions != null)
			{
				for (int i = 0; i < actions.Length; i++)
				{
					num += actions[i].GetBytesToMarshal();
				}
			}
			return num;
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00031004 File Offset: 0x0002F204
		internal unsafe static void MarshalToNative(ref byte* pb, params RuleAction[] actions)
		{
			byte* ptr = pb;
			_Actions* ptr2 = pb;
			ptr2->ulVersion = 1U;
			ptr += (_Actions.SizeOf + 7 & -8);
			if (actions != null)
			{
				ptr2->cActions = (uint)actions.Length;
				ptr2->lpAction = (_Action*)ptr;
				_Action* ptr3 = ptr2->lpAction;
				ptr += (IntPtr)(_Action.SizeOf + 7 & -8) * (IntPtr)actions.Length;
				foreach (RuleAction ruleAction in actions)
				{
					ruleAction.MarshalToNative(ptr3, ref ptr);
					ptr3++;
				}
			}
			else
			{
				ptr2->cActions = 0U;
			}
			pb = ptr;
		}
	}
}
