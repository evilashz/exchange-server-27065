using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Management.Tracking;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002C7 RID: 711
	[DataContract]
	public class MessageTrackingReportRow : BaseRow
	{
		// Token: 0x06002C2C RID: 11308 RVA: 0x00088E30 File Offset: 0x00087030
		public MessageTrackingReportRow(MessageTrackingReport messageTrackingReport) : base(new Identity(messageTrackingReport.MessageTrackingReportId.RawIdentity, OwaOptionStrings.DeliveryReport), messageTrackingReport)
		{
			this.MessageTrackingReport = messageTrackingReport;
			this.RecipientCounts = new RecipientCounts(messageTrackingReport.DeliveredCount, messageTrackingReport.PendingCount, messageTrackingReport.TransferredCount, messageTrackingReport.UnsuccessfulCount);
		}

		// Token: 0x17001DBE RID: 7614
		// (get) Token: 0x06002C2D RID: 11309 RVA: 0x00088E88 File Offset: 0x00087088
		// (set) Token: 0x06002C2E RID: 11310 RVA: 0x00088E90 File Offset: 0x00087090
		public MessageTrackingReport MessageTrackingReport { get; private set; }

		// Token: 0x17001DBF RID: 7615
		// (get) Token: 0x06002C2F RID: 11311 RVA: 0x00088E99 File Offset: 0x00087099
		// (set) Token: 0x06002C30 RID: 11312 RVA: 0x00088EA1 File Offset: 0x000870A1
		[DataMember]
		public RecipientCounts RecipientCounts { get; private set; }

		// Token: 0x17001DC0 RID: 7616
		// (get) Token: 0x06002C31 RID: 11313 RVA: 0x00088EAA File Offset: 0x000870AA
		// (set) Token: 0x06002C32 RID: 11314 RVA: 0x00088EB7 File Offset: 0x000870B7
		[DataMember]
		public string Subject
		{
			get
			{
				return this.MessageTrackingReport.Subject;
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001DC1 RID: 7617
		// (get) Token: 0x06002C33 RID: 11315 RVA: 0x00088EBE File Offset: 0x000870BE
		// (set) Token: 0x06002C34 RID: 11316 RVA: 0x00088ECB File Offset: 0x000870CB
		[DataMember]
		public string FromDisplayName
		{
			get
			{
				return this.MessageTrackingReport.FromDisplayName;
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001DC2 RID: 7618
		// (get) Token: 0x06002C35 RID: 11317 RVA: 0x00088ED2 File Offset: 0x000870D2
		// (set) Token: 0x06002C36 RID: 11318 RVA: 0x00088EE9 File Offset: 0x000870E9
		[DataMember]
		public string RecipientDisplayNames
		{
			get
			{
				return this.MessageTrackingReport.RecipientDisplayNames.StringArrayJoin(";");
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001DC3 RID: 7619
		// (get) Token: 0x06002C37 RID: 11319 RVA: 0x00088EF0 File Offset: 0x000870F0
		// (set) Token: 0x06002C38 RID: 11320 RVA: 0x00088F02 File Offset: 0x00087102
		[DataMember]
		public string SubmittedDateTime
		{
			get
			{
				return this.MessageTrackingReport.SubmittedDateTime.UtcToUserDateTimeString();
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001DC4 RID: 7620
		// (get) Token: 0x06002C39 RID: 11321 RVA: 0x00088F09 File Offset: 0x00087109
		// (set) Token: 0x06002C3A RID: 11322 RVA: 0x00088F1C File Offset: 0x0008711C
		[DataMember]
		public RecipientStatusRow[] RecipientStatuses
		{
			get
			{
				return this.CreateRecipientStatusRows(this.MessageTrackingReport.RecipientTrackingEvents);
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x00088F24 File Offset: 0x00087124
		private RecipientStatusRow[] CreateRecipientStatusRows(RecipientTrackingEvent[] recipientTrackingEvents)
		{
			int num = recipientTrackingEvents.Length;
			RecipientStatusRow[] array = new RecipientStatusRow[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = new RecipientStatusRow(base.Identity, recipientTrackingEvents[i]);
			}
			if (array.Length > 0)
			{
				Func<RecipientStatusRow[], RecipientStatusRow[]> sortFunction = new SortOptions
				{
					PropertyName = "RecipientDeliveryStatus"
				}.GetSortFunction<RecipientStatusRow>();
				array = sortFunction(array);
			}
			return array;
		}

		// Token: 0x040021EB RID: 8683
		private const string RecipientNamesSeparator = ";";
	}
}
