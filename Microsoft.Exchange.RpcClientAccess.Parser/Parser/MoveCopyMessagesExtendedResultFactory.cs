using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000FF RID: 255
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MoveCopyMessagesExtendedResultFactory : ResultFactory, IProgressResultFactory
	{
		// Token: 0x0600051B RID: 1307 RVA: 0x0000F762 File Offset: 0x0000D962
		internal MoveCopyMessagesExtendedResultFactory(byte logonId, uint destinationObjectHandleIndex)
		{
			this.destinationObjectHandleIndex = destinationObjectHandleIndex;
			this.logonId = logonId;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000F778 File Offset: 0x0000D978
		internal MoveCopyMessagesExtendedResultFactory(object progressToken)
		{
			if (progressToken == null)
			{
				throw new ArgumentNullException("progressToken");
			}
			LogonDestinationHandleProgressToken logonDestinationHandleProgressToken = (LogonDestinationHandleProgressToken)progressToken;
			if (logonDestinationHandleProgressToken.RopId != RopId.MoveCopyMessagesExtended)
			{
				throw new ArgumentException("Wrong RopId in progress token:" + logonDestinationHandleProgressToken.RopId, "progressToken");
			}
			this.destinationObjectHandleIndex = logonDestinationHandleProgressToken.DestinationObjectHandleIndex;
			this.logonId = logonDestinationHandleProgressToken.LogonId;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000F7ED File Offset: 0x0000D9ED
		public static RopResult Parse(Reader reader)
		{
			return ResultFactory.ParseResultOrProgress(RopId.MoveCopyMessagesExtended, reader, (Reader resultReader) => new MoveCopyMessagesExtendedResult(resultReader));
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000F817 File Offset: 0x0000DA17
		public override RopResult CreateStandardFailedResult(ErrorCode errorCode)
		{
			return this.CreateFailedResult(errorCode, false);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000F821 File Offset: 0x0000DA21
		public RopResult CreateFailedResult(ErrorCode errorCode, bool isPartiallyCompleted)
		{
			return new MoveCopyMessagesExtendedResult(errorCode, isPartiallyCompleted, this.destinationObjectHandleIndex);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000F830 File Offset: 0x0000DA30
		public RopResult CreateSuccessfulResult(bool isPartiallyCompleted)
		{
			return new MoveCopyMessagesExtendedResult(ErrorCode.None, isPartiallyCompleted, this.destinationObjectHandleIndex);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0000F83F File Offset: 0x0000DA3F
		public object CreateProgressToken()
		{
			return new LogonDestinationHandleProgressToken(RopId.MoveCopyMessagesExtended, this.destinationObjectHandleIndex, this.logonId);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0000F857 File Offset: 0x0000DA57
		public RopResult CreateProgressResult(uint completedTaskCount, uint totalTaskCount)
		{
			return new SuccessfulProgressResult(this.logonId, completedTaskCount, totalTaskCount);
		}

		// Token: 0x040002FB RID: 763
		private readonly byte logonId;

		// Token: 0x040002FC RID: 764
		private readonly uint destinationObjectHandleIndex;
	}
}
