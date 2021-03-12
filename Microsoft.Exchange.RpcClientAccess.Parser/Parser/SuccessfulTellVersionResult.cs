using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000371 RID: 881
	internal sealed class SuccessfulTellVersionResult : RopResult
	{
		// Token: 0x06001582 RID: 5506 RVA: 0x00037A62 File Offset: 0x00035C62
		internal SuccessfulTellVersionResult() : base(RopId.TellVersion, ErrorCode.None, null)
		{
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x00037A71 File Offset: 0x00035C71
		internal SuccessfulTellVersionResult(Reader reader) : base(reader)
		{
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x00037A7A File Offset: 0x00035C7A
		internal static SuccessfulTellVersionResult Parse(Reader reader)
		{
			return new SuccessfulTellVersionResult(reader);
		}
	}
}
