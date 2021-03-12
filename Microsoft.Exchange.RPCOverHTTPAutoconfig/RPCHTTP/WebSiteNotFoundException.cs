using System;

namespace Microsoft.Exchange.Servicelets.RPCHTTP
{
	// Token: 0x0200000D RID: 13
	internal sealed class WebSiteNotFoundException : Exception
	{
		// Token: 0x06000053 RID: 83 RVA: 0x000044E1 File Offset: 0x000026E1
		public WebSiteNotFoundException(string webSiteName)
		{
			this.webSiteName = webSiteName;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000044F0 File Offset: 0x000026F0
		public string WebSiteName
		{
			get
			{
				return this.webSiteName;
			}
		}

		// Token: 0x0400004E RID: 78
		private readonly string webSiteName;
	}
}
