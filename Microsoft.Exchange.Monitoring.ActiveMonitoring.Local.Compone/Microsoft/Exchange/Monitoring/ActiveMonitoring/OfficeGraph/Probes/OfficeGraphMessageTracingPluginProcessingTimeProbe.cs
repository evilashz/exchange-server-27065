using System;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.OfficeGraph.Probes
{
	// Token: 0x02000251 RID: 593
	public class OfficeGraphMessageTracingPluginProcessingTimeProbe : ProbeWorkItem
	{
		// Token: 0x060010A6 RID: 4262 RVA: 0x0006EBAC File Offset: 0x0006CDAC
		protected override void DoWork(CancellationToken cancellationToken)
		{
			string targetResource = base.Definition.TargetResource;
			using (ServiceController serviceController = new ServiceController("MSMessageTracingClient"))
			{
				ServiceControllerStatus status = serviceController.Status;
				OfficeGraphMonitoringHelper.LogInfo(this, "The service '{0}' status is: '{1}'", new object[]
				{
					"MSMessageTracingClient",
					status
				});
				if (!status.Equals(ServiceControllerStatus.Running))
				{
					OfficeGraphMonitoringHelper.LogInfo(this, "The service '{0}' is not running, thus no need to run the probe.", new object[]
					{
						"MSMessageTracingClient"
					});
					return;
				}
			}
			double stateAttribute = 1.0;
			double num = (double)OfficeGraphMonitoringHelper.GetPerformanceCounterValue("Office Graph Writer - Message Tracing Plugin", "Average Signal Processing Time");
			OfficeGraphMonitoringHelper.LogInfo(this, "Current signal processing time value: '{0}'", new object[]
			{
				num
			});
			ProbeResult lastProbeResult = OfficeGraphMonitoringHelper.GetLastProbeResult(this, base.Broker, cancellationToken);
			if (lastProbeResult == null)
			{
				base.Result.StateAttribute6 = stateAttribute;
				base.Result.StateAttribute7 = num;
				base.Result.StateAttribute8 = num;
				OfficeGraphMonitoringHelper.LogInfo(this, "No previous probe run results. Set only the current values to probe result'", new object[0]);
				return;
			}
			double stateAttribute2 = lastProbeResult.StateAttribute6;
			double stateAttribute3 = lastProbeResult.StateAttribute7;
			double stateAttribute4 = lastProbeResult.StateAttribute8;
			double num2 = stateAttribute2 + 1.0;
			double num3 = stateAttribute3 + num;
			base.Result.StateAttribute6 = num2;
			base.Result.StateAttribute7 = num3;
			if (num > stateAttribute4)
			{
				base.Result.StateAttribute8 = num;
			}
			else
			{
				base.Result.StateAttribute8 = stateAttribute4;
			}
			if (base.Result.StateAttribute8 > 10000.0)
			{
				throw new OfficeGraphProbeFailureException(string.Format("The max signal processing time '{0}' is larger than 10 secs.", base.Result.StateAttribute8));
			}
			OfficeGraphMonitoringHelper.LogInfo(this, "Max signal processing time value is '{0}'.", new object[]
			{
				base.Result.StateAttribute8
			});
			double num4 = num3 / num2;
			if (num4 > 5000.0)
			{
				throw new OfficeGraphProbeFailureException(string.Format("The average signal processing time '{0}' is larger than 5 secs.", num4));
			}
			OfficeGraphMonitoringHelper.LogInfo(this, "Average signal processing time value is '{0}'.", new object[]
			{
				num4
			});
		}
	}
}
