using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200026B RID: 619
	internal sealed class SuccessfulIncrementalConfigResult : RopResult
	{
		// Token: 0x06000D58 RID: 3416 RVA: 0x00028C47 File Offset: 0x00026E47
		internal SuccessfulIncrementalConfigResult(IServerObject synchronizer) : base(RopId.IncrementalConfig, ErrorCode.None, synchronizer)
		{
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00028C53 File Offset: 0x00026E53
		internal SuccessfulIncrementalConfigResult(Reader reader) : base(reader)
		{
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00028C5C File Offset: 0x00026E5C
		internal static SuccessfulIncrementalConfigResult Parse(Reader reader)
		{
			return new SuccessfulIncrementalConfigResult(reader);
		}
	}
}
