using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000100 RID: 256
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MoveCopyMessagesExtendedWithEntryIdsResultFactory : ResultFactory, IProgressResultFactory
	{
		// Token: 0x06000524 RID: 1316 RVA: 0x0000F866 File Offset: 0x0000DA66
		internal MoveCopyMessagesExtendedWithEntryIdsResultFactory(byte logonId, uint destinationObjectHandleIndex)
		{
			this.destinationObjectHandleIndex = destinationObjectHandleIndex;
			this.logonId = logonId;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0000F87C File Offset: 0x0000DA7C
		internal MoveCopyMessagesExtendedWithEntryIdsResultFactory(object progressToken)
		{
			if (progressToken == null)
			{
				throw new ArgumentNullException("progressToken");
			}
			LogonDestinationHandleProgressToken logonDestinationHandleProgressToken = (LogonDestinationHandleProgressToken)progressToken;
			if (logonDestinationHandleProgressToken.RopId != RopId.MoveCopyMessagesExtendedWithEntryIds)
			{
				throw new ArgumentException("Wrong RopId in progress token:" + logonDestinationHandleProgressToken.RopId, "progressToken");
			}
			this.destinationObjectHandleIndex = logonDestinationHandleProgressToken.DestinationObjectHandleIndex;
			this.logonId = logonDestinationHandleProgressToken.LogonId;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000F8F1 File Offset: 0x0000DAF1
		public static RopResult Parse(Reader reader)
		{
			return ResultFactory.ParseResultOrProgress(RopId.MoveCopyMessagesExtendedWithEntryIds, reader, (Reader resultReader) => new MoveCopyMessagesExtendedWithEntryIdsResult(resultReader));
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000F91B File Offset: 0x0000DB1B
		public override RopResult CreateStandardFailedResult(ErrorCode errorCode)
		{
			return this.CreateFailedResult(errorCode, false);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000F925 File Offset: 0x0000DB25
		public RopResult CreateFailedResult(ErrorCode errorCode, bool isPartiallyCompleted)
		{
			return new MoveCopyMessagesExtendedWithEntryIdsResult(errorCode, isPartiallyCompleted, null, null, this.destinationObjectHandleIndex);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000F936 File Offset: 0x0000DB36
		public RopResult CreateSuccessfulResult(bool isPartiallyCompleted, StoreId[] messageIds, ulong[] changeNumbers)
		{
			return new MoveCopyMessagesExtendedWithEntryIdsResult(ErrorCode.None, isPartiallyCompleted, messageIds, changeNumbers, this.destinationObjectHandleIndex);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000F947 File Offset: 0x0000DB47
		public object CreateProgressToken()
		{
			return new LogonDestinationHandleProgressToken(RopId.MoveCopyMessagesExtendedWithEntryIds, this.destinationObjectHandleIndex, this.logonId);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0000F95F File Offset: 0x0000DB5F
		public RopResult CreateProgressResult(uint completedTaskCount, uint totalTaskCount)
		{
			return new SuccessfulProgressResult(this.logonId, completedTaskCount, totalTaskCount);
		}

		// Token: 0x040002FE RID: 766
		private readonly byte logonId;

		// Token: 0x040002FF RID: 767
		private readonly uint destinationObjectHandleIndex;
	}
}
