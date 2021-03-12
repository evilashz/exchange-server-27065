using System;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Dal
{
	// Token: 0x0200005C RID: 92
	public enum DalType
	{
		// Token: 0x04000175 RID: 373
		Spam = 2,
		// Token: 0x04000176 RID: 374
		Domain,
		// Token: 0x04000177 RID: 375
		ServiceHealth,
		// Token: 0x04000178 RID: 376
		Reporting,
		// Token: 0x04000179 RID: 377
		Mtrt,
		// Token: 0x0400017A RID: 378
		MtrtAggregated,
		// Token: 0x0400017B RID: 379
		IdGen,
		// Token: 0x0400017C RID: 380
		MtrtEtl,
		// Token: 0x0400017D RID: 381
		Kes,
		// Token: 0x0400017E RID: 382
		BackgroundJobBackend,
		// Token: 0x0400017F RID: 383
		Migration,
		// Token: 0x04000180 RID: 384
		Global = 1001,
		// Token: 0x04000181 RID: 385
		Tenant,
		// Token: 0x04000182 RID: 386
		Recipient
	}
}
