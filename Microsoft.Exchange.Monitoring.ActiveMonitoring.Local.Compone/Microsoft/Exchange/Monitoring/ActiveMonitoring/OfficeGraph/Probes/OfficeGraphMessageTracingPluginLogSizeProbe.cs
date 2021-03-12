using System;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.OfficeGraph.Probes
{
	// Token: 0x02000250 RID: 592
	public class OfficeGraphMessageTracingPluginLogSizeProbe : ProbeWorkItem
	{
		// Token: 0x060010A4 RID: 4260 RVA: 0x0006E9F0 File Offset: 0x0006CBF0
		protected override void DoWork(CancellationToken cancellationToken)
		{
			string targetResource = base.Definition.TargetResource;
			string machineName = Environment.MachineName;
			string text = string.Empty;
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
			DirectoryInfo directoryInfo = new DirectoryInfo("D:\\OfficeGraph");
			if (!directoryInfo.Exists)
			{
				text = string.Format("Office Graph Message Tracing plugin log directory does not exist: '{0}', thus no need to run the probe.", "D:\\OfficeGraph");
				base.Result.StateAttribute1 = text;
				OfficeGraphMonitoringHelper.LogInfo(this, text, new object[0]);
			}
			else
			{
				long directorySize = OfficeGraphMonitoringHelper.GetDirectorySize("D:\\OfficeGraph");
				base.Result.StateAttribute6 = (double)directorySize;
				OfficeGraphMonitoringHelper.LogInfo(this, string.Format("Log directory size is '{0}' for machine '{1}'", directorySize, machineName), new object[0]);
				if (directorySize >= 52428800L)
				{
					double num = (double)(directorySize / 1024L / 1024L);
					double num2 = 50.0;
					text = "Log directory size exceeds the size limit";
					base.Result.StateAttribute1 = text;
					OfficeGraphMonitoringHelper.LogInfo(text, new object[0]);
					throw new OfficeGraphProbeFailureException(Strings.OfficeGraphMessageTracingPluginLogDirectoryExceedsSizeLimit(machineName, num.ToString("N"), num2.ToString("N")));
				}
				text = "Log directory size is within the size limit";
				base.Result.StateAttribute1 = text;
				OfficeGraphMonitoringHelper.LogInfo(text, new object[0]);
				return;
			}
		}

		// Token: 0x04000C71 RID: 3185
		private const int MaxLogDirectorySize = 52428800;
	}
}
