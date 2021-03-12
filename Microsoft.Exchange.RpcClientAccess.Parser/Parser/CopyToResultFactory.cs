using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000BA RID: 186
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CopyToResultFactory : ResultFactory, IProgressResultFactory
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x0000EA54 File Offset: 0x0000CC54
		internal CopyToResultFactory(byte logonId, uint destinationObjectHandleIndex)
		{
			this.destinationObjectHandleIndex = destinationObjectHandleIndex;
			this.logonId = logonId;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000EA6C File Offset: 0x0000CC6C
		internal CopyToResultFactory(object progressToken)
		{
			if (progressToken == null)
			{
				throw new ArgumentNullException("progressToken");
			}
			LogonDestinationHandleProgressToken logonDestinationHandleProgressToken = (LogonDestinationHandleProgressToken)progressToken;
			if (logonDestinationHandleProgressToken.RopId != RopId.CopyTo)
			{
				throw new ArgumentException("Wrong RopId in progress token:" + logonDestinationHandleProgressToken.RopId, "progressToken");
			}
			this.destinationObjectHandleIndex = logonDestinationHandleProgressToken.DestinationObjectHandleIndex;
			this.logonId = logonDestinationHandleProgressToken.LogonId;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000EAE8 File Offset: 0x0000CCE8
		public static RopResult Parse(Reader reader)
		{
			return ResultFactory.ParseResultOrProgress(RopId.CopyTo, reader, (Reader resultReader) => new SuccessfulCopyToResult(resultReader), (Reader resultReader) => new FailedCopyToResult(resultReader));
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000EB37 File Offset: 0x0000CD37
		public override RopResult CreateStandardFailedResult(ErrorCode errorCode)
		{
			return this.CreateFailedResult(errorCode);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000EB40 File Offset: 0x0000CD40
		public RopResult CreateFailedResult(ErrorCode errorCode)
		{
			return new FailedCopyToResult(errorCode, this.destinationObjectHandleIndex);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000EB4E File Offset: 0x0000CD4E
		public RopResult CreateSuccessfulResult(PropertyProblem[] propertyProblems)
		{
			return new SuccessfulCopyToResult(propertyProblems);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000EB56 File Offset: 0x0000CD56
		public object CreateProgressToken()
		{
			return new LogonDestinationHandleProgressToken(RopId.CopyTo, this.destinationObjectHandleIndex, this.logonId);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000EB6B File Offset: 0x0000CD6B
		public RopResult CreateProgressResult(uint completedTaskCount, uint totalTaskCount)
		{
			return new SuccessfulProgressResult(this.logonId, completedTaskCount, totalTaskCount);
		}

		// Token: 0x040002D6 RID: 726
		private readonly byte logonId;

		// Token: 0x040002D7 RID: 727
		private readonly uint destinationObjectHandleIndex;
	}
}
