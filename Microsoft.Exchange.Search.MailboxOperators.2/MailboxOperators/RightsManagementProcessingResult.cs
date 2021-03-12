using System;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x0200001B RID: 27
	public enum RightsManagementProcessingResult
	{
		// Token: 0x04000148 RID: 328
		NotRightsManaged,
		// Token: 0x04000149 RID: 329
		Success,
		// Token: 0x0400014A RID: 330
		IsSMIME,
		// Token: 0x0400014B RID: 331
		FailedTransient,
		// Token: 0x0400014C RID: 332
		FailedPermanent,
		// Token: 0x0400014D RID: 333
		Skipped
	}
}
