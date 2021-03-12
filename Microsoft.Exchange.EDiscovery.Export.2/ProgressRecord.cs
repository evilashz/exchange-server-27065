using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000065 RID: 101
	internal class ProgressRecord
	{
		// Token: 0x06000751 RID: 1873 RVA: 0x0001A932 File Offset: 0x00018B32
		public ProgressRecord()
		{
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001A93A File Offset: 0x00018B3A
		public ProgressRecord(DataContext dataContext)
		{
			this.DataContext = dataContext;
			this.ItemExportedRecords = new List<ExportRecord>();
			this.ItemErrorRecords = new List<ErrorRecord>();
			this.Duration = TimeSpan.Zero;
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0001A96A File Offset: 0x00018B6A
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x0001A972 File Offset: 0x00018B72
		public DataContext DataContext { get; private set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x0001A97B File Offset: 0x00018B7B
		// (set) Token: 0x06000756 RID: 1878 RVA: 0x0001A983 File Offset: 0x00018B83
		public List<ExportRecord> ItemExportedRecords { get; private set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x0001A98C File Offset: 0x00018B8C
		// (set) Token: 0x06000758 RID: 1880 RVA: 0x0001A994 File Offset: 0x00018B94
		public List<ErrorRecord> ItemErrorRecords { get; private set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x0001A99D File Offset: 0x00018B9D
		// (set) Token: 0x0600075A RID: 1882 RVA: 0x0001A9A5 File Offset: 0x00018BA5
		public long Size { get; private set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x0001A9AE File Offset: 0x00018BAE
		// (set) Token: 0x0600075C RID: 1884 RVA: 0x0001A9B6 File Offset: 0x00018BB6
		public TimeSpan Duration { get; private set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x0001A9BF File Offset: 0x00018BBF
		// (set) Token: 0x0600075E RID: 1886 RVA: 0x0001A9C7 File Offset: 0x00018BC7
		public ExportRecord RootExportedRecord { get; private set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0001A9D0 File Offset: 0x00018BD0
		// (set) Token: 0x06000760 RID: 1888 RVA: 0x0001A9D8 File Offset: 0x00018BD8
		public ErrorRecord SourceErrorRecord { get; private set; }

		// Token: 0x06000761 RID: 1889 RVA: 0x0001A9E1 File Offset: 0x00018BE1
		public void ReportRootRecord(ExportRecord rootExportRecord)
		{
			this.RootExportedRecord = rootExportRecord;
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001A9EC File Offset: 0x00018BEC
		public void ReportItemExported(ItemId itemId, string targetMessageId, ExportRecord rootExportRecord)
		{
			this.ItemExportedRecords.Add(new ExportRecord
			{
				Id = itemId.Id,
				Parent = rootExportRecord,
				SourceId = itemId.SourceId,
				ExportFile = null,
				OriginalPath = itemId.ReportingPath,
				TargetPath = targetMessageId,
				PrimaryIdOfDuplicates = itemId.PrimaryItemId,
				Title = itemId.Subject,
				Size = itemId.Size,
				Sender = itemId.Sender,
				SenderSmtpAddress = itemId.SenderSmtpAddress,
				SentTime = itemId.SentTime,
				ReceivedTime = itemId.ReceivedTime,
				BodyPreview = itemId.BodyPreview,
				Importance = itemId.Importance,
				IsRead = itemId.IsRead,
				HasAttachment = itemId.HasAttachment,
				ToRecipients = itemId.ToRecipients,
				CcRecipients = itemId.CcRecipients,
				BccRecipients = itemId.BccRecipients,
				DocumentId = itemId.DocumentId,
				InternetMessageId = itemId.InternetMessageId,
				ToGroupExpansionRecipients = itemId.ToGroupExpansionRecipients,
				CcGroupExpansionRecipients = itemId.CcGroupExpansionRecipients,
				BccGroupExpansionRecipients = itemId.BccGroupExpansionRecipients,
				DGGroupExpansionError = itemId.DGGroupExpansionError.ToString(),
				DocumentType = "Message",
				RelationshipType = "Container",
				IsUnsearchable = this.DataContext.IsUnsearchable
			});
			if (!itemId.IsDuplicate)
			{
				this.Size += (long)((ulong)itemId.Size);
			}
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001AB88 File Offset: 0x00018D88
		public void ReportItemError(ItemId itemId, ExportRecord rootExportRecord, ExportErrorType errorType, string diagnosticMessage)
		{
			this.ItemErrorRecords.Add(new ErrorRecord
			{
				Item = new ExportRecord
				{
					Id = itemId.Id,
					DocumentId = itemId.DocumentId,
					InternetMessageId = itemId.InternetMessageId,
					Parent = rootExportRecord,
					Size = itemId.Size,
					SourceId = itemId.SourceId,
					ExportFile = null,
					OriginalPath = itemId.ReportingPath,
					Title = (string.IsNullOrEmpty(itemId.Subject) ? string.Empty : itemId.Subject),
					IsUnsearchable = this.DataContext.IsUnsearchable
				},
				ErrorType = errorType,
				DiagnosticMessage = diagnosticMessage,
				SourceId = itemId.SourceId,
				Time = DateTime.UtcNow
			});
			if (!itemId.IsDuplicate)
			{
				this.Size += (long)((ulong)itemId.Size);
			}
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0001AC7D File Offset: 0x00018E7D
		public void ReportSourceError(ErrorRecord errorRecord)
		{
			this.SourceErrorRecord = errorRecord;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0001AC86 File Offset: 0x00018E86
		public void ReportDuration(TimeSpan duration)
		{
			this.Duration = duration;
		}
	}
}
