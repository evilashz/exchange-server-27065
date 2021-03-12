using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000259 RID: 601
	internal sealed class SuccessfulGetPermissionsTableResult : RopResult
	{
		// Token: 0x06000D01 RID: 3329 RVA: 0x000283E9 File Offset: 0x000265E9
		internal SuccessfulGetPermissionsTableResult(IServerObject serverObject) : base(RopId.GetPermissionsTable, ErrorCode.None, serverObject)
		{
			if (serverObject == null)
			{
				throw new ArgumentNullException("serverObject");
			}
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00028403 File Offset: 0x00026603
		internal SuccessfulGetPermissionsTableResult(Reader reader) : base(reader)
		{
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0002840C File Offset: 0x0002660C
		internal static SuccessfulGetPermissionsTableResult Parse(Reader reader)
		{
			return new SuccessfulGetPermissionsTableResult(reader);
		}
	}
}
