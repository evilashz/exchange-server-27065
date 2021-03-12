using System;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002FD RID: 765
	internal class TrackingContext
	{
		// Token: 0x060016B2 RID: 5810 RVA: 0x00069BAC File Offset: 0x00067DAC
		public TrackingContext(LogCache cache, DirectoryContext directoryContext, MessageTrackingReportId startingEventId)
		{
			this.tree = new EventTree();
			this.cache = cache;
			this.directoryContext = directoryContext;
			this.startingEventId = startingEventId;
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x00069BD4 File Offset: 0x00067DD4
		// (set) Token: 0x060016B4 RID: 5812 RVA: 0x00069BDC File Offset: 0x00067DDC
		public string SelectedRecipient
		{
			get
			{
				return this.selectedRecipient;
			}
			set
			{
				this.selectedRecipient = value;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060016B5 RID: 5813 RVA: 0x00069BE5 File Offset: 0x00067DE5
		// (set) Token: 0x060016B6 RID: 5814 RVA: 0x00069BED File Offset: 0x00067DED
		public ReportTemplate ReportTemplate
		{
			get
			{
				return this.reportTemplate;
			}
			set
			{
				this.reportTemplate = value;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x00069BF6 File Offset: 0x00067DF6
		// (set) Token: 0x060016B8 RID: 5816 RVA: 0x00069BFE File Offset: 0x00067DFE
		public MessageTrackingDetailLevel DetailLevel
		{
			get
			{
				return this.detailLevel;
			}
			set
			{
				this.detailLevel = value;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060016B9 RID: 5817 RVA: 0x00069C07 File Offset: 0x00067E07
		// (set) Token: 0x060016BA RID: 5818 RVA: 0x00069C0F File Offset: 0x00067E0F
		public EventTree Tree
		{
			get
			{
				return this.tree;
			}
			set
			{
				this.tree = value;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060016BB RID: 5819 RVA: 0x00069C18 File Offset: 0x00067E18
		public LogCache Cache
		{
			get
			{
				return this.cache;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x00069C20 File Offset: 0x00067E20
		public DirectoryContext DirectoryContext
		{
			get
			{
				return this.directoryContext;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x00069C28 File Offset: 0x00067E28
		public MessageTrackingReportId StartingEventId
		{
			get
			{
				return this.startingEventId;
			}
		}

		// Token: 0x04000E90 RID: 3728
		private MessageTrackingReportId startingEventId;

		// Token: 0x04000E91 RID: 3729
		private DirectoryContext directoryContext;

		// Token: 0x04000E92 RID: 3730
		private LogCache cache;

		// Token: 0x04000E93 RID: 3731
		private EventTree tree;

		// Token: 0x04000E94 RID: 3732
		private MessageTrackingDetailLevel detailLevel;

		// Token: 0x04000E95 RID: 3733
		private ReportTemplate reportTemplate;

		// Token: 0x04000E96 RID: 3734
		private string selectedRecipient;
	}
}
