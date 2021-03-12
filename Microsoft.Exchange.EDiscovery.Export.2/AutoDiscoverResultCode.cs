using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200003A RID: 58
	internal enum AutoDiscoverResultCode
	{
		// Token: 0x040000C2 RID: 194
		Success,
		// Token: 0x040000C3 RID: 195
		TransientError,
		// Token: 0x040000C4 RID: 196
		Error,
		// Token: 0x040000C5 RID: 197
		EmailAddressRedirected,
		// Token: 0x040000C6 RID: 198
		UrlRedirected,
		// Token: 0x040000C7 RID: 199
		UrlConfigurationNotFound,
		// Token: 0x040000C8 RID: 200
		InvalidUser
	}
}
