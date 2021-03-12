using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200013D RID: 317
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UploadStateStreamContinueResultFactory : StandardResultFactory
	{
		// Token: 0x060005FB RID: 1531 RVA: 0x00010FCB File Offset: 0x0000F1CB
		internal UploadStateStreamContinueResultFactory() : base(RopId.UploadStateStreamContinue)
		{
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00010FD5 File Offset: 0x0000F1D5
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.UploadStateStreamContinue, ErrorCode.None);
		}
	}
}
