using System;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x02000038 RID: 56
	internal static class InputStateHelper
	{
		// Token: 0x06000278 RID: 632 RVA: 0x0000AA0D File Offset: 0x00008C0D
		public static bool IsAllowed(InputState state)
		{
			return state != InputState.NotAllowed;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000AA16 File Offset: 0x00008C16
		public static bool IsStarted(InputState state)
		{
			return InputStateHelper.IsAllowed(state) && state != InputState.NotStarted;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000AA29 File Offset: 0x00008C29
		public static bool IsComplete(InputState state)
		{
			return InputStateHelper.IsStarted(state) && state != InputState.StartedNotComplete;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000AA3C File Offset: 0x00008C3C
		public static bool IsUnambiguous(InputState state)
		{
			return state == InputState.StartedCompleteNotAmbiguous;
		}
	}
}
