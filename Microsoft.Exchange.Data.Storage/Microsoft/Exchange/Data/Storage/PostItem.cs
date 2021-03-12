using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200007F RID: 127
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PostItem : Item, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06000932 RID: 2354 RVA: 0x000426A1 File Offset: 0x000408A1
		internal PostItem(ICoreItem coreItem) : base(coreItem, false)
		{
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x000426AB File Offset: 0x000408AB
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x000426C3 File Offset: 0x000408C3
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

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x000426DC File Offset: 0x000408DC
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x000426F4 File Offset: 0x000408F4
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
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x0004270D File Offset: 0x0004090D
		public ExDateTime PostedTime
		{
			get
			{
				this.CheckDisposed("PostedTime::get");
				return base.GetValueOrDefault<ExDateTime>(InternalSchema.CreationTime, ExDateTime.MinValue);
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x0004272A File Offset: 0x0004092A
		// (set) Token: 0x06000939 RID: 2361 RVA: 0x00042747 File Offset: 0x00040947
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

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x00042774 File Offset: 0x00040974
		// (set) Token: 0x0600093B RID: 2363 RVA: 0x000427D5 File Offset: 0x000409D5
		public byte[] ConversationIndex
		{
			get
			{
				this.CheckDisposed("ConversationIndex::get");
				byte[] array = base.GetValueOrDefault<byte[]>(InternalSchema.ConversationIndex);
				if (array == null)
				{
					array = Microsoft.Exchange.Data.Storage.ConversationIndex.CreateNew().ToByteArray();
					if (base.PropertyBag is AcrPropertyBag && !((AcrPropertyBag)base.PropertyBag).IsReadOnly)
					{
						this[InternalSchema.ConversationIndex] = array;
					}
				}
				return array;
			}
			set
			{
				this.CheckDisposed("ConversationIndex::set");
				base.CheckSetNull("Post::ConversationIndex", "ConversationIndex", value);
				this[InternalSchema.ConversationIndex] = value;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x00042800 File Offset: 0x00040A00
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x00042869 File Offset: 0x00040A69
		public string ConversationTopic
		{
			get
			{
				this.CheckDisposed("ConversationTopic::get");
				string text = base.TryGetProperty(InternalSchema.ConversationTopic) as string;
				if (text == null)
				{
					text = base.GetValueOrDefault<string>(InternalSchema.NormalizedSubjectInternal, string.Empty);
					if (base.PropertyBag is AcrPropertyBag && !((AcrPropertyBag)base.PropertyBag).IsReadOnly)
					{
						this[InternalSchema.ConversationTopic] = text;
					}
				}
				return text;
			}
			set
			{
				this.CheckDisposed("ConversationTopic::set");
				base.CheckSetNull("Post::ConversationTopic", "ConversationTopic", value);
				this[InternalSchema.ConversationTopic] = value;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x00042893 File Offset: 0x00040A93
		// (set) Token: 0x0600093F RID: 2367 RVA: 0x000428B0 File Offset: 0x00040AB0
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
				base.CheckSetNull("Post::InReplyTo", "InReplyTo", value);
				this[InternalSchema.InReplyTo] = value;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x000428DA File Offset: 0x00040ADA
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x000428F7 File Offset: 0x00040AF7
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
				base.CheckSetNull("Post::InternetReferences", "InternetReferences", value);
				this[InternalSchema.InternetReferences] = value;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x00042921 File Offset: 0x00040B21
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return PostItemSchema.Instance;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x00042933 File Offset: 0x00040B33
		// (set) Token: 0x06000944 RID: 2372 RVA: 0x0004294B File Offset: 0x00040B4B
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

		// Token: 0x06000945 RID: 2373 RVA: 0x0004296C File Offset: 0x00040B6C
		public static PostItem Create(StoreSession session, StoreId destFolderId)
		{
			if (session == null)
			{
				ExTraceGlobals.StorageTracer.TraceError<string>(0L, "PostItem::Create. {0} should not be null.", "session");
				throw new ArgumentNullException(ServerStrings.ExNullParameter("session", 1));
			}
			if (destFolderId == null)
			{
				ExTraceGlobals.StorageTracer.TraceError<string>(0L, "PostItem::Create. {0} should not be null.", "destFolderId");
				throw new ArgumentNullException(ServerStrings.ExNullParameter("destFolderId", 1));
			}
			PostItem postItem = null;
			bool flag = false;
			PostItem result;
			try
			{
				postItem = ItemBuilder.CreateNewItem<PostItem>(session, destFolderId, ItemCreateInfo.PostInfo);
				postItem[InternalSchema.ItemClass] = "IPM.Post";
				postItem[InternalSchema.IconIndex] = IconIndex.PostItem;
				MailboxSession mailboxSession = session as MailboxSession;
				if (mailboxSession != null)
				{
					postItem.From = new Participant(mailboxSession.MailboxOwner.MailboxInfo.DisplayName, mailboxSession.MailboxOwnerLegacyDN, "EX");
				}
				postItem.IsRead = false;
				flag = true;
				result = postItem;
			}
			finally
			{
				if (!flag && postItem != null)
				{
					postItem.Dispose();
					postItem = null;
				}
			}
			return result;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00042A68 File Offset: 0x00040C68
		public new static PostItem Bind(StoreSession session, StoreId postId)
		{
			return PostItem.Bind(session, postId, null);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00042A72 File Offset: 0x00040C72
		public new static PostItem Bind(StoreSession session, StoreId postId, ICollection<PropertyDefinition> propsToReturn)
		{
			return ItemBuilder.ItemBind<PostItem>(session, postId, PostItemSchema.Instance, propsToReturn);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00042A81 File Offset: 0x00040C81
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<PostItem>(this);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00042A89 File Offset: 0x00040C89
		public void MarkAsRead(bool deferToSave)
		{
			this.CheckDisposed("MarkAsRead");
			if (!deferToSave)
			{
				this.SetReadFlagsInternal(SetReadFlags.None);
			}
			this.IsRead = true;
			if (!deferToSave)
			{
				base.ClearFlagsPropertyForSet(InternalSchema.IsRead);
			}
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00042AB5 File Offset: 0x00040CB5
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

		// Token: 0x0600094B RID: 2379 RVA: 0x00042AE1 File Offset: 0x00040CE1
		public PostItem ReplyToFolder(StoreId parentFolderId, ReplyForwardConfiguration configuration)
		{
			return this.ReplyToFolder(base.Session, parentFolderId, configuration);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00042AF4 File Offset: 0x00040CF4
		public PostItem ReplyToFolder(StoreSession session, StoreId parentFolderId, ReplyForwardConfiguration configuration)
		{
			this.CheckDisposed("ReplyToFolder");
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
			Util.ThrowOnNullArgument(configuration, "configuration");
			PostItem postItem = null;
			bool flag = false;
			PostItem result;
			try
			{
				postItem = PostItem.Create(session, parentFolderId);
				ReplyCreation replyCreation = new ReplyCreation(this, postItem, configuration, false, false, true);
				replyCreation.PopulateProperties();
				postItem[InternalSchema.IconIndex] = IconIndex.PostItem;
				flag = true;
				result = postItem;
			}
			finally
			{
				if (!flag && postItem != null)
				{
					postItem.Dispose();
					postItem = null;
				}
			}
			return result;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00042B84 File Offset: 0x00040D84
		public MessageItem CreateForward(MailboxSession session, StoreId parentFolderId, ReplyForwardConfiguration configuration)
		{
			this.CheckDisposed("CreateForward");
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
			Util.ThrowOnNullArgument(configuration, "configuration");
			return base.InternalCreateForward(session, parentFolderId, configuration);
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00042BBB File Offset: 0x00040DBB
		public MessageItem CreateReply(MailboxSession session, StoreId parentFolderId, ReplyForwardConfiguration configuration)
		{
			this.CheckDisposed("CreateReply");
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
			Util.ThrowOnNullArgument(configuration, "configuration");
			return base.InternalCreateReply(session, parentFolderId, configuration);
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00042BF2 File Offset: 0x00040DF2
		public MessageItem CreateReplyAll(MailboxSession session, StoreId parentFolderId, ReplyForwardConfiguration configuration)
		{
			this.CheckDisposed("CreateReplyAll");
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
			Util.ThrowOnNullArgument(configuration, "configuration");
			return base.InternalCreateReplyAll(session, parentFolderId, configuration);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00042C2C File Offset: 0x00040E2C
		internal static void CoreObjectUpdateDraftFlag(CoreItem coreItem)
		{
			bool? valueAsNullable = coreItem.PropertyBag.GetValueAsNullable<bool>(InternalSchema.IsDraft);
			if (valueAsNullable == null || valueAsNullable.Value)
			{
				coreItem.PropertyBag[InternalSchema.IsDraft] = false;
			}
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00042C74 File Offset: 0x00040E74
		internal static void CoreObjectUpdateConversationTopic(CoreItem coreItem)
		{
			ICorePropertyBag propertyBag = coreItem.PropertyBag;
			string valueOrDefault = propertyBag.GetValueOrDefault<string>(InternalSchema.NormalizedSubjectInternal);
			string valueOrDefault2 = propertyBag.GetValueOrDefault<string>(InternalSchema.ConversationTopic);
			byte[] valueOrDefault3 = propertyBag.GetValueOrDefault<byte[]>(InternalSchema.ConversationIndex);
			if (valueOrDefault != null)
			{
				if (valueOrDefault2 == null)
				{
					propertyBag[InternalSchema.ConversationTopic] = valueOrDefault;
				}
				if (valueOrDefault3 == null)
				{
					propertyBag[InternalSchema.ConversationIndex] = Microsoft.Exchange.Data.Storage.ConversationIndex.CreateNew().ToByteArray();
				}
			}
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00042CDA File Offset: 0x00040EDA
		protected override void InternalDispose(bool disposing)
		{
			base.InternalDispose(disposing);
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00042CE4 File Offset: 0x00040EE4
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
					string.Format("PostItem::SetReadFlagsInternal.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSetReadFlags, ex2, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("PostItem::SetReadFlagsInternal.", new object[0]),
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
	}
}
