using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200036F RID: 879
	internal sealed class StandardRopResult : RopResult
	{
		// Token: 0x0600157B RID: 5499 RVA: 0x00037A10 File Offset: 0x00035C10
		internal StandardRopResult(RopId ropId, ErrorCode errorCode) : base(ropId, errorCode, null)
		{
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x00037A1B File Offset: 0x00035C1B
		internal StandardRopResult(Reader reader) : base(reader)
		{
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x00037A24 File Offset: 0x00035C24
		internal static RopResult ParseFailResult(Reader reader)
		{
			return new StandardRopResult(reader);
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x00037A2C File Offset: 0x00035C2C
		internal static RopResult ParseSuccessResult(Reader reader)
		{
			return new StandardRopResult(reader);
		}
	}
}
