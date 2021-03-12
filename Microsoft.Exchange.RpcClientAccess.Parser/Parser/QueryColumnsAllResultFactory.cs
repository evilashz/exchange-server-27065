using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200010D RID: 269
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class QueryColumnsAllResultFactory : StandardResultFactory
	{
		// Token: 0x06000579 RID: 1401 RVA: 0x000103FA File Offset: 0x0000E5FA
		internal QueryColumnsAllResultFactory() : base(RopId.QueryColumnsAll)
		{
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00010404 File Offset: 0x0000E604
		public RopResult CreateSuccessfulResult(PropertyTag[] propertyTags)
		{
			return new SuccessfulQueryColumnsAllResult(propertyTags);
		}
	}
}
