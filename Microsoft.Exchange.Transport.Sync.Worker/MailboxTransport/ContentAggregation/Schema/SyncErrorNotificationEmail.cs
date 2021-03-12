using System;
using System.IO;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema
{
	// Token: 0x02000216 RID: 534
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SyncErrorNotificationEmail : DisposeTrackableBase, ISyncEmail, ISyncObject, IDisposeTrackable, IDisposable
	{
		// Token: 0x06001309 RID: 4873 RVA: 0x00041053 File Offset: 0x0003F253
		public SyncErrorNotificationEmail(ExDateTime receivedTime, Stream mimeStream)
		{
			SyncUtilities.ThrowIfArgumentNull("mimeStream", mimeStream);
			this.receivedTime = receivedTime;
			this.mimeStream = mimeStream;
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x00041074 File Offset: 0x0003F274
		public SchemaType Type
		{
			get
			{
				return SchemaType.Email;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x00041077 File Offset: 0x0003F277
		public ExDateTime? LastModifiedTime
		{
			get
			{
				return new ExDateTime?(this.receivedTime);
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x00041084 File Offset: 0x0003F284
		public ISyncSourceSession SourceSession
		{
			get
			{
				return SyncErrorNotificationEmail.syncSourceSession;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x0600130D RID: 4877 RVA: 0x0004108B File Offset: 0x0003F28B
		public bool? IsRead
		{
			get
			{
				return new bool?(false);
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x00041093 File Offset: 0x0003F293
		public string From
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x0600130F RID: 4879 RVA: 0x0004109C File Offset: 0x0003F29C
		public string Subject
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x000410A5 File Offset: 0x0003F2A5
		public ExDateTime? ReceivedTime
		{
			get
			{
				base.CheckDisposed();
				return new ExDateTime?(this.receivedTime);
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x000410B8 File Offset: 0x0003F2B8
		public string MessageClass
		{
			get
			{
				return "IPM.Note";
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x000410C0 File Offset: 0x0003F2C0
		public Importance? Importance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001313 RID: 4883 RVA: 0x000410D6 File Offset: 0x0003F2D6
		public string ConversationTopic
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x000410D9 File Offset: 0x0003F2D9
		public string ConversationIndex
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x000410DC File Offset: 0x0003F2DC
		public Sensitivity? Sensitivity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x000410F4 File Offset: 0x0003F2F4
		public int? Size
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x0004110A File Offset: 0x0003F30A
		public bool? HasAttachments
		{
			get
			{
				return new bool?(false);
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x00041112 File Offset: 0x0003F312
		public bool? IsDraft
		{
			get
			{
				return new bool?(false);
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x0004111A File Offset: 0x0003F31A
		public string InternetMessageId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x0004111D File Offset: 0x0003F31D
		public Stream MimeStream
		{
			get
			{
				return this.mimeStream;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x00041128 File Offset: 0x0003F328
		public SyncMessageResponseType? SyncMessageResponseType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00041140 File Offset: 0x0003F340
		internal MessageItem CreateSyncErrorNotificationMessage(MailboxSession userMailboxSession, StoreId folderId, AggregationSubscription subscription, SyncLogSession syncLogSession)
		{
			MessageItem messageItem = MessageItem.Create(userMailboxSession, folderId);
			InboundConversionOptions scopedInboundConversionOptions = XSOSyncContentConversion.GetScopedInboundConversionOptions(userMailboxSession.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid));
			XSOSyncContentConversion.ConvertAnyMimeToItem(this.MimeStream, messageItem, scopedInboundConversionOptions);
			if (this.IsRead != null)
			{
				messageItem[MessageItemSchema.IsRead] = this.IsRead.Value;
			}
			messageItem[MessageItemSchema.IsDraft] = false;
			messageItem[ItemSchema.SpamConfidenceLevel] = -1;
			messageItem[ItemSchema.ReceivedTime] = this.receivedTime.ToUtc();
			messageItem[MessageItemSchema.SharingInstanceGuid] = subscription.SubscriptionGuid;
			messageItem[StoreObjectSchema.LastModifiedTime] = this.LastModifiedTime.Value;
			return messageItem;
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00041215 File Offset: 0x0003F415
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.mimeStream != null)
			{
				this.mimeStream.Dispose();
			}
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x0004122D File Offset: 0x0003F42D
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SyncErrorNotificationEmail>(this);
		}

		// Token: 0x04000A18 RID: 2584
		private static readonly ISyncSourceSession syncSourceSession = new SyncErrorNotificationEmail.SyncErrorNotificationEmailSourceSession();

		// Token: 0x04000A19 RID: 2585
		private readonly ExDateTime receivedTime;

		// Token: 0x04000A1A RID: 2586
		private readonly Stream mimeStream;

		// Token: 0x02000217 RID: 535
		private class SyncErrorNotificationEmailSourceSession : ISyncSourceSession
		{
			// Token: 0x17000692 RID: 1682
			// (get) Token: 0x06001320 RID: 4896 RVA: 0x00041241 File Offset: 0x0003F441
			public string Protocol
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x17000693 RID: 1683
			// (get) Token: 0x06001321 RID: 4897 RVA: 0x00041248 File Offset: 0x0003F448
			public string SessionId
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x17000694 RID: 1684
			// (get) Token: 0x06001322 RID: 4898 RVA: 0x0004124F File Offset: 0x0003F44F
			public string Server
			{
				get
				{
					return string.Empty;
				}
			}
		}
	}
}
