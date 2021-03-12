using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000C3 RID: 195
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class DeleteMessagesResultFactory : ResultFactory, IProgressResultFactory
	{
		// Token: 0x06000465 RID: 1125 RVA: 0x0000EC4D File Offset: 0x0000CE4D
		internal DeleteMessagesResultFactory(byte logonId)
		{
			this.logonId = logonId;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000EC5C File Offset: 0x0000CE5C
		internal DeleteMessagesResultFactory(object progressToken)
		{
			if (progressToken == null)
			{
				throw new ArgumentNullException("progressToken");
			}
			LogonProgressToken logonProgressToken = (LogonProgressToken)progressToken;
			if (logonProgressToken.RopId != RopId.DeleteMessages)
			{
				throw new ArgumentException("Incorrect progress token, token's RopId: " + logonProgressToken.RopId, "progressToken");
			}
			this.logonId = logonProgressToken.LogonId;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000ECC2 File Offset: 0x0000CEC2
		public static RopResult Parse(Reader reader)
		{
			return ResultFactory.ParseResultOrProgress(RopId.DeleteMessages, reader, (Reader resultReader) => new DeleteMessagesResult(resultReader));
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000ECE9 File Offset: 0x0000CEE9
		public override RopResult CreateStandardFailedResult(ErrorCode errorCode)
		{
			return this.CreateFailedResult(errorCode, false);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000ECF3 File Offset: 0x0000CEF3
		public RopResult CreateFailedResult(ErrorCode errorCode, bool isPartiallyCompleted)
		{
			return new DeleteMessagesResult(errorCode, isPartiallyCompleted);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000ECFC File Offset: 0x0000CEFC
		public RopResult CreateSuccessfulResult(bool isPartiallyCompleted)
		{
			return new DeleteMessagesResult(ErrorCode.None, isPartiallyCompleted);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000ED05 File Offset: 0x0000CF05
		public object CreateProgressToken()
		{
			return new LogonProgressToken(RopId.DeleteMessages, this.logonId);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000ED14 File Offset: 0x0000CF14
		public RopResult CreateProgressResult(uint completedTaskCount, uint totalTaskCount)
		{
			return new SuccessfulProgressResult(this.logonId, completedTaskCount, totalTaskCount);
		}

		// Token: 0x040002DB RID: 731
		private readonly byte logonId;
	}
}
