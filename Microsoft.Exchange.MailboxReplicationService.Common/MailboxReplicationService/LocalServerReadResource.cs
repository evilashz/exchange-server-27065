using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000270 RID: 624
	internal class LocalServerReadResource : LocalServerResource
	{
		// Token: 0x06001F45 RID: 8005 RVA: 0x000417A8 File Offset: 0x0003F9A8
		private LocalServerReadResource(WorkloadType workloadType) : base(workloadType)
		{
			WorkloadType wlmWorkloadType = base.WlmWorkloadType;
			if (wlmWorkloadType != WorkloadType.MailboxReplicationService)
			{
				if (wlmWorkloadType != WorkloadType.MailboxReplicationServiceHighPriority)
				{
					switch (wlmWorkloadType)
					{
					case WorkloadType.MailboxReplicationServiceInternalMaintenance:
						base.UtilizationPerfCounter = MailboxReplicationServicePerformanceCounters.UtilizationReadInternalMaintenance;
						break;
					case WorkloadType.MailboxReplicationServiceInteractive:
						base.UtilizationPerfCounter = MailboxReplicationServicePerformanceCounters.UtilizationReadCustomerExpectation;
						break;
					}
				}
				else
				{
					base.UtilizationPerfCounter = MailboxReplicationServicePerformanceCounters.UtilizationReadHiPri;
				}
			}
			else
			{
				base.UtilizationPerfCounter = MailboxReplicationServicePerformanceCounters.UtilizationRead;
			}
			base.TransferRatePerfCounter = LocalServerReadResource.ReadTransferRatePerfCounter;
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x06001F46 RID: 8006 RVA: 0x0004181C File Offset: 0x0003FA1C
		public override int StaticCapacity
		{
			get
			{
				int config;
				using (base.ConfigContext.Activate())
				{
					config = ConfigBase<MRSConfigSchema>.GetConfig<int>("MaxActiveMovesPerSourceServer");
				}
				return config;
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x06001F47 RID: 8007 RVA: 0x00041860 File Offset: 0x0003FA60
		public override string ResourceType
		{
			get
			{
				return string.Format("ServerRead{0}", base.WlmWorkloadTypeSuffix);
			}
		}

		// Token: 0x04000C9D RID: 3229
		public static readonly WlmResourceCache<LocalServerReadResource> Cache = new WlmResourceCache<LocalServerReadResource>((Guid id, WorkloadType wlt) => new LocalServerReadResource(wlt));

		// Token: 0x04000C9E RID: 3230
		public static readonly PerfCounterWithAverageRate ReadTransferRatePerfCounter = new PerfCounterWithAverageRate(null, MailboxReplicationServicePerformanceCounters.ReadTransferRate, MailboxReplicationServicePerformanceCounters.ReadTransferRateBase, 1024, TimeSpan.FromSeconds(1.0));
	}
}
