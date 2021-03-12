using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200022F RID: 559
	internal sealed class CopyToStreamResult : RopResult
	{
		// Token: 0x06000C35 RID: 3125 RVA: 0x00026D87 File Offset: 0x00024F87
		internal CopyToStreamResult(ErrorCode errorCode, ulong bytesRead, ulong bytesWritten, uint destinationObjectHandleIndex) : base(RopId.CopyToStream, errorCode, null)
		{
			this.bytesRead = bytesRead;
			this.bytesWritten = bytesWritten;
			if (errorCode == ErrorCode.DestinationNullObject)
			{
				this.destinationObjectHandleIndex = destinationObjectHandleIndex;
			}
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00026DB1 File Offset: 0x00024FB1
		internal CopyToStreamResult(Reader reader) : base(reader)
		{
			if (base.ErrorCode == ErrorCode.DestinationNullObject)
			{
				this.destinationObjectHandleIndex = reader.ReadUInt32();
			}
			this.bytesRead = reader.ReadUInt64();
			this.bytesWritten = reader.ReadUInt64();
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x00026DEB File Offset: 0x00024FEB
		public ulong BytesRead
		{
			get
			{
				return this.bytesRead;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x00026DF3 File Offset: 0x00024FF3
		public ulong BytesWritten
		{
			get
			{
				return this.bytesWritten;
			}
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x00026DFB File Offset: 0x00024FFB
		internal static RopResult Parse(Reader reader)
		{
			return new CopyToStreamResult(reader);
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00026E03 File Offset: 0x00025003
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			if (base.ErrorCode == ErrorCode.DestinationNullObject)
			{
				writer.WriteUInt32(this.destinationObjectHandleIndex);
			}
			writer.WriteUInt64(this.bytesRead);
			writer.WriteUInt64(this.bytesWritten);
		}

		// Token: 0x040006C1 RID: 1729
		private ulong bytesRead;

		// Token: 0x040006C2 RID: 1730
		private ulong bytesWritten;

		// Token: 0x040006C3 RID: 1731
		private uint destinationObjectHandleIndex;
	}
}
