using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200066C RID: 1644
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DefaultFolderContext
	{
		// Token: 0x060043F0 RID: 17392 RVA: 0x001205FF File Offset: 0x0011E7FF
		internal DefaultFolderContext(MailboxSession session, DefaultFolder[] defaultFolders)
		{
			this.mailboxSession = session;
			this.defaultFolders = defaultFolders;
			this.folderDataDictionary = null;
		}

		// Token: 0x170013D7 RID: 5079
		// (get) Token: 0x060043F1 RID: 17393 RVA: 0x00120623 File Offset: 0x0011E823
		internal MailboxSession Session
		{
			get
			{
				return this.mailboxSession;
			}
		}

		// Token: 0x170013D8 RID: 5080
		// (get) Token: 0x060043F2 RID: 17394 RVA: 0x0012062B File Offset: 0x0011E82B
		// (set) Token: 0x060043F3 RID: 17395 RVA: 0x00120633 File Offset: 0x0011E833
		internal Dictionary<string, DefaultFolderManager.FolderData> FolderDataDictionary
		{
			get
			{
				return this.folderDataDictionary;
			}
			set
			{
				this.folderDataDictionary = value;
			}
		}

		// Token: 0x170013D9 RID: 5081
		// (get) Token: 0x060043F4 RID: 17396 RVA: 0x0012063C File Offset: 0x0011E83C
		// (set) Token: 0x060043F5 RID: 17397 RVA: 0x00120644 File Offset: 0x0011E844
		internal bool DeferFolderIdInit
		{
			get
			{
				return this.deferFolderIdInit;
			}
			set
			{
				this.deferFolderIdInit = value;
			}
		}

		// Token: 0x170013DA RID: 5082
		// (get) Token: 0x060043F6 RID: 17398 RVA: 0x0012064D File Offset: 0x0011E84D
		// (set) Token: 0x060043F7 RID: 17399 RVA: 0x00120655 File Offset: 0x0011E855
		internal bool IgnoreForcedFolderInit { get; set; }

		// Token: 0x170013DB RID: 5083
		internal StoreObjectId this[DefaultFolderType defaultFolderType]
		{
			get
			{
				EnumValidator.AssertValid<DefaultFolderType>(defaultFolderType);
				DefaultFolder defaultFolder = this.defaultFolders[(int)defaultFolderType];
				if (defaultFolder != null)
				{
					StoreObjectId result;
					defaultFolder.TryGetFolderId(out result);
					return result;
				}
				return null;
			}
		}

		// Token: 0x060043F9 RID: 17401 RVA: 0x0012068C File Offset: 0x0011E88C
		internal PropertyBag GetMailboxPropertyBag()
		{
			MemoryPropertyBag memoryPropertyBag = this.mailboxPropertyBag;
			if (memoryPropertyBag == null)
			{
				memoryPropertyBag = DefaultFolderContext.SaveLocationContainer(this.Session.Mailbox, DefaultFolderInfo.MailboxProperties);
				if (this.isSessionOpenStage)
				{
					this.mailboxPropertyBag = memoryPropertyBag;
				}
			}
			return memoryPropertyBag;
		}

		// Token: 0x060043FA RID: 17402 RVA: 0x001206CC File Offset: 0x0011E8CC
		internal PropertyBag GetInboxOrConfigurationFolderPropertyBag()
		{
			MemoryPropertyBag memoryPropertyBag = this.inboxConfigurationPropertyBag;
			if (memoryPropertyBag == null)
			{
				using (Folder folder = this.OpenInboxOrConfigurationFolder())
				{
					memoryPropertyBag = DefaultFolderContext.SaveLocationContainer(folder, DefaultFolderInfo.InboxOrConfigurationFolderProperties);
				}
				if (this.isSessionOpenStage)
				{
					this.inboxConfigurationPropertyBag = memoryPropertyBag;
				}
			}
			return memoryPropertyBag;
		}

		// Token: 0x060043FB RID: 17403 RVA: 0x00120724 File Offset: 0x0011E924
		internal void DoneDefaultFolderInitialization()
		{
			if (!this.isSessionOpenStage)
			{
				throw new InvalidOperationException("Not expected to be called twice");
			}
			this.isSessionOpenStage = false;
			this.mailboxSession.SharedDataManager.DefaultFoldersInitialized = true;
			this.folderDataDictionary = null;
			this.mailboxPropertyBag = null;
			this.inboxConfigurationPropertyBag = null;
		}

		// Token: 0x060043FC RID: 17404 RVA: 0x00120774 File Offset: 0x0011E974
		private static MemoryPropertyBag SaveLocationContainer(IStorePropertyBag storeObject, StorePropertyDefinition[] properties)
		{
			MemoryPropertyBag memoryPropertyBag = new MemoryPropertyBag();
			memoryPropertyBag.PreLoadStoreProperty<StorePropertyDefinition>(properties, storeObject.GetProperties(properties));
			return memoryPropertyBag;
		}

		// Token: 0x060043FD RID: 17405 RVA: 0x00120798 File Offset: 0x0011E998
		private Folder OpenInboxOrConfigurationFolder()
		{
			StoreObjectId storeObjectId = this[DefaultFolderType.Configuration];
			StoreObjectId storeObjectId2 = this[DefaultFolderType.Inbox];
			if (storeObjectId == null)
			{
				throw new InvalidOperationException("Wrong order of default folders' initialization - No configuration folder information.");
			}
			if (storeObjectId2 == null)
			{
				ExTraceGlobals.DefaultFoldersTracer.TraceDebug<string>(-1L, "DefaultFolderContext::OpenInboxOrConfigurationFolder.  Unable to find StoreObjectId for inboxId. Wrong order of default folders' initialization - Inbox for {0} should be loaded before other non-free ones.", this.Session.DisplayName);
			}
			else
			{
				try
				{
					return Folder.Bind(this.Session, storeObjectId2, DefaultFolderInfo.InboxOrConfigurationFolderProperties);
				}
				catch (ObjectNotFoundException)
				{
					ExTraceGlobals.DefaultFoldersTracer.TraceDebug<string>(-1L, "DefaultFolderContext::OpenInboxOrConfigurationFolder. We cannot bind to the Inbox of Mailbox = {0}.", this.Session.DisplayName);
				}
			}
			return Folder.Bind(this.Session, storeObjectId, DefaultFolderInfo.InboxOrConfigurationFolderProperties);
		}

		// Token: 0x040024EB RID: 9451
		private readonly MailboxSession mailboxSession;

		// Token: 0x040024EC RID: 9452
		private readonly DefaultFolder[] defaultFolders;

		// Token: 0x040024ED RID: 9453
		private Dictionary<string, DefaultFolderManager.FolderData> folderDataDictionary;

		// Token: 0x040024EE RID: 9454
		private MemoryPropertyBag inboxConfigurationPropertyBag;

		// Token: 0x040024EF RID: 9455
		private MemoryPropertyBag mailboxPropertyBag;

		// Token: 0x040024F0 RID: 9456
		private bool isSessionOpenStage = true;

		// Token: 0x040024F1 RID: 9457
		private bool deferFolderIdInit;
	}
}
