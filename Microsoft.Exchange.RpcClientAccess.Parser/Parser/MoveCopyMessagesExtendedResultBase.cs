using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200026D RID: 621
	internal abstract class MoveCopyMessagesExtendedResultBase : RopResult
	{
		// Token: 0x06000D61 RID: 3425 RVA: 0x00028CEE File Offset: 0x00026EEE
		internal MoveCopyMessagesExtendedResultBase(RopId ropId, ErrorCode errorCode, bool isPartiallyCompleted, uint destinationObjectHandleIndex) : base(ropId, errorCode, null)
		{
			this.isPartiallyCompleted = isPartiallyCompleted;
			if (errorCode == ErrorCode.DestinationNullObject)
			{
				this.destinationObjectHandleIndex = destinationObjectHandleIndex;
			}
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00028D10 File Offset: 0x00026F10
		internal MoveCopyMessagesExtendedResultBase(Reader reader) : base(reader)
		{
			if (base.ErrorCode == ErrorCode.DestinationNullObject)
			{
				this.destinationObjectHandleIndex = reader.ReadUInt32();
			}
			this.isPartiallyCompleted = reader.ReadBool();
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x00028D3E File Offset: 0x00026F3E
		internal bool IsPartiallyCompleted
		{
			get
			{
				return this.isPartiallyCompleted;
			}
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00028D46 File Offset: 0x00026F46
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			if (base.ErrorCode == ErrorCode.DestinationNullObject)
			{
				writer.WriteUInt32(this.destinationObjectHandleIndex);
			}
			writer.WriteBool(this.isPartiallyCompleted);
		}

		// Token: 0x0400070C RID: 1804
		private readonly bool isPartiallyCompleted;

		// Token: 0x0400070D RID: 1805
		private uint destinationObjectHandleIndex;
	}
}
