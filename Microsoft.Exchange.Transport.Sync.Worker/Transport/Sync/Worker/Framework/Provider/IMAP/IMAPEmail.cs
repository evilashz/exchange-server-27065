using System;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.IMAP.Client;

namespace Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.IMAP
{
	// Token: 0x020001DC RID: 476
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class IMAPEmail : DisposeTrackableBase, ISyncEmail, ISyncObject, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000EB8 RID: 3768 RVA: 0x00028938 File Offset: 0x00026B38
		internal IMAPEmail(IMAPSyncStorageProviderState state, string folderCloudId, IMAPMailFlags mailFlags, IMAPMailFlags permanentFlags)
		{
			SyncUtilities.ThrowIfArgumentNull("state", state);
			this.sourceSession = state;
			if (state.GetDefaultFolderMapping(DefaultFolderType.Drafts) == folderCloudId)
			{
				mailFlags |= IMAPMailFlags.Draft;
				permanentFlags |= IMAPMailFlags.Draft;
			}
			if ((permanentFlags & IMAPMailFlags.Draft) == IMAPMailFlags.Draft)
			{
				this.draft = new bool?((mailFlags & IMAPMailFlags.Draft) == IMAPMailFlags.Draft);
			}
			if ((permanentFlags & IMAPMailFlags.Seen) == IMAPMailFlags.Seen)
			{
				this.read = new bool?((mailFlags & IMAPMailFlags.Seen) == IMAPMailFlags.Seen);
			}
			if ((permanentFlags & IMAPMailFlags.Answered) == IMAPMailFlags.Answered)
			{
				this.syncMessageResponseType = new SyncMessageResponseType?(((mailFlags & IMAPMailFlags.Answered) == IMAPMailFlags.Answered) ? Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema.SyncMessageResponseType.Replied : Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema.SyncMessageResponseType.None);
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x000289C9 File Offset: 0x00026BC9
		public ISyncSourceSession SourceSession
		{
			get
			{
				base.CheckDisposed();
				return this.sourceSession;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x000289D7 File Offset: 0x00026BD7
		public bool? IsRead
		{
			get
			{
				base.CheckDisposed();
				return this.read;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x000289E5 File Offset: 0x00026BE5
		public SyncMessageResponseType? SyncMessageResponseType
		{
			get
			{
				base.CheckDisposed();
				return this.syncMessageResponseType;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x000289F3 File Offset: 0x00026BF3
		public string From
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x000289FC File Offset: 0x00026BFC
		public string Subject
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x00028A05 File Offset: 0x00026C05
		public ExDateTime? ReceivedTime
		{
			get
			{
				base.CheckDisposed();
				return this.receivedTime;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x00028A13 File Offset: 0x00026C13
		public string MessageClass
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x00028A1C File Offset: 0x00026C1C
		public Importance? Importance
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x00028A38 File Offset: 0x00026C38
		public string ConversationTopic
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x00028A41 File Offset: 0x00026C41
		public string ConversationIndex
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x00028A4C File Offset: 0x00026C4C
		public Sensitivity? Sensitivity
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x00028A68 File Offset: 0x00026C68
		public int? Size
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x00028A84 File Offset: 0x00026C84
		public bool? HasAttachments
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x00028AA0 File Offset: 0x00026CA0
		public bool? IsDraft
		{
			get
			{
				base.CheckDisposed();
				return this.draft;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x00028AAE File Offset: 0x00026CAE
		public string InternetMessageId
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x00028AB7 File Offset: 0x00026CB7
		public Stream MimeStream
		{
			get
			{
				base.CheckDisposed();
				return this.mimeStream;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x00028AC8 File Offset: 0x00026CC8
		public ExDateTime? LastModifiedTime
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x00028AE4 File Offset: 0x00026CE4
		public SchemaType Type
		{
			get
			{
				base.CheckDisposed();
				return SchemaType.Email;
			}
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x00028AED File Offset: 0x00026CED
		internal void SetItemProperties(ISyncSourceSession sourceSession, Stream mimeStream, ExDateTime? internalDate)
		{
			SyncUtilities.ThrowIfArgumentNull("sourceSession", sourceSession);
			SyncUtilities.ThrowIfArgumentNull("mimeStream", mimeStream);
			this.sourceSession = sourceSession;
			this.mimeStream = mimeStream;
			this.receivedTime = ((internalDate != null) ? internalDate : this.FindReceivedTime());
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x00028B2B File Offset: 0x00026D2B
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x00028B2D File Offset: 0x00026D2D
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<IMAPEmail>(this);
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x00028B38 File Offset: 0x00026D38
		private ExDateTime? FindReceivedTime()
		{
			if (this.mimeStream == null)
			{
				return null;
			}
			ExDateTime receivedDate = SyncUtilities.GetReceivedDate(this.mimeStream, true);
			if (receivedDate == ExDateTime.MinValue)
			{
				return null;
			}
			return new ExDateTime?(receivedDate);
		}

		// Token: 0x0400084C RID: 2124
		private ISyncSourceSession sourceSession;

		// Token: 0x0400084D RID: 2125
		private Stream mimeStream;

		// Token: 0x0400084E RID: 2126
		private bool? draft;

		// Token: 0x0400084F RID: 2127
		private bool? read;

		// Token: 0x04000850 RID: 2128
		private ExDateTime? receivedTime;

		// Token: 0x04000851 RID: 2129
		private SyncMessageResponseType? syncMessageResponseType;
	}
}
