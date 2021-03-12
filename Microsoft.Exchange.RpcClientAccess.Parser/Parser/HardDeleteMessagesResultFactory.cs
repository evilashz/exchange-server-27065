using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000F4 RID: 244
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class HardDeleteMessagesResultFactory : ResultFactory, IProgressResultFactory
	{
		// Token: 0x060004FA RID: 1274 RVA: 0x0000F564 File Offset: 0x0000D764
		internal HardDeleteMessagesResultFactory(byte logonId)
		{
			this.logonId = logonId;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000F574 File Offset: 0x0000D774
		internal HardDeleteMessagesResultFactory(object progressToken)
		{
			if (progressToken == null)
			{
				throw new ArgumentNullException("progressToken");
			}
			LogonProgressToken logonProgressToken = (LogonProgressToken)progressToken;
			if (logonProgressToken.RopId != RopId.HardDeleteMessages)
			{
				throw new ArgumentException("Incorrect progress token, token's RopId: " + logonProgressToken.RopId, "progressToken");
			}
			this.logonId = logonProgressToken.LogonId;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000F5DD File Offset: 0x0000D7DD
		public static RopResult Parse(Reader reader)
		{
			return ResultFactory.ParseResultOrProgress(RopId.HardDeleteMessages, reader, (Reader resultReader) => new HardDeleteMessagesResult(resultReader));
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000F607 File Offset: 0x0000D807
		public override RopResult CreateStandardFailedResult(ErrorCode errorCode)
		{
			return this.CreateFailedResult(errorCode, false);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000F611 File Offset: 0x0000D811
		public RopResult CreateFailedResult(ErrorCode errorCode, bool isPartiallyCompleted)
		{
			return new HardDeleteMessagesResult(errorCode, isPartiallyCompleted);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000F61A File Offset: 0x0000D81A
		public RopResult CreateSuccessfulResult(bool isPartiallyCompleted)
		{
			return new HardDeleteMessagesResult(ErrorCode.None, isPartiallyCompleted);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000F623 File Offset: 0x0000D823
		public object CreateProgressToken()
		{
			return new LogonProgressToken(RopId.HardDeleteMessages, this.logonId);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000F635 File Offset: 0x0000D835
		public RopResult CreateProgressResult(uint completedTaskCount, uint totalTaskCount)
		{
			return new SuccessfulProgressResult(this.logonId, completedTaskCount, totalTaskCount);
		}

		// Token: 0x040002F5 RID: 757
		private readonly byte logonId;
	}
}
