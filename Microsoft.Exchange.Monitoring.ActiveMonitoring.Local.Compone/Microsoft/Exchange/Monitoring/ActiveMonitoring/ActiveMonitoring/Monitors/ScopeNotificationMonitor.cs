using System;
using System.Data.SqlTypes;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Monitors
{
	// Token: 0x020000B0 RID: 176
	public abstract class ScopeNotificationMonitor : MonitorWorkItem
	{
		// Token: 0x06000615 RID: 1557 RVA: 0x00024700 File Offset: 0x00022900
		protected override void DoMonitorWork(CancellationToken cancellationToken)
		{
			IDataAccessQuery<MonitorResult> lastSuccessfulMonitorResult = base.Broker.GetLastSuccessfulMonitorResult(base.Definition);
			Task<MonitorResult> task = lastSuccessfulMonitorResult.ExecuteAsync(cancellationToken, base.TraceContext);
			task.Continue(delegate(MonitorResult lastMonitorResult)
			{
				DateTime startTime = SqlDateTime.MinValue.Value;
				if (lastMonitorResult != null)
				{
					startTime = lastMonitorResult.ExecutionStartTime;
				}
				this.AddScopeNotification(startTime, cancellationToken);
			}, cancellationToken, TaskContinuationOptions.AttachedToParent);
		}

		// Token: 0x06000616 RID: 1558
		protected abstract void AddScopeNotification(DateTime startTime, CancellationToken cancellationToken);

		// Token: 0x06000617 RID: 1559 RVA: 0x00024760 File Offset: 0x00022960
		protected ResultType TranslateHealthState(object healthStateValue)
		{
			if (healthStateValue is ServiceHealthStatus)
			{
				switch ((ServiceHealthStatus)healthStateValue)
				{
				case ServiceHealthStatus.None:
				case ServiceHealthStatus.Healthy:
					return ResultType.Succeeded;
				default:
					return ResultType.Failed;
				}
			}
			else
			{
				if (healthStateValue is MonitorAlertState)
				{
					MonitorAlertState monitorAlertState = (MonitorAlertState)healthStateValue;
					switch (monitorAlertState)
					{
					case MonitorAlertState.Unknown:
					case MonitorAlertState.Healthy:
						break;
					default:
						if (monitorAlertState != MonitorAlertState.Disabled)
						{
							return ResultType.Failed;
						}
						break;
					}
					return ResultType.Succeeded;
				}
				throw new NotSupportedException(string.Format("healthStateValue '{0}' of type '{1}' is not supported.", healthStateValue, healthStateValue.GetType().FullName));
			}
		}

		// Token: 0x040003CF RID: 975
		protected const string SourceInstanceType = "SourceInstanceType";

		// Token: 0x020000B1 RID: 177
		public enum InstanceType
		{
			// Token: 0x040003D1 RID: 977
			LAM,
			// Token: 0x040003D2 RID: 978
			SM,
			// Token: 0x040003D3 RID: 979
			XAM
		}
	}
}
