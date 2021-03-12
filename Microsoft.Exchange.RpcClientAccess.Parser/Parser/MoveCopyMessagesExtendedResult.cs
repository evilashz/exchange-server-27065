using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200026E RID: 622
	internal sealed class MoveCopyMessagesExtendedResult : MoveCopyMessagesExtendedResultBase
	{
		// Token: 0x06000D65 RID: 3429 RVA: 0x00028D74 File Offset: 0x00026F74
		internal MoveCopyMessagesExtendedResult(ErrorCode errorCode, bool isPartiallyCompleted, uint destinationObjectHandleIndex) : base(RopId.MoveCopyMessagesExtended, errorCode, isPartiallyCompleted, destinationObjectHandleIndex)
		{
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00028D84 File Offset: 0x00026F84
		internal MoveCopyMessagesExtendedResult(Reader reader) : base(reader)
		{
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x00028D8D File Offset: 0x00026F8D
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
		}
	}
}
