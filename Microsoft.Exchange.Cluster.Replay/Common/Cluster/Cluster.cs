using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Common.Cluster
{
	// Token: 0x020000CE RID: 206
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Cluster
	{
		// Token: 0x0600085F RID: 2143 RVA: 0x00028626 File Offset: 0x00026826
		public static bool StringIEquals(string str1, string str2)
		{
			return string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);
		}
	}
}
