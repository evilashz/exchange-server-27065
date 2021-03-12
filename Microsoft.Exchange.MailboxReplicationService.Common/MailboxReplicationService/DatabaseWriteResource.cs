using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200026A RID: 618
	internal class DatabaseWriteResource : DatabaseResource
	{
		// Token: 0x06001F36 RID: 7990 RVA: 0x00041478 File Offset: 0x0003F678
		private DatabaseWriteResource(Guid mdbGuid, WorkloadType workloadType) : base(mdbGuid, workloadType)
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
						base.UtilizationPerfCounter = mdbhelper.PerfCounter.UtilizationWriteInternalMaintenance;
						break;
					case WorkloadType.MailboxReplicationServiceInteractive:
						base.UtilizationPerfCounter = mdbhelper.PerfCounter.UtilizationWriteCustomerExpectation;
						break;
					}
				}
				else
				{
					base.UtilizationPerfCounter = mdbhelper.PerfCounter.UtilizationWriteHiPri;
				}
			}
			else
			{
				base.UtilizationPerfCounter = mdbhelper.PerfCounter.UtilizationWrite;
			}
			base.TransferRatePerfCounter = mdbhelper.WriteTransferRate;
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06001F37 RID: 7991 RVA: 0x00041510 File Offset: 0x0003F710
		public override int StaticCapacity
		{
			get
			{
				int config;
				using (base.ConfigContext.Activate())
				{
					config = ConfigBase<MRSConfigSchema>.GetConfig<int>("MaxActiveMovesPerTargetMDB");
				}
				return config;
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06001F38 RID: 7992 RVA: 0x00041554 File Offset: 0x0003F754
		public override string ResourceType
		{
			get
			{
				return string.Format("MdbWrite{0}", base.WlmWorkloadTypeSuffix);
			}
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x00041568 File Offset: 0x0003F768
		public override List<WlmResourceHealthMonitor> GetWlmResources()
		{
			return new List<WlmResourceHealthMonitor>
			{
				new MDBLatencyHealthMonitor(this, base.ResourceGuid),
				new MDBReplicationHealthMonitor(this, base.ResourceGuid),
				new MDBAvailabilityHealthMonitor(this, base.ResourceGuid),
				new CIAgeOfLastNotificationHealthMonitor(this, base.ResourceGuid),
				new DiskLatencyHealthMonitor(this, base.ResourceGuid)
			};
		}

		// Token: 0x04000C8E RID: 3214
		public static readonly WlmResourceCache<DatabaseWriteResource> Cache = new WlmResourceCache<DatabaseWriteResource>((Guid id, WorkloadType wlt) => new DatabaseWriteResource(id, wlt));
	}
}
