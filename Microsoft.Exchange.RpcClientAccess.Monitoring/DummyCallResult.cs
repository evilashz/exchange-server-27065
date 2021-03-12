using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200000B RID: 11
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class DummyCallResult : EmsmdbCallResult
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002725 File Offset: 0x00000925
		public DummyCallResult(Exception exception) : base(exception, ErrorCode.None, null)
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002730 File Offset: 0x00000930
		public DummyCallResult(ErrorCode errorCode) : base(null, errorCode, null)
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000273B File Offset: 0x0000093B
		private DummyCallResult() : base(null, ErrorCode.None, null)
		{
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002746 File Offset: 0x00000946
		public static DummyCallResult CreateSuccessfulResult()
		{
			return DummyCallResult.successResult;
		}

		// Token: 0x0400001C RID: 28
		private static readonly DummyCallResult successResult = new DummyCallResult();
	}
}
