using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200013E RID: 318
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UploadStateStreamEndResultFactory : StandardResultFactory
	{
		// Token: 0x060005FD RID: 1533 RVA: 0x00010FDF File Offset: 0x0000F1DF
		internal UploadStateStreamEndResultFactory() : base(RopId.UploadStateStreamEnd)
		{
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00010FE9 File Offset: 0x0000F1E9
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.UploadStateStreamEnd, ErrorCode.None);
		}
	}
}
