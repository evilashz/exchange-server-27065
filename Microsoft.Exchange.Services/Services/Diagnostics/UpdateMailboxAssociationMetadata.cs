using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x0200005F RID: 95
	internal enum UpdateMailboxAssociationMetadata
	{
		// Token: 0x0400053A RID: 1338
		[DisplayName("UMA", "Guid")]
		ExchangeGuid,
		// Token: 0x0400053B RID: 1339
		[DisplayName("UMA", "ExtId")]
		ExternalDirectoryObjectId,
		// Token: 0x0400053C RID: 1340
		[DisplayName("UMA", "ADUserCached")]
		IsPopulateADUserInCacheSuccessful,
		// Token: 0x0400053D RID: 1341
		[DisplayName("UMA", "MiniRecipCached")]
		IsPopulateMiniRecipientInCacheSuccessful
	}
}
