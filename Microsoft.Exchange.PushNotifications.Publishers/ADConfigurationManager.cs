using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200008D RID: 141
	internal class ADConfigurationManager
	{
		// Token: 0x060004C4 RID: 1220 RVA: 0x0000FA92 File Offset: 0x0000DC92
		public ADConfigurationManager(ITopologyConfigurationSession configSession = null)
		{
			this.ConfigSession = (configSession ?? DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 29, ".ctor", "f:\\15.00.1497\\sources\\dev\\PushNotifications\\src\\publishers\\Configuration\\ADConfigurationManager.cs"));
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0000FAC2 File Offset: 0x0000DCC2
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x0000FACA File Offset: 0x0000DCCA
		public ITopologyConfigurationSession ConfigSession { get; set; }

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000FAD3 File Offset: 0x0000DCD3
		public virtual IEnumerable<PushNotificationApp> GetAllPushNotficationApps()
		{
			return this.ConfigSession.FindAllPaged<PushNotificationApp>();
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000FAE0 File Offset: 0x0000DCE0
		public virtual void Save(PushNotificationApp pushApp)
		{
			this.ConfigSession.Save(pushApp);
		}
	}
}
