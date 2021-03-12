using System;
using System.Reflection;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability.Probes
{
	// Token: 0x020001A8 RID: 424
	public class ClusterNetworkProbe : ReplicationHealthChecksProbeBase
	{
		// Token: 0x06000C32 RID: 3122 RVA: 0x0004F089 File Offset: 0x0004D289
		public static ProbeDefinition CreateDefinition(string probeName, int recurrenceInterval)
		{
			return ClusterNetworkProbe.CreateDefinition(HighAvailabilityConstants.ClusteringServiceName, "MSExchangeRepl", probeName, recurrenceInterval, recurrenceInterval / 2, 3);
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0004F0A0 File Offset: 0x0004D2A0
		public static ProbeDefinition CreateDefinition(string serviceName, string targetResource, string probeName, int recurrenceInterval, int timeout, int maxRetry)
		{
			return new ProbeDefinition
			{
				AssemblyPath = Assembly.GetExecutingAssembly().Location,
				ServiceName = serviceName,
				TypeName = typeof(ClusterNetworkProbe).FullName,
				Name = probeName,
				RecurrenceIntervalSeconds = recurrenceInterval,
				TimeoutSeconds = timeout,
				MaxRetryAttempts = maxRetry,
				TargetResource = targetResource
			};
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0004F105 File Offset: 0x0004D305
		protected override Type GetCheckType()
		{
			return typeof(ClusterNetworkCheck);
		}
	}
}
