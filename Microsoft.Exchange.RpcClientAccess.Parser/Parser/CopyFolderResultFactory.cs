using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000B7 RID: 183
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CopyFolderResultFactory : ResultFactory, IProgressResultFactory
	{
		// Token: 0x06000430 RID: 1072 RVA: 0x0000E803 File Offset: 0x0000CA03
		internal CopyFolderResultFactory(byte logonId, uint destinationObjectHandleIndex)
		{
			this.destinationObjectHandleIndex = destinationObjectHandleIndex;
			this.logonId = logonId;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000E81C File Offset: 0x0000CA1C
		internal CopyFolderResultFactory(object progressToken)
		{
			if (progressToken == null)
			{
				throw new ArgumentNullException("progressToken");
			}
			LogonDestinationHandleProgressToken logonDestinationHandleProgressToken = (LogonDestinationHandleProgressToken)progressToken;
			if (logonDestinationHandleProgressToken.RopId != RopId.CopyFolder)
			{
				throw new ArgumentException("Incorrect progress token, token's RopId: " + logonDestinationHandleProgressToken.RopId, "progressToken");
			}
			this.destinationObjectHandleIndex = logonDestinationHandleProgressToken.DestinationObjectHandleIndex;
			this.logonId = logonDestinationHandleProgressToken.LogonId;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000E88E File Offset: 0x0000CA8E
		public static RopResult Parse(Reader reader)
		{
			return ResultFactory.ParseResultOrProgress(RopId.CopyFolder, reader, (Reader resultReader) => new CopyFolderResult(resultReader));
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000E8B5 File Offset: 0x0000CAB5
		public override RopResult CreateStandardFailedResult(ErrorCode errorCode)
		{
			return this.CreateFailedResult(errorCode, false);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000E8BF File Offset: 0x0000CABF
		public RopResult CreateFailedResult(ErrorCode errorCode, bool isPartiallyCompleted)
		{
			return new CopyFolderResult(errorCode, isPartiallyCompleted, this.destinationObjectHandleIndex);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000E8CE File Offset: 0x0000CACE
		public RopResult CreateSuccessfulResult(bool isPartiallyCompleted)
		{
			return new CopyFolderResult(ErrorCode.None, isPartiallyCompleted, this.destinationObjectHandleIndex);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000E8DD File Offset: 0x0000CADD
		public object CreateProgressToken()
		{
			return new LogonDestinationHandleProgressToken(RopId.CopyFolder, this.destinationObjectHandleIndex, this.logonId);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000E8F2 File Offset: 0x0000CAF2
		public RopResult CreateProgressResult(uint completedTaskCount, uint totalTaskCount)
		{
			return new SuccessfulProgressResult(this.logonId, completedTaskCount, totalTaskCount);
		}

		// Token: 0x040002CE RID: 718
		private readonly uint destinationObjectHandleIndex;

		// Token: 0x040002CF RID: 719
		private readonly byte logonId;
	}
}
