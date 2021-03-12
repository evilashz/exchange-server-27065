using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000120 RID: 288
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SeekRowResultFactory : StandardResultFactory
	{
		// Token: 0x060005B9 RID: 1465 RVA: 0x00010C8B File Offset: 0x0000EE8B
		internal SeekRowResultFactory() : base(RopId.SeekRow)
		{
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00010C95 File Offset: 0x0000EE95
		public RopResult CreateSuccessfulResult(bool soughtLessThanRequested, int rowsSought)
		{
			return new SuccessfulSeekRowResult(soughtLessThanRequested, rowsSought);
		}
	}
}
