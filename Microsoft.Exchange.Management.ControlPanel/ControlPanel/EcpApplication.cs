using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001A0 RID: 416
	[ClientScriptResource("EcpApplication", "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	public class EcpApplication : ScriptComponent
	{
		// Token: 0x17001AC3 RID: 6851
		// (get) Token: 0x06002337 RID: 9015 RVA: 0x0006B1A7 File Offset: 0x000693A7
		// (set) Token: 0x06002338 RID: 9016 RVA: 0x0006B1AF File Offset: 0x000693AF
		public string NavigationID { get; set; }

		// Token: 0x17001AC4 RID: 6852
		// (get) Token: 0x06002339 RID: 9017 RVA: 0x0006B1B8 File Offset: 0x000693B8
		// (set) Token: 0x0600233A RID: 9018 RVA: 0x0006B1C0 File Offset: 0x000693C0
		public int MaxQueuedPerfRecords { get; set; }

		// Token: 0x17001AC5 RID: 6853
		// (get) Token: 0x0600233B RID: 9019 RVA: 0x0006B1C9 File Offset: 0x000693C9
		// (set) Token: 0x0600233C RID: 9020 RVA: 0x0006B1D1 File Offset: 0x000693D1
		public int? NotificationRefreshInterval { get; set; }

		// Token: 0x17001AC6 RID: 6854
		// (get) Token: 0x0600233D RID: 9021 RVA: 0x0006B1DA File Offset: 0x000693DA
		// (set) Token: 0x0600233E RID: 9022 RVA: 0x0006B1E2 File Offset: 0x000693E2
		public int? HeartbeatRefreshInterval { get; set; }

		// Token: 0x0600233F RID: 9023 RVA: 0x0006B1EC File Offset: 0x000693EC
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddComponentProperty("Navigation", this.NavigationID);
			descriptor.AddProperty("MaxQueuedPerfRecords", this.MaxQueuedPerfRecords, 50);
			if (this.NotificationRefreshInterval != null)
			{
				descriptor.AddProperty("NotificationRefreshInterval", this.NotificationRefreshInterval.Value, 30000);
			}
			if (this.HeartbeatRefreshInterval != null)
			{
				descriptor.AddProperty("HeartbeatRefreshInterval", this.HeartbeatRefreshInterval.Value);
			}
		}
	}
}
