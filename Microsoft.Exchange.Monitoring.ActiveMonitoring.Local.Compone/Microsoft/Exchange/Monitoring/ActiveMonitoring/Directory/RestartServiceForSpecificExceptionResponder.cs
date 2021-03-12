using System;
using System.Threading;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Directory
{
	// Token: 0x02000143 RID: 323
	public sealed class RestartServiceForSpecificExceptionResponder : RestartServiceResponder
	{
		// Token: 0x06000974 RID: 2420 RVA: 0x00039ADF File Offset: 0x00037CDF
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			DirectoryUtils.InvokeBaseResponderMethodIfRequired(this, delegate(CancellationToken cancelToken)
			{
				this.<>n__FabricatedMethod1(cancelToken);
			}, base.TraceContext, cancellationToken);
		}
	}
}
