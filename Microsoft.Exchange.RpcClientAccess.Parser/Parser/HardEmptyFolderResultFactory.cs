using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000F3 RID: 243
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class HardEmptyFolderResultFactory : ResultFactory, IProgressResultFactory
	{
		// Token: 0x060004F1 RID: 1265 RVA: 0x0000F482 File Offset: 0x0000D682
		internal HardEmptyFolderResultFactory(byte logonId)
		{
			this.logonId = logonId;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000F494 File Offset: 0x0000D694
		internal HardEmptyFolderResultFactory(object progressToken)
		{
			if (progressToken == null)
			{
				throw new ArgumentNullException("progressToken");
			}
			LogonProgressToken logonProgressToken = (LogonProgressToken)progressToken;
			if (logonProgressToken.RopId != RopId.HardEmptyFolder)
			{
				throw new ArgumentException("Incorrect progress token, token's RopId: " + logonProgressToken.RopId, "progressToken");
			}
			this.logonId = logonProgressToken.LogonId;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000F4FD File Offset: 0x0000D6FD
		public static RopResult Parse(Reader reader)
		{
			return ResultFactory.ParseResultOrProgress(RopId.HardEmptyFolder, reader, (Reader resultReader) => new HardEmptyFolderResult(resultReader));
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000F527 File Offset: 0x0000D727
		public override RopResult CreateStandardFailedResult(ErrorCode errorCode)
		{
			return this.CreateFailedResult(errorCode, false);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000F531 File Offset: 0x0000D731
		public RopResult CreateFailedResult(ErrorCode errorCode, bool isPartiallyCompleted)
		{
			return new HardEmptyFolderResult(errorCode, isPartiallyCompleted);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000F53A File Offset: 0x0000D73A
		public RopResult CreateSuccessfulResult(bool isPartiallyCompleted)
		{
			return new HardEmptyFolderResult(ErrorCode.None, isPartiallyCompleted);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000F543 File Offset: 0x0000D743
		public object CreateProgressToken()
		{
			return new LogonProgressToken(RopId.HardEmptyFolder, this.logonId);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000F555 File Offset: 0x0000D755
		public RopResult CreateProgressResult(uint completedTaskCount, uint totalTaskCount)
		{
			return new SuccessfulProgressResult(this.logonId, completedTaskCount, totalTaskCount);
		}

		// Token: 0x040002F3 RID: 755
		private readonly byte logonId;
	}
}
