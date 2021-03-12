using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000267 RID: 615
	internal class SuccessfulImportMessageChangeResultBase : RopResult
	{
		// Token: 0x06000D49 RID: 3401 RVA: 0x00028B4F File Offset: 0x00026D4F
		internal SuccessfulImportMessageChangeResultBase(RopId ropId, IServerObject serverObject, StoreId messageId) : base(ropId, ErrorCode.None, serverObject)
		{
			if (serverObject == null)
			{
				throw new ArgumentNullException("serverObject");
			}
			this.messageId = messageId;
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00028B6F File Offset: 0x00026D6F
		internal SuccessfulImportMessageChangeResultBase(Reader reader) : base(reader)
		{
			this.messageId = StoreId.Parse(reader);
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x00028B84 File Offset: 0x00026D84
		internal StoreId MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00028B8C File Offset: 0x00026D8C
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			this.messageId.Serialize(writer);
		}

		// Token: 0x04000709 RID: 1801
		private readonly StoreId messageId;
	}
}
