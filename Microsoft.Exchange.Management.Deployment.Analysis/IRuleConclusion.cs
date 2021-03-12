using System;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000006 RID: 6
	public interface IRuleConclusion
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005C RID: 92
		// (set) Token: 0x0600005D RID: 93
		bool IsConditionMet { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005E RID: 94
		// (set) Token: 0x0600005F RID: 95
		Severity Severity { get; set; }
	}
}
