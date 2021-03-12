using System;
using Microsoft.Exchange.Cluster.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000F8 RID: 248
	internal static class EventUtil
	{
		// Token: 0x06000A03 RID: 2563 RVA: 0x0002EB7F File Offset: 0x0002CD7F
		public static string TruncateStringInput(string str, int maxLength = 32766)
		{
			if (str != null && str.Length > maxLength)
			{
				return string.Format("{0}{1}", StringUtil.Truncate(str, maxLength - "...".Length), "...");
			}
			return str;
		}

		// Token: 0x0400044A RID: 1098
		private const int MaxEventStringLength = 32766;

		// Token: 0x0400044B RID: 1099
		private const string TruncatedStringIndicator = "...";

		// Token: 0x0400044C RID: 1100
		public const int SoftLimitMaxEventStringLength = 16383;
	}
}
