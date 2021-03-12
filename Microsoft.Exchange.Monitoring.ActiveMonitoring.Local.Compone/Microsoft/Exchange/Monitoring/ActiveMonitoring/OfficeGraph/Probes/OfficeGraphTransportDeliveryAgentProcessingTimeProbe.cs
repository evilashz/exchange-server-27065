using System;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.OfficeGraph.Probes
{
	// Token: 0x02000254 RID: 596
	public class OfficeGraphTransportDeliveryAgentProcessingTimeProbe : ProbeWorkItem
	{
		// Token: 0x060010B4 RID: 4276 RVA: 0x0006F150 File Offset: 0x0006D350
		protected override void DoWork(CancellationToken cancellationToken)
		{
			string targetResource = base.Definition.TargetResource;
			using (ServiceController serviceController = new ServiceController("MSExchangeDelivery"))
			{
				ServiceControllerStatus status = serviceController.Status;
				OfficeGraphMonitoringHelper.LogInfo(this, "The service '{0}' status is: '{1}'", new object[]
				{
					"MSExchangeDelivery",
					status
				});
				if (!status.Equals(ServiceControllerStatus.Running))
				{
					OfficeGraphMonitoringHelper.LogInfo(this, "The service '{0}' is not running, thus no need to run the probe.", new object[]
					{
						"MSExchangeDelivery"
					});
					return;
				}
			}
			double stateAttribute = 1.0;
			double num = (double)OfficeGraphMonitoringHelper.GetPerformanceCounterValue("MSExchange Delivery Extensibility Agents", "Average Agent Processing Time (sec)", "office graph agent");
			OfficeGraphMonitoringHelper.LogInfo(this, "Current processing time value: '{0}'", new object[]
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
			if (base.Result.StateAttribute8 > 2.0)
			{
				throw new OfficeGraphProbeFailureException(string.Format("The max processing time '{0}' is larger than 2 secs.", base.Result.StateAttribute8));
			}
			OfficeGraphMonitoringHelper.LogInfo(this, "Max processing time value is '{0}'.", new object[]
			{
				base.Result.StateAttribute8
			});
			double num4 = num3 / num2;
			if (num4 > 1.0)
			{
				throw new OfficeGraphProbeFailureException(string.Format("The average processing time '{0}' is larger than 1 sec.", num4));
			}
			OfficeGraphMonitoringHelper.LogInfo(this, "Average processing time value is '{0}'.", new object[]
			{
				num4
			});
		}
	}
}
