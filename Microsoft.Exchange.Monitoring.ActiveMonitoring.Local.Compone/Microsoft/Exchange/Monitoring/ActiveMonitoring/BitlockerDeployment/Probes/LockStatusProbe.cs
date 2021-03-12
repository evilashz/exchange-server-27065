using System;
using System.Management;
using System.Reflection;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.BitlockerDeployment.Probes
{
	// Token: 0x0200003A RID: 58
	public class LockStatusProbe : ProbeWorkItem
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x0000CADF File Offset: 0x0000ACDF
		public static ProbeDefinition CreateDefinition(string probeName, int recurrenceInterval)
		{
			return LockStatusProbe.CreateDefinition("BitlockerDeployment", Environment.MachineName, probeName, recurrenceInterval, recurrenceInterval / 2, 3);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000CAF8 File Offset: 0x0000ACF8
		public static ProbeDefinition CreateDefinition(string serviceName, string targetResource, string probeName, int recurrenceInterval, int timeout, int maxRetry)
		{
			return new ProbeDefinition
			{
				AssemblyPath = Assembly.GetExecutingAssembly().Location,
				ServiceName = serviceName,
				TypeName = typeof(LockStatusProbe).FullName,
				Name = probeName,
				RecurrenceIntervalSeconds = recurrenceInterval,
				TimeoutSeconds = timeout,
				MaxRetryAttempts = maxRetry,
				TargetResource = targetResource
			};
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000CB60 File Offset: 0x0000AD60
		protected override void DoWork(CancellationToken cancellationToken)
		{
			ManagementObjectCollection encryptableVolumes = BitlockerDeploymentUtility.GetEncryptableVolumes();
			if (encryptableVolumes == null || encryptableVolumes.Count == 0)
			{
				base.Result.StateAttribute1 = string.Format("Bitlocker Not Installed on this server {0}; Skipping the Probe", Environment.MachineName);
				return;
			}
			string lockedVolumes = BitlockerDeploymentUtility.GetLockedVolumes();
			string unlockedVolumes = BitlockerDeploymentUtility.GetUnlockedVolumes();
			base.Result.StateAttribute1 = "Locked Volumes:" + lockedVolumes;
			base.Result.StateAttribute2 = "Unlocked Volumes:" + unlockedVolumes;
			if (string.IsNullOrEmpty(lockedVolumes))
			{
				base.Result.StateAttribute11 = "Passed";
				return;
			}
			base.Result.StateAttribute11 = "Failed";
			throw new Exception(string.Format("Some Volumes {0} on Server {1} are locked", lockedVolumes, Environment.MachineName));
		}
	}
}
