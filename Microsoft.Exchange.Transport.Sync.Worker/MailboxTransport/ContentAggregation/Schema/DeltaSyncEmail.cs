using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net.Protocols.DeltaSync;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema
{
	// Token: 0x02000051 RID: 81
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class DeltaSyncEmail : DisposeTrackableBase, ISyncEmail, ISyncObject, IDisposeTrackable, IDisposable
	{
		// Token: 0x0600038B RID: 907 RVA: 0x0001074D File Offset: 0x0000E94D
		internal DeltaSyncEmail(ISyncSourceSession sourceSession, DeltaSyncMail deltaSyncMail)
		{
			this.sourceSession = sourceSession;
			this.deltaSyncMail = deltaSyncMail;
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00010763 File Offset: 0x0000E963
		public SchemaType Type
		{
			get
			{
				base.CheckDisposed();
				return SchemaType.Email;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0001076C File Offset: 0x0000E96C
		public ISyncSourceSession SourceSession
		{
			get
			{
				base.CheckDisposed();
				return this.sourceSession;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0001077A File Offset: 0x0000E97A
		public bool? IsRead
		{
			get
			{
				base.CheckDisposed();
				return new bool?(this.deltaSyncMail.Read);
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00010794 File Offset: 0x0000E994
		public SyncMessageResponseType? SyncMessageResponseType
		{
			get
			{
				base.CheckDisposed();
				if (this.deltaSyncMail.ReplyToOrForward == null)
				{
					return null;
				}
				switch (this.deltaSyncMail.ReplyToOrForward.Value)
				{
				case DeltaSyncMail.ReplyToOrForwardState.None:
					return new SyncMessageResponseType?(Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema.SyncMessageResponseType.None);
				case DeltaSyncMail.ReplyToOrForwardState.RepliedTo:
					return new SyncMessageResponseType?(Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema.SyncMessageResponseType.Replied);
				case DeltaSyncMail.ReplyToOrForwardState.Forwarded:
					return new SyncMessageResponseType?(Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema.SyncMessageResponseType.Forwarded);
				default:
					throw new InvalidDataException(string.Format(CultureInfo.InvariantCulture, "Unknown ReplyToOrForwardState: {0}", new object[]
					{
						this.deltaSyncMail.ReplyToOrForward
					}));
				}
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00010835 File Offset: 0x0000EA35
		public string From
		{
			get
			{
				base.CheckDisposed();
				return this.deltaSyncMail.From;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000391 RID: 913 RVA: 0x00010848 File Offset: 0x0000EA48
		public string Subject
		{
			get
			{
				base.CheckDisposed();
				return this.deltaSyncMail.Subject;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0001085B File Offset: 0x0000EA5B
		public ExDateTime? ReceivedTime
		{
			get
			{
				base.CheckDisposed();
				return new ExDateTime?(this.deltaSyncMail.DateReceived);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00010873 File Offset: 0x0000EA73
		public string MessageClass
		{
			get
			{
				base.CheckDisposed();
				return this.deltaSyncMail.MessageClass;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00010888 File Offset: 0x0000EA88
		public Importance? Importance
		{
			get
			{
				base.CheckDisposed();
				switch (this.deltaSyncMail.Importance)
				{
				case DeltaSyncMail.ImportanceLevel.Low:
					return new Importance?(Microsoft.Exchange.Data.Storage.Importance.Low);
				case DeltaSyncMail.ImportanceLevel.Normal:
					return new Importance?(Microsoft.Exchange.Data.Storage.Importance.Normal);
				case DeltaSyncMail.ImportanceLevel.High:
					return new Importance?(Microsoft.Exchange.Data.Storage.Importance.High);
				default:
					throw new InvalidDataException(string.Format(CultureInfo.InvariantCulture, "Unknown Importance: {0}", new object[]
					{
						this.deltaSyncMail.Importance
					}));
				}
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000395 RID: 917 RVA: 0x000108FF File Offset: 0x0000EAFF
		public string ConversationTopic
		{
			get
			{
				base.CheckDisposed();
				return this.deltaSyncMail.ConversationTopic;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00010912 File Offset: 0x0000EB12
		public string ConversationIndex
		{
			get
			{
				base.CheckDisposed();
				return this.deltaSyncMail.ConversationIndex;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000397 RID: 919 RVA: 0x00010928 File Offset: 0x0000EB28
		public Sensitivity? Sensitivity
		{
			get
			{
				base.CheckDisposed();
				switch (this.deltaSyncMail.Sensitivity)
				{
				case DeltaSyncMail.SensitivityLevel.Normal:
					return new Sensitivity?(Microsoft.Exchange.Data.Storage.Sensitivity.Normal);
				case DeltaSyncMail.SensitivityLevel.Personal:
					return new Sensitivity?(Microsoft.Exchange.Data.Storage.Sensitivity.Personal);
				case DeltaSyncMail.SensitivityLevel.Private:
					return new Sensitivity?(Microsoft.Exchange.Data.Storage.Sensitivity.Private);
				case DeltaSyncMail.SensitivityLevel.Confidential:
					return new Sensitivity?(Microsoft.Exchange.Data.Storage.Sensitivity.CompanyConfidential);
				default:
					throw new InvalidDataException(string.Format(CultureInfo.InvariantCulture, "Unknown Sensitivity: {0}", new object[]
					{
						this.deltaSyncMail.Sensitivity
					}));
				}
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000398 RID: 920 RVA: 0x000109AA File Offset: 0x0000EBAA
		public int? Size
		{
			get
			{
				base.CheckDisposed();
				return new int?(this.deltaSyncMail.Size);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000399 RID: 921 RVA: 0x000109C2 File Offset: 0x0000EBC2
		public bool? HasAttachments
		{
			get
			{
				base.CheckDisposed();
				return new bool?(this.deltaSyncMail.HasAttachments);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600039A RID: 922 RVA: 0x000109DA File Offset: 0x0000EBDA
		public bool? IsDraft
		{
			get
			{
				base.CheckDisposed();
				return new bool?(this.deltaSyncMail.IsDraft);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600039B RID: 923 RVA: 0x000109F2 File Offset: 0x0000EBF2
		public string InternetMessageId
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600039C RID: 924 RVA: 0x000109FC File Offset: 0x0000EBFC
		public ExDateTime? LastModifiedTime
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00010A18 File Offset: 0x0000EC18
		public Stream MimeStream
		{
			get
			{
				base.CheckDisposed();
				return this.deltaSyncMail.EmailMessage;
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00010A2B File Offset: 0x0000EC2B
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00010A2D File Offset: 0x0000EC2D
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<DeltaSyncEmail>(this);
		}

		// Token: 0x040001EC RID: 492
		private ISyncSourceSession sourceSession;

		// Token: 0x040001ED RID: 493
		private DeltaSyncMail deltaSyncMail;
	}
}
