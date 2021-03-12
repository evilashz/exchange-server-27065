using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000101 RID: 257
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MoveCopyMessagesResultFactory : ResultFactory, IProgressResultFactory
	{
		// Token: 0x0600052D RID: 1325 RVA: 0x0000F96E File Offset: 0x0000DB6E
		internal MoveCopyMessagesResultFactory(byte logonId, uint destinationObjectHandleIndex)
		{
			this.destinationObjectHandleIndex = destinationObjectHandleIndex;
			this.logonId = logonId;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000F984 File Offset: 0x0000DB84
		internal MoveCopyMessagesResultFactory(object progressToken)
		{
			if (progressToken == null)
			{
				throw new ArgumentNullException("progressToken");
			}
			LogonDestinationHandleProgressToken logonDestinationHandleProgressToken = (LogonDestinationHandleProgressToken)progressToken;
			if (logonDestinationHandleProgressToken.RopId != RopId.MoveCopyMessages)
			{
				throw new ArgumentException("Wrong RopId in progress token:" + logonDestinationHandleProgressToken.RopId, "progressToken");
			}
			this.destinationObjectHandleIndex = logonDestinationHandleProgressToken.DestinationObjectHandleIndex;
			this.logonId = logonDestinationHandleProgressToken.LogonId;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0000F9F6 File Offset: 0x0000DBF6
		public static RopResult Parse(Reader reader)
		{
			return ResultFactory.ParseResultOrProgress(RopId.MoveCopyMessages, reader, (Reader resultReader) => new MoveCopyMessagesResult(resultReader));
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000FA1D File Offset: 0x0000DC1D
		public override RopResult CreateStandardFailedResult(ErrorCode errorCode)
		{
			return this.CreateFailedResult(errorCode, false);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0000FA27 File Offset: 0x0000DC27
		public RopResult CreateFailedResult(ErrorCode errorCode, bool isPartiallyCompleted)
		{
			return new MoveCopyMessagesResult(errorCode, isPartiallyCompleted, this.destinationObjectHandleIndex);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000FA36 File Offset: 0x0000DC36
		public RopResult CreateSuccessfulResult(bool isPartiallyCompleted)
		{
			return new MoveCopyMessagesResult(ErrorCode.None, isPartiallyCompleted, this.destinationObjectHandleIndex);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000FA45 File Offset: 0x0000DC45
		public object CreateProgressToken()
		{
			return new LogonDestinationHandleProgressToken(RopId.MoveCopyMessages, this.destinationObjectHandleIndex, this.logonId);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0000FA5A File Offset: 0x0000DC5A
		public RopResult CreateProgressResult(uint completedTaskCount, uint totalTaskCount)
		{
			return new SuccessfulProgressResult(this.logonId, completedTaskCount, totalTaskCount);
		}

		// Token: 0x04000301 RID: 769
		private readonly byte logonId;

		// Token: 0x04000302 RID: 770
		private readonly uint destinationObjectHandleIndex;
	}
}
