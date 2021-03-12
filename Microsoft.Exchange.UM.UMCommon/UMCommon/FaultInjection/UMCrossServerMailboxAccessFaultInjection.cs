using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCommon.FaultInjection
{
	// Token: 0x02000079 RID: 121
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UMCrossServerMailboxAccessFaultInjection
	{
		// Token: 0x06000458 RID: 1112 RVA: 0x0000F212 File Offset: 0x0000D412
		internal static bool TryCreateException(string exceptionType, ref Exception exception)
		{
			return false;
		}

		// Token: 0x040002E9 RID: 745
		internal const uint UseEWSForLocalMailboxAccess = 3576048957U;
	}
}
