using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020002C6 RID: 710
	public struct HygieneDataTags
	{
		// Token: 0x04001300 RID: 4864
		public const int WebstoreProvider = 0;

		// Token: 0x04001301 RID: 4865
		public const int FaultInjection = 1;

		// Token: 0x04001302 RID: 4866
		public const int DomainSession = 2;

		// Token: 0x04001303 RID: 4867
		public const int GLSQuery = 3;

		// Token: 0x04001304 RID: 4868
		public const int WebServiceProvider = 4;

		// Token: 0x04001305 RID: 4869
		public static Guid guid = new Guid("4B65DA35-2EAC-4452-B7B7-375D986BCA91");
	}
}
