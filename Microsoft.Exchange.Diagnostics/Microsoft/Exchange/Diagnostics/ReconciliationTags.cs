using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000210 RID: 528
	public struct ReconciliationTags
	{
		// Token: 0x04000B64 RID: 2916
		public const int StartProcessingMessage = 0;

		// Token: 0x04000B65 RID: 2917
		public const int EndProcessingMessage = 255;

		// Token: 0x04000B66 RID: 2918
		public static Guid guid = new Guid("E06E0123-1B5C-4f61-959D-8258BF6C689A");
	}
}
