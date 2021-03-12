using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000269 RID: 617
	internal class DatabaseReadResource : DatabaseResource
	{
		// Token: 0x06001F30 RID: 7984 RVA: 0x0004131C File Offset: 0x0003F51C
		private DatabaseReadResource(Guid mdbGuid, WorkloadType workloadType) : base(mdbGuid, workloadType)
		{
			MDBPerfCounterHelper mdbhelper = MDBPerfCounterHelperCollection.GetMDBHelper(mdbGuid, true);
			WorkloadType wlmWorkloadType = base.WlmWorkloadType;
			if (wlmWorkloadType != WorkloadType.MailboxReplicationService)
			{
				if (wlmWorkloadType != WorkloadType.MailboxReplicationServiceHighPriority)
				{
					switch (wlmWorkloadType)
					{
					case WorkloadType.MailboxReplicationServiceInternalMaintenance:
						base.UtilizationPerfCounter = mdbhelper.PerfCounter.UtilizationReadInternalMaintenance;
						break;
					case WorkloadType.MailboxReplicationServiceInteractive:
						base.UtilizationPerfCounter = mdbhelper.PerfCounter.UtilizationReadCustomerExpectation;
						break;
					}
				}
				else
				{
					base.UtilizationPerfCounter = mdbhelper.PerfCounter.UtilizationReadHiPri;
				}
			}
			else
			{
				base.UtilizationPerfCounter = mdbhelper.PerfCounter.UtilizationRead;
			}
			base.TransferRatePerfCounter = mdbhelper.ReadTransferRate;
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06001F31 RID: 7985 RVA: 0x000413B4 File Offset: 0x0003F5B4
		public override int StaticCapacity
		{
			get
			{
				int config;
				using (base.ConfigContext.Activate())
				{
					config = ConfigBase<MRSConfigSchema>.GetConfig<int>("MaxActiveMovesPerSourceMDB");
				}
				return config;
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06001F32 RID: 7986 RVA: 0x000413F8 File Offset: 0x0003F5F8
		public override string ResourceType
		{
			get
			{
				return string.Format("MdbRead{0}", base.WlmWorkloadTypeSuffix);
			}
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x0004140C File Offset: 0x0003F60C
		public override List<WlmResourceHealthMonitor> GetWlmResources()
		{
			return new List<WlmResourceHealthMonitor>
			{
				new MDBLatencyHealthMonitor(this, base.ResourceGuid),
				new DiskLatencyHealthMonitor(this, base.ResourceGuid)
			};
		}

		// Token: 0x04000C8C RID: 3212
		public static readonly WlmResourceCache<DatabaseReadResource> Cache = new WlmResourceCache<DatabaseReadResource>((Guid id, WorkloadType wlt) => new DatabaseReadResource(id, wlt));
	}
}
