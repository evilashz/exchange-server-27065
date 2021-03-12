using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002D0 RID: 720
	[Serializable]
	internal sealed class MessageTrackingReport
	{
		// Token: 0x06001470 RID: 5232 RVA: 0x0005F695 File Offset: 0x0005D895
		public MessageTrackingReport()
		{
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x0005F6A0 File Offset: 0x0005D8A0
		public MessageTrackingReport(MessageTrackingReportId identity, DateTime submittedDateTime, string subject, SmtpAddress? fromAddress, string fromDisplayName, SmtpAddress[] recipientAddresses, string[] recipientDisplayNames, RecipientEventData eventData)
		{
			if (recipientAddresses == null)
			{
				throw new ArgumentNullException("recipientAddresses", "Param cannot be null, pass in empty SmtpAddress[] instead");
			}
			if (recipientDisplayNames == null)
			{
				throw new ArgumentNullException("recipientDisplayNames", "Param cannot be null, pass in empty string[] instead");
			}
			this.identity = identity;
			this.submittedDateTime = submittedDateTime;
			this.subject = subject;
			this.fromAddress = fromAddress;
			this.fromDisplayName = fromDisplayName;
			this.recipientAddresses = recipientAddresses;
			this.recipientDisplayNames = recipientDisplayNames;
			this.eventData = eventData;
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x0005F718 File Offset: 0x0005D918
		public MessageTrackingReportId MessageTrackingReportId
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x0005F720 File Offset: 0x0005D920
		public DateTime SubmittedDateTime
		{
			get
			{
				return this.submittedDateTime;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x0005F728 File Offset: 0x0005D928
		public string Subject
		{
			get
			{
				return this.subject;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x0005F730 File Offset: 0x0005D930
		public SmtpAddress? FromAddress
		{
			get
			{
				return this.fromAddress;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x0005F738 File Offset: 0x0005D938
		public string FromDisplayName
		{
			get
			{
				return this.fromDisplayName;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x0005F740 File Offset: 0x0005D940
		// (set) Token: 0x06001478 RID: 5240 RVA: 0x0005F748 File Offset: 0x0005D948
		public SmtpAddress[] RecipientAddresses
		{
			get
			{
				return this.recipientAddresses;
			}
			internal set
			{
				this.recipientAddresses = value;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x0005F751 File Offset: 0x0005D951
		// (set) Token: 0x0600147A RID: 5242 RVA: 0x0005F759 File Offset: 0x0005D959
		public string[] RecipientDisplayNames
		{
			get
			{
				return this.recipientDisplayNames;
			}
			internal set
			{
				this.recipientDisplayNames = value;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x0005F762 File Offset: 0x0005D962
		public RecipientTrackingEvent[] RecipientTrackingEvents
		{
			get
			{
				if (this.eventData == null)
				{
					return null;
				}
				return this.eventData.Events.ToArray();
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x0005F77E File Offset: 0x0005D97E
		public bool HasHandedOffPaths
		{
			get
			{
				return this.eventData.HandoffPaths != null;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x0005F791 File Offset: 0x0005D991
		public List<RecipientTrackingEvent> RawSerializedEvents
		{
			get
			{
				return this.eventData.Serialize();
			}
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0005F7A0 File Offset: 0x0005D9A0
		public void MergeRecipientEventsFrom(MessageTrackingReport report)
		{
			if (report.eventData.Events != null)
			{
				this.eventData.Events.AddRange(report.eventData.Events);
			}
			if (report.eventData.HandoffPaths != null)
			{
				this.eventData.HandoffPaths.AddRange(report.eventData.HandoffPaths);
			}
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x0005F800 File Offset: 0x0005DA00
		public void AssignReportIdToRecipEvents()
		{
			string reportId = this.identity.ToString();
			if (this.eventData.Events != null)
			{
				this.AssignReportIdToRecipEvents(this.eventData.Events, reportId);
			}
			if (this.eventData.HandoffPaths != null)
			{
				foreach (List<RecipientTrackingEvent> recipEvents in this.eventData.HandoffPaths)
				{
					this.AssignReportIdToRecipEvents(recipEvents, reportId);
				}
			}
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0005F894 File Offset: 0x0005DA94
		private void AssignReportIdToRecipEvents(IEnumerable<RecipientTrackingEvent> recipEvents, string reportId)
		{
			foreach (RecipientTrackingEvent recipientTrackingEvent in recipEvents)
			{
				recipientTrackingEvent.ExtendedProperties.MessageTrackingReportId = reportId;
			}
		}

		// Token: 0x04000D58 RID: 3416
		private MessageTrackingReportId identity;

		// Token: 0x04000D59 RID: 3417
		private DateTime submittedDateTime;

		// Token: 0x04000D5A RID: 3418
		private string subject;

		// Token: 0x04000D5B RID: 3419
		private SmtpAddress? fromAddress;

		// Token: 0x04000D5C RID: 3420
		private string fromDisplayName;

		// Token: 0x04000D5D RID: 3421
		private SmtpAddress[] recipientAddresses;

		// Token: 0x04000D5E RID: 3422
		private string[] recipientDisplayNames;

		// Token: 0x04000D5F RID: 3423
		private RecipientEventData eventData;
	}
}
