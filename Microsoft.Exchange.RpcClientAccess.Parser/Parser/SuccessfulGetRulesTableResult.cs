using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200025F RID: 607
	internal sealed class SuccessfulGetRulesTableResult : RopResult
	{
		// Token: 0x06000D23 RID: 3363 RVA: 0x00028851 File Offset: 0x00026A51
		internal SuccessfulGetRulesTableResult(IServerObject serverObject) : base(RopId.GetRulesTable, ErrorCode.None, serverObject)
		{
			if (serverObject == null)
			{
				throw new ArgumentNullException("serverObject");
			}
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0002886B File Offset: 0x00026A6B
		internal SuccessfulGetRulesTableResult(Reader reader) : base(reader)
		{
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00028874 File Offset: 0x00026A74
		internal static SuccessfulGetRulesTableResult Parse(Reader reader)
		{
			return new SuccessfulGetRulesTableResult(reader);
		}
	}
}
