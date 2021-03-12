using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCommon.FaultInjection
{
	// Token: 0x0200007C RID: 124
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UMReportingFaultInjection
	{
		// Token: 0x06000460 RID: 1120 RVA: 0x0000F296 File Offset: 0x0000D496
		internal static bool TryCreateException(string exceptionType, ref Exception exception)
		{
			return false;
		}

		// Token: 0x040002EF RID: 751
		public const uint UMReportingAssert = 3945147709U;
	}
}
