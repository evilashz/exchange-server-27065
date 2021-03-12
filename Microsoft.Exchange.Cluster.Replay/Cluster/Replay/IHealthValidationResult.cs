using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000223 RID: 547
	internal interface IHealthValidationResult : IHealthValidationResultMinimal
	{
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600149E RID: 5278
		bool IsTargetCopyHealthy { get; }

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600149F RID: 5279
		bool IsActiveCopyHealthy { get; }

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060014A0 RID: 5280
		CopyStatusClientCachedEntry TargetCopyStatus { get; }

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060014A1 RID: 5281
		CopyStatusClientCachedEntry ActiveCopyStatus { get; }
	}
}
