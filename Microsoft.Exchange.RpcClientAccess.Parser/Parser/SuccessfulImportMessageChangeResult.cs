using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000269 RID: 617
	internal sealed class SuccessfulImportMessageChangeResult : SuccessfulImportMessageChangeResultBase
	{
		// Token: 0x06000D50 RID: 3408 RVA: 0x00028BCF File Offset: 0x00026DCF
		internal SuccessfulImportMessageChangeResult(IServerObject serverObject, StoreId messageId) : base(RopId.ImportMessageChange, serverObject, messageId)
		{
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00028BDB File Offset: 0x00026DDB
		internal SuccessfulImportMessageChangeResult(Reader reader) : base(reader)
		{
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00028BE4 File Offset: 0x00026DE4
		internal static SuccessfulImportMessageChangeResult Parse(Reader reader)
		{
			return new SuccessfulImportMessageChangeResult(reader);
		}
	}
}
