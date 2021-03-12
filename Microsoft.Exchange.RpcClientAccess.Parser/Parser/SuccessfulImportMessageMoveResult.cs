using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200026A RID: 618
	internal sealed class SuccessfulImportMessageMoveResult : RopResult
	{
		// Token: 0x06000D53 RID: 3411 RVA: 0x00028BEC File Offset: 0x00026DEC
		internal SuccessfulImportMessageMoveResult(StoreId messageId) : base(RopId.ImportMessageMove, ErrorCode.None, null)
		{
			this.messageId = messageId;
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00028BFF File Offset: 0x00026DFF
		internal SuccessfulImportMessageMoveResult(Reader reader) : base(reader)
		{
			this.messageId = StoreId.Parse(reader);
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x00028C14 File Offset: 0x00026E14
		public StoreId MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x00028C1C File Offset: 0x00026E1C
		internal static SuccessfulImportMessageMoveResult Parse(Reader reader)
		{
			return new SuccessfulImportMessageMoveResult(reader);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00028C24 File Offset: 0x00026E24
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			this.messageId.Serialize(writer);
		}

		// Token: 0x0400070A RID: 1802
		private readonly StoreId messageId;
	}
}
