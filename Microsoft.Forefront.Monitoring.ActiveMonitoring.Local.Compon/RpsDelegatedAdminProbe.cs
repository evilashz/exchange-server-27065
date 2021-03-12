using System;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x020001FD RID: 509
	public class RpsDelegatedAdminProbe : ProbeWorkItem
	{
		// Token: 0x06000F87 RID: 3975 RVA: 0x00028B70 File Offset: 0x00026D70
		protected override void DoWork(CancellationToken cancellationToken)
		{
			new RpsProbeHelper(this, true).DoWork(cancellationToken);
		}
	}
}
