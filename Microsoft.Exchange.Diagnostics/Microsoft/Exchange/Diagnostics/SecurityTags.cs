using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020002A6 RID: 678
	public struct SecurityTags
	{
		// Token: 0x04001262 RID: 4706
		public const int Authentication = 0;

		// Token: 0x04001263 RID: 4707
		public const int PartnerToken = 1;

		// Token: 0x04001264 RID: 4708
		public const int X509CertAuth = 2;

		// Token: 0x04001265 RID: 4709
		public const int OAuth = 3;

		// Token: 0x04001266 RID: 4710
		public const int FaultInjection = 4;

		// Token: 0x04001267 RID: 4711
		public const int BackendRehydration = 5;

		// Token: 0x04001268 RID: 4712
		public static Guid guid = new Guid("5ce0dc7e-6229-4bd9-9464-c92d7813bc3b");
	}
}
