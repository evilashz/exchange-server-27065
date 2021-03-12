using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.AvailabilityService.Monitors
{
	// Token: 0x0200001D RID: 29
	public class AvailabilityServiceOffBoxRequestMonitor : AvailabilityServiceMonitor
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00009B68 File Offset: 0x00007D68
		protected override IDataAccessQuery<AvailabilityServiceMonitor.TargetServerPair> GetTargetServerQuery()
		{
			IEnumerable<AvailabilityServiceMonitor.TargetServerPair> query = from r in base.Broker.GetProbeResults(base.Definition.SampleMask, base.MonitoringWindowStartTime, base.Result.ExecutionStartTime).Take(base.Threshold)
			where r.ResultType == ResultType.Failed
			group r by r.StateAttribute14 into r
			select new AvailabilityServiceMonitor.TargetServerPair
			{
				Server = r.Key,
				Count = r.Count<ProbeResult>()
			} into r
			where r.Count == base.Threshold
			select r;
			return base.Broker.AsDataAccessQuery<AvailabilityServiceMonitor.TargetServerPair>(query);
		}
	}
}
