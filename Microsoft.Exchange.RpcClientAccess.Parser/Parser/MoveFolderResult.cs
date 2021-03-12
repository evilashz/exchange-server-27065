using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000271 RID: 625
	internal sealed class MoveFolderResult : RopResult
	{
		// Token: 0x06000D72 RID: 3442 RVA: 0x00028F11 File Offset: 0x00027111
		internal MoveFolderResult(ErrorCode errorCode, bool isPartiallyCompleted, uint destinationObjectHandleIndex) : base(RopId.MoveFolder, errorCode, null)
		{
			this.isPartiallyCompleted = isPartiallyCompleted;
			if (errorCode == ErrorCode.DestinationNullObject)
			{
				this.destinationObjectHandleIndex = destinationObjectHandleIndex;
			}
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00028F33 File Offset: 0x00027133
		internal MoveFolderResult(Reader reader) : base(reader)
		{
			if (base.ErrorCode == ErrorCode.DestinationNullObject)
			{
				this.destinationObjectHandleIndex = reader.ReadUInt32();
			}
			this.isPartiallyCompleted = reader.ReadBool();
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x00028F61 File Offset: 0x00027161
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			if (base.ErrorCode == ErrorCode.DestinationNullObject)
			{
				writer.WriteUInt32(this.destinationObjectHandleIndex);
			}
			writer.WriteBool(this.isPartiallyCompleted);
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x00028F8F File Offset: 0x0002718F
		internal bool IsPartiallyCompleted
		{
			get
			{
				return this.isPartiallyCompleted;
			}
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00028F97 File Offset: 0x00027197
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Partial=").Append(this.isPartiallyCompleted);
		}

		// Token: 0x04000712 RID: 1810
		private readonly bool isPartiallyCompleted;

		// Token: 0x04000713 RID: 1811
		private uint destinationObjectHandleIndex;
	}
}
