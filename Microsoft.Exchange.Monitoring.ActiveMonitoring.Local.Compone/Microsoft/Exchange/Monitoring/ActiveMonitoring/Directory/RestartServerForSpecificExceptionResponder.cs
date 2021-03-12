using System;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Directory
{
	// Token: 0x02000142 RID: 322
	public class RestartServerForSpecificExceptionResponder : ForceRebootServerResponder
	{
		// Token: 0x06000970 RID: 2416 RVA: 0x00039AAA File Offset: 0x00037CAA
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			DirectoryUtils.InvokeBaseResponderMethodIfRequired(this, delegate(CancellationToken cancelToken)
			{
				this.<>n__FabricatedMethod1(cancelToken);
			}, base.TraceContext, cancellationToken);
		}
	}
}
