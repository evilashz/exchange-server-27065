using System;
using System.Reflection;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability.Probes
{
	// Token: 0x020001A5 RID: 421
	public class ActiveManagerProbe : ReplicationHealthChecksProbeBase
	{
		// Token: 0x06000C24 RID: 3108 RVA: 0x0004ECB5 File Offset: 0x0004CEB5
		public static ProbeDefinition CreateDefinition(string probeName, int recurrenceInterval)
		{
			return ActiveManagerProbe.CreateDefinition(HighAvailabilityConstants.ServiceName, "MSExchangeRepl", probeName, recurrenceInterval, recurrenceInterval / 2, 3);
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0004ECCC File Offset: 0x0004CECC
		public static ProbeDefinition CreateDefinition(string serviceName, string targetResource, string probeName, int recurrenceInterval, int timeout, int maxRetry)
		{
			return new ProbeDefinition
			{
				AssemblyPath = Assembly.GetExecutingAssembly().Location,
				ServiceName = serviceName,
				TypeName = typeof(ActiveManagerProbe).FullName,
				Name = probeName,
				RecurrenceIntervalSeconds = recurrenceInterval,
				TimeoutSeconds = timeout,
				MaxRetryAttempts = maxRetry,
				TargetResource = targetResource
			};
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0004ED31 File Offset: 0x0004CF31
		protected override Type GetCheckType()
		{
			return typeof(ActiveManagerCheck);
		}
	}
}
