using System;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000892 RID: 2194
	internal interface IPooledServiceProxy<TClient>
	{
		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06002EF3 RID: 12019
		TClient Client { get; }

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06002EF4 RID: 12020
		// (set) Token: 0x06002EF5 RID: 12021
		string Tag { get; set; }
	}
}
