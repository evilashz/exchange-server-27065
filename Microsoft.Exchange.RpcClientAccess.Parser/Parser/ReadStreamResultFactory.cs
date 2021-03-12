using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000113 RID: 275
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ReadStreamResultFactory : ResultFactory
	{
		// Token: 0x06000587 RID: 1415 RVA: 0x000104B7 File Offset: 0x0000E6B7
		internal ReadStreamResultFactory()
		{
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x000104BF File Offset: 0x0000E6BF
		public override RopResult CreateStandardFailedResult(ErrorCode errorCode)
		{
			return this.CreateFailedResult(errorCode);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000104C8 File Offset: 0x0000E6C8
		public RopResult CreateFailedResult(ErrorCode errorCode)
		{
			return new ReadStreamResult(errorCode, Array<byte>.EmptySegment);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x000104D5 File Offset: 0x0000E6D5
		public RopResult CreateSuccessfulResult(ArraySegment<byte> dataSegment)
		{
			return new ReadStreamResult(ErrorCode.None, dataSegment);
		}
	}
}
