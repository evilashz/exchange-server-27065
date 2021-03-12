using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000268 RID: 616
	internal sealed class SuccessfulImportMessageChangePartialResult : SuccessfulImportMessageChangeResultBase
	{
		// Token: 0x06000D4D RID: 3405 RVA: 0x00028BAF File Offset: 0x00026DAF
		internal SuccessfulImportMessageChangePartialResult(IServerObject serverObject, StoreId messageId) : base(RopId.ImportMessageChangePartial, serverObject, messageId)
		{
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00028BBE File Offset: 0x00026DBE
		internal SuccessfulImportMessageChangePartialResult(Reader reader) : base(reader)
		{
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00028BC7 File Offset: 0x00026DC7
		internal static SuccessfulImportMessageChangePartialResult Parse(Reader reader)
		{
			return new SuccessfulImportMessageChangePartialResult(reader);
		}
	}
}
