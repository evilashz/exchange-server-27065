using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000C9 RID: 201
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EmptyFolderResultFactory : ResultFactory, IProgressResultFactory
	{
		// Token: 0x06000478 RID: 1144 RVA: 0x0000ED89 File Offset: 0x0000CF89
		internal EmptyFolderResultFactory(byte logonId)
		{
			this.logonId = logonId;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000ED98 File Offset: 0x0000CF98
		internal EmptyFolderResultFactory(object progressToken)
		{
			if (progressToken == null)
			{
				throw new ArgumentNullException("progressToken");
			}
			LogonProgressToken logonProgressToken = (LogonProgressToken)progressToken;
			if (logonProgressToken.RopId != RopId.EmptyFolder)
			{
				throw new ArgumentException("Incorrect progress token, token's RopId: " + logonProgressToken.RopId, "progressToken");
			}
			this.logonId = logonProgressToken.LogonId;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000EDFE File Offset: 0x0000CFFE
		public static RopResult Parse(Reader reader)
		{
			return ResultFactory.ParseResultOrProgress(RopId.EmptyFolder, reader, (Reader resultReader) => new EmptyFolderResult(resultReader));
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000EE25 File Offset: 0x0000D025
		public override RopResult CreateStandardFailedResult(ErrorCode errorCode)
		{
			return this.CreateFailedResult(errorCode, false);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000EE2F File Offset: 0x0000D02F
		public RopResult CreateFailedResult(ErrorCode errorCode, bool isPartiallyCompleted)
		{
			return new EmptyFolderResult(errorCode, isPartiallyCompleted);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000EE38 File Offset: 0x0000D038
		public RopResult CreateSuccessfulResult(bool isPartiallyCompleted)
		{
			return new EmptyFolderResult(ErrorCode.None, isPartiallyCompleted);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000EE41 File Offset: 0x0000D041
		public object CreateProgressToken()
		{
			return new LogonProgressToken(RopId.EmptyFolder, this.logonId);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000EE50 File Offset: 0x0000D050
		public RopResult CreateProgressResult(uint completedTaskCount, uint totalTaskCount)
		{
			return new SuccessfulProgressResult(this.logonId, completedTaskCount, totalTaskCount);
		}

		// Token: 0x040002DD RID: 733
		private readonly byte logonId;
	}
}
