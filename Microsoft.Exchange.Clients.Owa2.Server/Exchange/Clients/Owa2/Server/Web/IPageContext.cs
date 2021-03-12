using System;
using Microsoft.Exchange.Clients.Common;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x0200046B RID: 1131
	public interface IPageContext
	{
		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x060025B9 RID: 9657
		UserAgent UserAgent { get; }

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x060025BA RID: 9658
		string Theme { get; }

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x060025BB RID: 9659
		bool IsAppCacheEnabledClient { get; }

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x060025BC RID: 9660
		SlabManifestType ManifestType { get; }

		// Token: 0x060025BD RID: 9661
		string FormatURIForCDN(string relativeUri, bool isBootResourceUri);
	}
}
