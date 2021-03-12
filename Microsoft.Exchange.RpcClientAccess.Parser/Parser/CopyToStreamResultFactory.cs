using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000BB RID: 187
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CopyToStreamResultFactory : ResultFactory
	{
		// Token: 0x06000451 RID: 1105 RVA: 0x0000EB7A File Offset: 0x0000CD7A
		internal CopyToStreamResultFactory(uint destinationObjectHandleIndex)
		{
			this.destinationObjectHandleIndex = destinationObjectHandleIndex;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000EB89 File Offset: 0x0000CD89
		public override RopResult CreateStandardFailedResult(ErrorCode errorCode)
		{
			return this.CreateFailedResult(errorCode, 0UL, 0UL);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000EB96 File Offset: 0x0000CD96
		public RopResult CreateFailedResult(ErrorCode errorCode, ulong bytesRead, ulong bytesWritten)
		{
			return new CopyToStreamResult(errorCode, bytesRead, bytesWritten, this.destinationObjectHandleIndex);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000EBA6 File Offset: 0x0000CDA6
		public RopResult CreateSuccessfulResult(ulong bytesRead, ulong bytesWritten)
		{
			return new CopyToStreamResult(ErrorCode.None, bytesRead, bytesWritten, 0U);
		}

		// Token: 0x040002DA RID: 730
		private readonly uint destinationObjectHandleIndex;
	}
}
