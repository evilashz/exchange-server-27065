using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200022A RID: 554
	internal sealed class FailedCopyPropertiesResult : RopResult
	{
		// Token: 0x06000C1E RID: 3102 RVA: 0x00026B3A File Offset: 0x00024D3A
		internal FailedCopyPropertiesResult(ErrorCode errorCode, uint destinationObjectHandleIndex) : base(RopId.CopyProperties, errorCode, null)
		{
			if (errorCode == ErrorCode.DestinationNullObject)
			{
				this.destinationObjectHandleIndex = destinationObjectHandleIndex;
			}
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00026B55 File Offset: 0x00024D55
		internal FailedCopyPropertiesResult(Reader reader) : base(reader)
		{
			if (ErrorCode.DestinationNullObject == base.ErrorCode)
			{
				this.destinationObjectHandleIndex = reader.ReadUInt32();
			}
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00026B77 File Offset: 0x00024D77
		internal static FailedCopyPropertiesResult Parse(Reader reader)
		{
			return new FailedCopyPropertiesResult(reader);
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00026B7F File Offset: 0x00024D7F
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			if (base.ErrorCode == ErrorCode.DestinationNullObject)
			{
				writer.WriteUInt32(this.destinationObjectHandleIndex);
			}
		}

		// Token: 0x040006BC RID: 1724
		private uint destinationObjectHandleIndex;
	}
}
