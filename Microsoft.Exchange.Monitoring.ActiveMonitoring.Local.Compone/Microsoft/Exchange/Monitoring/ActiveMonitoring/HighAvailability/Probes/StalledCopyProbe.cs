using System;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability.Probes
{
	// Token: 0x020001B4 RID: 436
	internal class StalledCopyProbe : CopyStatusProbeBase
	{
		// Token: 0x06000C82 RID: 3202 RVA: 0x00050EDC File Offset: 0x0004F0DC
		public static ProbeDefinition CreateDefinition(string name, string serviceName, MailboxDatabaseInfo targetDatabase, int recurrenceInterval)
		{
			return CopyStatusProbeBase.CreateDefinition(name, typeof(StalledCopyProbe).FullName, serviceName, targetDatabase, recurrenceInterval, recurrenceInterval / 2, 3);
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00050F08 File Offset: 0x0004F108
		protected override void DoWork(CancellationToken cancellationToken)
		{
			base.DoWork(cancellationToken);
			if (base.CopyStatus.CopyStatus == CopyStatusEnum.DisconnectedAndHealthy || base.CopyStatus.CopyStatus == CopyStatusEnum.DisconnectedAndResynchronizing)
			{
				base.Result.StateAttribute11 = "Failed";
				throw new HighAvailabilityMAProbeRedResultException(string.Format("Database Copy {0} on server {1} is {2}", base.CopyStatus.DatabaseName, Environment.MachineName, base.CopyStatus.CopyStatus));
			}
			base.Result.StateAttribute11 = string.Format("Passed. CurrentState={0}", base.CopyStatus.CopyStatus);
		}
	}
}
