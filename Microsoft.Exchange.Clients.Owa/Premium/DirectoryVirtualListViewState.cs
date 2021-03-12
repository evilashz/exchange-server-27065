using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000497 RID: 1175
	[OwaEventObjectId(typeof(ADObjectId))]
	[OwaEventStruct("ADVLVS")]
	public class DirectoryVirtualListViewState : VirtualListViewState
	{
		// Token: 0x04001DFB RID: 7675
		public const string StructNamespace = "ADVLVS";

		// Token: 0x04001DFC RID: 7676
		public const string CookieName = "cki";

		// Token: 0x04001DFD RID: 7677
		public const string CookieIndexName = "ckii";

		// Token: 0x04001DFE RID: 7678
		public const string CookieLcidName = "clcid";

		// Token: 0x04001DFF RID: 7679
		public const string PreferredDCName = "cPfdDC";

		// Token: 0x04001E00 RID: 7680
		[OwaEventField("cki", true, null)]
		public string Cookie;

		// Token: 0x04001E01 RID: 7681
		[OwaEventField("ckii", true, 0)]
		public int CookieIndex;

		// Token: 0x04001E02 RID: 7682
		[OwaEventField("clcid", true, -1)]
		public int CookieLcid;

		// Token: 0x04001E03 RID: 7683
		[OwaEventField("cPfdDC", true, null)]
		public string PreferredDC;
	}
}
