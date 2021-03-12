using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020002CA RID: 714
	public struct SpamEngineTags
	{
		// Token: 0x04001315 RID: 4885
		public const int RulesEngine = 0;

		// Token: 0x04001316 RID: 4886
		public const int Common = 1;

		// Token: 0x04001317 RID: 4887
		public const int BackScatter = 2;

		// Token: 0x04001318 RID: 4888
		public const int SenderAuthentication = 3;

		// Token: 0x04001319 RID: 4889
		public const int UriScan = 4;

		// Token: 0x0400131A RID: 4890
		public const int DnsChecks = 5;

		// Token: 0x0400131B RID: 4891
		public const int Dkim = 6;

		// Token: 0x0400131C RID: 4892
		public const int Dmarc = 7;

		// Token: 0x0400131D RID: 4893
		public static Guid guid = new Guid("D47F7E4B-8F27-43fa-9BE6-DDF69C7AE225");
	}
}
