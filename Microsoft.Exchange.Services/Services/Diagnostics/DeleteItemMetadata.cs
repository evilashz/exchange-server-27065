using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x02000031 RID: 49
	public enum DeleteItemMetadata
	{
		// Token: 0x0400021E RID: 542
		[DisplayName("DI", "ACT")]
		ActionType,
		// Token: 0x0400021F RID: 543
		[DisplayName("DI", "ST")]
		SessionType,
		// Token: 0x04000220 RID: 544
		[DisplayName("DI", "PRIP")]
		Principal
	}
}
