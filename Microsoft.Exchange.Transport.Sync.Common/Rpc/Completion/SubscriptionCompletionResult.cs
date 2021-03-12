using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Rpc.Completion
{
	// Token: 0x0200009D RID: 157
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class SubscriptionCompletionResult
	{
		// Token: 0x060003FD RID: 1021 RVA: 0x00016113 File Offset: 0x00014313
		public static bool IsSuccess(uint errorCode)
		{
			return (errorCode & 4026531840U) == 0U;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00016120 File Offset: 0x00014320
		public static string GetStringForErrorCode(uint errorCode)
		{
			uint num = errorCode;
			if (num == 0U)
			{
				return "Success";
			}
			switch (num)
			{
			case 268435457U:
				return "ServerVersionMismatch";
			case 268435458U:
				return "ServerStopped";
			case 268435459U:
				return "InvalidSubscriptionMessageId";
			default:
				return errorCode.ToString("X");
			}
		}

		// Token: 0x0400022B RID: 555
		public const uint Success = 0U;

		// Token: 0x0400022C RID: 556
		public const uint ServerVersionMismatch = 268435457U;

		// Token: 0x0400022D RID: 557
		public const uint ServerStopped = 268435458U;

		// Token: 0x0400022E RID: 558
		public const uint InvalidSubscriptionMessageId = 268435459U;

		// Token: 0x0400022F RID: 559
		public const uint InvalidSubscription = 268435460U;

		// Token: 0x04000230 RID: 560
		private const uint SuccessBit = 0U;

		// Token: 0x04000231 RID: 561
		private const uint UnexpectedErrorBit = 268435456U;

		// Token: 0x04000232 RID: 562
		private const uint CategoryBit = 4026531840U;
	}
}
