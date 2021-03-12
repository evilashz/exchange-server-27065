using System;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200013A RID: 314
	public interface IEacHttpContext
	{
		// Token: 0x17001A4A RID: 6730
		// (get) Token: 0x060020F9 RID: 8441
		// (set) Token: 0x060020FA RID: 8442
		ShouldContinueContext ShouldContinueContext { get; set; }

		// Token: 0x17001A4B RID: 6731
		// (get) Token: 0x060020FB RID: 8443
		bool PostHydrationActionPresent { get; }
	}
}
