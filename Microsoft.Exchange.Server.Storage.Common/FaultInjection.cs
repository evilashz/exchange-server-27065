using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200003B RID: 59
	public static class FaultInjection
	{
		// Token: 0x0600045A RID: 1114 RVA: 0x0000C7F8 File Offset: 0x0000A9F8
		public static void InjectFault(Action action)
		{
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000C803 File Offset: 0x0000AA03
		public static void InjectFault(Hookable<Action> action)
		{
			if (action.Value != null)
			{
				action.Value();
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000C818 File Offset: 0x0000AA18
		public static ErrorCode InjectError(Hookable<Func<ErrorCode>> action)
		{
			if (action.Value == null)
			{
				return ErrorCode.NoError;
			}
			return action.Value();
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000C833 File Offset: 0x0000AA33
		public static T Replace<T>(Hookable<Func<T>> action, T original)
		{
			if (action.Value == null)
			{
				return original;
			}
			return action.Value();
		}
	}
}
