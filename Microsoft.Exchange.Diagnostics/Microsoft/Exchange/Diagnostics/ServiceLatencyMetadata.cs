using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200006E RID: 110
	public enum ServiceLatencyMetadata
	{
		// Token: 0x04000238 RID: 568
		AuthModuleLatency,
		// Token: 0x04000239 RID: 569
		CallerADLatency,
		// Token: 0x0400023A RID: 570
		CallContextInitLatency,
		// Token: 0x0400023B RID: 571
		CheckAccessCoreLatency,
		// Token: 0x0400023C RID: 572
		CoreExecutionLatency,
		// Token: 0x0400023D RID: 573
		ExchangePrincipalLatency,
		// Token: 0x0400023E RID: 574
		HttpPipelineLatency,
		// Token: 0x0400023F RID: 575
		PreExecutionLatency,
		// Token: 0x04000240 RID: 576
		RecipientLookupLatency,
		// Token: 0x04000241 RID: 577
		RequestedUserADLatency,
		// Token: 0x04000242 RID: 578
		DetailedExchangePrincipalLatency,
		// Token: 0x04000243 RID: 579
		EPCacheGetAdSessionSettingsForOrg
	}
}
