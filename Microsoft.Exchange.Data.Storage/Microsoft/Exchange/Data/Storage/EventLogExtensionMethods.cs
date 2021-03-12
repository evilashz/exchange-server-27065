using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000281 RID: 641
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class EventLogExtensionMethods
	{
		// Token: 0x06001AC2 RID: 6850 RVA: 0x0007DA46 File Offset: 0x0007BC46
		internal static string TruncateToUseInEventLog(this string originalString)
		{
			if (originalString.Length > 30720)
			{
				return originalString.Substring(0, 30720);
			}
			return originalString;
		}

		// Token: 0x040012C4 RID: 4804
		private const int MaxEventLogStringSize = 30720;
	}
}
