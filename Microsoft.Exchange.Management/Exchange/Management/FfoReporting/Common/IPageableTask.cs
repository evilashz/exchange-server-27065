using System;

namespace Microsoft.Exchange.Management.FfoReporting.Common
{
	// Token: 0x0200038F RID: 911
	internal interface IPageableTask
	{
		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06001FCA RID: 8138
		// (set) Token: 0x06001FCB RID: 8139
		int Page { get; set; }

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06001FCC RID: 8140
		// (set) Token: 0x06001FCD RID: 8141
		int PageSize { get; set; }
	}
}
