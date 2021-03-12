using System;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000331 RID: 817
	internal interface IAsyncServiceCommand
	{
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060016FC RID: 5884
		// (set) Token: 0x060016FD RID: 5885
		CompleteRequestAsyncCallback CompleteRequestAsyncCallback { get; set; }
	}
}
