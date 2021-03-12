using System;
using System.Reflection;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.BitlockerDeployment.Probes
{
	// Token: 0x02000036 RID: 54
	public class DraProtectorProbe : ProbeWorkItem
	{
		// Token: 0x0600018F RID: 399 RVA: 0x0000C549 File Offset: 0x0000A749
		public static ProbeDefinition CreateDefinition(string probeName, int recurrenceInterval)
		{
			return DraProtectorProbe.CreateDefinition("BitlockerDeployment", Environment.MachineName, probeName, recurrenceInterval, recurrenceInterval / 2, 3);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000C560 File Offset: 0x0000A760
		public static ProbeDefinition CreateDefinition(string serviceName, string targetResource, string probeName, int recurrenceInterval, int timeout, int maxRetry)
		{
			return new ProbeDefinition
			{
				AssemblyPath = Assembly.GetExecutingAssembly().Location,
				ServiceName = serviceName,
				TypeName = typeof(DraProtectorProbe).FullName,
				Name = probeName,
				RecurrenceIntervalSeconds = recurrenceInterval,
				TimeoutSeconds = timeout,
				MaxRetryAttempts = maxRetry,
				TargetResource = targetResource
			};
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000C5C8 File Offset: 0x0000A7C8
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (!BitlockerDeploymentConstants.WorkflowFve)
			{
				base.Result.StateAttribute1 = "WorkflowFve=false; Skipping the Probe";
				return;
			}
			string volumesNotProtectedWithDra = BitlockerDeploymentUtility.GetVolumesNotProtectedWithDra();
			base.Result.StateAttribute1 = "Volumes not protected with DRA:" + volumesNotProtectedWithDra;
			if (string.IsNullOrEmpty(volumesNotProtectedWithDra))
			{
				base.Result.StateAttribute11 = "Passed";
				return;
			}
			base.Result.StateAttribute11 = "Failed";
			throw new Exception(string.Format("Some Volumes {0} on Server {1} are not protected with DRA", volumesNotProtectedWithDra, Environment.MachineName));
		}
	}
}
