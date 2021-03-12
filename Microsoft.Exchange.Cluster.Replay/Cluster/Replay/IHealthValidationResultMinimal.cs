using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000222 RID: 546
	public interface IHealthValidationResultMinimal
	{
		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001492 RID: 5266
		Guid IdentityGuid { get; }

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001493 RID: 5267
		string Identity { get; }

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001494 RID: 5268
		int HealthyCopiesCount { get; }

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001495 RID: 5269
		int HealthyPassiveCopiesCount { get; }

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001496 RID: 5270
		int TotalPassiveCopiesCount { get; }

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001497 RID: 5271
		bool IsValidationSuccessful { get; }

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001498 RID: 5272
		bool IsSiteValidationSuccessful { get; }

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001499 RID: 5273
		bool IsAnyCachedCopyStatusStale { get; }

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600149A RID: 5274
		// (set) Token: 0x0600149B RID: 5275
		string ErrorMessage { get; set; }

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600149C RID: 5276
		// (set) Token: 0x0600149D RID: 5277
		string ErrorMessageWithoutFullStatus { get; set; }
	}
}
