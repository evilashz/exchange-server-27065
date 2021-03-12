using System;
using System.Reflection;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.BitlockerDeployment.Probes
{
	// Token: 0x02000035 RID: 53
	public class BootVolumeEncryptionStatusProbe : ProbeWorkItem
	{
		// Token: 0x0600018A RID: 394 RVA: 0x0000C440 File Offset: 0x0000A640
		public static ProbeDefinition CreateDefinition(string probeName, int recurrenceInterval)
		{
			return BootVolumeEncryptionStatusProbe.CreateDefinition("BitlockerDeployment", Environment.MachineName, probeName, recurrenceInterval, recurrenceInterval / 2, 3);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000C458 File Offset: 0x0000A658
		public static ProbeDefinition CreateDefinition(string serviceName, string targetResource, string probeName, int recurrenceInterval, int timeout, int maxRetry)
		{
			return new ProbeDefinition
			{
				AssemblyPath = Assembly.GetExecutingAssembly().Location,
				ServiceName = serviceName,
				TypeName = typeof(BootVolumeEncryptionStatusProbe).FullName,
				Name = probeName,
				RecurrenceIntervalSeconds = recurrenceInterval,
				TimeoutSeconds = timeout,
				MaxRetryAttempts = maxRetry,
				TargetResource = targetResource
			};
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000C4BD File Offset: 0x0000A6BD
		private string ConstructEncryptionStatusLine(string DeviceId, BitlockerDeploymentConstants.BitlockerEncryptionStates conversionStatus, int percentage)
		{
			return string.Format("Volume = {0}. ConversionStatus = {1}. EncryptionPercentage = {2} \r\n", DeviceId, conversionStatus, percentage);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000C4D8 File Offset: 0x0000A6D8
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (BitlockerDeploymentUtility.IsBootVolumeEncrypted())
			{
				base.Result.StateAttribute1 = "Boot Volume is Encrypted";
				base.Result.StateAttribute11 = "Failed";
				throw new Exception(string.Format("Boot Volume {0} is encrypted", Environment.MachineName));
			}
			base.Result.StateAttribute1 = "Boot Volume is not Encrypted";
			base.Result.StateAttribute11 = "Passed";
		}
	}
}
