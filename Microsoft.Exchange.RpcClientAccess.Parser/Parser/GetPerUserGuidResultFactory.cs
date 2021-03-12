using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000E5 RID: 229
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetPerUserGuidResultFactory : StandardResultFactory
	{
		// Token: 0x060004D3 RID: 1235 RVA: 0x0000F271 File Offset: 0x0000D471
		internal GetPerUserGuidResultFactory() : base(RopId.GetPerUserGuid)
		{
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000F27B File Offset: 0x0000D47B
		public RopResult CreateSuccessfulResult(Guid databaseGuid)
		{
			return new SuccessfulGetPerUserGuidResult(databaseGuid);
		}
	}
}
