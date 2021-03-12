using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000270 RID: 624
	internal sealed class MoveCopyMessagesResult : RopResult
	{
		// Token: 0x06000D6D RID: 3437 RVA: 0x00028E6B File Offset: 0x0002706B
		internal MoveCopyMessagesResult(ErrorCode errorCode, bool isPartiallyCompleted, uint destinationObjectHandleIndex) : base(RopId.MoveCopyMessages, errorCode, null)
		{
			this.isPartiallyCompleted = isPartiallyCompleted;
			if (errorCode == ErrorCode.DestinationNullObject)
			{
				this.destinationObjectHandleIndex = destinationObjectHandleIndex;
			}
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00028E8D File Offset: 0x0002708D
		internal MoveCopyMessagesResult(Reader reader) : base(reader)
		{
			if (base.ErrorCode == ErrorCode.DestinationNullObject)
			{
				this.destinationObjectHandleIndex = reader.ReadUInt32();
			}
			this.isPartiallyCompleted = reader.ReadBool();
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x00028EBB File Offset: 0x000270BB
		internal bool IsPartiallyCompleted
		{
			get
			{
				return this.isPartiallyCompleted;
			}
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00028EC3 File Offset: 0x000270C3
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			if (base.ErrorCode == ErrorCode.DestinationNullObject)
			{
				writer.WriteUInt32(this.destinationObjectHandleIndex);
			}
			writer.WriteBool(this.isPartiallyCompleted);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00028EF1 File Offset: 0x000270F1
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Partial=").Append(this.isPartiallyCompleted);
		}

		// Token: 0x04000710 RID: 1808
		private readonly bool isPartiallyCompleted;

		// Token: 0x04000711 RID: 1809
		private uint destinationObjectHandleIndex;
	}
}
