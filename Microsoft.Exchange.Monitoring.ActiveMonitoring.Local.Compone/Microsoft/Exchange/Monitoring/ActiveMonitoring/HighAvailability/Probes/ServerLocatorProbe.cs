using System;
using System.Reflection;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability.Probes
{
	// Token: 0x020001B0 RID: 432
	public class ServerLocatorProbe : ReplicationHealthChecksProbeBase
	{
		// Token: 0x06000C68 RID: 3176 RVA: 0x00050650 File Offset: 0x0004E850
		public static ProbeDefinition CreateDefinition(string probeNamePrefix, int recurrenceInterval)
		{
			return ServerLocatorProbe.CreateDefinition(HighAvailabilityConstants.ServiceName, "ServerLocator", probeNamePrefix, recurrenceInterval, recurrenceInterval / 2, 3);
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00050668 File Offset: 0x0004E868
		public static ProbeDefinition CreateDefinition(string serviceName, string targetResource, string probeName, int recurrenceInterval, int timeout, int maxRetry)
		{
			return new ProbeDefinition
			{
				AssemblyPath = Assembly.GetExecutingAssembly().Location,
				ServiceName = serviceName,
				TypeName = typeof(ServerLocatorProbe).FullName,
				Name = probeName,
				RecurrenceIntervalSeconds = recurrenceInterval,
				TimeoutSeconds = timeout,
				MaxRetryAttempts = maxRetry,
				TargetResource = targetResource
			};
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x000506CD File Offset: 0x0004E8CD
		protected override Type GetCheckType()
		{
			return typeof(ServerLocatorServiceCheck);
		}
	}
}
