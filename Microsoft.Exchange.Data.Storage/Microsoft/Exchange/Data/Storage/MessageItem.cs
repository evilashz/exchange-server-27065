using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200007C RID: 124
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MessageItem : Item, IMessageItem, IToDoItem, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x060008AD RID: 2221 RVA: 0x000408AC File Offset: 0x0003EAAC
		internal MessageItem(ICoreItem coreItem, bool shallowDispose = false) : base(coreItem, shallowDispose)
		{
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x000408F7 File Offset: 0x0003EAF7
		public static MessageItem Create(StoreSession session, StoreId destFolderId)
		{
			return MessageItem.InternalCreate(session, destFolderId, CreateMessageType.Normal);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00040901 File Offset: 0x0003EB01
		public static MessageItem CloneMessage(StoreSession session, StoreId parentFolderId, MessageItem itemToClone)
		{
			return MessageItem.CloneMessage(session, parentFolderId, itemToClone, null);
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0004090C File Offset: 0x0003EB0C
		public static MessageItem CloneMessage(StoreSession session, StoreId parentFolderId, MessageItem itemToClone, ICollection<PropertyDefinition> propsToReturn)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (parentFolderId == null)
			{
				throw new ArgumentNullException("parentFolderId");
			}
			if (itemToClone == null)
			{
				throw new ArgumentNullException("itemToClone");
			}
			return (MessageItem)Microsoft.Exchange.Data.Storage.Item.CloneItem(session, parentFolderId, itemToClone, true, false, propsToReturn);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00040948 File Offset: 0x0003EB48
		public static MessageItem CreateAssociated(StoreSession session, StoreId destFolderId)
		{
			return MessageItem.InternalCreate(session, destFolderId, CreateMessageType.Associated);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00040952 File Offset: 0x0003EB52
		public static MessageItem CreateAggregated(StoreSession session, StoreId destFolderId)
		{
			return MessageItem.InternalCreate(session, destFolderId, CreateMessageType.Aggregated);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0004095C File Offset: 0x0003EB5C
		public static MessageItem CreateForDelivery(StoreSession session, StoreId destFolderId, string internetMessageId, ExDateTime? clientSubmitTime)
		{
			return MessageItem.InternalCreateForDelivery(session, destFolderId, CreateMessageType.Normal, internetMessageId, clientSubmitTime);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00040968 File Offset: 0x0003EB68
		public static MessageItem CreateAggregatedForDelivery(StoreSession session, StoreId destFolderId, string internetMessageId, ExDateTime? clientSubmitTime)
		{
			return MessageItem.InternalCreateForDelivery(session, destFolderId, CreateMessageType.Aggregated, internetMessageId, clientSubmitTime);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00040974 File Offset: 0x0003EB74
		public new static MessageItem Bind(StoreSession session, StoreId messageId)
		{
			return MessageItem.Bind(session, messageId, null);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0004097E File Offset: 0x0003EB7E
		public new static MessageItem Bind(StoreSession session, StoreId messageId, params PropertyDefinition[] propsToReturn)
		{
			return MessageItem.Bind(session, messageId, (ICollection<PropertyDefinition>)propsToReturn);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0004098D File Offset: 0x0003EB8D
		public new static MessageItem Bind(StoreSession session, StoreId messageId, ItemBindOption itemBindOption, ICollection<PropertyDefinition> propsToReturn)
		{
			return ItemBuilder.ItemBind<MessageItem>(session, messageId, MessageItemSchema.Instance, null, itemBindOption, propsToReturn);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0004099E File Offset: 0x0003EB9E
		public new static MessageItem Bind(StoreSession session, StoreId messageId, ICollection<PropertyDefinition> propsToReturn)
		{
			return ItemBuilder.ItemBind<MessageItem>(session, messageId, MessageItemSchema.Instance, propsToReturn);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x000409AD File Offset: 0x0003EBAD
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.smimeContent != null)
				{
					this.smimeContent.Dispose();
					this.smimeContent = null;
				}
				base.InternalDispose(disposing);
			}
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x000409D4 File Offset: 0x0003EBD4
		private static MessageItem InternalCreate(StoreSession session, StoreId destFolderId, CreateMessageType mapiMessageType)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(destFolderId, "destFolderId");
			MessageItem result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				MessageItem messageItem = ItemBuilder.CreateNewItem<MessageItem>(session, destFolderId, ItemCreateInfo.MessageItemInfo, mapiMessageType);
				disposeGuard.Add<MessageItem>(messageItem);
				messageItem[InternalSchema.ItemClass] = "IPM.Note";
				disposeGuard.Success();
				result = messageItem;
			}
			return result;
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00040A80 File Offset: 0x0003EC80
		private static MessageItem InternalCreateForDelivery(StoreSession session, StoreId destFolderId, CreateMessageType mapiMessageType, string internetMessageId, ExDateTime? clientSubmitTime)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(destFolderId, "destFolderId");
			MessageItem result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				MessageItem messageItem = ItemBuilder.CreateNewItem<MessageItem>(session, ItemCreateInfo.MessageItemInfo, () => Folder.InternalCreateMapiMessageForDelivery(session, destFolderId, mapiMessageType, internetMessageId, clientSubmitTime));
				disposeGuard.Add<MessageItem>(messageItem);
				messageItem[InternalSchema.ItemClass] = "IPM.Note";
				messageItem.SetNoMessageDecoding(true);
				disposeGuard.Success();
				result = messageItem;
			}
			return result;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00040B50 File Offset: 0x0003ED50
		private static bool MessageMimeChanged(CoreItem coreItem)
		{
			if ((coreItem.IsRecipientCollectionLoaded && coreItem.Recipients.IsDirty) || (((ICoreItem)coreItem).IsAttachmentCollectionLoaded && coreItem.AttachmentCollection.IsDirty))
			{
				return true;
			}
			foreach (NativeStorePropertyDefinition nativeStorePropertyDefinition in Microsoft.Exchange.Data.Storage.CoreObject.GetPersistablePropertyBag(coreItem).AllNativeProperties)
			{
				if (coreItem.PropertyBag.IsPropertyDirty(nativeStorePropertyDefinition) && ItemConversion.DoesPropertyAffectMime(nativeStorePropertyDefinition))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00040BE4 File Offset: 0x0003EDE4
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MessageItem>(this);
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x00040BEC File Offset: 0x0003EDEC
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x00040C04 File Offset: 0x0003EE04
		public Participant Sender
		{
			get
			{
				this.CheckDisposed("Sender::get");
				return base.GetValueOrDefault<Participant>(InternalSchema.Sender);
			}
			set
			{
				this.CheckDisposed("Sender::set");
				base.SetOrDeleteProperty(InternalSchema.Sender, value);
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x00040C1D File Offset: 0x0003EE1D
		// (set) Token: 0x060008C1 RID: 2241 RVA: 0x00040C38 File Offset: 0x0003EE38
		public Participant From
		{
			get
			{
				this.CheckDisposed("From::get");
				return base.GetValueOrDefault<Participant>(InternalSchema.From);
			}
			set
			{
				this.CheckDisposed("From::set");
				base.SetOrDeleteProperty(InternalSchema.From, value);
				if (null == value)
				{
					foreach (PropertyDefinition propertyDefinition in this.ExtraPropertiesToDeleteFrom)
					{
						base.Delete(propertyDefinition);
					}
				}
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x00040C85 File Offset: 0x0003EE85
		public ExDateTime ReceivedTime
		{
			get
			{
				this.CheckDisposed("ReceivedTime::get");
				return base.GetValueOrDefault<ExDateTime>(InternalSchema.ReceivedTime, ExDateTime.MinValue);
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00040CA2 File Offset: 0x0003EEA2
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x00040CBA File Offset: 0x0003EEBA
		public Participant ReadReceiptAddressee
		{
			get
			{
				this.CheckDisposed("ReadReceiptAddressee::get");
				return base.GetValueOrDefault<Participant>(InternalSchema.ReadReceiptAddressee);
			}
			set
			{
				this.CheckDisposed("ReadReceiptAddressee::set");
				base.SetOrDeleteProperty(InternalSchema.ReadReceiptAddressee, value);
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x00040CD3 File Offset: 0x0003EED3
		// (set) Token: 0x060008C6 RID: 2246 RVA: 0x00040CEB File Offset: 0x0003EEEB
		public bool IsReadReceiptRequested
		{
			get
			{
				this.CheckDisposed("IsReadReceiptRequested::get");
				return base.GetValueOrDefault<bool>(InternalSchema.IsReadReceiptRequested);
			}
			set
			{
				this.CheckDisposed("IsReadReceiptRequested::set");
				this[InternalSchema.IsReadReceiptRequested] = value;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x00040D09 File Offset: 0x0003EF09
		// (set) Token: 0x060008C8 RID: 2248 RVA: 0x00040D21 File Offset: 0x0003EF21
		public bool IsDeliveryReceiptRequested
		{
			get
			{
				this.CheckDisposed("IsDeliveryReceiptRequested::get");
				return base.GetValueOrDefault<bool>(InternalSchema.IsDeliveryReceiptRequested);
			}
			set
			{
				this.CheckDisposed("IsDeliveryReceiptRequested::set");
				this[InternalSchema.IsDeliveryReceiptRequested] = value;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00040D3F File Offset: 0x0003EF3F
		// (set) Token: 0x060008CA RID: 2250 RVA: 0x00040D57 File Offset: 0x0003EF57
		public bool IsGroupEscalationMessage
		{
			get
			{
				this.CheckDisposed("IsGroupEscalationMessage::get");
				return base.GetValueOrDefault<bool>(InternalSchema.IsGroupEscalationMessage);
			}
			set
			{
				this.CheckDisposed("IsGroupEscalationMessage::set");
				this[InternalSchema.IsGroupEscalationMessage] = value;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x00040D75 File Offset: 0x0003EF75
		// (set) Token: 0x060008CC RID: 2252 RVA: 0x00040D8D File Offset: 0x0003EF8D
		public AutoResponseSuppress AutoResponseSuppress
		{
			get
			{
				this.CheckDisposed("AutoResponseSuppress::get");
				return base.GetValueOrDefault<AutoResponseSuppress>(InternalSchema.AutoResponseSuppress);
			}
			set
			{
				this.CheckDisposed("AutoResponseSuppress::set");
				EnumValidator.ThrowIfInvalid<AutoResponseSuppress>(value);
				this[InternalSchema.AutoResponseSuppress] = value;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x00040DB1 File Offset: 0x0003EFB1
		public ExDateTime SentTime
		{
			get
			{
				this.CheckDisposed("SentTime::get");
				return base.GetValueOrDefault<ExDateTime>(InternalSchema.SentTime, ExDateTime.MinValue);
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x00040DCE File Offset: 0x0003EFCE
		// (set) Token: 0x060008CF RID: 2255 RVA: 0x00040DE7 File Offset: 0x0003EFE7
		public IconIndex IconIndex
		{
			get
			{
				this.CheckDisposed("IconIndex::get");
				return base.GetValueOrDefault<IconIndex>(InternalSchema.IconIndex, IconIndex.Default);
			}
			set
			{
				this.CheckDisposed("IconIndex::set");
				EnumValidator.ThrowIfInvalid<IconIndex>(value, "value");
				this[InternalSchema.IconIndex] = value;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x00040E10 File Offset: 0x0003F010
		// (set) Token: 0x060008D1 RID: 2257 RVA: 0x00040E2D File Offset: 0x0003F02D
		public string InternetMessageId
		{
			get
			{
				this.CheckDisposed("InternetMessageId::get");
				return base.GetValueOrDefault<string>(InternalSchema.InternetMessageId, string.Empty);
			}
			set
			{
				this.CheckDisposed("InternetMessageId::set");
				base.CheckSetNull("Message::InternetMessageId", "InternetMessageId", value);
				this[InternalSchema.InternetMessageId] = value;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x00040E58 File Offset: 0x0003F058
		// (set) Token: 0x060008D3 RID: 2259 RVA: 0x00040E7D File Offset: 0x0003F07D
		public byte[] ConversationIndex
		{
			get
			{
				this.CheckDisposed("ConversationIndex::get");
				return base.GetValueOrDefault<byte[]>(InternalSchema.ConversationIndex);
			}
			set
			{
				this.CheckDisposed("ConversationIndex::set");
				base.CheckSetNull("Message::ConversationIndex", "ConversationIndex", value);
				this[InternalSchema.ConversationIndex] = value;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x00040EA7 File Offset: 0x0003F0A7
		// (set) Token: 0x060008D5 RID: 2261 RVA: 0x00040EBA File Offset: 0x0003F0BA
		public string ConversationTopic
		{
			get
			{
				this.CheckDisposed("ConversationTopic::get");
				return MessageItem.CalculateConversationTopic(this);
			}
			set
			{
				this.CheckDisposed("ConversationTopic::set");
				base.CheckSetNull("Message::ConversationTopic", "ConversationTopic", value);
				this[InternalSchema.ConversationTopic] = value;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x00040EE4 File Offset: 0x0003F0E4
		// (set) Token: 0x060008D7 RID: 2263 RVA: 0x00040F01 File Offset: 0x0003F101
		public string InReplyTo
		{
			get
			{
				this.CheckDisposed("InReplyTo::get");
				return base.GetValueOrDefault<string>(InternalSchema.InReplyTo, string.Empty);
			}
			set
			{
				this.CheckDisposed("InReplyTo::set");
				base.CheckSetNull("Message::InReplyTo", "InReplyTo", value);
				this[InternalSchema.InReplyTo] = value;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x00040F2B File Offset: 0x0003F12B
		// (set) Token: 0x060008D9 RID: 2265 RVA: 0x00040F48 File Offset: 0x0003F148
		public string References
		{
			get
			{
				this.CheckDisposed("References::get");
				return base.GetValueOrDefault<string>(InternalSchema.InternetReferences, string.Empty);
			}
			set
			{
				this.CheckDisposed("References::set");
				base.CheckSetNull("Message::InternetReferences", "InternetReferences", value);
				this[InternalSchema.InternetReferences] = value;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x00040F72 File Offset: 0x0003F172
		public IList<Participant> Likers
		{
			get
			{
				this.CheckDisposed("Likers::get");
				if (this.likers == null)
				{
					this.likers = new Likers(base.PropertyBag);
				}
				return this.likers;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x00040F9E File Offset: 0x0003F19E
		public IList<Participant> ReplyTo
		{
			get
			{
				this.CheckDisposed("ReplyTo::get");
				if (this.replyTo == null)
				{
					this.replyTo = new ReplyTo(base.PropertyBag);
				}
				return this.replyTo;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x00040FCA File Offset: 0x0003F1CA
		// (set) Token: 0x060008DD RID: 2269 RVA: 0x00040FE2 File Offset: 0x0003F1E2
		public bool IsResponseRequested
		{
			get
			{
				this.CheckDisposed("IsResponseRequested::get");
				return base.GetValueOrDefault<bool>(InternalSchema.IsResponseRequested);
			}
			set
			{
				this.CheckDisposed("IsResponseRequested::set");
				this[InternalSchema.IsResponseRequested] = value;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x00041000 File Offset: 0x0003F200
		// (set) Token: 0x060008DF RID: 2271 RVA: 0x00041018 File Offset: 0x0003F218
		public bool IsRead
		{
			get
			{
				this.CheckDisposed("IsRead::get");
				return base.GetValueOrDefault<bool>(InternalSchema.IsRead);
			}
			set
			{
				this.CheckDisposed("IsRead::set");
				this[InternalSchema.IsRead] = value;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x00041036 File Offset: 0x0003F236
		// (set) Token: 0x060008E1 RID: 2273 RVA: 0x00041051 File Offset: 0x0003F251
		public bool DeferUnreadFlag
		{
			get
			{
				this.CheckDisposed("DeferUnreadFlag::get");
				return base.GetValueOrDefault<int>(InternalSchema.ItemTemporaryFlag) == 1;
			}
			set
			{
				this.CheckDisposed("DeferUnreadFlag::set");
				if (value)
				{
					this[InternalSchema.ItemTemporaryFlag] = ItemTemporaryFlags.DeferUnread;
					return;
				}
				base.PropertyBag.Delete(InternalSchema.ItemTemporaryFlag);
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x00041083 File Offset: 0x0003F283
		// (set) Token: 0x060008E3 RID: 2275 RVA: 0x0004109B File Offset: 0x0003F29B
		public bool WasDeliveredViaBcc
		{
			get
			{
				this.CheckDisposed("WasDeliveredViaBcc::get");
				return base.GetValueOrDefault<bool>(InternalSchema.MessageBccMe);
			}
			set
			{
				this.CheckDisposed("WasDeliveredViaBcc::set");
				this[InternalSchema.MessageBccMe] = value;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x000410B9 File Offset: 0x0003F2B9
		// (set) Token: 0x060008E5 RID: 2277 RVA: 0x000410D1 File Offset: 0x0003F2D1
		public bool IsFromFavoriteSender
		{
			get
			{
				this.CheckDisposed("IsFromFavoriteSender::get");
				return base.GetValueOrDefault<bool>(MessageItemSchema.IsFromFavoriteSender);
			}
			set
			{
				this.CheckDisposed("IsFromFavoriteSender::set");
				this[MessageItemSchema.IsFromFavoriteSender] = value;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x000410EF File Offset: 0x0003F2EF
		// (set) Token: 0x060008E7 RID: 2279 RVA: 0x00041107 File Offset: 0x0003F307
		public bool IsFromPerson
		{
			get
			{
				this.CheckDisposed("IsFromPerson::get");
				return base.GetValueOrDefault<bool>(MessageItemSchema.IsFromPerson);
			}
			set
			{
				this.CheckDisposed("IsFromPerson::set");
				this[MessageItemSchema.IsFromPerson] = value;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x00041125 File Offset: 0x0003F325
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return MessageItemSchema.Instance;
			}
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00041137 File Offset: 0x0003F337
		public void ExpandRecipientTable()
		{
			this.CheckDisposed("ExpandRecipientTable");
			this.InternalExpandRecipientTable();
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0004114A File Offset: 0x0003F34A
		public void Send()
		{
			this.Send(SubmitMessageFlags.None);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00041154 File Offset: 0x0003F354
		public void Send(SubmitMessageFlags submitFlags)
		{
			this.CheckDisposed("Send");
			base.Session.CheckCapabilities(base.Session.Capabilities.CanSend, "CanSend");
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "Storage.Message.Send.");
			MailboxSession mailboxSession = base.Session as MailboxSession;
			if (mailboxSession != null)
			{
				this.InternalSend(mailboxSession.GetDefaultFolderId(DefaultFolderType.SentItems), submitFlags, false);
				return;
			}
			this.InternalSend(null, submitFlags, true);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x000411CB File Offset: 0x0003F3CB
		public void Send(StoreObjectId saveSentMessageFolder, SaveMode saveMode = SaveMode.ResolveConflicts)
		{
			this.Send(saveSentMessageFolder, SubmitMessageFlags.None, false, saveMode);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x000411D7 File Offset: 0x0003F3D7
		public void Send(StoreObjectId saveSentMessageFolder, SubmitMessageFlags submitFlags)
		{
			this.Send(saveSentMessageFolder, submitFlags, false, SaveMode.ResolveConflicts);
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x000411E3 File Offset: 0x0003F3E3
		// (set) Token: 0x060008EF RID: 2287 RVA: 0x000411FB File Offset: 0x0003F3FB
		public bool IsDraft
		{
			get
			{
				this.CheckDisposed("IsDraft::get");
				return base.GetValueOrDefault<bool>(InternalSchema.IsDraft);
			}
			set
			{
				this.CheckDisposed("IsDraft::set");
				this[InternalSchema.IsDraft] = value;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x00041219 File Offset: 0x0003F419
		// (set) Token: 0x060008F1 RID: 2289 RVA: 0x00041232 File Offset: 0x0003F432
		public bool IsResend
		{
			get
			{
				this.CheckDisposed("IsResend::get");
				return base.GetValueOrDefault<bool>(InternalSchema.IsResend, false);
			}
			set
			{
				this.CheckDisposed("IsResend::set");
				this[InternalSchema.IsResend] = value;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x00041250 File Offset: 0x0003F450
		public VotingInfo VotingInfo
		{
			get
			{
				if (this.votingInfo == null)
				{
					this.votingInfo = new VotingInfo(this);
				}
				return this.votingInfo;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x0004126C File Offset: 0x0003F46C
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x00041298 File Offset: 0x0003F498
		public Reminders<ModernReminder> ModernReminders
		{
			get
			{
				this.CheckDisposed("ModernReminders::get");
				if (this.modernReminders == null)
				{
					this.modernReminders = Reminders<ModernReminder>.Get(this, InternalSchema.ModernReminders);
				}
				return this.modernReminders;
			}
			set
			{
				this.CheckDisposed("ModernReminders::set");
				base.Load(new PropertyDefinition[]
				{
					InternalSchema.GlobalObjectId
				});
				if (base.GetValueOrDefault<byte[]>(InternalSchema.GlobalObjectId, null) == null)
				{
					GlobalObjectId globalObjectId = new GlobalObjectId();
					this[InternalSchema.GlobalObjectId] = globalObjectId.Bytes;
				}
				Reminders<ModernReminder>.Set(this, InternalSchema.ModernReminders, value);
				this.modernReminders = value;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x00041300 File Offset: 0x0003F500
		// (set) Token: 0x060008F6 RID: 2294 RVA: 0x0004132C File Offset: 0x0003F52C
		public RemindersState<ModernReminderState> ModernRemindersState
		{
			get
			{
				this.CheckDisposed("ModernRemindersState::get");
				if (this.modernRemindersState == null)
				{
					this.modernRemindersState = RemindersState<ModernReminderState>.Get(this, InternalSchema.ModernRemindersState);
				}
				return this.modernRemindersState;
			}
			set
			{
				this.CheckDisposed("ModernRemindersState::set");
				RemindersState<ModernReminderState>.Set(this, InternalSchema.ModernRemindersState, value);
				this.modernRemindersState = value;
			}
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0004134C File Offset: 0x0003F54C
		public GlobalObjectId GetGlobalObjectId()
		{
			base.Load(new PropertyDefinition[]
			{
				InternalSchema.GlobalObjectId
			});
			byte[] valueOrDefault = base.GetValueOrDefault<byte[]>(InternalSchema.GlobalObjectId, null);
			if (valueOrDefault == null)
			{
				return null;
			}
			return new GlobalObjectId(valueOrDefault);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00041388 File Offset: 0x0003F588
		public bool IsEventReminderMessage()
		{
			string valueOrDefault = base.PropertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass, string.Empty);
			return ObjectClass.IsOfClass(valueOrDefault, "IPM.Note.Reminder.Event");
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x000413B8 File Offset: 0x0003F5B8
		public bool IsValidApprovalRequest()
		{
			string valueOrDefault = base.PropertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass, string.Empty);
			if (!ObjectClass.IsOfClass(valueOrDefault, "IPM.Note.Microsoft.Approval.Request"))
			{
				return false;
			}
			if (ObjectClass.IsSmime(valueOrDefault))
			{
				return false;
			}
			VotingInfo votingInfo = this.VotingInfo;
			if (votingInfo == null)
			{
				return false;
			}
			string[] array = (string[])votingInfo.GetOptionsList();
			return array != null && array.Length == 2;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00041418 File Offset: 0x0003F618
		public bool IsValidUndecidedApprovalRequest()
		{
			if (!this.IsValidApprovalRequest())
			{
				return false;
			}
			int? valueAsNullable = base.PropertyBag.GetValueAsNullable<int>(MessageItemSchema.LastVerbExecuted);
			if (valueAsNullable != null && valueAsNullable.Value >= 1 && valueAsNullable.Value < 100)
			{
				return false;
			}
			int? valueAsNullable2 = base.PropertyBag.GetValueAsNullable<int>(MessageItemSchema.ApprovalDecision);
			return valueAsNullable2 == null || valueAsNullable2.Value < 1 || valueAsNullable2.Value >= 100;
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00041494 File Offset: 0x0003F694
		public MessageItem CreateForward(StoreId parentFolderId, ReplyForwardConfiguration configuration)
		{
			MailboxSession mailboxSession = base.Session as MailboxSession;
			if (mailboxSession == null)
			{
				string message = "MessageItem::CreateForward: Unable to create reply/forward on non-Mailbox session";
				ExTraceGlobals.StorageTracer.TraceError((long)this.GetHashCode(), message);
				throw new NotSupportedException(message);
			}
			return this.CreateForward(mailboxSession, parentFolderId, configuration);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x000414D8 File Offset: 0x0003F6D8
		public virtual MessageItem CreateForward(MailboxSession session, StoreId parentFolderId, ReplyForwardConfiguration configuration)
		{
			this.CheckDisposed("CreateForward");
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
			Util.ThrowOnNullArgument(configuration, "configuration");
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "Item::CreateReply.");
			return base.InternalCreateForward(session, parentFolderId, configuration);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00041530 File Offset: 0x0003F730
		public MessageItem CreateReply(StoreId parentFolderId, ReplyForwardConfiguration configuration)
		{
			MailboxSession mailboxSession = base.Session as MailboxSession;
			if (mailboxSession == null)
			{
				string message = "MessageItem::CreateReply: Unable to create reply/forward on non-Mailbox session";
				ExTraceGlobals.StorageTracer.TraceError((long)this.GetHashCode(), message);
				throw new NotSupportedException(message);
			}
			return this.CreateReply(mailboxSession, parentFolderId, configuration);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00041574 File Offset: 0x0003F774
		public MessageItem CreateVotingResponse(string bodyPrefix, BodyFormat format, StoreId parentFolderId, string votingOption)
		{
			this.CheckDisposed("CreateVotingResponse");
			EnumValidator.ThrowIfInvalid<BodyFormat>(format, "format");
			Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
			Util.ThrowOnNullArgument(bodyPrefix, "bodyPrefix");
			Util.ThrowOnNullArgument(votingOption, "votingOption");
			if (!this.VotingInfo.GetOptionsList().Contains(votingOption))
			{
				throw new ArgumentException("Invalid VotingOption", votingOption);
			}
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "MessageItem::CreateVotingResponse");
			MessageItem messageItem = null;
			bool flag = false;
			MessageItem result;
			try
			{
				messageItem = MessageItem.Create(base.Session, parentFolderId);
				ReplyForwardConfiguration replyForwardConfiguration = new ReplyForwardConfiguration(format);
				replyForwardConfiguration.AddBodyPrefix(bodyPrefix);
				VotingResponse votingResponse = new VotingResponse(this, messageItem, replyForwardConfiguration, votingOption);
				votingResponse.PopulateProperties();
				flag = true;
				result = messageItem;
			}
			finally
			{
				if (!flag && messageItem != null)
				{
					messageItem.Dispose();
					messageItem = null;
				}
			}
			return result;
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00041648 File Offset: 0x0003F848
		public virtual MessageItem CreateReply(MailboxSession session, StoreId parentFolderId, ReplyForwardConfiguration configuration)
		{
			this.CheckDisposed("CreateReply");
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
			Util.ThrowOnNullArgument(configuration, "configuration");
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "Item::CreateReply.");
			this.CheckReplyAllowed();
			MessageItem messageItem = base.InternalCreateReply(session, parentFolderId, configuration);
			this.SetApprovalRequestReplyRecipient(messageItem);
			return messageItem;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x000416B0 File Offset: 0x0003F8B0
		public MessageItem CreateReplyAll(StoreId parentFolderId, ReplyForwardConfiguration configuration)
		{
			MailboxSession mailboxSession = base.Session as MailboxSession;
			if (mailboxSession == null)
			{
				string message = "MessageItem::CreateReplyAll: Unable to create reply/forward on non-Mailbox session";
				ExTraceGlobals.StorageTracer.TraceError((long)this.GetHashCode(), message);
				throw new NotSupportedException(message);
			}
			return this.CreateReplyAll(mailboxSession, parentFolderId, configuration);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x000416F4 File Offset: 0x0003F8F4
		public virtual MessageItem CreateReplyAll(MailboxSession session, StoreId parentFolderId, ReplyForwardConfiguration configuration)
		{
			this.CheckDisposed("CreateReplyAll");
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
			Util.ThrowOnNullArgument(configuration, "configuration");
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "Item::CreateReplyAll.");
			this.CheckReplyAllowed();
			MessageItem messageItem = base.InternalCreateReplyAll(session, parentFolderId, configuration);
			this.SetApprovalRequestReplyRecipient(messageItem);
			return messageItem;
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x0004175B File Offset: 0x0003F95B
		// (set) Token: 0x06000903 RID: 2307 RVA: 0x00041774 File Offset: 0x0003F974
		public VersionedId ParentMessageId
		{
			get
			{
				this.CheckDisposed("ParentMessageId::get");
				this.GetReplyForwardStatus();
				return this.parentMessageId;
			}
			internal set
			{
				this.CheckDisposed("ParentMessageId::set");
				this.GetReplyForwardStatus();
				this.parentMessageId = value;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x0004178E File Offset: 0x0003F98E
		// (set) Token: 0x06000905 RID: 2309 RVA: 0x000417A7 File Offset: 0x0003F9A7
		public MessageResponseType MessageResponseType
		{
			get
			{
				this.CheckDisposed("MessageResponseType::get");
				this.GetReplyForwardStatus();
				return this.messageResponseType;
			}
			internal set
			{
				this.CheckDisposed("MessageResponseType::set");
				this.GetReplyForwardStatus();
				this.messageResponseType = value;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x000417C1 File Offset: 0x0003F9C1
		public RecipientCollection Recipients
		{
			get
			{
				this.CheckDisposed("Recipients::get");
				return this.FetchRecipientCollection(true);
			}
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x000417D8 File Offset: 0x0003F9D8
		internal RecipientCollection FetchRecipientCollection(bool forceOpen)
		{
			if (this.recipients == null)
			{
				CoreRecipientCollection recipientCollection = base.CoreItem.GetRecipientCollection(forceOpen);
				if (recipientCollection != null)
				{
					this.recipients = new RecipientCollection(recipientCollection);
				}
			}
			return this.recipients;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0004180F File Offset: 0x0003FA0F
		internal Item FetchSmimeContent(string imceaDomain)
		{
			if (this.smimeContent != null)
			{
				this.smimeContent.Dispose();
				this.smimeContent = null;
			}
			if (!ItemConversion.TryOpenSMimeContent(this, imceaDomain, out this.smimeContent))
			{
				return null;
			}
			return this.smimeContent;
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00041844 File Offset: 0x0003FA44
		public bool IsRestricted
		{
			get
			{
				string contentClass = base.TryGetProperty(InternalSchema.ContentClass) as string;
				if (!ObjectClass.IsRightsManagedContentClass(contentClass))
				{
					return false;
				}
				if (base.AttachmentCollection.Count != 1)
				{
					return false;
				}
				using (Attachment attachment = base.AttachmentCollection.TryOpenFirstAttachment(AttachmentType.Stream))
				{
					if (attachment != null)
					{
						return string.Equals(attachment.FileName, "message.rpmsg", StringComparison.OrdinalIgnoreCase);
					}
				}
				return false;
			}
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x000418C0 File Offset: 0x0003FAC0
		public void SendReadReceipt()
		{
			this.SetReadFlagsInternal(SetReadFlags.DeferredErrors);
			this.SetReadFlagsInternal(SetReadFlags.GenerateReceiptOnly);
			this.IsRead = true;
			base.ClearFlagsPropertyForSet(InternalSchema.IsRead);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x000418E4 File Offset: 0x0003FAE4
		public void MarkAsRead(bool suppressReadReceipt, bool deferToSave)
		{
			this.CheckDisposed("MarkAsRead");
			if (!deferToSave)
			{
				this.SetReadFlagsInternal(suppressReadReceipt ? SetReadFlags.SuppressReceipt : SetReadFlags.None);
			}
			this.IsRead = true;
			this[InternalSchema.SuppressReadReceipt] = suppressReadReceipt;
			if (!deferToSave)
			{
				base.ClearFlagsPropertyForSet(InternalSchema.IsRead);
				base.ClearFlagsPropertyForSet(InternalSchema.SuppressReadReceipt);
			}
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0004193D File Offset: 0x0003FB3D
		public void MarkAsUnread(bool deferToSave)
		{
			this.CheckDisposed("MarkAsUnread");
			if (!deferToSave)
			{
				this.SetReadFlagsInternal(SetReadFlags.ClearRead);
			}
			this.IsRead = false;
			if (!deferToSave)
			{
				base.ClearFlagsPropertyForSet(InternalSchema.IsRead);
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0004196C File Offset: 0x0003FB6C
		public CultureInfo GetPreferredAcceptLanguage()
		{
			this.CheckDisposed("GetPreferredAcceptLanguage");
			string valueOrDefault = base.GetValueOrDefault<string>(InternalSchema.AcceptLanguage);
			if (valueOrDefault != null)
			{
				return DsnHumanReadableWriter.DefaultDsnHumanReadableWriter.FindLanguage(valueOrDefault, true);
			}
			return null;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x000419A1 File Offset: 0x0003FBA1
		public void AbortSubmit()
		{
			this.CheckDisposed("AbortSubmit");
			if (base.Session == null)
			{
				throw new InvalidOperationException("Cannot invoke AbortSubmit on an in-memory item.");
			}
			if (base.StoreObjectId != null)
			{
				base.Session.AbortSubmit(base.StoreObjectId);
			}
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x000419DA File Offset: 0x0003FBDA
		public void SetNoMessageDecoding(bool value)
		{
			base.CoreItem.CharsetDetector.NoMessageDecoding = value;
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x000419F0 File Offset: 0x0003FBF0
		public void MarkRecipientAsSubmitted(IEnumerable<Participant> submittedParticipants)
		{
			this[MessageItemSchema.NeedSpecialRecipientProcessing] = true;
			foreach (Recipient recipient in this.Recipients)
			{
				recipient.Submitted = recipient.Participant.ExistIn(submittedParticipants);
			}
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00041A5C File Offset: 0x0003FC5C
		public void MarkAllRecipientAsSubmitted()
		{
			this[MessageItemSchema.NeedSpecialRecipientProcessing] = true;
			foreach (Recipient recipient in this.Recipients)
			{
				recipient.Submitted = true;
			}
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00041ABC File Offset: 0x0003FCBC
		public void MarkAsNonDraft()
		{
			if (this.IsDraft || ((MessageFlags)this[MessageItemSchema.Flags] & MessageFlags.IsDraft) == MessageFlags.IsDraft)
			{
				this[MessageItemSchema.Flags] = ((MessageFlags)this[MessageItemSchema.Flags] ^ MessageFlags.IsDraft);
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00041B08 File Offset: 0x0003FD08
		public void SuppressAllAutoResponses()
		{
			this.AutoResponseSuppress = ~(AutoResponseSuppress.DR | AutoResponseSuppress.NDR);
			this.IsDeliveryReceiptRequested = false;
			this[MessageItemSchema.IsNonDeliveryReceiptRequested] = false;
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x00041B2A File Offset: 0x0003FD2A
		// (set) Token: 0x06000915 RID: 2325 RVA: 0x00041B3D File Offset: 0x0003FD3D
		internal override VersionedId AssociatedItemId
		{
			get
			{
				this.CheckDisposed("AssociatedItemId::get");
				return ReplyForwardUtils.GetAssociatedId(this);
			}
			set
			{
				this.CheckDisposed("AssociatedItemId::set");
				ReplyForwardUtils.SetAssociatedId(this, value);
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x00041B51 File Offset: 0x0003FD51
		public virtual bool IsReplyAllowed
		{
			get
			{
				this.CheckDisposed("IsReplyAllowed::get");
				return true;
			}
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00041B60 File Offset: 0x0003FD60
		internal static string CalculateConversationTopic(Item item)
		{
			string text = item.TryGetProperty(InternalSchema.ConversationTopic) as string;
			if (text == null)
			{
				text = item.GetValueOrDefault<string>(InternalSchema.NormalizedSubjectInternal, string.Empty);
				if (item.PropertyBag is AcrPropertyBag && !((AcrPropertyBag)item.PropertyBag).IsReadOnly)
				{
					item[InternalSchema.ConversationTopic] = text;
				}
			}
			return text;
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00041BD4 File Offset: 0x0003FDD4
		public static MessageItem CreateInMemory(PropertyDefinition[] prefetchProperties)
		{
			Util.ThrowOnNullArgument(prefetchProperties, "prefetchProperties");
			return ItemBuilder.ConstructItem<MessageItem>(null, null, null, prefetchProperties, () => new InMemoryPersistablePropertyBag(prefetchProperties), ItemCreateInfo.MessageItemInfo.Creator, Origin.Existing, ItemLevel.TopLevel);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00041C24 File Offset: 0x0003FE24
		public void SendWithoutSavingMessage()
		{
			this.SendWithoutSavingMessage(SubmitMessageFlags.None);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00041C2D File Offset: 0x0003FE2D
		public void SendWithoutSavingMessage(SubmitMessageFlags submitFlags)
		{
			this.CheckDisposed("SendWithoutSavingMessage");
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "Storage.Message.SendWithoutSavingMessage.");
			this.InternalSend(null, submitFlags, true);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00041C5C File Offset: 0x0003FE5C
		public void SendWithoutMovingMessage(StoreObjectId folderId, SaveMode saveMode = SaveMode.ResolveConflicts)
		{
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "Storage.Message.SendWithoutMovingMessage.");
			if (folderId == null)
			{
				MailboxSession mailboxSession = base.Session as MailboxSession;
				if (mailboxSession != null)
				{
					folderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.SentItems);
				}
			}
			this.Send(folderId, SubmitMessageFlags.None, true, saveMode);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00041CA5 File Offset: 0x0003FEA5
		public void CommitReplyTo()
		{
			if (this.replyTo == null || !this.replyTo.IsDirty)
			{
				return;
			}
			if (base.PropertyBag is AcrPropertyBag)
			{
				((AcrPropertyBag)base.PropertyBag).SetIrresolvableChange();
			}
			this.replyTo.Save();
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00041CE8 File Offset: 0x0003FEE8
		public void StampMessageBodyTag()
		{
			int num;
			this[InternalSchema.BodyTag] = base.Body.CalculateBodyTag(out num);
			if (num >= 0)
			{
				this[InternalSchema.LatestMessageWordCount] = num;
			}
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00041D22 File Offset: 0x0003FF22
		protected override void OnBeforeSave()
		{
			this.CommitReplyTo();
			this.CommitLikers();
			base.OnBeforeSave();
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00041D36 File Offset: 0x0003FF36
		protected override void OnAfterSave(ConflictResolutionResult acrResults)
		{
			base.OnAfterSave(acrResults);
			if (acrResults.SaveStatus == SaveResult.SuccessWithConflictResolution)
			{
				this.recipients = null;
			}
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00041D50 File Offset: 0x0003FF50
		protected virtual void OnBeforeSend()
		{
			ConflictResolutionResult conflictResolutionResult = base.SaveInternal(this.sendSaveMode, true, null, CoreItemOperation.Send);
			if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
			{
				throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(base.InternalObjectId), conflictResolutionResult);
			}
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00041D88 File Offset: 0x0003FF88
		private void SetReadFlagsInternal(SetReadFlags flags)
		{
			StoreSession session = base.Session;
			bool flag = false;
			try
			{
				if (session != null)
				{
					session.BeginMapiCall();
					session.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				base.MapiMessage.SetReadFlag(flags);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSetReadFlags, ex, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("MessageItem::SetReadFlagsInternal.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSetReadFlags, ex2, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("MessageItem::SetReadFlagsInternal.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (session != null)
					{
						session.EndMapiCall();
						if (flag)
						{
							session.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00041EA8 File Offset: 0x000400A8
		private void CheckReplyAllowed()
		{
			if (!this.IsReplyAllowed)
			{
				throw new NotSupportedException(ServerStrings.OperationNotAllowed("Reply", base.GetType().Name));
			}
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00041ED4 File Offset: 0x000400D4
		private void GetReplyForwardStatus()
		{
			if (this.isReplyFwdStatusParsed)
			{
				return;
			}
			this.messageResponseType = MessageResponseType.None;
			this.parentMessageId = null;
			string valueOrDefault = base.GetValueOrDefault<string>(InternalSchema.ReplyForwardStatus, string.Empty);
			if (valueOrDefault.Length == 0)
			{
				return;
			}
			object[] array = ReplyForwardUtils.DecodeReplyForwardStatus(valueOrDefault);
			if (array == null || array.Length != 3)
			{
				ExTraceGlobals.StorageTracer.TraceError<int, int>((long)this.GetHashCode(), "Ex12Mesage::GetReplyForwardStatus. The reply/forward status data saved on the new item has been corrupted. Actual field count = {0}, Expect = {1}.", (array == null) ? 0 : array.Length, 3);
				return;
			}
			try
			{
				int num = (int)array[0];
				if (num == 102 || num == 103)
				{
					this.messageResponseType = MessageResponseType.Reply;
				}
				else if (num == 104)
				{
					this.messageResponseType = MessageResponseType.Forward;
				}
				string base64Id = (string)array[2];
				this.parentMessageId = VersionedId.Deserialize(base64Id);
				this.isReplyFwdStatusParsed = true;
			}
			catch (FormatException arg)
			{
				ExTraceGlobals.StorageTracer.TraceError<FormatException>((long)this.GetHashCode(), "Message::InternalParseReplyForwardStatus. The reply forward status data has been corrupted. Exception = {0}.", arg);
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00041FB8 File Offset: 0x000401B8
		private void CommitLikers()
		{
			if (this.likers != null && this.likers.IsDirty)
			{
				if (base.PropertyBag is AcrPropertyBag)
				{
					((AcrPropertyBag)base.PropertyBag).SetIrresolvableChange();
				}
				this.likers.Save();
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00041FF8 File Offset: 0x000401F8
		private void InternalExpandRecipientTable()
		{
			int num = this.Recipients.Count;
			for (int i = 0; i < num; i++)
			{
				Recipient recipient = this.Recipients[i];
				if (recipient.Participant.RoutingType == "MAPIPDL")
				{
					recipient.Participant.Validate();
					foreach (Participant participant in DistributionList.ExpandDeep(base.Session, ((StoreParticipantOrigin)recipient.Participant.Origin).OriginItemId, true))
					{
						this.Recipients.Add(participant, recipient.RecipientItemType);
					}
					this.Recipients.Remove(recipient);
					i--;
					num--;
				}
			}
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x000420B8 File Offset: 0x000402B8
		private void Send(StoreObjectId saveSentMessageFolder, SubmitMessageFlags submitFlags, bool doNotMove, SaveMode saveMode = SaveMode.ResolveConflicts)
		{
			this.CheckDisposed("Send(saveSentMessageFolder, submitFlags, doNotMove, saveMode)");
			base.Session.CheckCapabilities(base.Session.Capabilities.CanSend, "CanSend");
			if (saveSentMessageFolder == null)
			{
				ExTraceGlobals.StorageTracer.TraceError((long)this.GetHashCode(), "Message::Send(saveSentMessageFolder, submitFlags, doNotMove, saveMode). The folder Id parameter is null.");
				throw new ArgumentNullException(ServerStrings.ExNullParameter("saveSentMessageFolder", 1));
			}
			ExTraceGlobals.StorageTracer.Information<string>((long)this.GetHashCode(), "Storage.Message.Send(saveSentMessageFolder, submitFlags, doNotMove, saveMode). saveSentMessageFolder = {0}.", saveSentMessageFolder.ToBase64String());
			this.sendSaveMode = saveMode;
			this.InternalSend(saveSentMessageFolder, submitFlags, doNotMove);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0004214C File Offset: 0x0004034C
		private void InternalSend(StoreObjectId saveSentMessageFolder, SubmitMessageFlags submitFlags, bool doNotMove)
		{
			this.InternalExpandRecipientTable();
			this.CommitReplyTo();
			ReplyForwardUtils.UpdateOriginalItemProperties(this);
			this[InternalSchema.ClientSubmittedSecurely] = true;
			base.Delete(InternalSchema.ReplyForwardStatus);
			if (saveSentMessageFolder == null)
			{
				this[InternalSchema.DeleteAfterSubmit] = true;
			}
			else
			{
				if (!doNotMove)
				{
					this[InternalSchema.SentMailEntryId] = saveSentMessageFolder.ProviderLevelItemId;
				}
				this.StampMessageBodyTag();
			}
			base.CoreItem.BeforeSend += this.OnBeforeSend;
			try
			{
				base.CoreItem.Submit(submitFlags);
			}
			catch (StoragePermanentException)
			{
				this.AbortSubmitOnFailure();
				throw;
			}
			catch (StorageTransientException)
			{
				this.AbortSubmitOnFailure();
				throw;
			}
			finally
			{
				base.CoreItem.BeforeSend -= this.OnBeforeSend;
			}
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00042234 File Offset: 0x00040434
		private void AbortSubmitOnFailure()
		{
			try
			{
				base.Load(null);
				this.AbortSubmit();
			}
			catch (StoragePermanentException)
			{
			}
			catch (StorageTransientException)
			{
			}
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00042274 File Offset: 0x00040474
		private void SetApprovalRequestReplyRecipient(MessageItem replyMessage)
		{
			if (this.ClassName.Equals("IPM.Note.Microsoft.Approval.Request", StringComparison.OrdinalIgnoreCase))
			{
				base.Load(new PropertyDefinition[]
				{
					MessageItemSchema.ApprovalRequestor
				});
				string valueOrDefault = base.GetValueOrDefault<string>(MessageItemSchema.ApprovalRequestor, string.Empty);
				if (!string.IsNullOrEmpty(valueOrDefault))
				{
					replyMessage.Recipients.Clear();
					replyMessage.Recipients.Add(new Participant(string.Empty, valueOrDefault, "SMTP"));
				}
			}
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x000422EC File Offset: 0x000404EC
		internal static void CoreObjectUpdateConversationTopic(CoreItem coreItem)
		{
			string valueOrDefault = coreItem.PropertyBag.GetValueOrDefault<string>(InternalSchema.NormalizedSubjectInternal);
			string valueOrDefault2 = coreItem.PropertyBag.GetValueOrDefault<string>(InternalSchema.ConversationTopic);
			if (valueOrDefault != null && valueOrDefault != valueOrDefault2)
			{
				coreItem.PropertyBag[InternalSchema.ConversationTopic] = valueOrDefault;
			}
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x00042338 File Offset: 0x00040538
		internal static void CoreObjectUpdateConversationIndex(CoreItem coreItem)
		{
			if (coreItem.Session != null && PropertyError.IsPropertyNotFound(coreItem.PropertyBag.TryGetProperty(ItemSchema.ConversationIndex)))
			{
				coreItem.PropertyBag[ItemSchema.ConversationIndex] = Microsoft.Exchange.Data.Storage.ConversationIndex.CreateNew().ToByteArray();
			}
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00042381 File Offset: 0x00040581
		internal static void CoreObjectUpdateConversationIndexFixup(ICoreItem item, CoreItemOperation operation)
		{
			MessageItem.AggregateMessageInConversation(item, operation);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0004238C File Offset: 0x0004058C
		internal static void CoreObjectUpdateIconIndex(CoreItem coreItem)
		{
			string valueOrDefault = coreItem.PropertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass);
			if (ObjectClass.IsMeetingRequest(valueOrDefault) || ObjectClass.IsMeetingCancellation(valueOrDefault))
			{
				return;
			}
			IconIndex? valueAsNullable = coreItem.PropertyBag.GetValueAsNullable<IconIndex>(InternalSchema.IconIndex);
			if (valueAsNullable == null)
			{
				return;
			}
			if (ObjectClass.IsMessage(valueOrDefault, false) && (valueAsNullable.Value & IconIndex.BaseAppointment) != (IconIndex)0 && valueAsNullable.Value != IconIndex.Default)
			{
				coreItem.PropertyBag[InternalSchema.IconIndex] = IconIndex.Default;
			}
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00042410 File Offset: 0x00040610
		internal static void CoreObjectUpdateMimeSkeleton(CoreItem coreItem)
		{
			if (coreItem.PropertyBag.IsPropertyDirty(InternalSchema.MimeSkeleton))
			{
				return;
			}
			if (MessageItem.MessageMimeChanged(coreItem))
			{
				if (!PropertyError.IsPropertyNotFound(coreItem.PropertyBag.TryGetProperty(InternalSchema.MimeSkeleton)))
				{
					coreItem.PropertyBag.Delete(InternalSchema.MimeSkeleton);
				}
				PersistablePropertyBag persistablePropertyBag = coreItem.PropertyBag as PersistablePropertyBag;
				if (persistablePropertyBag != null && !coreItem.IsMoveUser)
				{
					persistablePropertyBag.SetUpdateImapIdFlag();
				}
			}
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0004247C File Offset: 0x0004067C
		public void RemoteSend()
		{
			MailboxSession mailboxSession = base.Session as MailboxSession;
			if (mailboxSession == null)
			{
				throw new InvalidOperationException("RemoteSend is used for aggregation mailbox");
			}
			if (mailboxSession.ActivitySession == null)
			{
				throw new ActivitySessionIsNullException();
			}
			mailboxSession.ActivitySession.CaptureRemoteSend(base.StoreObjectId);
			MailboxReplicationServiceClientSlim.NotifyToSync(SyncNowNotificationFlags.Send, mailboxSession.MailboxGuid, mailboxSession.MdbGuid);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x000424D4 File Offset: 0x000406D4
		private static void AggregateMessageInConversation(ICoreItem item, CoreItemOperation operation)
		{
			ICorePropertyBag propertyBag = item.PropertyBag;
			MailboxSession mailboxSession = item.Session as MailboxSession;
			if (mailboxSession == null)
			{
				return;
			}
			ConversationIndexTrackingEx conversationIndexTrackingEx = ConversationIndexTrackingEx.Create();
			IConversationAggregator conversationAggregator;
			if (ConversationAggregatorFactory.TryInstantiateAggregatorForSave(mailboxSession, operation, item, conversationIndexTrackingEx, out conversationAggregator))
			{
				mailboxSession.Mailbox.Load(new PropertyDefinition[]
				{
					InternalSchema.LogonRightsOnMailbox
				});
				if (mailboxSession.CanActAsOwner || mailboxSession.IsGroupMailbox())
				{
					ConversationAggregationResult conversationAggregationResult = conversationAggregator.Aggregate(item);
					byte[] newValue;
					if (MessageItem.TryCalculateConversationCreatorSid(mailboxSession, conversationAggregationResult, operation, item.PropertyBag, out newValue))
					{
						propertyBag.SetOrDeleteProperty(ItemSchema.ConversationCreatorSID, newValue);
					}
					conversationIndexTrackingEx.TraceVersionAndHeuristics(conversationAggregationResult.Stage.ToString());
					propertyBag[ItemSchema.ConversationIndex] = conversationAggregationResult.ConversationIndex.ToByteArray();
					propertyBag[ItemSchema.ConversationIndexTrackingEx] = conversationIndexTrackingEx.ToString();
					propertyBag[ItemSchema.SupportsSideConversation] = conversationAggregationResult.SupportsSideConversation;
					propertyBag.SetOrDeleteProperty(ItemSchema.ConversationFamilyId, conversationAggregationResult.ConversationFamilyId);
					if (!Microsoft.Exchange.Data.Storage.ConversationIndex.CheckStageValue(conversationAggregationResult.Stage, Microsoft.Exchange.Data.Storage.ConversationIndex.FixupStage.L1))
					{
						propertyBag[ItemSchema.ConversationIndexTracking] = true;
						return;
					}
				}
				else
				{
					string valueOrDefault = propertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass, string.Empty);
					if (ObjectClass.IsMeetingMessage(valueOrDefault) || (string.IsNullOrEmpty(propertyBag.GetValueOrDefault<string>(ItemSchema.InReplyTo)) && string.IsNullOrEmpty(propertyBag.GetValueOrDefault<string>(ItemSchema.InternetReferences)) && string.IsNullOrEmpty(propertyBag.GetValueOrDefault<string>(ItemSchema.SubjectPrefix))))
					{
						propertyBag[ItemSchema.ConversationIndexTracking] = true;
					}
				}
			}
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0004265C File Offset: 0x0004085C
		private static bool TryCalculateConversationCreatorSid(IMailboxSession session, ConversationAggregationResult aggregationResult, CoreItemOperation operation, ICorePropertyBag deliveredMessage, out byte[] conversationCreatorSid)
		{
			conversationCreatorSid = null;
			ConversationCreatorSidCalculatorFactory conversationCreatorSidCalculatorFactory = new ConversationCreatorSidCalculatorFactory(XSOFactory.Default);
			IConversationCreatorSidCalculator conversationCreatorSidCalculator;
			return conversationCreatorSidCalculatorFactory.TryCreate(session, session.MailboxOwner, out conversationCreatorSidCalculator) && conversationCreatorSidCalculator.TryCalculateOnSave(deliveredMessage, aggregationResult.Stage, aggregationResult.ConversationIndex, operation, out conversationCreatorSid);
		}

		// Token: 0x04000241 RID: 577
		private const SaveMode DefaultSendSaveMode = SaveMode.ResolveConflicts;

		// Token: 0x04000242 RID: 578
		private RecipientCollection recipients;

		// Token: 0x04000243 RID: 579
		private ReplyTo replyTo;

		// Token: 0x04000244 RID: 580
		private VersionedId parentMessageId;

		// Token: 0x04000245 RID: 581
		private MessageResponseType messageResponseType;

		// Token: 0x04000246 RID: 582
		private bool isReplyFwdStatusParsed;

		// Token: 0x04000247 RID: 583
		private VotingInfo votingInfo;

		// Token: 0x04000248 RID: 584
		private Item smimeContent;

		// Token: 0x04000249 RID: 585
		private Reminders<ModernReminder> modernReminders;

		// Token: 0x0400024A RID: 586
		private RemindersState<ModernReminderState> modernRemindersState;

		// Token: 0x0400024B RID: 587
		private SaveMode sendSaveMode;

		// Token: 0x0400024C RID: 588
		private Likers likers;

		// Token: 0x0400024D RID: 589
		private readonly PropertyDefinition[] ExtraPropertiesToDeleteFrom = new PropertyDefinition[]
		{
			InternalSchema.SentRepresentingSimpleDisplayName,
			InternalSchema.SentRepresentingOrgAddressType,
			InternalSchema.SentRepresentingOrgEmailAddr,
			InternalSchema.SentRepresentingSID,
			InternalSchema.SentRepresentingGuid
		};
	}
}
