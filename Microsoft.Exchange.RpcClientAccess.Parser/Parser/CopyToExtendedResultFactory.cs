using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000B9 RID: 185
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CopyToExtendedResultFactory : ResultFactory
	{
		// Token: 0x06000443 RID: 1091 RVA: 0x0000EA26 File Offset: 0x0000CC26
		internal CopyToExtendedResultFactory(uint destinationObjectHandleIndex)
		{
			this.destinationObjectHandleIndex = destinationObjectHandleIndex;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000EA35 File Offset: 0x0000CC35
		public override RopResult CreateStandardFailedResult(ErrorCode errorCode)
		{
			return this.CreateFailedResult(errorCode);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000EA3E File Offset: 0x0000CC3E
		public RopResult CreateFailedResult(ErrorCode errorCode)
		{
			return new FailedCopyToExtendedResult(errorCode, this.destinationObjectHandleIndex);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000EA4C File Offset: 0x0000CC4C
		public RopResult CreateSuccessfulResult(PropertyProblem[] propertyProblems)
		{
			return new SuccessfulCopyToExtendedResult(propertyProblems);
		}

		// Token: 0x040002D5 RID: 725
		private readonly uint destinationObjectHandleIndex;
	}
}
