using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000265 RID: 613
	internal sealed class SuccessfulIdFromLongTermIdResult : RopResult
	{
		// Token: 0x06000D3E RID: 3390 RVA: 0x00028A5C File Offset: 0x00026C5C
		internal SuccessfulIdFromLongTermIdResult(StoreId storeId) : base(RopId.IdFromLongTermId, ErrorCode.None, null)
		{
			this.storeId = storeId;
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00028A6F File Offset: 0x00026C6F
		internal SuccessfulIdFromLongTermIdResult(Reader reader) : base(reader)
		{
			this.storeId = StoreId.Parse(reader);
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x00028A84 File Offset: 0x00026C84
		public StoreId StoreId
		{
			get
			{
				return this.storeId;
			}
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00028A8C File Offset: 0x00026C8C
		internal static SuccessfulIdFromLongTermIdResult Parse(Reader reader)
		{
			return new SuccessfulIdFromLongTermIdResult(reader);
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x00028A94 File Offset: 0x00026C94
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			this.storeId.Serialize(writer);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00028AB8 File Offset: 0x00026CB8
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" ID=").Append(this.storeId.ToString());
		}

		// Token: 0x04000707 RID: 1799
		private readonly StoreId storeId;
	}
}
