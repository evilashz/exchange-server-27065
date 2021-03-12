using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Nspi.Rfri;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000025 RID: 37
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RfriCallResult : RpcCallResult
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x0000410E File Offset: 0x0000230E
		public RfriCallResult(RpcException exception) : base(exception, ErrorCode.None, null)
		{
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004119 File Offset: 0x00002319
		public RfriCallResult(RfriStatus rfriStatus) : base(null, (ErrorCode)rfriStatus, null)
		{
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004124 File Offset: 0x00002324
		private RfriCallResult() : base(null, ErrorCode.None, null)
		{
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000412F File Offset: 0x0000232F
		public static RfriCallResult CreateSuccessfulResult()
		{
			return RfriCallResult.successResult;
		}

		// Token: 0x0400004D RID: 77
		private static readonly RfriCallResult successResult = new RfriCallResult();
	}
}
