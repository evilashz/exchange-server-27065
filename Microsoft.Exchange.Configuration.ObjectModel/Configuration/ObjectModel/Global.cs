using System;
using System.Web;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x0200020B RID: 523
	public class Global : HttpApplication
	{
		// Token: 0x0600123A RID: 4666 RVA: 0x00039508 File Offset: 0x00037708
		private void Application_Start(object sender, EventArgs e)
		{
			this.InitializePerformanceCounter();
			this.InitDirectoryTopologyMode();
			ProvisioningCache.InitializeAppRegistrySettings(((IAppSettings)PswsAppSettings.Instance).ProvisioningCacheIdentification);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00039525 File Offset: 0x00037725
		private void InitializePerformanceCounter()
		{
			Globals.InitializeMultiPerfCounterInstance("Psws");
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00039531 File Offset: 0x00037731
		private void InitDirectoryTopologyMode()
		{
			ADSession.DisableAdminTopologyMode();
		}
	}
}
