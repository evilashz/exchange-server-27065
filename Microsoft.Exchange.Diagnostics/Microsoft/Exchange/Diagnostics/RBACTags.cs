using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020002A3 RID: 675
	public struct RBACTags
	{
		// Token: 0x0400124D RID: 4685
		public const int ADConfig = 0;

		// Token: 0x0400124E RID: 4686
		public const int AccessDenied = 1;

		// Token: 0x0400124F RID: 4687
		public const int RunspaceConfig = 2;

		// Token: 0x04001250 RID: 4688
		public const int AccessCheck = 3;

		// Token: 0x04001251 RID: 4689
		public const int PublicCreationAPI = 4;

		// Token: 0x04001252 RID: 4690
		public const int PublicInstanceAPI = 5;

		// Token: 0x04001253 RID: 4691
		public const int IssBuilder = 6;

		// Token: 0x04001254 RID: 4692
		public const int PublicPluginAPI = 7;

		// Token: 0x04001255 RID: 4693
		public const int IssBuilderDetail = 8;

		// Token: 0x04001256 RID: 4694
		public const int FaultInjection = 9;

		// Token: 0x04001257 RID: 4695
		public static Guid guid = new Guid("96825f4e-464a-44ef-af25-a76d1d0cec77");
	}
}
