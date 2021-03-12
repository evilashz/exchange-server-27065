using System;
using System.Threading;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Store.Probes
{
	// Token: 0x020004D9 RID: 1241
	public class PassiveDatabaseAvailabilityProbe : DatabaseAvailabilityProbeBase
	{
		// Token: 0x06001ED3 RID: 7891 RVA: 0x000B98D8 File Offset: 0x000B7AD8
		protected override void DoWork(CancellationToken cancellationToken)
		{
			base.ActiveCopy = false;
			base.DoWork(cancellationToken);
		}
	}
}
