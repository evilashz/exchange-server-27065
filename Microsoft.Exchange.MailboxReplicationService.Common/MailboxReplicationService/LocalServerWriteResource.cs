using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000271 RID: 625
	internal class LocalServerWriteResource : LocalServerResource
	{
		// Token: 0x06001F4A RID: 8010 RVA: 0x000418D8 File Offset: 0x0003FAD8
		private LocalServerWriteResource(WorkloadType workloadType) : base(workloadType)
		{
			WorkloadType wlmWorkloadType = base.WlmWorkloadType;
			if (wlmWorkloadType != WorkloadType.MailboxReplicationService)
			{
				if (wlmWorkloadType != WorkloadType.MailboxReplicationServiceHighPriority)
				{
					switch (wlmWorkloadType)
					{
					case WorkloadType.MailboxReplicationServiceInternalMaintenance:
						base.UtilizationPerfCounter = MailboxReplicationServicePerformanceCounters.UtilizationWriteInternalMaintenance;
						break;
					case WorkloadType.MailboxReplicationServiceInteractive:
						base.UtilizationPerfCounter = MailboxReplicationServicePerformanceCounters.UtilizationWriteCustomerExpectation;
						break;
					}
				}
				else
				{
					base.UtilizationPerfCounter = MailboxReplicationServicePerformanceCounters.UtilizationWriteHiPri;
				}
			}
			else
			{
				base.UtilizationPerfCounter = MailboxReplicationServicePerformanceCounters.UtilizationWrite;
			}
			base.TransferRatePerfCounter = LocalServerWriteResource.WriteTransferRatePerfCounter;
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06001F4B RID: 8011 RVA: 0x0004194C File Offset: 0x0003FB4C
		public override int StaticCapacity
		{
			get
			{
				int config;
				using (base.ConfigContext.Activate())
				{
					config = ConfigBase<MRSConfigSchema>.GetConfig<int>("MaxActiveMovesPerTargetServer");
				}
				return config;
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x00041990 File Offset: 0x0003FB90
		public override string ResourceType
		{
			get
			{
				return string.Format("ServerWrite{0}", base.WlmWorkloadTypeSuffix);
			}
		}

		// Token: 0x04000CA0 RID: 3232
		public static readonly WlmResourceCache<LocalServerWriteResource> Cache = new WlmResourceCache<LocalServerWriteResource>((Guid id, WorkloadType wlt) => new LocalServerWriteResource(wlt));

		// Token: 0x04000CA1 RID: 3233
		public static readonly PerfCounterWithAverageRate WriteTransferRatePerfCounter = new PerfCounterWithAverageRate(null, MailboxReplicationServicePerformanceCounters.WriteTransferRate, MailboxReplicationServicePerformanceCounters.WriteTransferRateBase, 1024, TimeSpan.FromSeconds(1.0));
	}
}
