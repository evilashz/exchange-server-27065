using System;
using System.Threading;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Store.Probes
{
	// Token: 0x020004D8 RID: 1240
	public class ActiveDatabaseAvailabilityProbe : DatabaseAvailabilityProbeBase
	{
		// Token: 0x06001ED1 RID: 7889 RVA: 0x000B98C0 File Offset: 0x000B7AC0
		protected override void DoWork(CancellationToken cancellationToken)
		{
			base.ActiveCopy = true;
			base.DoWork(cancellationToken);
		}
	}
}
