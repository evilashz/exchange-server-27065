using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000295 RID: 661
	public struct ManagedStore_LazyIndexingTags
	{
		// Token: 0x04001199 RID: 4505
		public const int PseudoIndex = 0;

		// Token: 0x0400119A RID: 4506
		public const int CategoryHeaderViewPopulation = 1;

		// Token: 0x0400119B RID: 4507
		public const int CategoryHeaderViewMaintenance = 2;

		// Token: 0x0400119C RID: 4508
		public const int CategorizedViews = 3;

		// Token: 0x0400119D RID: 4509
		public const int FaultInjection = 20;

		// Token: 0x0400119E RID: 4510
		public static Guid guid = new Guid("0e12474e-7e64-471f-93f5-901f795c4ae0");
	}
}
