using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000261 RID: 609
	internal sealed class SuccessfulGetStoreStateResult : RopResult
	{
		// Token: 0x06000D2A RID: 3370 RVA: 0x000288C1 File Offset: 0x00026AC1
		internal SuccessfulGetStoreStateResult(StoreState storeState) : base(RopId.GetStoreState, ErrorCode.None, null)
		{
			this.storeState = storeState;
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x000288D4 File Offset: 0x00026AD4
		internal SuccessfulGetStoreStateResult(Reader reader) : base(reader)
		{
			this.storeState = (StoreState)reader.ReadUInt32();
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x000288E9 File Offset: 0x00026AE9
		internal static SuccessfulGetStoreStateResult Parse(Reader reader)
		{
			return new SuccessfulGetStoreStateResult(reader);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x000288F1 File Offset: 0x00026AF1
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt32((uint)this.storeState);
		}

		// Token: 0x04000703 RID: 1795
		private StoreState storeState;
	}
}
