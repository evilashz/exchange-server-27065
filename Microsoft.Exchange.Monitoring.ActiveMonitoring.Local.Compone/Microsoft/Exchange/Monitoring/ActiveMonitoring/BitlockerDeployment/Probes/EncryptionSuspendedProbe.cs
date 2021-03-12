using System;
using System.Management;
using System.Reflection;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.BitlockerDeployment.Probes
{
	// Token: 0x02000039 RID: 57
	public class EncryptionSuspendedProbe : ProbeWorkItem
	{
		// Token: 0x0600019C RID: 412 RVA: 0x0000C9AC File Offset: 0x0000ABAC
		public static ProbeDefinition CreateDefinition(string probeName, int recurrenceInterval)
		{
			return EncryptionSuspendedProbe.CreateDefinition("BitlockerDeployment", Environment.MachineName, probeName, recurrenceInterval, recurrenceInterval / 2, 3);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000C9C4 File Offset: 0x0000ABC4
		public static ProbeDefinition CreateDefinition(string serviceName, string targetResource, string probeName, int recurrenceInterval, int timeout, int maxRetry)
		{
			return new ProbeDefinition
			{
				AssemblyPath = Assembly.GetExecutingAssembly().Location,
				ServiceName = serviceName,
				TypeName = typeof(EncryptionSuspendedProbe).FullName,
				Name = probeName,
				RecurrenceIntervalSeconds = recurrenceInterval,
				TimeoutSeconds = timeout,
				MaxRetryAttempts = maxRetry,
				TargetResource = targetResource
			};
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000CA29 File Offset: 0x0000AC29
		private string ConstructEncryptionStatusLine(string DeviceId, BitlockerDeploymentConstants.BitlockerEncryptionStates conversionStatus, BitlockerDeploymentConstants.BitlokerEncryptionProtectionStates protectionStatus)
		{
			return string.Format("Volume = {0}. ConversionStatus = {1}. ProtectionStatus = {2} \r\n", DeviceId, conversionStatus, protectionStatus);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000CA44 File Offset: 0x0000AC44
		protected override void DoWork(CancellationToken cancellationToken)
		{
			ManagementObjectCollection encryptableDataVolumes = BitlockerDeploymentUtility.GetEncryptableDataVolumes();
			if (encryptableDataVolumes == null || encryptableDataVolumes.Count == 0)
			{
				base.Result.StateAttribute1 = string.Format("Bitlocker Not Installed on this server {0}.; Skipping the Probe", Environment.MachineName);
				return;
			}
			string suspendedVolumes = BitlockerDeploymentUtility.GetSuspendedVolumes();
			base.Result.StateAttribute1 = "Suspended Volumes:" + suspendedVolumes;
			if (string.IsNullOrEmpty(suspendedVolumes))
			{
				base.Result.StateAttribute11 = "Passed";
				return;
			}
			base.Result.StateAttribute11 = "Failed";
			throw new Exception(string.Format("Some Volumes {0} on Server {1} are suspended", suspendedVolumes, Environment.MachineName));
		}
	}
}
