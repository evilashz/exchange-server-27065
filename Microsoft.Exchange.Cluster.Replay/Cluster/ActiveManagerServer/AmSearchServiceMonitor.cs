using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000067 RID: 103
	internal class AmSearchServiceMonitor : AmServiceMonitor
	{
		// Token: 0x06000478 RID: 1144 RVA: 0x0001801B File Offset: 0x0001621B
		internal AmSearchServiceMonitor() : base(ComponentInstance.Globals.Search.ServiceName)
		{
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0001802D File Offset: 0x0001622D
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x00018035 File Offset: 0x00016235
		public bool IsServiceRunning { get; private set; }

		// Token: 0x0600047B RID: 1147 RVA: 0x0001803E File Offset: 0x0001623E
		protected override void OnStop()
		{
			AmTrace.Debug("AmSearchServiceMonitor: service stop detected.", new object[0]);
			this.IsServiceRunning = false;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00018057 File Offset: 0x00016257
		protected override void OnStart()
		{
			AmTrace.Debug("AmSearchServiceMonitor: service start detected.", new object[0]);
			this.IsServiceRunning = true;
		}
	}
}
