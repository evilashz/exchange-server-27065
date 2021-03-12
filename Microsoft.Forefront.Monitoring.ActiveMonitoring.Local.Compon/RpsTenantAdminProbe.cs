using System;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000205 RID: 517
	public class RpsTenantAdminProbe : ProbeWorkItem
	{
		// Token: 0x06000FCB RID: 4043 RVA: 0x000295B2 File Offset: 0x000277B2
		protected override void DoWork(CancellationToken cancellationToken)
		{
			new RpsProbeHelper(this, false).DoWork(cancellationToken);
		}
	}
}
