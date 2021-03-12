using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000102 RID: 258
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MoveFolderResultFactory : ResultFactory, IProgressResultFactory
	{
		// Token: 0x06000536 RID: 1334 RVA: 0x0000FA69 File Offset: 0x0000DC69
		internal MoveFolderResultFactory(byte logonId, uint destinationObjectHandleIndex)
		{
			this.destinationObjectHandleIndex = destinationObjectHandleIndex;
			this.logonId = logonId;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0000FA80 File Offset: 0x0000DC80
		internal MoveFolderResultFactory(object progressToken)
		{
			if (progressToken == null)
			{
				throw new ArgumentNullException("progressToken");
			}
			LogonDestinationHandleProgressToken logonDestinationHandleProgressToken = (LogonDestinationHandleProgressToken)progressToken;
			if (logonDestinationHandleProgressToken.RopId != RopId.MoveFolder)
			{
				throw new ArgumentException("Incorrect progress token, token's RopId: " + logonDestinationHandleProgressToken.RopId, "progressToken");
			}
			this.destinationObjectHandleIndex = logonDestinationHandleProgressToken.DestinationObjectHandleIndex;
			this.logonId = logonDestinationHandleProgressToken.LogonId;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0000FAF2 File Offset: 0x0000DCF2
		public static RopResult Parse(Reader reader)
		{
			return ResultFactory.ParseResultOrProgress(RopId.MoveFolder, reader, (Reader resultReader) => new MoveFolderResult(resultReader));
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0000FB19 File Offset: 0x0000DD19
		public override RopResult CreateStandardFailedResult(ErrorCode errorCode)
		{
			return this.CreateFailedResult(errorCode, false);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0000FB23 File Offset: 0x0000DD23
		public RopResult CreateFailedResult(ErrorCode errorCode, bool isPartiallyCompleted)
		{
			return new MoveFolderResult(errorCode, isPartiallyCompleted, this.destinationObjectHandleIndex);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0000FB32 File Offset: 0x0000DD32
		public RopResult CreateSuccessfulResult(bool isPartiallyCompleted)
		{
			return new MoveFolderResult(ErrorCode.None, isPartiallyCompleted, this.destinationObjectHandleIndex);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0000FB41 File Offset: 0x0000DD41
		public object CreateProgressToken()
		{
			return new LogonDestinationHandleProgressToken(RopId.MoveFolder, this.destinationObjectHandleIndex, this.logonId);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000FB56 File Offset: 0x0000DD56
		public RopResult CreateProgressResult(uint completedTaskCount, uint totalTaskCount)
		{
			return new SuccessfulProgressResult(this.logonId, completedTaskCount, totalTaskCount);
		}

		// Token: 0x04000304 RID: 772
		private readonly byte logonId;

		// Token: 0x04000305 RID: 773
		private readonly uint destinationObjectHandleIndex;
	}
}
