using System;
using Microsoft.Web.Administration;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200002B RID: 43
	internal class WebAdminDataProvider : IWebAdminDataProvider
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00003CA8 File Offset: 0x00001EA8
		public WebAdminDataProvider()
		{
			this.iisSeverManager = new ServerManager();
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003CBB File Offset: 0x00001EBB
		public bool Enable32BitAppOnWin64
		{
			get
			{
				return this.iisSeverManager.ApplicationPoolDefaults.Enable32BitAppOnWin64;
			}
		}

		// Token: 0x0400009B RID: 155
		private ServerManager iisSeverManager;
	}
}
