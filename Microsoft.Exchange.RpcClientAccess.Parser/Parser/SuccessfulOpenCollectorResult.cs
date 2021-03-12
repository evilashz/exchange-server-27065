using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000274 RID: 628
	internal sealed class SuccessfulOpenCollectorResult : RopResult
	{
		// Token: 0x06000D85 RID: 3461 RVA: 0x0002912C File Offset: 0x0002732C
		internal SuccessfulOpenCollectorResult(IServerObject serverObject) : base(RopId.OpenCollector, ErrorCode.None, serverObject)
		{
			if (serverObject == null)
			{
				throw new ArgumentNullException("serverObject");
			}
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00029146 File Offset: 0x00027346
		internal SuccessfulOpenCollectorResult(Reader reader) : base(reader)
		{
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x0002914F File Offset: 0x0002734F
		internal static SuccessfulOpenCollectorResult Parse(Reader reader)
		{
			return new SuccessfulOpenCollectorResult(reader);
		}
	}
}
