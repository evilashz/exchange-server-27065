using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200026F RID: 623
	internal sealed class MoveCopyMessagesExtendedWithEntryIdsResult : MoveCopyMessagesExtendedResultBase
	{
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x00028D96 File Offset: 0x00026F96
		internal StoreId[] MessageIds
		{
			get
			{
				return this.messageIds;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x00028D9E File Offset: 0x00026F9E
		internal ulong[] ChangeNumbers
		{
			get
			{
				return this.changeNumbers;
			}
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00028DA8 File Offset: 0x00026FA8
		internal MoveCopyMessagesExtendedWithEntryIdsResult(ErrorCode errorCode, bool isPartiallyCompleted, StoreId[] messageIds, ulong[] changeNumbers, uint destinationObjectHandleIndex) : base(RopId.MoveCopyMessagesExtendedWithEntryIds, errorCode, isPartiallyCompleted, destinationObjectHandleIndex)
		{
			if ((messageIds == null && changeNumbers != null) || (messageIds != null && changeNumbers == null) || (messageIds != null && changeNumbers != null && messageIds.Length != changeNumbers.Length))
			{
				throw new ArgumentException("Number of messageIds elements doesn't match number of changeNumbers elements.");
			}
			this.messageIds = messageIds;
			this.changeNumbers = changeNumbers;
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00028DFC File Offset: 0x00026FFC
		internal MoveCopyMessagesExtendedWithEntryIdsResult(Reader reader) : base(reader)
		{
			uint length = reader.ReadUInt32();
			this.messageIds = reader.ReadStoreIdArray(length);
			this.changeNumbers = reader.ReadUInt64Array(length);
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00028E31 File Offset: 0x00027031
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt32((uint)((this.messageIds != null) ? this.messageIds.Length : 0));
			writer.WriteStoreIds(this.messageIds);
			writer.WriteUInt64Array(this.changeNumbers);
		}

		// Token: 0x0400070E RID: 1806
		private readonly StoreId[] messageIds;

		// Token: 0x0400070F RID: 1807
		private readonly ulong[] changeNumbers;
	}
}
