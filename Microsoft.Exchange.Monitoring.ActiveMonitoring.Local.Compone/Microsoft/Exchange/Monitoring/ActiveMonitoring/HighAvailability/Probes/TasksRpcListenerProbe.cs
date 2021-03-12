using System;
using System.Reflection;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability.Probes
{
	// Token: 0x020001B6 RID: 438
	public class TasksRpcListenerProbe : ReplicationHealthChecksProbeBase
	{
		// Token: 0x06000C8B RID: 3211 RVA: 0x0005120D File Offset: 0x0004F40D
		public static ProbeDefinition CreateDefinition(string probeNamePrefix, int recurrenceInterval)
		{
			return TasksRpcListenerProbe.CreateDefinition(HighAvailabilityConstants.ServiceName, "RPC", probeNamePrefix, recurrenceInterval, recurrenceInterval / 2, 3);
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00051224 File Offset: 0x0004F424
		public static ProbeDefinition CreateDefinition(string serviceName, string targetResource, string probeName, int recurrenceInterval, int timeout, int maxRetry)
		{
			return new ProbeDefinition
			{
				AssemblyPath = Assembly.GetExecutingAssembly().Location,
				ServiceName = serviceName,
				TypeName = typeof(TasksRpcListenerProbe).FullName,
				Name = probeName,
				RecurrenceIntervalSeconds = recurrenceInterval,
				TimeoutSeconds = timeout,
				MaxRetryAttempts = maxRetry,
				TargetResource = targetResource
			};
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00051289 File Offset: 0x0004F489
		protected override Type GetCheckType()
		{
			return typeof(TasksRpcListenerCheck);
		}
	}
}
