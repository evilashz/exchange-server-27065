using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000228 RID: 552
	internal sealed class CopyFolderResult : RopResult
	{
		// Token: 0x06000C13 RID: 3091 RVA: 0x00026A07 File Offset: 0x00024C07
		internal CopyFolderResult(ErrorCode errorCode, bool isPartiallyCompleted, uint destinationObjectHandleIndex) : base(RopId.CopyFolder, errorCode, null)
		{
			this.isPartiallyCompleted = isPartiallyCompleted;
			if (errorCode == ErrorCode.DestinationNullObject)
			{
				this.destinationObjectHandleIndex = destinationObjectHandleIndex;
			}
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00026A29 File Offset: 0x00024C29
		internal CopyFolderResult(Reader reader) : base(reader)
		{
			if (base.ErrorCode == ErrorCode.DestinationNullObject)
			{
				this.destinationObjectHandleIndex = reader.ReadUInt32();
			}
			this.isPartiallyCompleted = reader.ReadBool();
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x00026A57 File Offset: 0x00024C57
		internal bool IsPartiallyCompleted
		{
			get
			{
				return this.isPartiallyCompleted;
			}
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00026A5F File Offset: 0x00024C5F
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			if (base.ErrorCode == ErrorCode.DestinationNullObject)
			{
				writer.WriteUInt32(this.destinationObjectHandleIndex);
			}
			writer.WriteBool(this.isPartiallyCompleted);
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00026A8D File Offset: 0x00024C8D
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Partial=").Append(this.isPartiallyCompleted);
		}

		// Token: 0x040006B9 RID: 1721
		private readonly bool isPartiallyCompleted;

		// Token: 0x040006BA RID: 1722
		private uint destinationObjectHandleIndex;
	}
}
