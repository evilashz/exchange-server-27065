using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200027F RID: 639
	internal class MRSResource : LocalServerResource
	{
		// Token: 0x06001F92 RID: 8082 RVA: 0x00042758 File Offset: 0x00040958
		private MRSResource(WorkloadType workloadType) : base(workloadType)
		{
			WorkloadType wlmWorkloadType = base.WlmWorkloadType;
			if (wlmWorkloadType != WorkloadType.MailboxReplicationService)
			{
				if (wlmWorkloadType != WorkloadType.MailboxReplicationServiceHighPriority)
				{
					switch (wlmWorkloadType)
					{
					case WorkloadType.MailboxReplicationServiceInternalMaintenance:
						base.UtilizationPerfCounter = MailboxReplicationServicePerformanceCounters.UtilizationMRSInternalMaintenance;
						break;
					case WorkloadType.MailboxReplicationServiceInteractive:
						base.UtilizationPerfCounter = MailboxReplicationServicePerformanceCounters.UtilizationMRSCustomerExpectation;
						break;
					}
				}
				else
				{
					base.UtilizationPerfCounter = MailboxReplicationServicePerformanceCounters.UtilizationMRSHiPri;
				}
			}
			else
			{
				base.UtilizationPerfCounter = MailboxReplicationServicePerformanceCounters.UtilizationMRS;
			}
			base.TransferRatePerfCounter = MRSResource.MRSTransferRatePerfCounter;
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06001F93 RID: 8083 RVA: 0x000427CC File Offset: 0x000409CC
		public override int StaticCapacity
		{
			get
			{
				int config;
				using (base.ConfigContext.Activate())
				{
					config = ConfigBase<MRSConfigSchema>.GetConfig<int>("MaxTotalRequestsPerMRS");
				}
				return config;
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06001F94 RID: 8084 RVA: 0x00042810 File Offset: 0x00040A10
		public override string ResourceType
		{
			get
			{
				return string.Format("MRS{0}", base.WlmWorkloadTypeSuffix);
			}
		}

		// Token: 0x04000CB9 RID: 3257
		public static readonly WlmResourceCache<MRSResource> Cache = new WlmResourceCache<MRSResource>((Guid id, WorkloadType wlt) => new MRSResource(wlt));

		// Token: 0x04000CBA RID: 3258
		public static readonly ADObjectId Id = new ADObjectId(new Guid("6647EA79-5A87-400D-8659-E1181164044F"));

		// Token: 0x04000CBB RID: 3259
		public static readonly PerfCounterWithAverageRate MRSTransferRatePerfCounter = new PerfCounterWithAverageRate(null, MailboxReplicationServicePerformanceCounters.MRSTransferRate, MailboxReplicationServicePerformanceCounters.MRSTransferRateBase, 1024, TimeSpan.FromSeconds(1.0));
	}
}
