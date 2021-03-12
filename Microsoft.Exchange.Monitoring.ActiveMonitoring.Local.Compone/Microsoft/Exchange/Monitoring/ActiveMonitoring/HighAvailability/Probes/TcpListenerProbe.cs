using System;
using System.Reflection;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability.Probes
{
	// Token: 0x020001B7 RID: 439
	public class TcpListenerProbe : ReplicationHealthChecksProbeBase
	{
		// Token: 0x06000C8F RID: 3215 RVA: 0x0005129D File Offset: 0x0004F49D
		public static ProbeDefinition CreateDefinition(string probeNamePrefix, int recurrenceInterval)
		{
			return TcpListenerProbe.CreateDefinition(HighAvailabilityConstants.ServiceName, "TCP", probeNamePrefix, recurrenceInterval, recurrenceInterval / 2, 3);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x000512B4 File Offset: 0x0004F4B4
		public static ProbeDefinition CreateDefinition(string serviceName, string targetResource, string probeName, int recurrenceInterval, int timeout, int maxRetry)
		{
			return new ProbeDefinition
			{
				AssemblyPath = Assembly.GetExecutingAssembly().Location,
				ServiceName = serviceName,
				TypeName = typeof(TcpListenerProbe).FullName,
				Name = probeName,
				RecurrenceIntervalSeconds = recurrenceInterval,
				TimeoutSeconds = timeout,
				MaxRetryAttempts = maxRetry,
				TargetResource = targetResource
			};
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00051319 File Offset: 0x0004F519
		protected override Type GetCheckType()
		{
			return typeof(TcpListenerCheck);
		}
	}
}
