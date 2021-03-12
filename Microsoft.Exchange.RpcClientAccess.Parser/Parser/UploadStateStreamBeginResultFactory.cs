using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200013C RID: 316
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UploadStateStreamBeginResultFactory : StandardResultFactory
	{
		// Token: 0x060005F9 RID: 1529 RVA: 0x00010FB7 File Offset: 0x0000F1B7
		internal UploadStateStreamBeginResultFactory() : base(RopId.UploadStateStreamBegin)
		{
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00010FC1 File Offset: 0x0000F1C1
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.UploadStateStreamBegin, ErrorCode.None);
		}
	}
}
