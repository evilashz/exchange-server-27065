using System;
using System.Management;
using System.Reflection;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.BitlockerDeployment.Probes
{
	// Token: 0x02000038 RID: 56
	public class EncryptionStatusProbe : ProbeWorkItem
	{
		// Token: 0x06000197 RID: 407 RVA: 0x0000C7E0 File Offset: 0x0000A9E0
		public static ProbeDefinition CreateDefinition(string probeName, int recurrenceInterval)
		{
			return EncryptionStatusProbe.CreateDefinition("BitlockerDeployment", Environment.MachineName, probeName, recurrenceInterval, recurrenceInterval / 2, 3);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000C7F8 File Offset: 0x0000A9F8
		public static ProbeDefinition CreateDefinition(string serviceName, string targetResource, string probeName, int recurrenceInterval, int timeout, int maxRetry)
		{
			return new ProbeDefinition
			{
				AssemblyPath = Assembly.GetExecutingAssembly().Location,
				ServiceName = serviceName,
				TypeName = typeof(EncryptionStatusProbe).FullName,
				Name = probeName,
				RecurrenceIntervalSeconds = recurrenceInterval,
				TimeoutSeconds = timeout,
				MaxRetryAttempts = maxRetry,
				TargetResource = targetResource
			};
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000C85D File Offset: 0x0000AA5D
		private string ConstructEncryptionStatusLine(string DeviceId, BitlockerDeploymentConstants.BitlockerEncryptionStates conversionStatus, int percentage)
		{
			return string.Format("Volume = {0}. ConversionStatus = {1}. EncryptionPercentage = {2} \r\n", DeviceId, conversionStatus, percentage);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000C878 File Offset: 0x0000AA78
		protected override void DoWork(CancellationToken cancellationToken)
		{
			ManagementObjectCollection encryptableDataVolumes = BitlockerDeploymentUtility.GetEncryptableDataVolumes();
			if (encryptableDataVolumes == null || encryptableDataVolumes.Count == 0)
			{
				base.Result.StateAttribute1 = string.Format("Bitlocker Not Installed on this server {0}.; Skipping the Probe", Environment.MachineName);
				return;
			}
			if (!BitlockerDeploymentConstants.WorkflowFve)
			{
				base.Result.StateAttribute1 = "WorkflowFve=false; Skipping the Probe";
				return;
			}
			bool flag = true;
			foreach (ManagementBaseObject managementBaseObject in encryptableDataVolumes)
			{
				ManagementObject encryptableVolume = (ManagementObject)managementBaseObject;
				if (!BitlockerDeploymentUtility.IsVolumeLocked(encryptableVolume))
				{
					BitlockerDeploymentUtility.GetDeviceID(encryptableVolume);
					BitlockerDeploymentConstants.BitlockerEncryptionStates conversionStatus = BitlockerDeploymentUtility.GetConversionStatus(encryptableVolume);
					BitlockerDeploymentUtility.EncryptionPercentage(encryptableVolume);
					if (conversionStatus == BitlockerDeploymentConstants.BitlockerEncryptionStates.FullyDecrypted)
					{
						flag = false;
					}
					else if (conversionStatus == BitlockerDeploymentConstants.BitlockerEncryptionStates.DecryptionInProgress || conversionStatus == BitlockerDeploymentConstants.BitlockerEncryptionStates.DecryptionPaused)
					{
						flag = false;
					}
				}
			}
			base.Result.StateAttribute1 = "Encrypted Volumes : " + BitlockerDeploymentUtility.GetEncryptedEncryptableDataVolumes();
			base.Result.StateAttribute2 = "Unencrypted Volumes : " + BitlockerDeploymentUtility.GetUnencryptedEncryptableDataVolumes();
			base.Result.StateAttribute11 = (flag ? "Passed" : "Failed");
			if (!flag)
			{
				throw new Exception(string.Format("Some Volumes {0} on Server {1} aren't encrypted", BitlockerDeploymentUtility.GetUnencryptedEncryptableDataVolumes(), Environment.MachineName));
			}
		}
	}
}
