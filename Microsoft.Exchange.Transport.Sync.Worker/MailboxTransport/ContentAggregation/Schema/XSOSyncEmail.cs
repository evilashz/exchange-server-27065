using System;
using System.IO;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Worker.Throttling;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema
{
	// Token: 0x02000234 RID: 564
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class XSOSyncEmail : DisposeTrackableBase, ISyncEmail, ISyncObject, IDisposeTrackable, IDisposable
	{
		// Token: 0x060014AC RID: 5292 RVA: 0x0004AA04 File Offset: 0x00048C04
		internal XSOSyncEmail(XSOSyncStorageProviderState providerState, StoreObjectId nativeId, ChangeType changeType)
		{
			SyncUtilities.ThrowIfArgumentNull("providerState", providerState);
			SyncUtilities.ThrowIfArgumentNull("nativeId", nativeId);
			if (providerState.SyncMailboxSession == null || providerState.SyncMailboxSession.MailboxSession == null)
			{
				throw new ArgumentException("session");
			}
			PropertyDefinition[] properties;
			switch (changeType)
			{
			case ChangeType.Add:
			case ChangeType.Change:
				properties = XSOSyncEmail.ChangeProperties;
				using (Item item = SyncStoreLoadManager.ItemBind(providerState.SyncMailboxSession.MailboxSession, nativeId, StoreObjectSchema.ContentConversionProperties, new EventHandler<RoundtripCompleteEventArgs>(providerState.OnRoundtripComplete)))
				{
					this.mimeStream = XSOSyncContentConversion.ConvertItemToMime(item, providerState.ScopedOutboundConversionOptions);
					goto IL_B1;
				}
				break;
			case (ChangeType)3:
			case ChangeType.Delete:
				goto IL_AF;
			case ChangeType.ReadFlagChange:
				break;
			default:
				goto IL_AF;
			}
			throw new InvalidOperationException("ReadFlagChange should have used the other constructor.");
			IL_AF:
			properties = null;
			IL_B1:
			this.item = SyncStoreLoadManager.ItemBind(providerState.SyncMailboxSession.MailboxSession, nativeId, properties, new EventHandler<RoundtripCompleteEventArgs>(providerState.OnRoundtripComplete));
			this.sourceSession = providerState;
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x0004AB00 File Offset: 0x00048D00
		internal XSOSyncEmail(ISyncSourceSession sourceSession, bool read)
		{
			this.read = new bool?(read);
			this.sourceSession = sourceSession;
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x0004AB1B File Offset: 0x00048D1B
		public ISyncSourceSession SourceSession
		{
			get
			{
				base.CheckDisposed();
				return this.sourceSession;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x0004AB29 File Offset: 0x00048D29
		public bool? IsRead
		{
			get
			{
				base.CheckDisposed();
				if (this.read != null)
				{
					return this.read;
				}
				return new bool?(SyncUtilities.SafeGetProperty<bool>(this.item, MessageItemSchema.IsRead));
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x0004AB5C File Offset: 0x00048D5C
		public SyncMessageResponseType? SyncMessageResponseType
		{
			get
			{
				base.CheckDisposed();
				IconIndex iconIndex = SyncUtilities.SafeGetProperty<IconIndex>(this.item, ItemSchema.IconIndex);
				IconIndex iconIndex2 = iconIndex;
				switch (iconIndex2)
				{
				case IconIndex.MailReplied:
					break;
				case IconIndex.MailForwarded:
					goto IL_52;
				default:
					switch (iconIndex2)
					{
					case IconIndex.MailEncryptedReplied:
					case IconIndex.MailSmimeSignedReplied:
						break;
					case IconIndex.MailEncryptedForwarded:
					case IconIndex.MailSmimeSignedForwarded:
						goto IL_52;
					default:
						return new SyncMessageResponseType?(Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema.SyncMessageResponseType.None);
					}
					break;
				}
				return new SyncMessageResponseType?(Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema.SyncMessageResponseType.Replied);
				IL_52:
				return new SyncMessageResponseType?(Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema.SyncMessageResponseType.Forwarded);
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x0004ABC8 File Offset: 0x00048DC8
		public string From
		{
			get
			{
				base.CheckDisposed();
				Participant participant = SyncUtilities.SafeGetProperty<Participant>(this.item, ItemSchema.From);
				if (null == participant)
				{
					return null;
				}
				if (participant.RoutingType != "SMTP")
				{
					Participant participant2 = Participant.TryConvertTo(participant, "SMTP", true);
					if (null != participant2)
					{
						participant = participant2;
					}
				}
				return participant.EmailAddress;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x0004AC27 File Offset: 0x00048E27
		public string Subject
		{
			get
			{
				base.CheckDisposed();
				return SyncUtilities.SafeGetProperty<string>(this.item, ItemSchema.Subject);
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x0004AC40 File Offset: 0x00048E40
		public ExDateTime? ReceivedTime
		{
			get
			{
				base.CheckDisposed();
				ExDateTime exDateTime = SyncUtilities.SafeGetProperty<ExDateTime>(this.item, ItemSchema.ReceivedTime);
				if (exDateTime == ExDateTime.MinValue)
				{
					return null;
				}
				return new ExDateTime?(exDateTime);
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x0004AC81 File Offset: 0x00048E81
		public string MessageClass
		{
			get
			{
				base.CheckDisposed();
				return this.item.ClassName;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x0004AC94 File Offset: 0x00048E94
		public Importance? Importance
		{
			get
			{
				base.CheckDisposed();
				return new Importance?(this.item.Importance);
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x0004ACAC File Offset: 0x00048EAC
		public string ConversationTopic
		{
			get
			{
				base.CheckDisposed();
				return SyncUtilities.SafeGetProperty<string>(this.item, ItemSchema.ConversationTopic);
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x0004ACC4 File Offset: 0x00048EC4
		public string ConversationIndex
		{
			get
			{
				base.CheckDisposed();
				byte[] array = SyncUtilities.SafeGetProperty<byte[]>(this.item, ItemSchema.ConversationIndex);
				if (array == null)
				{
					return null;
				}
				return HexConverter.ByteArrayToHexString(array);
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x0004ACF3 File Offset: 0x00048EF3
		public Sensitivity? Sensitivity
		{
			get
			{
				base.CheckDisposed();
				return new Sensitivity?(this.item.Sensitivity);
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x060014B9 RID: 5305 RVA: 0x0004AD0B File Offset: 0x00048F0B
		public int? Size
		{
			get
			{
				base.CheckDisposed();
				return new int?((int)this.item.Size());
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x0004AD24 File Offset: 0x00048F24
		public bool? HasAttachments
		{
			get
			{
				base.CheckDisposed();
				return new bool?(SyncUtilities.SafeGetProperty<bool>(this.item, ItemSchema.HasAttachment));
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x0004AD41 File Offset: 0x00048F41
		public bool? IsDraft
		{
			get
			{
				base.CheckDisposed();
				return new bool?(SyncUtilities.SafeGetProperty<bool>(this.item, MessageItemSchema.IsDraft));
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x0004AD5E File Offset: 0x00048F5E
		public string InternetMessageId
		{
			get
			{
				base.CheckDisposed();
				return SyncUtilities.SafeGetProperty<string>(this.item, ItemSchema.InternetMessageId);
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x0004AD76 File Offset: 0x00048F76
		public Stream MimeStream
		{
			get
			{
				base.CheckDisposed();
				return this.mimeStream;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x0004AD84 File Offset: 0x00048F84
		public virtual SchemaType Type
		{
			get
			{
				base.CheckDisposed();
				return SchemaType.Email;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x0004AD8D File Offset: 0x00048F8D
		public ExDateTime? LastModifiedTime
		{
			get
			{
				base.CheckDisposed();
				return new ExDateTime?(this.item.LastModifiedTime);
			}
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0004ADA5 File Offset: 0x00048FA5
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.mimeStream != null)
				{
					this.mimeStream.Dispose();
					this.mimeStream = null;
				}
				if (this.item != null)
				{
					this.item.Dispose();
					this.item = null;
				}
			}
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0004ADDE File Offset: 0x00048FDE
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<XSOSyncEmail>(this);
		}

		// Token: 0x04000ABC RID: 2748
		private static readonly PropertyDefinition[] ChangeProperties = new PropertyDefinition[]
		{
			MessageItemSchema.IsRead,
			MessageItemSchema.ReplyForwardStatus,
			ItemSchema.From,
			ItemSchema.Subject,
			ItemSchema.ReceivedTime,
			StoreObjectSchema.ContentClass,
			ItemSchema.Importance,
			ItemSchema.ConversationTopic,
			ItemSchema.ConversationIndex,
			ItemSchema.Sensitivity,
			ItemSchema.Size,
			ItemSchema.HasAttachment,
			MessageItemSchema.IsDraft,
			ItemSchema.InternetMessageId,
			ItemSchema.IconIndex
		};

		// Token: 0x04000ABD RID: 2749
		private ISyncSourceSession sourceSession;

		// Token: 0x04000ABE RID: 2750
		private Item item;

		// Token: 0x04000ABF RID: 2751
		private Stream mimeStream;

		// Token: 0x04000AC0 RID: 2752
		private bool? read;
	}
}
