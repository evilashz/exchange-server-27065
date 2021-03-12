using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Management.Search
{
	// Token: 0x02000154 RID: 340
	[Serializable]
	public class FailedDocument : ConfigurableObject
	{
		// Token: 0x06000C3D RID: 3133 RVA: 0x0003874C File Offset: 0x0003694C
		internal FailedDocument(IFailureEntry failure, string subject, string databaseName, ADUser user) : base(new SimpleProviderPropertyBag())
		{
			this[FailedDocumentSchema.DocID] = failure.DocumentId;
			this[FailedDocumentSchema.EntryID] = failure.EntryId;
			this[FailedDocumentSchema.Database] = databaseName;
			this[FailedDocumentSchema.MailboxGuid] = failure.MailboxGuid;
			this[FailedDocumentSchema.ErrorCode] = (int)EvaluationErrorsHelper.GetErrorCode(failure.ErrorCode);
			this[FailedDocumentSchema.Description] = failure.ErrorDescription;
			this[FailedDocumentSchema.FailedTime] = failure.LastAttemptTime;
			this[FailedDocumentSchema.IsPartialIndexed] = failure.IsPartiallyIndexed;
			this[FailedDocumentSchema.AdditionalInfo] = failure.AdditionalInfo;
			this[FailedDocumentSchema.Subject] = subject;
			this[FailedDocumentSchema.FailureMode] = (failure.IsPermanentFailure ? FailureMode.Permanent : FailureMode.Transient);
			this[FailedDocumentSchema.AttemptCount] = failure.AttemptCount;
			if (user != null)
			{
				this[FailedDocumentSchema.Mailbox] = user.Name;
				this[FailedDocumentSchema.SmtpAddress] = user.PrimarySmtpAddress.ToString();
				return;
			}
			this[FailedDocumentSchema.Mailbox] = string.Empty;
			this[FailedDocumentSchema.SmtpAddress] = string.Empty;
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x000388B0 File Offset: 0x00036AB0
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return FailedDocument.schema;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x000388B7 File Offset: 0x00036AB7
		public int DocID
		{
			get
			{
				return (int)this[FailedDocumentSchema.DocID];
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x000388C9 File Offset: 0x00036AC9
		public string Database
		{
			get
			{
				return (string)this[FailedDocumentSchema.Database];
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x000388DB File Offset: 0x00036ADB
		public Guid MailboxGuid
		{
			get
			{
				return (Guid)this[FailedDocumentSchema.MailboxGuid];
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x000388ED File Offset: 0x00036AED
		public string Mailbox
		{
			get
			{
				return (string)this[FailedDocumentSchema.Mailbox];
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x000388FF File Offset: 0x00036AFF
		public string SmtpAddress
		{
			get
			{
				return (string)this[FailedDocumentSchema.SmtpAddress];
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x00038911 File Offset: 0x00036B11
		public string EntryID
		{
			get
			{
				return (string)this[FailedDocumentSchema.EntryID];
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x00038923 File Offset: 0x00036B23
		public string Subject
		{
			get
			{
				return (string)this[FailedDocumentSchema.Subject];
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x00038935 File Offset: 0x00036B35
		public int ErrorCode
		{
			get
			{
				return (int)this[FailedDocumentSchema.ErrorCode];
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x00038947 File Offset: 0x00036B47
		public LocalizedString Description
		{
			get
			{
				return (LocalizedString)this[FailedDocumentSchema.Description];
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x00038959 File Offset: 0x00036B59
		public string AdditionalInfo
		{
			get
			{
				return (string)this[FailedDocumentSchema.AdditionalInfo];
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x0003896B File Offset: 0x00036B6B
		public bool IsPartialIndexed
		{
			get
			{
				return (bool)this[FailedDocumentSchema.IsPartialIndexed];
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x0003897D File Offset: 0x00036B7D
		public DateTime? FailedTime
		{
			get
			{
				return (DateTime?)this[FailedDocumentSchema.FailedTime];
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x0003898F File Offset: 0x00036B8F
		public FailureMode FailureMode
		{
			get
			{
				return (FailureMode)this[FailedDocumentSchema.FailureMode];
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x000389A1 File Offset: 0x00036BA1
		public int AttemptCount
		{
			get
			{
				return (int)this[FailedDocumentSchema.AttemptCount];
			}
		}

		// Token: 0x04000604 RID: 1540
		private static ObjectSchema schema = ObjectSchema.GetInstance<FailedDocumentSchema>();
	}
}
