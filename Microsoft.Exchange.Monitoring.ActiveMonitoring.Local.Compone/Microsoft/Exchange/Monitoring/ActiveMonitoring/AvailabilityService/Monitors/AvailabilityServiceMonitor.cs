using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.AvailabilityService.Monitors
{
	// Token: 0x0200001A RID: 26
	public class AvailabilityServiceMonitor : MonitorWorkItem
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000097BE File Offset: 0x000079BE
		protected int Threshold
		{
			get
			{
				return (int)base.Definition.MonitoringThreshold;
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000985C File Offset: 0x00007A5C
		protected override void DoMonitorWork(CancellationToken cancellationToken)
		{
			base.GetLastFailedProbeResultId(base.Definition.SampleMask, cancellationToken);
			IDataAccessQuery<AvailabilityServiceMonitor.ErrorCodesPair> errorCodeQuery = this.GetErrorCodeQuery();
			errorCodeQuery.ExecuteAsync(delegate(AvailabilityServiceMonitor.ErrorCodesPair result)
			{
				if (result != null)
				{
					this.ErrorCodes.AppendLine(string.Format("[{0}] ErrorCode:{1} ProbeId:{2}", result.ExecutionTime, result.ErrorCode, result.ExecutionId));
				}
			}, cancellationToken, base.TraceContext);
			IDataAccessQuery<AvailabilityServiceMonitor.TargetServerPair> targetServerQuery = this.GetTargetServerQuery();
			targetServerQuery.ExecuteAsync(delegate(AvailabilityServiceMonitor.TargetServerPair result)
			{
				if (result != null)
				{
					base.Result.StateAttribute1 = result.Server;
					base.Result.StateAttribute2 = this.ErrorCodes.ToString();
					base.Result.StateAttribute6 = (double)result.Count;
					base.Result.IsAlert = true;
				}
			}, cancellationToken, base.TraceContext);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00009904 File Offset: 0x00007B04
		protected virtual IDataAccessQuery<AvailabilityServiceMonitor.ErrorCodesPair> GetErrorCodeQuery()
		{
			IEnumerable<AvailabilityServiceMonitor.ErrorCodesPair> query = (from r in base.Broker.GetProbeResults(base.Definition.SampleMask, base.MonitoringWindowStartTime, base.Result.ExecutionStartTime)
			where r.ResultType == ResultType.Failed
			select new AvailabilityServiceMonitor.ErrorCodesPair
			{
				ExecutionTime = r.ExecutionStartTime,
				ErrorCode = r.StateAttribute2,
				ExecutionId = r.ExecutionId
			}).Take(this.Threshold);
			return base.Broker.AsDataAccessQuery<AvailabilityServiceMonitor.ErrorCodesPair>(query);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000099E8 File Offset: 0x00007BE8
		protected virtual IDataAccessQuery<AvailabilityServiceMonitor.TargetServerPair> GetTargetServerQuery()
		{
			IEnumerable<AvailabilityServiceMonitor.TargetServerPair> query = from r in base.Broker.GetProbeResults(base.Definition.SampleMask, base.MonitoringWindowStartTime, base.Result.ExecutionStartTime)
			where r.ResultType == ResultType.Failed
			group r by r.StateAttribute14 into r
			select new AvailabilityServiceMonitor.TargetServerPair
			{
				Server = r.Key,
				Count = r.Count<ProbeResult>()
			} into r
			where r.Count >= this.Threshold
			select r;
			return base.Broker.AsDataAccessQuery<AvailabilityServiceMonitor.TargetServerPair>(query);
		}

		// Token: 0x040000B9 RID: 185
		protected StringBuilder ErrorCodes = new StringBuilder();

		// Token: 0x0200001B RID: 27
		protected class ErrorCodesPair
		{
			// Token: 0x17000047 RID: 71
			// (get) Token: 0x06000137 RID: 311 RVA: 0x00009AB3 File Offset: 0x00007CB3
			// (set) Token: 0x06000138 RID: 312 RVA: 0x00009ABB File Offset: 0x00007CBB
			internal DateTime ExecutionTime { get; set; }

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x06000139 RID: 313 RVA: 0x00009AC4 File Offset: 0x00007CC4
			// (set) Token: 0x0600013A RID: 314 RVA: 0x00009ACC File Offset: 0x00007CCC
			internal string ErrorCode { get; set; }

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x0600013B RID: 315 RVA: 0x00009AD5 File Offset: 0x00007CD5
			// (set) Token: 0x0600013C RID: 316 RVA: 0x00009ADD File Offset: 0x00007CDD
			internal int ExecutionId { get; set; }
		}

		// Token: 0x0200001C RID: 28
		protected class TargetServerPair
		{
			// Token: 0x1700004A RID: 74
			// (get) Token: 0x0600013E RID: 318 RVA: 0x00009AEE File Offset: 0x00007CEE
			// (set) Token: 0x0600013F RID: 319 RVA: 0x00009AF6 File Offset: 0x00007CF6
			internal string Server { get; set; }

			// Token: 0x1700004B RID: 75
			// (get) Token: 0x06000140 RID: 320 RVA: 0x00009AFF File Offset: 0x00007CFF
			// (set) Token: 0x06000141 RID: 321 RVA: 0x00009B07 File Offset: 0x00007D07
			internal int Count { get; set; }
		}
	}
}
