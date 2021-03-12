using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200011D RID: 285
	internal static class RopSummaryResolver
	{
		// Token: 0x06000B28 RID: 2856 RVA: 0x00038C85 File Offset: 0x00036E85
		public static void Add(OperationType operationType, Func<byte, string> resolver)
		{
			RopSummaryResolver.resolvers[operationType] = resolver;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00038C93 File Offset: 0x00036E93
		public static bool ContainsKey(OperationType operationType)
		{
			return RopSummaryResolver.resolvers.ContainsKey(operationType);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00038CA0 File Offset: 0x00036EA0
		public static Func<byte, string> Get(OperationType operationType)
		{
			if (RopSummaryResolver.resolvers.ContainsKey(operationType))
			{
				return RopSummaryResolver.resolvers[operationType];
			}
			return null;
		}

		// Token: 0x04000623 RID: 1571
		private static Dictionary<OperationType, Func<byte, string>> resolvers = new Dictionary<OperationType, Func<byte, string>>(5);
	}
}
