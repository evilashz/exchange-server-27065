using System;

namespace Microsoft.Exchange.Data.Directory.ResourceHealth
{
	// Token: 0x020009EF RID: 2543
	internal interface IResourceHealthPoller
	{
		// Token: 0x17002A48 RID: 10824
		// (get) Token: 0x06007606 RID: 30214
		TimeSpan Interval { get; }

		// Token: 0x17002A49 RID: 10825
		// (get) Token: 0x06007607 RID: 30215
		bool IsActive { get; }

		// Token: 0x06007608 RID: 30216
		void Execute();
	}
}
