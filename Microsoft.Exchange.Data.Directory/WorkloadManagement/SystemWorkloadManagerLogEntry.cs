using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000A08 RID: 2568
	internal class SystemWorkloadManagerLogEntry
	{
		// Token: 0x060076C3 RID: 30403 RVA: 0x0018727A File Offset: 0x0018547A
		public SystemWorkloadManagerLogEntry(SystemWorkloadManagerLogEntryType type, ResourceKey resource, WorkloadClassification classification, SystemWorkloadManagerEvent currentEvent, SystemWorkloadManagerEvent previousEvent)
		{
			this.Type = type;
			this.Resource = resource;
			this.Classification = classification;
			this.CurrentEvent = currentEvent;
			this.PreviousEvent = previousEvent;
		}

		// Token: 0x17002A7D RID: 10877
		// (get) Token: 0x060076C4 RID: 30404 RVA: 0x001872A7 File Offset: 0x001854A7
		// (set) Token: 0x060076C5 RID: 30405 RVA: 0x001872AF File Offset: 0x001854AF
		public SystemWorkloadManagerLogEntryType Type { get; private set; }

		// Token: 0x17002A7E RID: 10878
		// (get) Token: 0x060076C6 RID: 30406 RVA: 0x001872B8 File Offset: 0x001854B8
		// (set) Token: 0x060076C7 RID: 30407 RVA: 0x001872C0 File Offset: 0x001854C0
		public ResourceKey Resource { get; private set; }

		// Token: 0x17002A7F RID: 10879
		// (get) Token: 0x060076C8 RID: 30408 RVA: 0x001872C9 File Offset: 0x001854C9
		// (set) Token: 0x060076C9 RID: 30409 RVA: 0x001872D1 File Offset: 0x001854D1
		public WorkloadClassification Classification { get; private set; }

		// Token: 0x17002A80 RID: 10880
		// (get) Token: 0x060076CA RID: 30410 RVA: 0x001872DA File Offset: 0x001854DA
		// (set) Token: 0x060076CB RID: 30411 RVA: 0x001872E2 File Offset: 0x001854E2
		public SystemWorkloadManagerEvent CurrentEvent { get; private set; }

		// Token: 0x17002A81 RID: 10881
		// (get) Token: 0x060076CC RID: 30412 RVA: 0x001872EB File Offset: 0x001854EB
		// (set) Token: 0x060076CD RID: 30413 RVA: 0x001872F3 File Offset: 0x001854F3
		public SystemWorkloadManagerEvent PreviousEvent { get; private set; }

		// Token: 0x060076CE RID: 30414 RVA: 0x001872FC File Offset: 0x001854FC
		public override string ToString()
		{
			if (this.PreviousEvent != null)
			{
				return string.Format("{0}(Resource={1},Classification={2},Current=({3}),Previous=({4}))", new object[]
				{
					this.Type,
					this.Resource,
					this.Classification,
					this.CurrentEvent,
					this.PreviousEvent
				});
			}
			return string.Format("{0}(Resource={1},Classification={2},Current=({3}))", new object[]
			{
				this.Type,
				this.Resource,
				this.Classification,
				this.CurrentEvent
			});
		}

		// Token: 0x060076CF RID: 30415 RVA: 0x0018739B File Offset: 0x0018559B
		internal void Update(SystemWorkloadManagerEvent currentEvent)
		{
			this.CurrentEvent = currentEvent;
		}
	}
}
