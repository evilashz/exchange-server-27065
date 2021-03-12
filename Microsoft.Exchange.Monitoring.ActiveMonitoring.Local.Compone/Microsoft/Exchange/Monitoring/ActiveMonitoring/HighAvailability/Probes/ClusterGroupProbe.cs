using System;
using System.Reflection;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability.Probes
{
	// Token: 0x020001A7 RID: 423
	public class ClusterGroupProbe : ReplicationHealthChecksProbeBase
	{
		// Token: 0x06000C2E RID: 3118 RVA: 0x0004EFF8 File Offset: 0x0004D1F8
		public static ProbeDefinition CreateDefinition(string probeName, int recurrenceInterval)
		{
			return ClusterGroupProbe.CreateDefinition(HighAvailabilityConstants.ClusteringServiceName, "MSExchangeRepl", probeName, recurrenceInterval, recurrenceInterval / 2, 3);
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0004F010 File Offset: 0x0004D210
		public static ProbeDefinition CreateDefinition(string serviceName, string targetResource, string probeName, int recurrenceInterval, int timeout, int maxRetry)
		{
			return new ProbeDefinition
			{
				AssemblyPath = Assembly.GetExecutingAssembly().Location,
				ServiceName = serviceName,
				TypeName = typeof(ClusterGroupProbe).FullName,
				Name = probeName,
				RecurrenceIntervalSeconds = recurrenceInterval,
				TimeoutSeconds = timeout,
				MaxRetryAttempts = maxRetry,
				TargetResource = targetResource
			};
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0004F075 File Offset: 0x0004D275
		protected override Type GetCheckType()
		{
			return typeof(QuorumGroupCheck);
		}
	}
}
