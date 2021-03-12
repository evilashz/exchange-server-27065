using System;
using System.Reflection;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability.Probes
{
	// Token: 0x020001A9 RID: 425
	public class ClusterRpcProbe : ReplicationHealthChecksProbeBase
	{
		// Token: 0x06000C36 RID: 3126 RVA: 0x0004F119 File Offset: 0x0004D319
		public static ProbeDefinition CreateDefinition(string probeName, int recurrenceInterval)
		{
			return ClusterRpcProbe.CreateDefinition(HighAvailabilityConstants.ClusteringServiceName, "MSExchangeRepl", probeName, recurrenceInterval, recurrenceInterval / 2, 3);
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0004F130 File Offset: 0x0004D330
		public static ProbeDefinition CreateDefinition(string serviceName, string targetResource, string probeName, int recurrenceInterval, int timeout, int maxRetry)
		{
			return new ProbeDefinition
			{
				AssemblyPath = Assembly.GetExecutingAssembly().Location,
				ServiceName = serviceName,
				TypeName = typeof(ClusterRpcProbe).FullName,
				Name = probeName,
				RecurrenceIntervalSeconds = recurrenceInterval,
				TimeoutSeconds = timeout,
				MaxRetryAttempts = maxRetry,
				TargetResource = targetResource
			};
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0004F195 File Offset: 0x0004D395
		protected override Type GetCheckType()
		{
			return typeof(ClusterRpcCheck);
		}
	}
}
