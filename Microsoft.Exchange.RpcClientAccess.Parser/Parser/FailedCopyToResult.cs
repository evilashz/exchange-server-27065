using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200022E RID: 558
	internal sealed class FailedCopyToResult : RopResult
	{
		// Token: 0x06000C31 RID: 3121 RVA: 0x00026D20 File Offset: 0x00024F20
		internal FailedCopyToResult(ErrorCode errorCode, uint destinationObjectHandleIndex) : base(RopId.CopyTo, errorCode, null)
		{
			if (errorCode == ErrorCode.DestinationNullObject)
			{
				this.destinationObjectHandleIndex = destinationObjectHandleIndex;
			}
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00026D3B File Offset: 0x00024F3B
		internal FailedCopyToResult(Reader reader) : base(reader)
		{
			if (ErrorCode.DestinationNullObject == base.ErrorCode)
			{
				this.destinationObjectHandleIndex = reader.ReadUInt32();
			}
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00026D5D File Offset: 0x00024F5D
		internal static FailedCopyToResult Parse(Reader reader)
		{
			return new FailedCopyToResult(reader);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00026D65 File Offset: 0x00024F65
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			if (base.ErrorCode == ErrorCode.DestinationNullObject)
			{
				writer.WriteUInt32(this.destinationObjectHandleIndex);
			}
		}

		// Token: 0x040006C0 RID: 1728
		private uint destinationObjectHandleIndex;
	}
}
