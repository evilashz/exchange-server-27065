using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Rpc.Cache
{
	// Token: 0x0200009B RID: 155
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class TestUserCacheResult
	{
		// Token: 0x04000218 RID: 536
		public const uint Success = 0U;

		// Token: 0x04000219 RID: 537
		public const uint UnexpectedError = 268435456U;

		// Token: 0x0400021A RID: 538
		public const uint ServerStopped = 268435457U;

		// Token: 0x0400021B RID: 539
		public const uint ServerVersionMismatch = 268435458U;

		// Token: 0x0400021C RID: 540
		private const uint SuccessBit = 0U;

		// Token: 0x0400021D RID: 541
		private const uint UnexpectedErrorBit = 268435456U;

		// Token: 0x0400021E RID: 542
		private const uint CategoryBit = 4026531840U;
	}
}
