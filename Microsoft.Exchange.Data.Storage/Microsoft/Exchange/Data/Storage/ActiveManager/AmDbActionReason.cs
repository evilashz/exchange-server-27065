using System;

namespace Microsoft.Exchange.Data.Storage.ActiveManager
{
	// Token: 0x0200030D RID: 781
	internal enum AmDbActionReason
	{
		// Token: 0x0400148E RID: 5262
		None,
		// Token: 0x0400148F RID: 5263
		Startup,
		// Token: 0x04001490 RID: 5264
		Cmdlet,
		// Token: 0x04001491 RID: 5265
		FailureItem,
		// Token: 0x04001492 RID: 5266
		StoreStarted,
		// Token: 0x04001493 RID: 5267
		StoreStopped,
		// Token: 0x04001494 RID: 5268
		NodeUp,
		// Token: 0x04001495 RID: 5269
		NodeDown,
		// Token: 0x04001496 RID: 5270
		SystemShutdown,
		// Token: 0x04001497 RID: 5271
		PeriodicAction,
		// Token: 0x04001498 RID: 5272
		MapiNetFailure,
		// Token: 0x04001499 RID: 5273
		CatalogFailureItem,
		// Token: 0x0400149A RID: 5274
		NodeDownConfirmed,
		// Token: 0x0400149B RID: 5275
		ManagedAvailability,
		// Token: 0x0400149C RID: 5276
		Rebalance,
		// Token: 0x0400149D RID: 5277
		ActivationDisabled,
		// Token: 0x0400149E RID: 5278
		TimeoutFailure,
		// Token: 0x0400149F RID: 5279
		ReplayDown
	}
}
