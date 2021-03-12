using System;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Directory
{
	// Token: 0x02000141 RID: 321
	public class EscalateForSpecificExceptionResponder : EscalateResponder
	{
		// Token: 0x0600096C RID: 2412 RVA: 0x00039A75 File Offset: 0x00037C75
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			DirectoryUtils.InvokeBaseResponderMethodIfRequired(this, delegate(CancellationToken cancelToken)
			{
				this.<>n__FabricatedMethod1(cancelToken);
			}, base.TraceContext, cancellationToken);
		}
	}
}
