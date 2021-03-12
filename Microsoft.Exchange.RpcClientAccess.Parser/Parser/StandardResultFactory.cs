using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000AF RID: 175
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class StandardResultFactory : ResultFactory
	{
		// Token: 0x06000420 RID: 1056 RVA: 0x0000E76D File Offset: 0x0000C96D
		internal StandardResultFactory(RopId ropId)
		{
			this.ropId = ropId;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000E77C File Offset: 0x0000C97C
		public override RopResult CreateStandardFailedResult(ErrorCode errorCode)
		{
			return this.CreateFailedResult(errorCode);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000E785 File Offset: 0x0000C985
		public RopResult CreateFailedResult(ErrorCode errorCode)
		{
			return new StandardRopResult(this.ropId, errorCode);
		}

		// Token: 0x040002CD RID: 717
		private readonly RopId ropId;
	}
}
