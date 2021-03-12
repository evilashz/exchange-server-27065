using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000D8 RID: 216
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetAllPerUserLongTermIdsResultFactory : StandardResultFactory
	{
		// Token: 0x060004AD RID: 1197 RVA: 0x0000F0D6 File Offset: 0x0000D2D6
		internal GetAllPerUserLongTermIdsResultFactory(int availableBufferSize) : base(RopId.GetAllPerUserLongTermIds)
		{
			this.availableBufferSize = availableBufferSize;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000F0E8 File Offset: 0x0000D2E8
		public PerUserDataCollector CreatePerUserDataCollector()
		{
			int num = 6;
			return new PerUserDataCollector(this.availableBufferSize - num);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000F104 File Offset: 0x0000D304
		public RopResult CreateSuccessfulResult(PerUserDataCollector perUserDataCollector, bool finished)
		{
			return new SuccessfulGetAllPerUserLongTermIdsResult(perUserDataCollector, finished);
		}

		// Token: 0x040002E7 RID: 743
		private readonly int availableBufferSize;
	}
}
