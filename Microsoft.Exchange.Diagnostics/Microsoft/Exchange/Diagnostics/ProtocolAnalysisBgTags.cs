using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200028B RID: 651
	public struct ProtocolAnalysisBgTags
	{
		// Token: 0x04001161 RID: 4449
		public const int Factory = 0;

		// Token: 0x04001162 RID: 4450
		public const int Agent = 1;

		// Token: 0x04001163 RID: 4451
		public const int Database = 2;

		// Token: 0x04001164 RID: 4452
		public const int OnOpenProxyDetection = 3;

		// Token: 0x04001165 RID: 4453
		public const int OnDnsQuery = 4;

		// Token: 0x04001166 RID: 4454
		public const int OnSenderBlocking = 5;

		// Token: 0x04001167 RID: 4455
		public static Guid guid = new Guid("E30C077B-EBAD-4AC8-B2BF-197C3329952F");
	}
}
