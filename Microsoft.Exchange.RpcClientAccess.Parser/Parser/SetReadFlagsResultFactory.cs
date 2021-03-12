using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200012A RID: 298
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SetReadFlagsResultFactory : ResultFactory, IProgressResultFactory
	{
		// Token: 0x060005CE RID: 1486 RVA: 0x00010D88 File Offset: 0x0000EF88
		internal SetReadFlagsResultFactory(byte logonId)
		{
			this.logonId = logonId;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00010D98 File Offset: 0x0000EF98
		internal SetReadFlagsResultFactory(object progressToken)
		{
			if (progressToken == null)
			{
				throw new ArgumentNullException("progressToken");
			}
			LogonProgressToken logonProgressToken = (LogonProgressToken)progressToken;
			if (logonProgressToken.RopId != RopId.SetReadFlags)
			{
				throw new ArgumentException("Incorrect progress token, token's RopId: " + logonProgressToken.RopId, "progressToken");
			}
			this.logonId = logonProgressToken.LogonId;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00010DFE File Offset: 0x0000EFFE
		public static RopResult Parse(Reader reader)
		{
			return ResultFactory.ParseResultOrProgress(RopId.SetReadFlags, reader, (Reader resultReader) => new SetReadFlagsResult(resultReader));
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00010E25 File Offset: 0x0000F025
		public override RopResult CreateStandardFailedResult(ErrorCode errorCode)
		{
			return this.CreateFailedResult(errorCode, false);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00010E2F File Offset: 0x0000F02F
		public RopResult CreateFailedResult(ErrorCode errorCode, bool isPartiallyCompleted)
		{
			return new SetReadFlagsResult(errorCode, isPartiallyCompleted);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00010E38 File Offset: 0x0000F038
		public RopResult CreateSuccessfulResult(bool isPartiallyCompleted)
		{
			return new SetReadFlagsResult(ErrorCode.None, isPartiallyCompleted);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00010E41 File Offset: 0x0000F041
		public object CreateProgressToken()
		{
			return new LogonProgressToken(RopId.SetReadFlags, this.logonId);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00010E50 File Offset: 0x0000F050
		public RopResult CreateProgressResult(uint completedTaskCount, uint totalTaskCount)
		{
			return new SuccessfulProgressResult(this.logonId, completedTaskCount, totalTaskCount);
		}

		// Token: 0x04000326 RID: 806
		private readonly byte logonId;
	}
}
