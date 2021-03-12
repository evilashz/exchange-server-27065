using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200022C RID: 556
	internal sealed class FailedCopyToExtendedResult : RopResult
	{
		// Token: 0x06000C27 RID: 3111 RVA: 0x00026C29 File Offset: 0x00024E29
		internal FailedCopyToExtendedResult(ErrorCode errorCode, uint destinationObjectHandleIndex) : base(RopId.CopyToExtended, errorCode, null)
		{
			if (errorCode == ErrorCode.DestinationNullObject)
			{
				this.destinationObjectHandleIndex = destinationObjectHandleIndex;
			}
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00026C47 File Offset: 0x00024E47
		internal FailedCopyToExtendedResult(Reader reader) : base(reader)
		{
			if (ErrorCode.DestinationNullObject == base.ErrorCode)
			{
				this.destinationObjectHandleIndex = reader.ReadUInt32();
			}
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x00026C69 File Offset: 0x00024E69
		internal static FailedCopyToExtendedResult Parse(Reader reader)
		{
			return new FailedCopyToExtendedResult(reader);
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x00026C71 File Offset: 0x00024E71
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			if (base.ErrorCode == ErrorCode.DestinationNullObject)
			{
				writer.WriteUInt32(this.destinationObjectHandleIndex);
			}
		}

		// Token: 0x040006BE RID: 1726
		private uint destinationObjectHandleIndex;
	}
}
