using System;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x02000775 RID: 1909
	internal class AuthenticationParameters
	{
		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x060025D5 RID: 9685 RVA: 0x0004FD32 File Offset: 0x0004DF32
		// (set) Token: 0x060025D6 RID: 9686 RVA: 0x0004FD3A File Offset: 0x0004DF3A
		internal CommonAccessToken CommonAccessToken { get; set; }

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x060025D7 RID: 9687 RVA: 0x0004FD43 File Offset: 0x0004DF43
		// (set) Token: 0x060025D8 RID: 9688 RVA: 0x0004FD4B File Offset: 0x0004DF4B
		internal bool ShouldDownloadStaticFileOnLogonPage
		{
			get
			{
				return this.shouldDownloadStaticFileOnLogonPage;
			}
			set
			{
				this.shouldDownloadStaticFileOnLogonPage = value;
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x060025D9 RID: 9689 RVA: 0x0004FD54 File Offset: 0x0004DF54
		// (set) Token: 0x060025DA RID: 9690 RVA: 0x0004FD5C File Offset: 0x0004DF5C
		internal bool ShouldUseTenantHintOnLiveIdLogon
		{
			get
			{
				return this.shouldUseTenantHintOnLiveIdLogon;
			}
			set
			{
				this.shouldUseTenantHintOnLiveIdLogon = value;
			}
		}

		// Token: 0x040022F2 RID: 8946
		private bool shouldUseTenantHintOnLiveIdLogon = true;

		// Token: 0x040022F3 RID: 8947
		private bool shouldDownloadStaticFileOnLogonPage;
	}
}
