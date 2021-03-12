using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000EA RID: 234
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetIdsFromNamesResultFactory : StandardResultFactory
	{
		// Token: 0x060004DD RID: 1245 RVA: 0x0000F3B8 File Offset: 0x0000D5B8
		internal GetIdsFromNamesResultFactory() : base(RopId.GetIdsFromNames)
		{
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000F3C2 File Offset: 0x0000D5C2
		public RopResult CreateSuccessfulResult(PropertyId[] propertyIds)
		{
			return new SuccessfulGetIdsFromNamesResult(propertyIds);
		}
	}
}
