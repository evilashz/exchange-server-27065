using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200066E RID: 1646
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class DefaultFolderInfo
	{
		// Token: 0x170013DC RID: 5084
		// (get) Token: 0x060043FE RID: 17406 RVA: 0x0012083C File Offset: 0x0011EA3C
		internal static DefaultFolderInfo[] Instance
		{
			get
			{
				return DefaultFolderInfo.defaultFolderInfos;
			}
		}

		// Token: 0x170013DD RID: 5085
		// (get) Token: 0x060043FF RID: 17407 RVA: 0x00120843 File Offset: 0x0011EA43
		internal static StorePropertyDefinition[] MailboxProperties
		{
			get
			{
				return DefaultFolderInfo.mailboxProperties;
			}
		}

		// Token: 0x170013DE RID: 5086
		// (get) Token: 0x06004400 RID: 17408 RVA: 0x0012084A File Offset: 0x0011EA4A
		internal static StorePropertyDefinition[] InboxOrConfigurationFolderProperties
		{
			get
			{
				return DefaultFolderInfo.inboxOrConfigurationFolderProperties;
			}
		}

		// Token: 0x170013DF RID: 5087
		// (get) Token: 0x06004401 RID: 17409 RVA: 0x00120851 File Offset: 0x0011EA51
		internal static int DefaultFolderTypeCount
		{
			get
			{
				return DefaultFolderInfo.defaultFolderTypeCount;
			}
		}

		// Token: 0x170013E0 RID: 5088
		// (get) Token: 0x06004402 RID: 17410 RVA: 0x00120858 File Offset: 0x0011EA58
		internal StoreObjectType StoreObjectType
		{
			get
			{
				return this.storeObjectType;
			}
		}

		// Token: 0x170013E1 RID: 5089
		// (get) Token: 0x06004403 RID: 17411 RVA: 0x00120860 File Offset: 0x0011EA60
		internal EntryIdStrategy EntryIdStrategy
		{
			get
			{
				return this.entryIdStrategy;
			}
		}

		// Token: 0x170013E2 RID: 5090
		// (get) Token: 0x06004404 RID: 17412 RVA: 0x00120868 File Offset: 0x0011EA68
		internal DefaultFolderCreator FolderCreator
		{
			get
			{
				return this.folderCreator;
			}
		}

		// Token: 0x170013E3 RID: 5091
		// (get) Token: 0x06004405 RID: 17413 RVA: 0x00120870 File Offset: 0x0011EA70
		internal DefaultFolderBehavior Behavior
		{
			get
			{
				return this.behavior;
			}
		}

		// Token: 0x170013E4 RID: 5092
		// (get) Token: 0x06004406 RID: 17414 RVA: 0x00120878 File Offset: 0x0011EA78
		internal DefaultFolderValidator FolderValidationStrategy
		{
			get
			{
				return this.folderValidationStrategy;
			}
		}

		// Token: 0x170013E5 RID: 5093
		// (get) Token: 0x06004407 RID: 17415 RVA: 0x00120880 File Offset: 0x0011EA80
		internal DefaultFolderLocalization Localizable
		{
			get
			{
				return this.localizable;
			}
		}

		// Token: 0x170013E6 RID: 5094
		// (get) Token: 0x06004408 RID: 17416 RVA: 0x00120888 File Offset: 0x0011EA88
		internal LocalizedString LocalizableDisplayName
		{
			get
			{
				return this.localizableDisplayName;
			}
		}

		// Token: 0x170013E7 RID: 5095
		// (get) Token: 0x06004409 RID: 17417 RVA: 0x00120890 File Offset: 0x0011EA90
		internal DefaultFolderType DefaultFolderType
		{
			get
			{
				return this.defaultFolderType;
			}
		}

		// Token: 0x170013E8 RID: 5096
		// (get) Token: 0x0600440A RID: 17418 RVA: 0x00120898 File Offset: 0x0011EA98
		internal CorruptDataRecoveryStrategy CorruptDataRecoveryStrategy
		{
			get
			{
				return this.corruptDataRecoveryStrategy;
			}
		}

		// Token: 0x0600440B RID: 17419 RVA: 0x001208A0 File Offset: 0x0011EAA0
		private DefaultFolderInfo(DefaultFolderType defaultFolderType, StoreObjectType storeObjectType, LocalizedString localizableDisplayName, EntryIdStrategy entryIdStrategy, DefaultFolderCreator folderCreator, DefaultFolderValidator folderValidationStrategy, DefaultFolderLocalization localizable, DefaultFolderBehavior behavior, CorruptDataRecoveryStrategy recovery)
		{
			this.defaultFolderType = defaultFolderType;
			this.storeObjectType = storeObjectType;
			this.entryIdStrategy = entryIdStrategy;
			this.folderCreator = folderCreator;
			this.folderValidationStrategy = folderValidationStrategy;
			this.localizable = localizable;
			this.localizableDisplayName = localizableDisplayName;
			this.behavior = behavior;
			this.corruptDataRecoveryStrategy = recovery;
		}

		// Token: 0x0600440C RID: 17420 RVA: 0x001208F8 File Offset: 0x0011EAF8
		public override string ToString()
		{
			return this.DefaultFolderType.ToString();
		}

		// Token: 0x0600440D RID: 17421 RVA: 0x0012090C File Offset: 0x0011EB0C
		private static DefaultFolderInfo[] CreateDefaultFolderInfos()
		{
			DefaultFolderInfo.defaultFolderInfos = new DefaultFolderInfo[DefaultFolderInfo.DefaultFolderTypeCount];
			DefaultFolderInfo.defaultFolderInfos[0] = new DefaultFolderInfo(DefaultFolderType.None, StoreObjectType.Folder, LocalizedString.Empty, EntryIdStrategy.NoEntryId, DefaultFolderCreator.NoCreator, DefaultFolderValidator.NullValidator, DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.None, CorruptDataRecoveryStrategy.DoNothing);
			DefaultFolderInfo.defaultFolderInfos[34] = new DefaultFolderInfo(DefaultFolderType.Root, StoreObjectType.Folder, ClientStrings.Root, new FreeEntryIdStrategy(new FreeEntryIdStrategy.GetFreeIdDelegate(FreeEntryIdStrategy.GetRootIdDelegate)), DefaultFolderCreator.NoCreator, DefaultFolderValidator.NullValidator, DefaultFolderLocalization.CanLocalize, DefaultFolderBehavior.None, CorruptDataRecoveryStrategy.Throw);
			DefaultFolderInfo.defaultFolderInfos[35] = new DefaultFolderInfo(DefaultFolderType.Configuration, StoreObjectType.Folder, LocalizedString.Empty, new FreeEntryIdStrategy(new FreeEntryIdStrategy.GetFreeIdDelegate(FreeEntryIdStrategy.GetConfigurationIdDelegate)), DefaultFolderCreator.NoCreator, DefaultFolderValidator.NullValidator, DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.None, CorruptDataRecoveryStrategy.Throw);
			DefaultFolderInfo.defaultFolderInfos[5] = new DefaultFolderInfo(DefaultFolderType.Inbox, StoreObjectType.Folder, ClientStrings.Inbox, new FreeEntryIdStrategy(new FreeEntryIdStrategy.GetFreeIdDelegate(FreeEntryIdStrategy.GetInboxIdDelegate)), DefaultFolderCreator.NoCreator, DefaultFolderValidator.NullValidator, DefaultFolderLocalization.CanLocalize, DefaultFolderBehavior.CanHideFolderFromOutlook, CorruptDataRecoveryStrategy.Throw);
			DefaultFolderInfo.defaultFolderInfos[9] = new DefaultFolderInfo(DefaultFolderType.Outbox, StoreObjectType.Folder, ClientStrings.Outbox, new LocationEntryIdStrategy(MailboxSchema.OutboxEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetMailboxPropertyBag)), DefaultFolderCreator.NoCreator, DefaultFolderValidator.NullValidator, DefaultFolderLocalization.CanLocalize, DefaultFolderBehavior.CanHideFolderFromOutlook, CorruptDataRecoveryStrategy.Throw);
			DefaultFolderInfo.defaultFolderInfos[10] = new DefaultFolderInfo(DefaultFolderType.SentItems, StoreObjectType.Folder, ClientStrings.SentItems, new LocationEntryIdStrategy(MailboxSchema.SentItemsEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetMailboxPropertyBag)), DefaultFolderCreator.NoCreator, DefaultFolderValidator.NullValidator, DefaultFolderLocalization.CanLocalize, DefaultFolderBehavior.CanHideFolderFromOutlook, CorruptDataRecoveryStrategy.Throw);
			DefaultFolderInfo.defaultFolderInfos[3] = new DefaultFolderInfo(DefaultFolderType.DeletedItems, StoreObjectType.Folder, ClientStrings.DeletedItems, new LocationEntryIdStrategy(MailboxSchema.DeletedItemsEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetMailboxPropertyBag)), DefaultFolderCreator.NoCreator, DefaultFolderValidator.NullValidator, DefaultFolderLocalization.CanLocalize, DefaultFolderBehavior.None, CorruptDataRecoveryStrategy.Throw);
			DefaultFolderInfo.defaultFolderInfos[17] = new DefaultFolderInfo(DefaultFolderType.RssSubscription, StoreObjectType.Folder, ClientStrings.RssSubscriptions, new Ex12RenEntryIdStrategy(InternalSchema.AdditionalRenEntryIdsEx, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag), Ex12RenEntryIdStrategy.PersistenceId.RssSubscriptions), DefaultFolderCreator.NoCreator, new DefaultFolderValidator(new IValidator[]
			{
				new CompositeValidator(new IValidator[]
				{
					new MatchContainerClass("IPF.Note.OutlookHomepage"),
					new MatchMapiFolderType(FolderType.Generic)
				})
			}), DefaultFolderLocalization.CanLocalize, DefaultFolderBehavior.CanHideFolderFromOutlook, CorruptDataRecoveryStrategy.Throw);
			DefaultFolderInfo.defaultFolderInfos[19] = new DefaultFolderInfo(DefaultFolderType.ToDoSearch, StoreObjectType.SearchFolder, new LocalizedString("To-Do Search"), new Ex12RenEntryIdStrategy(InternalSchema.AdditionalRenEntryIdsEx, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag), Ex12RenEntryIdStrategy.PersistenceId.ToDoSearch), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.SearchFolder, true), new ToDoSearchValidation(), DefaultFolderLocalization.CanNotLocalize, (DefaultFolderBehavior)35, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[13] = new DefaultFolderInfo(DefaultFolderType.Conflicts, StoreObjectType.Folder, ClientStrings.Conflicts, new Ex12MultivalueEntryIdStrategy(InternalSchema.AdditionalRenEntryIds, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag), 0), DefaultFolderCreator.NoCreator, DefaultFolderValidator.MessageFolderGenericTypeValidator, DefaultFolderLocalization.CanLocalize, DefaultFolderBehavior.None, CorruptDataRecoveryStrategy.Throw);
			DefaultFolderInfo.defaultFolderInfos[55] = new DefaultFolderInfo(DefaultFolderType.DocumentSyncIssues, StoreObjectType.Folder, ClientStrings.DocumentSyncIssues, new Ex12MultivalueEntryIdStrategyForDocumentSyncIssue(InternalSchema.AdditionalRenEntryIds, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Root, StoreObjectType.Folder, true), DefaultFolderValidator.MessageFolderGenericTypeValidator, DefaultFolderLocalization.CanLocalize, DefaultFolderBehavior.CanCreate | DefaultFolderBehavior.CanNotRename | DefaultFolderBehavior.AlwaysDeferInitialization, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[14] = new DefaultFolderInfo(DefaultFolderType.SyncIssues, StoreObjectType.Folder, ClientStrings.SyncIssues, new Ex12MultivalueEntryIdStrategyForSyncIssue(InternalSchema.AdditionalRenEntryIds, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), DefaultFolderCreator.NoCreator, DefaultFolderValidator.MessageFolderGenericTypeValidator, DefaultFolderLocalization.CanLocalize, DefaultFolderBehavior.CanHideFolderFromOutlook, CorruptDataRecoveryStrategy.Throw);
			DefaultFolderInfo.defaultFolderInfos[15] = new DefaultFolderInfo(DefaultFolderType.LocalFailures, StoreObjectType.Folder, ClientStrings.LocalFailures, new Ex12MultivalueEntryIdStrategy(InternalSchema.AdditionalRenEntryIds, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag), 2), DefaultFolderCreator.NoCreator, DefaultFolderValidator.MessageFolderGenericTypeValidator, DefaultFolderLocalization.CanLocalize, DefaultFolderBehavior.None, CorruptDataRecoveryStrategy.Throw);
			DefaultFolderInfo.defaultFolderInfos[16] = new DefaultFolderInfo(DefaultFolderType.ServerFailures, StoreObjectType.Folder, ClientStrings.ServerFailures, new Ex12MultivalueEntryIdStrategy(InternalSchema.AdditionalRenEntryIds, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag), 3), DefaultFolderCreator.NoCreator, DefaultFolderValidator.MessageFolderGenericTypeValidator, DefaultFolderLocalization.CanLocalize, DefaultFolderBehavior.None, CorruptDataRecoveryStrategy.Throw);
			DefaultFolderInfo.defaultFolderInfos[6] = new DefaultFolderInfo(DefaultFolderType.JunkEmail, StoreObjectType.Folder, ClientStrings.JunkEmail, new Ex12MultivalueEntryIdStrategy(InternalSchema.AdditionalRenEntryIds, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag), 4), new DefaultFolderCreator(DefaultFolderType.Root, StoreObjectType.Folder, true), DefaultFolderValidator.MessageFolderGenericTypeValidator, DefaultFolderLocalization.CanLocalize, (DefaultFolderBehavior)195, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[1] = new DefaultFolderInfo(DefaultFolderType.Calendar, StoreObjectType.CalendarFolder, ClientStrings.Calendar, new LocationEntryIdStrategy(InternalSchema.CalendarFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Root, StoreObjectType.Folder, true), new DefaultFolderValidator(new IValidator[]
			{
				new CompositeValidator(new IValidator[]
				{
					new MatchMapiFolderType(FolderType.Generic),
					new MatchContainerClass("IPF.Appointment")
				})
			}), DefaultFolderLocalization.CanLocalize, (DefaultFolderBehavior)67, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[2] = new DefaultFolderInfo(DefaultFolderType.Contacts, StoreObjectType.ContactsFolder, ClientStrings.Contacts, new LocationEntryIdStrategy(InternalSchema.ContactsFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Root, StoreObjectType.Folder, true), new DefaultFolderValidator(new IValidator[]
			{
				new CompositeValidator(new IValidator[]
				{
					new MatchMapiFolderType(FolderType.Generic),
					new MatchContainerClass("IPF.Contact")
				})
			}), DefaultFolderLocalization.CanLocalize, (DefaultFolderBehavior)67, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[4] = new DefaultFolderInfo(DefaultFolderType.Drafts, StoreObjectType.Folder, ClientStrings.Drafts, new LocationEntryIdStrategy(InternalSchema.DraftsFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Root, StoreObjectType.Folder, true), DefaultFolderValidator.MessageFolderGenericTypeValidator, DefaultFolderLocalization.CanLocalize, (DefaultFolderBehavior)67, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[11] = new DefaultFolderInfo(DefaultFolderType.Tasks, StoreObjectType.TasksFolder, ClientStrings.Tasks, new LocationEntryIdStrategy(InternalSchema.TasksFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Root, StoreObjectType.Folder, true), new DefaultFolderValidator(new IValidator[]
			{
				new CompositeValidator(new IValidator[]
				{
					new MatchMapiFolderType(FolderType.Generic),
					new MatchContainerClass("IPF.Task")
				})
			}), DefaultFolderLocalization.CanLocalize, (DefaultFolderBehavior)67, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[7] = new DefaultFolderInfo(DefaultFolderType.Journal, StoreObjectType.JournalFolder, ClientStrings.Journal, new LocationEntryIdStrategy(InternalSchema.JournalFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), DefaultFolderCreator.NoCreator, new DefaultFolderValidator(new IValidator[]
			{
				new CompositeValidator(new IValidator[]
				{
					new MatchMapiFolderType(FolderType.Generic),
					new MatchContainerClass("IPF.Journal")
				})
			}), DefaultFolderLocalization.CanLocalize, DefaultFolderBehavior.CanHideFolderFromOutlook, CorruptDataRecoveryStrategy.DoNothing);
			DefaultFolderInfo.defaultFolderInfos[8] = new DefaultFolderInfo(DefaultFolderType.Notes, StoreObjectType.NotesFolder, ClientStrings.Notes, new LocationEntryIdStrategy(InternalSchema.NotesFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), DefaultFolderCreator.NoCreator, new DefaultFolderValidator(new IValidator[]
			{
				new CompositeValidator(new IValidator[]
				{
					new MatchMapiFolderType(FolderType.Generic),
					new MatchContainerClass("IPF.StickyNote")
				})
			}), DefaultFolderLocalization.CanLocalize, DefaultFolderBehavior.CanHideFolderFromOutlook, CorruptDataRecoveryStrategy.DoNothing);
			DefaultFolderInfo.defaultFolderInfos[21] = new DefaultFolderInfo(DefaultFolderType.CommunicatorHistory, StoreObjectType.Folder, ClientStrings.CommunicatorHistoryFolderName, new LocationEntryIdStrategy(InternalSchema.CommunicatorHistoryFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Root, StoreObjectType.Folder, true), DefaultFolderValidator.FolderGenericTypeValidator, DefaultFolderLocalization.CanLocalize, DefaultFolderBehavior.CanCreate | DefaultFolderBehavior.CanHideFolderFromOutlook, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[52] = new DefaultFolderInfo(DefaultFolderType.LegacyArchiveJournals, StoreObjectType.Folder, ClientStrings.LegacyArchiveJournals, new LocationEntryIdStrategy(InternalSchema.LegacyArchiveJournalsFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Root, StoreObjectType.Folder, true), DefaultFolderValidator.FolderGenericTypeValidator, DefaultFolderLocalization.CanLocalize, DefaultFolderBehavior.CanCreate | DefaultFolderBehavior.AlwaysDeferInitialization, CorruptDataRecoveryStrategy.DoNothing);
			DefaultFolderInfo.defaultFolderInfos[20] = new DefaultFolderInfo(DefaultFolderType.ElcRoot, StoreObjectType.Folder, ClientStrings.ElcRoot, new LocationEntryIdStrategy(InternalSchema.ElcRootFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Root, StoreObjectType.Folder, true), new DefaultFolderValidator(new IValidator[]
			{
				new CompositeValidator(new IValidator[]
				{
					new MatchMapiFolderType(FolderType.Generic),
					new MatchAdminFolderFlags(ELCFolderFlags.Provisioned | ELCFolderFlags.Protected | ELCFolderFlags.ELCRoot)
				})
			}), DefaultFolderLocalization.CanLocalize, DefaultFolderBehavior.CanCreate | DefaultFolderBehavior.CanHideFolderFromOutlook, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[32] = new DefaultFolderInfo(DefaultFolderType.RecoverableItemsRoot, StoreObjectType.Folder, new LocalizedString("Recoverable Items"), new LocationEntryIdStrategy(InternalSchema.RecoverableItemsRootFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.Folder, true), new DefaultFolderValidator(new IValidator[]
			{
				new CompositeValidator(new IValidator[]
				{
					new MatchMapiFolderType(FolderType.Generic),
					new MatchAdminFolderFlags(ELCFolderFlags.DumpsterFolder)
				})
			}), DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.CanCreate | DefaultFolderBehavior.CanNotRename | DefaultFolderBehavior.AlwaysDeferInitialization | DefaultFolderBehavior.RefreshIfMissing, CorruptDataRecoveryStrategy.LegalHold);
			DefaultFolderInfo.defaultFolderInfos[40] = new DefaultFolderInfo(DefaultFolderType.RecoverableItemsDeletions, StoreObjectType.Folder, new LocalizedString("Deletions"), new LocationEntryIdStrategy(InternalSchema.RecoverableItemsDeletionsFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.RecoverableItemsRoot, StoreObjectType.Folder, true), new DefaultSubFolderValidator(DefaultFolderType.RecoverableItemsRoot, new IValidator[]
			{
				new CompositeValidator(new IValidator[]
				{
					new MatchMapiFolderType(FolderType.Generic),
					new MatchAdminFolderFlags(ELCFolderFlags.DumpsterFolder)
				})
			}), DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.CanCreate | DefaultFolderBehavior.CanNotRename | DefaultFolderBehavior.AlwaysDeferInitialization | DefaultFolderBehavior.RefreshIfMissing, CorruptDataRecoveryStrategy.LegalHold);
			DefaultFolderInfo.defaultFolderInfos[33] = new DefaultFolderInfo(DefaultFolderType.RecoverableItemsVersions, StoreObjectType.Folder, new LocalizedString("Versions"), new LocationEntryIdStrategy(InternalSchema.RecoverableItemsVersionsFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.RecoverableItemsRoot, StoreObjectType.Folder, true), new DefaultSubFolderValidator(DefaultFolderType.RecoverableItemsRoot, new IValidator[]
			{
				new CompositeValidator(new IValidator[]
				{
					new MatchMapiFolderType(FolderType.Generic),
					new MatchAdminFolderFlags(ELCFolderFlags.DumpsterFolder)
				})
			}), DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.CanCreate | DefaultFolderBehavior.CanNotRename | DefaultFolderBehavior.AlwaysDeferInitialization | DefaultFolderBehavior.RefreshIfMissing, CorruptDataRecoveryStrategy.LegalHold);
			DefaultFolderInfo.defaultFolderInfos[41] = new DefaultFolderInfo(DefaultFolderType.RecoverableItemsPurges, StoreObjectType.Folder, new LocalizedString("Purges"), new LocationEntryIdStrategy(InternalSchema.RecoverableItemsPurgesFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.RecoverableItemsRoot, StoreObjectType.Folder, true), new DefaultSubFolderValidator(DefaultFolderType.RecoverableItemsRoot, new IValidator[]
			{
				new CompositeValidator(new IValidator[]
				{
					new MatchMapiFolderType(FolderType.Generic),
					new MatchAdminFolderFlags(ELCFolderFlags.DumpsterFolder)
				})
			}), DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.CanCreate | DefaultFolderBehavior.CanNotRename | DefaultFolderBehavior.AlwaysDeferInitialization | DefaultFolderBehavior.RefreshIfMissing, CorruptDataRecoveryStrategy.LegalHold);
			DefaultFolderInfo.defaultFolderInfos[46] = new DefaultFolderInfo(DefaultFolderType.RecoverableItemsDiscoveryHolds, StoreObjectType.Folder, new LocalizedString("DiscoveryHolds"), new LocationEntryIdStrategy(InternalSchema.RecoverableItemsDiscoveryHoldsFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.RecoverableItemsRoot, StoreObjectType.Folder, true), new DefaultSubFolderValidator(DefaultFolderType.RecoverableItemsRoot, new IValidator[]
			{
				new CompositeValidator(new IValidator[]
				{
					new MatchMapiFolderType(FolderType.Generic),
					new MatchAdminFolderFlags(ELCFolderFlags.DumpsterFolder)
				})
			}), DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.CanCreate | DefaultFolderBehavior.CanNotRename | DefaultFolderBehavior.AlwaysDeferInitialization | DefaultFolderBehavior.RefreshIfMissing, CorruptDataRecoveryStrategy.LegalHold);
			DefaultFolderInfo.defaultFolderInfos[59] = new DefaultFolderInfo(DefaultFolderType.RecoverableItemsMigratedMessages, StoreObjectType.Folder, new LocalizedString("MigratedMessages"), new LocationEntryIdStrategy(InternalSchema.RecoverableItemsMigratedMessagesFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.RecoverableItemsRoot, StoreObjectType.Folder, true), new DefaultSubFolderValidator(DefaultFolderType.RecoverableItemsRoot, new IValidator[]
			{
				new CompositeValidator(new IValidator[]
				{
					new MatchMapiFolderType(FolderType.Generic),
					new MatchAdminFolderFlags(ELCFolderFlags.DumpsterFolder)
				})
			}), DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.CanCreate | DefaultFolderBehavior.CanNotRename | DefaultFolderBehavior.AlwaysDeferInitialization | DefaultFolderBehavior.RefreshIfMissing, CorruptDataRecoveryStrategy.LegalHold);
			DefaultFolderInfo.defaultFolderInfos[57] = new DefaultFolderInfo(DefaultFolderType.CalendarLogging, StoreObjectType.Folder, new LocalizedString("Calendar Logging"), new LocationEntryIdStrategy(InternalSchema.CalendarLoggingFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.RecoverableItemsRoot, StoreObjectType.Folder, true), new DefaultSubFolderValidator(DefaultFolderType.RecoverableItemsRoot, new IValidator[]
			{
				new CompositeValidator(new IValidator[]
				{
					new MatchMapiFolderType(FolderType.Generic),
					new MatchAdminFolderFlags(ELCFolderFlags.DumpsterFolder)
				})
			}), DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.CanCreate | DefaultFolderBehavior.CanNotRename | DefaultFolderBehavior.AlwaysDeferInitialization | DefaultFolderBehavior.RefreshIfMissing, CorruptDataRecoveryStrategy.LegalHold);
			DefaultFolderInfo.defaultFolderInfos[22] = new DefaultFolderInfo(DefaultFolderType.SyncRoot, StoreObjectType.Folder, new LocalizedString("ExchangeSyncData"), new LocationEntryIdStrategy(InternalSchema.SyncRootFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.Folder, true), new DefaultFolderValidator(new IValidator[]
			{
				new MatchMapiFolderType(FolderType.Generic)
			}), DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.CanCreate | DefaultFolderBehavior.CanNotRename, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[23] = new DefaultFolderInfo(DefaultFolderType.UMVoicemail, StoreObjectType.OutlookSearchFolder, ClientStrings.UMVoiceMailFolderName, new LocationEntryIdStrategy(InternalSchema.UMVoicemailFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.SearchFolders, StoreObjectType.OutlookSearchFolder, true), new UMVoiceMailValidation(), DefaultFolderLocalization.CanLocalize, DefaultFolderBehavior.CanCreate | DefaultFolderBehavior.AlwaysDeferInitialization, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[30] = new DefaultFolderInfo(DefaultFolderType.UMFax, StoreObjectType.OutlookSearchFolder, ClientStrings.UMFaxFolderName, new LocationEntryIdStrategy(InternalSchema.UMFaxFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.SearchFolders, StoreObjectType.OutlookSearchFolder, true), new UMFaxValidation(), DefaultFolderLocalization.CanLocalize, DefaultFolderBehavior.CanCreate | DefaultFolderBehavior.AlwaysDeferInitialization, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[12] = new DefaultFolderInfo(DefaultFolderType.Reminders, StoreObjectType.SearchFolder, ClientStrings.RemindersSearchFolderName(string.Empty), new LocationEntryIdStrategy(InternalSchema.RemindersSearchFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.SearchFolder, true), new RemindersSearchFolderValidation(), DefaultFolderLocalization.CanNotLocalize, (DefaultFolderBehavior)39, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[24] = new DefaultFolderInfo(DefaultFolderType.AllItems, StoreObjectType.SearchFolder, new LocalizedString("AllItems"), new LocationEntryIdStrategy(InternalSchema.AllItemsFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.SearchFolder, true), new AllItemsFolderValidation(), DefaultFolderLocalization.CanNotLocalize, (DefaultFolderBehavior)39, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[37] = new DefaultFolderInfo(DefaultFolderType.FreeBusyData, StoreObjectType.Folder, new LocalizedString("Freebusy Data"), new Ex12MultivalueEntryIdNoMoveStampStrategy(InternalSchema.FreeBusyEntryIds, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag), 3), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.Folder, true), new DefaultFolderValidator(new IValidator[]
			{
				new MatchMapiFolderType(FolderType.Generic)
			}), DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.CreateIfMissing, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[18] = new DefaultFolderInfo(DefaultFolderType.CommonViews, StoreObjectType.Folder, ClientStrings.CommonViews, new LocationEntryIdStrategy(InternalSchema.CommonViewsEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetMailboxPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.Folder, true), DefaultFolderValidator.NullValidator, DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.None, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[36] = new DefaultFolderInfo(DefaultFolderType.SearchFolders, StoreObjectType.Folder, ClientStrings.SearchFolders, new LocationEntryIdStrategy(InternalSchema.FinderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetMailboxPropertyBag)), DefaultFolderCreator.NoCreator, DefaultFolderValidator.NullValidator, DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.None, CorruptDataRecoveryStrategy.Throw);
			DefaultFolderInfo.defaultFolderInfos[25] = new DefaultFolderInfo(DefaultFolderType.DeferredActionFolder, StoreObjectType.Folder, new LocalizedString("Deferred Actions"), new LocationEntryIdStrategy(MailboxSchema.DeferredActionFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetMailboxPropertyBag)), DefaultFolderCreator.NoCreator, DefaultFolderValidator.NullValidator, DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.None, CorruptDataRecoveryStrategy.Throw);
			DefaultFolderInfo.defaultFolderInfos[26] = new DefaultFolderInfo(DefaultFolderType.LegacySpoolerQueue, StoreObjectType.SearchFolder, new LocalizedString("Spooler Queue"), new FreeEntryIdStrategy(new FreeEntryIdStrategy.GetFreeIdDelegate(FreeEntryIdStrategy.GetSpoolerQueueIdDelegate)), DefaultFolderCreator.NoCreator, DefaultFolderValidator.NullValidator, DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.AlwaysDeferInitialization, CorruptDataRecoveryStrategy.DoNothing);
			DefaultFolderInfo.defaultFolderInfos[27] = new DefaultFolderInfo(DefaultFolderType.LegacySchedule, StoreObjectType.Folder, new LocalizedString("Schedule"), new LocationEntryIdStrategy(MailboxSchema.LegacyScheduleFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetMailboxPropertyBag)), DefaultFolderCreator.NoCreator, DefaultFolderValidator.NullValidator, DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.None, CorruptDataRecoveryStrategy.DoNothing);
			DefaultFolderInfo.defaultFolderInfos[29] = new DefaultFolderInfo(DefaultFolderType.LegacyShortcuts, StoreObjectType.Folder, new LocalizedString("Shortcuts"), new LocationEntryIdStrategy(MailboxSchema.LegacyShortcutsFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetMailboxPropertyBag)), DefaultFolderCreator.NoCreator, DefaultFolderValidator.NullValidator, DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.None, CorruptDataRecoveryStrategy.DoNothing);
			DefaultFolderInfo.defaultFolderInfos[28] = new DefaultFolderInfo(DefaultFolderType.LegacyViews, StoreObjectType.Folder, new LocalizedString("Views"), new LocationEntryIdStrategy(MailboxSchema.LegacyViewsFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetMailboxPropertyBag)), DefaultFolderCreator.NoCreator, DefaultFolderValidator.NullValidator, DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.None, CorruptDataRecoveryStrategy.DoNothing);
			DefaultFolderInfo.defaultFolderInfos[31] = new DefaultFolderInfo(DefaultFolderType.ConversationActions, StoreObjectType.Folder, new LocalizedString("Conversation Action Settings"), new Ex12RenEntryIdStrategy(InternalSchema.AdditionalRenEntryIdsEx, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag), Ex12RenEntryIdStrategy.PersistenceId.ConversationActions), new DefaultFolderCreator(DefaultFolderType.Root, StoreObjectType.Folder, true), new ConversationActionsValidator(), DefaultFolderLocalization.CanNotLocalize, (DefaultFolderBehavior)195, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[38] = new DefaultFolderInfo(DefaultFolderType.Sharing, StoreObjectType.Folder, new LocalizedString("Sharing"), new LocationEntryIdStrategy(InternalSchema.SharingFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.Folder, true), DefaultFolderValidator.MessageFolderGenericTypeValidator, DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.CreateIfMissing, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[45] = new DefaultFolderInfo(DefaultFolderType.Location, StoreObjectType.Folder, new LocalizedString("Location"), new LocationEntryIdStrategy(InternalSchema.LocationFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.Folder, true), DefaultFolderValidator.FolderGenericTypeValidator, DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.CreateIfMissing, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[39] = new DefaultFolderInfo(DefaultFolderType.System, StoreObjectType.Folder, new LocalizedString("System"), new LocationEntryIdStrategy(InternalSchema.SystemFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.Folder, true), new DefaultSubFolderValidator(DefaultFolderType.Configuration, new IValidator[]
			{
				new MatchIsSystemFolder()
			}), DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.CreateIfMissing, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[42] = new DefaultFolderInfo(DefaultFolderType.CalendarVersionStore, StoreObjectType.SearchFolder, new LocalizedString("Calendar Version Store"), new LocationEntryIdStrategy(InternalSchema.CalendarVersionStoreFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.SearchFolder, true), new CalendarVersionStoreValidation(), DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.CanCreate | DefaultFolderBehavior.CanNotRename | DefaultFolderBehavior.AlwaysDeferInitialization | DefaultFolderBehavior.RefreshIfMissing, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[43] = new DefaultFolderInfo(DefaultFolderType.AdminAuditLogs, StoreObjectType.Folder, new LocalizedString("AdminAuditLogs"), new LocationEntryIdStrategy(InternalSchema.AdminAuditLogsFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.RecoverableItemsRoot, StoreObjectType.Folder, true), new DefaultSubFolderValidator(DefaultFolderType.RecoverableItemsRoot, new IValidator[]
			{
				new CompositeValidator(new IValidator[]
				{
					new MatchMapiFolderType(FolderType.Generic),
					new MatchAdminFolderFlags(ELCFolderFlags.DumpsterFolder)
				})
			}), DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.CanCreate | DefaultFolderBehavior.CanNotRename | DefaultFolderBehavior.AlwaysDeferInitialization, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[44] = new DefaultFolderInfo(DefaultFolderType.Audits, StoreObjectType.Folder, new LocalizedString("Audits"), new LocationEntryIdStrategy(InternalSchema.AuditsFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.RecoverableItemsRoot, StoreObjectType.Folder, true), new DefaultSubFolderValidator(DefaultFolderType.RecoverableItemsRoot, new IValidator[]
			{
				new CompositeValidator(new IValidator[]
				{
					new MatchMapiFolderType(FolderType.Generic),
					new MatchAdminFolderFlags(ELCFolderFlags.DumpsterFolder)
				})
			}), DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.CanCreate | DefaultFolderBehavior.CanNotRename | DefaultFolderBehavior.AlwaysDeferInitialization, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[47] = new DefaultFolderInfo(DefaultFolderType.AllContacts, StoreObjectType.SearchFolder, new LocalizedString("AllContacts"), new LocationEntryIdStrategy(InternalSchema.AllContactsFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.SearchFolder, true), new AllContactsFolderValidation(), DefaultFolderLocalization.CanLocalize, (DefaultFolderBehavior)39, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[54] = new DefaultFolderInfo(DefaultFolderType.MyContacts, StoreObjectType.SearchFolder, ClientStrings.MyContactsFolderName, new LocationEntryIdStrategy(InternalSchema.MyContactsFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.SearchFolder, true), new MyContactsFolderValidation(ContactsSearchFolderCriteria.MyContacts), DefaultFolderLocalization.CanLocalize, (DefaultFolderBehavior)39, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[58] = new DefaultFolderInfo(DefaultFolderType.MyContactsExtended, StoreObjectType.SearchFolder, new LocalizedString("MyContactsExtended"), new LocationEntryIdStrategy(InternalSchema.MyContactsExtendedFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.SearchFolder, true), new MyContactsFolderValidation(ContactsSearchFolderCriteria.MyContactsExtended), DefaultFolderLocalization.CanLocalize, (DefaultFolderBehavior)39, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[48] = new DefaultFolderInfo(DefaultFolderType.RecipientCache, StoreObjectType.ContactsFolder, new LocalizedString("Recipient Cache"), new LocationEntryIdStrategy(InternalSchema.RecipientCacheFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Contacts, StoreObjectType.ContactsFolder, true), new DefaultSubFolderValidator(DefaultFolderType.Contacts, new IValidator[]
			{
				new CompositeValidator(new IValidator[]
				{
					new MatchIsHidden(true),
					new MatchMapiFolderType(FolderType.Generic),
					new MatchContainerClass("IPF.Contact.RecipientCache")
				})
			}), DefaultFolderLocalization.CanNotLocalize, (DefaultFolderBehavior)135, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[49] = new DefaultFolderInfo(DefaultFolderType.PeopleConnect, StoreObjectType.Folder, new LocalizedString("PeopleConnect"), new LocationEntryIdStrategy(InternalSchema.PeopleConnectFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.Folder, true), DefaultFolderValidator.FolderGenericTypeValidator, DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.CreateIfMissing, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[50] = new DefaultFolderInfo(DefaultFolderType.QuickContacts, StoreObjectType.ContactsFolder, new LocalizedString("{06967759-274D-40B2-A3EB-D7F9E73727D7}"), new Ex12RenEntryIdStrategy(InternalSchema.AdditionalRenEntryIdsEx, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag), Ex12RenEntryIdStrategy.PersistenceId.QuickContacts), new QuickContactsDefaultFolderCreator(), new DefaultSubFolderValidator(DefaultFolderType.Contacts, new IValidator[]
			{
				new CompositeValidator(new IValidator[]
				{
					new MatchIsClientReadOnly(),
					new MatchMapiFolderType(FolderType.Generic),
					new MatchContainerClass("IPF.Contact.MOC.QuickContacts")
				})
			}), DefaultFolderLocalization.CanNotLocalize, (DefaultFolderBehavior)163, CorruptDataRecoveryStrategy.Repair);
			DefaultFolderInfo.defaultFolderInfos[51] = new DefaultFolderInfo(DefaultFolderType.ImContactList, StoreObjectType.ContactsFolder, new LocalizedString("{A9E2BC46-B3A0-4243-B315-60D991004455}"), new Ex12RenEntryIdStrategy(InternalSchema.AdditionalRenEntryIdsEx, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag), Ex12RenEntryIdStrategy.PersistenceId.ImContactList), new MessageClassBasedDefaultFolderCreator(DefaultFolderType.Contacts, "IPF.Contact.MOC.ImContactList", true), new DefaultSubFolderValidator(DefaultFolderType.Contacts, new IValidator[]
			{
				new CompositeValidator(new IValidator[]
				{
					new MatchIsHidden(true),
					new MatchMapiFolderType(FolderType.Generic),
					new MatchContainerClass("IPF.Contact.MOC.ImContactList")
				})
			}), DefaultFolderLocalization.CanNotLocalize, (DefaultFolderBehavior)167, CorruptDataRecoveryStrategy.Repair);
			DefaultFolderInfo.defaultFolderInfos[53] = new DefaultFolderInfo(DefaultFolderType.OrganizationalContacts, StoreObjectType.ContactsFolder, new LocalizedString("Organizational Contacts"), new LocationEntryIdStrategy(InternalSchema.OrganizationalContactsFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Contacts, StoreObjectType.ContactsFolder, true), new DefaultSubFolderValidator(DefaultFolderType.Contacts, new IValidator[]
			{
				new CompositeValidator(new IValidator[]
				{
					new MatchIsHidden(true),
					new MatchMapiFolderType(FolderType.Generic),
					new MatchContainerClass("IPF.Contact.OrganizationalContacts")
				})
			}), DefaultFolderLocalization.CanNotLocalize, (DefaultFolderBehavior)167, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[62] = new DefaultFolderInfo(DefaultFolderType.MailboxAssociation, StoreObjectType.Folder, new LocalizedString("MailboxAssociations"), new LocationEntryIdStrategy(InternalSchema.MailboxAssociationFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.Folder, true), DefaultFolderValidator.NullValidator, DefaultFolderLocalization.CanNotLocalize, (DefaultFolderBehavior)135, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[56] = new DefaultFolderInfo(DefaultFolderType.PushNotificationRoot, StoreObjectType.Folder, new LocalizedString("Notification Subscriptions"), new LocationEntryIdStrategy(InternalSchema.PushNotificationFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.Folder, true), new DefaultFolderValidator(new IValidator[]
			{
				new MatchMapiFolderType(FolderType.Generic)
			}), DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.CanCreate | DefaultFolderBehavior.CanNotRename, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[60] = new DefaultFolderInfo(DefaultFolderType.GroupNotifications, StoreObjectType.Folder, new LocalizedString("GroupNotifications"), new LocationEntryIdStrategy(InternalSchema.GroupNotificationsFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.Folder, true), DefaultFolderValidator.FolderGenericTypeValidator, DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.CanCreate | DefaultFolderBehavior.CanNotRename | DefaultFolderBehavior.RefreshIfMissing, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[61] = new DefaultFolderInfo(DefaultFolderType.Favorites, StoreObjectType.SearchFolder, ClientStrings.FavoritesFolderName, new LocationEntryIdStrategy(InternalSchema.FavoritesFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.SearchFolder, true), new FavoritesFolderValidation(), DefaultFolderLocalization.CanLocalize, (DefaultFolderBehavior)39, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[63] = new DefaultFolderInfo(DefaultFolderType.FromFavoriteSenders, StoreObjectType.SearchFolder, ClientStrings.FromFavoriteSendersFolderName, new LocationEntryIdStrategy(InternalSchema.FromFavoriteSendersFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.SearchFolder, true), new FromFavoriteSendersFolderValidation(), DefaultFolderLocalization.CanLocalize, (DefaultFolderBehavior)39, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[73] = new DefaultFolderInfo(DefaultFolderType.FromPeople, StoreObjectType.SearchFolder, new LocalizedString("From People"), new LocationEntryIdStrategy(InternalSchema.FromPeopleFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.SearchFolder, true), new FromPeopleFolderValidation(), DefaultFolderLocalization.CanNotLocalize, (DefaultFolderBehavior)39, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[64] = new DefaultFolderInfo(DefaultFolderType.OutlookService, StoreObjectType.Folder, new LocalizedString("OutlookService"), new LocationEntryIdStrategy(InternalSchema.OutlookServiceFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.Folder, true), new DefaultFolderValidator(new IValidator[]
			{
				new MatchMapiFolderType(FolderType.Generic)
			}), DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.CanCreate | DefaultFolderBehavior.CanNotRename, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[65] = new DefaultFolderInfo(DefaultFolderType.GalContacts, StoreObjectType.ContactsFolder, new LocalizedString("GAL Contacts"), new LocationEntryIdStrategy(InternalSchema.GalContactsFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Contacts, StoreObjectType.ContactsFolder, true), new DefaultSubFolderValidator(DefaultFolderType.Contacts, new IValidator[]
			{
				new CompositeValidator(new IValidator[]
				{
					new MatchIsHidden(true),
					new MatchMapiFolderType(FolderType.Generic),
					new MatchContainerClass("IPF.Contact.GalContacts")
				})
			}), DefaultFolderLocalization.CanNotLocalize, (DefaultFolderBehavior)135, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[66] = new DefaultFolderInfo(DefaultFolderType.UserActivity, StoreObjectType.Folder, new LocalizedString("UserActivity"), new LocationEntryIdStrategy(InternalSchema.UserActivityFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.Folder, true), new DefaultFolderValidator(new IValidator[]
			{
				new MatchMapiFolderType(FolderType.Generic)
			}), DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.CanCreate | DefaultFolderBehavior.CanNotRename | DefaultFolderBehavior.CanHideFolderFromOutlook | DefaultFolderBehavior.RefreshIfMissing, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[67] = new DefaultFolderInfo(DefaultFolderType.WorkingSet, StoreObjectType.Folder, new LocalizedString("Working Set"), new LocationEntryIdStrategy(InternalSchema.WorkingSetFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Root, StoreObjectType.Folder, true), new DefaultFolderValidator(new IValidator[]
			{
				new MatchIsHidden(true)
			}), DefaultFolderLocalization.CanNotLocalize, (DefaultFolderBehavior)135, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[68] = new DefaultFolderInfo(DefaultFolderType.Clutter, StoreObjectType.Folder, ClientStrings.ClutterFolderName, new LocationEntryIdStrategy(InternalSchema.ClutterFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Root, StoreObjectType.Folder, true), new DefaultSubFolderValidator(DefaultFolderType.Root, new IValidator[]
			{
				new MatchMapiFolderType(FolderType.Generic),
				new MatchContainerClassOrThrow("IPF.Note")
			}), DefaultFolderLocalization.CanLocalize, DefaultFolderBehavior.CanCreate | DefaultFolderBehavior.CanNotRename | DefaultFolderBehavior.RefreshIfMissing, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[72] = new DefaultFolderInfo(DefaultFolderType.BirthdayCalendar, StoreObjectType.CalendarFolder, ClientStrings.BirthdayCalendarFolderName, new LocationEntryIdStrategy(InternalSchema.BirthdayCalendarFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new BirthdayCalendarFolderCreator(), new DefaultFolderValidator(new IValidator[]
			{
				new CompositeValidator(new IValidator[]
				{
					new MatchMapiFolderType(FolderType.Generic),
					new MatchContainerClass("IPF.Appointment.Birthday")
				})
			}), DefaultFolderLocalization.CanLocalize, DefaultFolderBehavior.CanCreate | DefaultFolderBehavior.CanHideFolderFromOutlook | DefaultFolderBehavior.RefreshIfMissing, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[69] = new DefaultFolderInfo(DefaultFolderType.ParkedMessages, StoreObjectType.Folder, new LocalizedString("ParkedMessages"), new LocationEntryIdStrategy(InternalSchema.ParkedMessagesFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.Folder, true), DefaultFolderValidator.NullValidator, DefaultFolderLocalization.CanNotLocalize, (DefaultFolderBehavior)135, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[70] = new DefaultFolderInfo(DefaultFolderType.UnifiedInbox, StoreObjectType.SearchFolder, ClientStrings.UnifiedInbox, new LocationEntryIdStrategy(InternalSchema.UnifiedInboxFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.SearchFolder, true), new UnifiedInboxFolderValidation(), DefaultFolderLocalization.CanLocalize, (DefaultFolderBehavior)39, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[71] = new DefaultFolderInfo(DefaultFolderType.TemporarySaves, StoreObjectType.Folder, new LocalizedString("TemporarySaves"), new LocationEntryIdStrategy(InternalSchema.TemporarySavesFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.Folder, true), DefaultFolderValidator.NullValidator, DefaultFolderLocalization.CanNotLocalize, (DefaultFolderBehavior)135, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[74] = new DefaultFolderInfo(DefaultFolderType.SnackyApps, StoreObjectType.Folder, new LocalizedString("SnackyApps"), new LocationEntryIdStrategy(InternalSchema.SnackyAppsFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Configuration, StoreObjectType.Folder, true), DefaultFolderValidator.NullValidator, DefaultFolderLocalization.CanNotLocalize, (DefaultFolderBehavior)39, CorruptDataRecoveryStrategy.Recreate);
			DefaultFolderInfo.defaultFolderInfos[75] = new DefaultFolderInfo(DefaultFolderType.SmsAndChatsSync, StoreObjectType.Folder, new LocalizedString("SmsAndChatsSync"), new LocationEntryIdStrategy(InternalSchema.SmsAndChatsSyncFolderEntryId, new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag)), new DefaultFolderCreator(DefaultFolderType.Root, StoreObjectType.Folder, true), new DefaultFolderValidator(new IValidator[]
			{
				new CompositeValidator(new IValidator[]
				{
					new MatchIsHidden(true),
					new MatchMapiFolderType(FolderType.Generic),
					new MatchContainerClass("IPF.SmsAndChatsSync")
				})
			}), DefaultFolderLocalization.CanNotLocalize, DefaultFolderBehavior.CanCreate | DefaultFolderBehavior.CanNotRename | DefaultFolderBehavior.CanHideFolderFromOutlook | DefaultFolderBehavior.RefreshIfMissing, CorruptDataRecoveryStrategy.Recreate);
			return DefaultFolderInfo.defaultFolderInfos;
		}

		// Token: 0x0600440E RID: 17422 RVA: 0x001222B4 File Offset: 0x001204B4
		private static StorePropertyDefinition[] GetDependentProperties(LocationEntryIdStrategy.GetLocationPropertyBagDelegate location)
		{
			List<StorePropertyDefinition> list = new List<StorePropertyDefinition>(DefaultFolderInfo.Instance.Length + 1);
			foreach (DefaultFolderInfo defaultFolderInfo in DefaultFolderInfo.Instance)
			{
				defaultFolderInfo.EntryIdStrategy.GetDependentProperties(location, list);
			}
			if (location == new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag))
			{
				list.Add(InternalSchema.SystemFolderFlags);
			}
			return list.ToArray();
		}

		// Token: 0x040024FB RID: 9467
		public const string RecipientCacheFolderName = "Recipient Cache";

		// Token: 0x040024FC RID: 9468
		public const string GALContactsFolderName = "GAL Contacts";

		// Token: 0x040024FD RID: 9469
		public const string QuickContactsFolderName = "{06967759-274D-40B2-A3EB-D7F9E73727D7}";

		// Token: 0x040024FE RID: 9470
		public const string ImContactListFolderName = "{A9E2BC46-B3A0-4243-B315-60D991004455}";

		// Token: 0x040024FF RID: 9471
		private const string ToDoSearchFolderName = "To-Do Search";

		// Token: 0x04002500 RID: 9472
		private const string SyncRootFolderName = "ExchangeSyncData";

		// Token: 0x04002501 RID: 9473
		private const string FreeBusyFolderName = "Freebusy Data";

		// Token: 0x04002502 RID: 9474
		private const string ExternalIdentitiesFolderName = "External Identities";

		// Token: 0x04002503 RID: 9475
		private const string SharingFolderName = "Sharing";

		// Token: 0x04002504 RID: 9476
		private const string ConversationActionsFolderName = "Conversation Action Settings";

		// Token: 0x04002505 RID: 9477
		internal const string RecoverableItemsRootFolderName = "Recoverable Items";

		// Token: 0x04002506 RID: 9478
		internal const string RecoverableItemsDeletionsFolderName = "Deletions";

		// Token: 0x04002507 RID: 9479
		internal const string RecoverableItemsVersionsFolderName = "Versions";

		// Token: 0x04002508 RID: 9480
		internal const string RecoverableItemsPurgesFolderName = "Purges";

		// Token: 0x04002509 RID: 9481
		internal const string RecoverableItemsDiscoveryHoldsFolderName = "DiscoveryHolds";

		// Token: 0x0400250A RID: 9482
		internal const string RecoverableItemsMigratedMessagesFolderName = "MigratedMessages";

		// Token: 0x0400250B RID: 9483
		private const string SystemFolderName = "System";

		// Token: 0x0400250C RID: 9484
		internal const string AdminAuditLogsFolderName = "AdminAuditLogs";

		// Token: 0x0400250D RID: 9485
		private const string CalendarVersionStoreFolderName = "Calendar Version Store";

		// Token: 0x0400250E RID: 9486
		internal const string AuditsFolderName = "Audits";

		// Token: 0x0400250F RID: 9487
		internal const string AllContactsFolderName = "AllContacts";

		// Token: 0x04002510 RID: 9488
		internal const string MyContactsExtendedFolderName = "MyContactsExtended";

		// Token: 0x04002511 RID: 9489
		private const string NotificationRootFolderName = "Notification Subscriptions";

		// Token: 0x04002512 RID: 9490
		private const string CalendarLoggingFolderName = "Calendar Logging";

		// Token: 0x04002513 RID: 9491
		private const string MailboxAssociationFolderName = "MailboxAssociations";

		// Token: 0x04002514 RID: 9492
		private const string DeferredActionsFolderName = "Deferred Actions";

		// Token: 0x04002515 RID: 9493
		private const string LegacySpoolerQueueFolderName = "Spooler Queue";

		// Token: 0x04002516 RID: 9494
		private const string LegacyScheduleFolderName = "Schedule";

		// Token: 0x04002517 RID: 9495
		private const string LegacyShortcutsFolderName = "Shortcuts";

		// Token: 0x04002518 RID: 9496
		private const string LegacyViewsFolderName = "Views";

		// Token: 0x04002519 RID: 9497
		private const string LocationFolderName = "Location";

		// Token: 0x0400251A RID: 9498
		private const string PeopleConnectFolderName = "PeopleConnect";

		// Token: 0x0400251B RID: 9499
		private const string OrganizationalContactsFolderName = "Organizational Contacts";

		// Token: 0x0400251C RID: 9500
		private const string GroupNotificationsFolderName = "GroupNotifications";

		// Token: 0x0400251D RID: 9501
		private const string FromPeopleFolderName = "From People";

		// Token: 0x0400251E RID: 9502
		private const string OutlookServiceFolderName = "OutlookService";

		// Token: 0x0400251F RID: 9503
		private const string UserActivityFolderName = "UserActivity";

		// Token: 0x04002520 RID: 9504
		private const string WorkingSetFolderName = "Working Set";

		// Token: 0x04002521 RID: 9505
		private const string ParkedMessagesFolderName = "ParkedMessages";

		// Token: 0x04002522 RID: 9506
		private const string TemporarySavesFolderName = "TemporarySaves";

		// Token: 0x04002523 RID: 9507
		private const string SnackyAppsFolderName = "SnackyApps";

		// Token: 0x04002524 RID: 9508
		private const string SmsAndChatsSyncFolderName = "SmsAndChatsSync";

		// Token: 0x04002525 RID: 9509
		private static readonly int defaultFolderTypeCount = Enum.GetValues(typeof(DefaultFolderType)).Length;

		// Token: 0x04002526 RID: 9510
		private static DefaultFolderInfo[] defaultFolderInfos = DefaultFolderInfo.CreateDefaultFolderInfos();

		// Token: 0x04002527 RID: 9511
		private static readonly StorePropertyDefinition[] mailboxProperties = DefaultFolderInfo.GetDependentProperties(new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetMailboxPropertyBag));

		// Token: 0x04002528 RID: 9512
		private static readonly StorePropertyDefinition[] inboxOrConfigurationFolderProperties = DefaultFolderInfo.GetDependentProperties(new LocationEntryIdStrategy.GetLocationPropertyBagDelegate(LocationEntryIdStrategy.GetInboxOrConfigurationFolderPropertyBag));

		// Token: 0x04002529 RID: 9513
		private readonly DefaultFolderValidator folderValidationStrategy;

		// Token: 0x0400252A RID: 9514
		private readonly DefaultFolderLocalization localizable;

		// Token: 0x0400252B RID: 9515
		private readonly LocalizedString localizableDisplayName;

		// Token: 0x0400252C RID: 9516
		private readonly DefaultFolderType defaultFolderType;

		// Token: 0x0400252D RID: 9517
		private readonly EntryIdStrategy entryIdStrategy;

		// Token: 0x0400252E RID: 9518
		private readonly StoreObjectType storeObjectType;

		// Token: 0x0400252F RID: 9519
		private readonly DefaultFolderCreator folderCreator;

		// Token: 0x04002530 RID: 9520
		private readonly DefaultFolderBehavior behavior;

		// Token: 0x04002531 RID: 9521
		private readonly CorruptDataRecoveryStrategy corruptDataRecoveryStrategy;
	}
}
