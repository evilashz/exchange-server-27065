using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000064 RID: 100
	internal class SystemMailboxLightJobs : SystemMailboxScanJobs
	{
		// Token: 0x060004FE RID: 1278 RVA: 0x0001DECC File Offset: 0x0001C0CC
		public SystemMailboxLightJobs(Guid mdbGuid) : base(mdbGuid)
		{
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001DEF0 File Offset: 0x0001C0F0
		protected override void ProcessJobs(MapiStore systemMbx, MapiTable contentsTable, RequestJobNamedPropertySet nps)
		{
			SortOrder sortOrder = new SortOrder(nps.PropTags[17], SortFlags.Descend);
			sortOrder.Add(nps.PropTags[7], SortFlags.Ascend);
			MrsTracer.Service.Debug("Searching for MRs to Rehome...", new object[0]);
			Restriction restriction = Restriction.And(new Restriction[]
			{
				Restriction.EQ(nps.PropTags[4], false),
				Restriction.EQ(nps.PropTags[20], true)
			});
			MrsTracer.Service.Debug("Searching for MRs to Suspend...", new object[0]);
			base.ProcessJobsInBatches(restriction, false, sortOrder, contentsTable, systemMbx, null);
			Restriction restriction2 = Restriction.And(new Restriction[]
			{
				Restriction.BitMaskNonZero(nps.PropTags[10], 256),
				Restriction.EQ(nps.PropTags[4], false),
				Restriction.EQ(nps.PropTags[20], false),
				Restriction.NE(nps.PropTags[0], RequestStatus.Failed),
				Restriction.NE(nps.PropTags[0], RequestStatus.Suspended),
				Restriction.NE(nps.PropTags[0], RequestStatus.AutoSuspended),
				Restriction.NE(nps.PropTags[0], RequestStatus.Completed),
				Restriction.NE(nps.PropTags[0], RequestStatus.CompletedWithWarning)
			});
			base.ProcessJobsInBatches(restriction2, false, sortOrder, contentsTable, systemMbx, null);
			MrsTracer.Service.Debug("Searching for MRs to Resume...", new object[0]);
			Restriction restriction3 = Restriction.And(new Restriction[]
			{
				Restriction.BitMaskZero(nps.PropTags[10], 256),
				Restriction.EQ(nps.PropTags[4], false),
				Restriction.EQ(nps.PropTags[20], false),
				Restriction.Or(new Restriction[]
				{
					Restriction.EQ(nps.PropTags[0], RequestStatus.Failed),
					Restriction.EQ(nps.PropTags[0], RequestStatus.Suspended),
					Restriction.EQ(nps.PropTags[0], RequestStatus.AutoSuspended)
				})
			});
			base.ProcessJobsInBatches(restriction3, false, sortOrder, contentsTable, systemMbx, null);
			SortOrder sort = new SortOrder(nps.PropTags[13], SortFlags.Ascend);
			DateTime utcNow = DateTime.UtcNow;
			MrsTracer.Service.Debug("Searching for Completed MRs to clean up...", new object[0]);
			Restriction restriction4 = Restriction.And(new Restriction[]
			{
				Restriction.EQ(nps.PropTags[20], false),
				Restriction.EQ(nps.PropTags[4], false),
				Restriction.EQ(nps.PropTags[0], RequestStatus.Completed),
				Restriction.NE(nps.PropTags[13], SystemMailboxLightJobs.defaultDoNotPickUntil)
			});
			base.ProcessJobsInBatches(restriction4, false, sort, contentsTable, systemMbx, (MoveJob moveJob) => moveJob.DoNotPickUntilTimestamp > utcNow);
		}

		// Token: 0x04000213 RID: 531
		private const int DateTimeMinValue_SerializedAs = 0;

		// Token: 0x04000214 RID: 532
		private static DateTime defaultDoNotPickUntil = DateTime.FromFileTimeUtc(0L);
	}
}
