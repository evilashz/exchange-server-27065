using System;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004A2 RID: 1186
	internal interface IUMLocalMonitoringMonitorAndResponder
	{
		// Token: 0x06001DD8 RID: 7640
		void InitializeMonitorAndResponder(IMaintenanceWorkBroker broker, TracingContext traceContext);
	}
}
