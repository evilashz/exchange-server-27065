using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.BitlockerDeployment.Probes
{
	// Token: 0x02000037 RID: 55
	public class DraDecryptorProbe : ProbeWorkItem
	{
		// Token: 0x06000193 RID: 403 RVA: 0x0000C64F File Offset: 0x0000A84F
		public static ProbeDefinition CreateDefinition(string probeName, int recurrenceInterval)
		{
			return DraDecryptorProbe.CreateDefinition("BitlockerDeployment", Environment.MachineName, probeName, recurrenceInterval, recurrenceInterval / 2, 3);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000C668 File Offset: 0x0000A868
		public static ProbeDefinition CreateDefinition(string serviceName, string targetResource, string probeName, int recurrenceInterval, int timeout, int maxRetry)
		{
			return new ProbeDefinition
			{
				AssemblyPath = Assembly.GetExecutingAssembly().Location,
				ServiceName = serviceName,
				TypeName = typeof(DraDecryptorProbe).FullName,
				Name = probeName,
				RecurrenceIntervalSeconds = recurrenceInterval,
				TimeoutSeconds = timeout,
				MaxRetryAttempts = maxRetry,
				TargetResource = targetResource
			};
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000C6D0 File Offset: 0x0000A8D0
		protected override void DoWork(CancellationToken cancellationToken)
		{
			Dictionary<string, string> volumesWithoutDraDecryptor = BitlockerDeploymentUtility.GetVolumesWithoutDraDecryptor();
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			foreach (KeyValuePair<string, string> keyValuePair in volumesWithoutDraDecryptor)
			{
				stringBuilder.Append(string.Format("[{0}]", keyValuePair.Key));
				stringBuilder2.Append(string.Format("[{0}]", keyValuePair.Value));
			}
			string text = stringBuilder.ToString();
			string str = stringBuilder2.ToString();
			if (stringBuilder.Length <= 0)
			{
				base.Result.StateAttribute11 = "Passed";
				return;
			}
			base.Result.StateAttribute11 = "Failed";
			base.Result.StateAttribute1 = "Volumes protected with DRA without decryptor:" + text;
			base.Result.StateAttribute2 = "Decryptors not found in store:" + str;
			throw new Exception(string.Format("Some Volumes {0} on Server {1} are not protected with DRA", text, Environment.MachineName));
		}
	}
}
