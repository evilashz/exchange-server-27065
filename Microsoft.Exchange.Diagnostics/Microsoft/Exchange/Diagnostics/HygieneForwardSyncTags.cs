using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020002C8 RID: 712
	public struct HygieneForwardSyncTags
	{
		// Token: 0x04001308 RID: 4872
		public const int ServiceInstanceSync = 0;

		// Token: 0x04001309 RID: 4873
		public const int FullTenantSync = 1;

		// Token: 0x0400130A RID: 4874
		public const int Persistence = 2;

		// Token: 0x0400130B RID: 4875
		public const int Provisioning = 3;

		// Token: 0x0400130C RID: 4876
		public const int MsoServices = 4;

		// Token: 0x0400130D RID: 4877
		public const int GlsServices = 5;

		// Token: 0x0400130E RID: 4878
		public const int DNSServices = 6;

		// Token: 0x0400130F RID: 4879
		public static Guid guid = new Guid("952887AB-4E9A-4CF8-867F-3C5BD5BB67A3");
	}
}
