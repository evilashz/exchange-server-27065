using System;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000041 RID: 65
	internal enum DiscoverSettingsResult
	{
		// Token: 0x040001B1 RID: 433
		Succeeded,
		// Token: 0x040001B2 RID: 434
		SettingsNotFound,
		// Token: 0x040001B3 RID: 435
		AuthenticationError,
		// Token: 0x040001B4 RID: 436
		InsecureSettingsNotSupported
	}
}
