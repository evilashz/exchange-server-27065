using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C20 RID: 3104
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FolderSchema : StoreObjectSchema
	{
		// Token: 0x06006E73 RID: 28275 RVA: 0x001DAD6C File Offset: 0x001D8F6C
		protected FolderSchema()
		{
			base.AddDependencies(new Schema[]
			{
				CoreFolderSchema.Instance
			});
		}

		// Token: 0x17001DE3 RID: 7651
		// (get) Token: 0x06006E74 RID: 28276 RVA: 0x001DAD95 File Offset: 0x001D8F95
		public new static FolderSchema Instance
		{
			get
			{
				if (FolderSchema.instance == null)
				{
					FolderSchema.instance = new FolderSchema();
				}
				return FolderSchema.instance;
			}
		}

		// Token: 0x06006E75 RID: 28277 RVA: 0x001DADAD File Offset: 0x001D8FAD
		internal virtual void CoreObjectUpdate(CoreFolder coreFolder)
		{
			Folder.CoreObjectUpdateRetentionProperties(coreFolder);
		}

		// Token: 0x040040C5 RID: 16581
		[Autoload]
		public new static readonly StorePropertyDefinition DisplayName = CoreFolderSchema.DisplayName;

		// Token: 0x040040C6 RID: 16582
		[Autoload]
		public static readonly StorePropertyDefinition Id = CoreFolderSchema.Id;

		// Token: 0x040040C7 RID: 16583
		[Autoload]
		public static readonly StorePropertyDefinition CorrelationId = InternalSchema.CorrelationId;

		// Token: 0x040040C8 RID: 16584
		[Autoload]
		internal static readonly StorePropertyDefinition SystemFolderFlags = InternalSchema.SystemFolderFlags;

		// Token: 0x040040C9 RID: 16585
		[Autoload]
		public static readonly StorePropertyDefinition HasRules = CoreFolderSchema.HasRules;

		// Token: 0x040040CA RID: 16586
		[Autoload]
		public static readonly StorePropertyDefinition DeletedCountTotal = InternalSchema.DeletedCountTotal;

		// Token: 0x040040CB RID: 16587
		[Autoload]
		public static readonly StorePropertyDefinition DeletedMsgCount = InternalSchema.DeletedMsgCount;

		// Token: 0x040040CC RID: 16588
		[Autoload]
		public static readonly StorePropertyDefinition DeletedAssocMsgCount = InternalSchema.DeletedAssocMsgCount;

		// Token: 0x040040CD RID: 16589
		[Autoload]
		public static readonly StorePropertyDefinition Description = CoreFolderSchema.Description;

		// Token: 0x040040CE RID: 16590
		[Autoload]
		public static readonly StorePropertyDefinition ExtendedFolderFlags = InternalSchema.ExtendedFolderFlags;

		// Token: 0x040040CF RID: 16591
		[Autoload]
		public static readonly StorePropertyDefinition HasChildren = InternalSchema.HasChildren;

		// Token: 0x040040D0 RID: 16592
		[Autoload]
		public static readonly StorePropertyDefinition ItemCount = CoreFolderSchema.ItemCount;

		// Token: 0x040040D1 RID: 16593
		[Autoload]
		public static readonly StorePropertyDefinition AssociatedItemCount = CoreFolderSchema.AssociatedItemCount;

		// Token: 0x040040D2 RID: 16594
		[Autoload]
		public static readonly StorePropertyDefinition EhaMigrationMessageCount = InternalSchema.EHAMigrationMessageCount;

		// Token: 0x040040D3 RID: 16595
		public static readonly StorePropertyDefinition SearchFolderItemCount = InternalSchema.SearchFolderItemCount;

		// Token: 0x040040D4 RID: 16596
		[Autoload]
		public static readonly StorePropertyDefinition MapiFolderType = InternalSchema.MapiFolderType;

		// Token: 0x040040D5 RID: 16597
		[Autoload]
		public static readonly StorePropertyDefinition ChildCount = CoreFolderSchema.ChildCount;

		// Token: 0x040040D6 RID: 16598
		[Autoload]
		public static readonly StorePropertyDefinition IsOutlookSearchFolder = InternalSchema.IsOutlookSearchFolder;

		// Token: 0x040040D7 RID: 16599
		[Autoload]
		public static readonly StorePropertyDefinition UnreadCount = InternalSchema.UnreadCount;

		// Token: 0x040040D8 RID: 16600
		public static readonly StorePropertyDefinition AccessRights = InternalSchema.AccessRights;

		// Token: 0x040040D9 RID: 16601
		[Autoload]
		public static readonly StorePropertyDefinition AdminFolderFlags = InternalSchema.AdminFolderFlags;

		// Token: 0x040040DA RID: 16602
		public static readonly StorePropertyDefinition ELCFolderComment = InternalSchema.ELCFolderComment;

		// Token: 0x040040DB RID: 16603
		[Autoload]
		public static readonly StorePropertyDefinition ELCPolicyIds = InternalSchema.ELCPolicyIds;

		// Token: 0x040040DC RID: 16604
		public static readonly StorePropertyDefinition RetentionDate = InternalSchema.RetentionDate;

		// Token: 0x040040DD RID: 16605
		public static readonly StorePropertyDefinition FolderQuota = InternalSchema.FolderQuota;

		// Token: 0x040040DE RID: 16606
		public static readonly StorePropertyDefinition FolderSize = InternalSchema.FolderSize;

		// Token: 0x040040DF RID: 16607
		public static readonly StorePropertyDefinition RetentionAgeLimit = InternalSchema.RetentionAgeLimit;

		// Token: 0x040040E0 RID: 16608
		public static readonly StorePropertyDefinition OverallAgeLimit = InternalSchema.OverallAgeLimit;

		// Token: 0x040040E1 RID: 16609
		public static readonly GuidNamePropertyDefinition LastMovedTimeStamp = InternalSchema.LastMovedTimeStamp;

		// Token: 0x040040E2 RID: 16610
		public static readonly StorePropertyDefinition PfOverHardQuotaLimit = InternalSchema.PfOverHardQuotaLimit;

		// Token: 0x040040E3 RID: 16611
		public static readonly StorePropertyDefinition PfStorageQuota = InternalSchema.PfStorageQuota;

		// Token: 0x040040E4 RID: 16612
		public static readonly StorePropertyDefinition PfMsgSizeLimit = InternalSchema.PfMsgSizeLimit;

		// Token: 0x040040E5 RID: 16613
		public static readonly StorePropertyDefinition PfQuotaStyle = InternalSchema.PfQuotaStyle;

		// Token: 0x040040E6 RID: 16614
		public static readonly StorePropertyDefinition DisablePerUserRead = InternalSchema.DisablePerUserRead;

		// Token: 0x040040E7 RID: 16615
		public static readonly StorePropertyDefinition LocalCommitTimeMax = InternalSchema.LocalCommitTimeMax;

		// Token: 0x040040E8 RID: 16616
		public static readonly StorePropertyDefinition EformsLocaleId = InternalSchema.EformsLocaleId;

		// Token: 0x040040E9 RID: 16617
		public static readonly StorePropertyDefinition DefaultFoldersLocaleId = InternalSchema.DefaultFoldersLocaleId;

		// Token: 0x040040EA RID: 16618
		public static readonly StorePropertyDefinition HiddenFromAddressListsEnabled = InternalSchema.PublishInAddressBook;

		// Token: 0x040040EB RID: 16619
		public static readonly PropertyTagPropertyDefinition FolderRulesSize = InternalSchema.FolderRulesSize;

		// Token: 0x040040EC RID: 16620
		public static readonly StorePropertyDefinition PopImapConversionVersion = InternalSchema.PopImapConversionVersion;

		// Token: 0x040040ED RID: 16621
		public static readonly StorePropertyDefinition ImapLastSeenArticleId = InternalSchema.ImapLastSeenArticleId;

		// Token: 0x040040EE RID: 16622
		public static readonly StorePropertyDefinition ImapLastUidFixTime = InternalSchema.ImapLastUidFixTime;

		// Token: 0x040040EF RID: 16623
		[Autoload]
		public static readonly StorePropertyDefinition IsHidden = InternalSchema.IsHidden;

		// Token: 0x040040F0 RID: 16624
		public static readonly StorePropertyDefinition NextArticleId = InternalSchema.NextArticleId;

		// Token: 0x040040F1 RID: 16625
		public static readonly StorePropertyDefinition FolderHierarchyDepth = InternalSchema.FolderHierarchyDepth;

		// Token: 0x040040F2 RID: 16626
		public static readonly StorePropertyDefinition UrlName = InternalSchema.UrlName;

		// Token: 0x040040F3 RID: 16627
		public static readonly StorePropertyDefinition FolderPathName = InternalSchema.FolderPathName;

		// Token: 0x040040F4 RID: 16628
		public static readonly StorePropertyDefinition ExtendedSize = InternalSchema.ExtendedSize;

		// Token: 0x040040F5 RID: 16629
		public static readonly StorePropertyDefinition SyncFolderSourceKey = InternalSchema.SyncFolderSourceKey;

		// Token: 0x040040F6 RID: 16630
		internal static readonly StorePropertyDefinition AssociatedSearchFolderLastUsedTime = InternalSchema.AssociatedSearchFolderLastUsedTime;

		// Token: 0x040040F7 RID: 16631
		public static readonly StorePropertyDefinition FolderHomePageUrl = InternalSchema.FolderHomePageUrl;

		// Token: 0x040040F8 RID: 16632
		public static readonly StorePropertyDefinition ElcRootFolderEntryId = InternalSchema.ElcRootFolderEntryId;

		// Token: 0x040040F9 RID: 16633
		public static readonly StorePropertyDefinition RetentionTagEntryId = InternalSchema.RetentionTagEntryId;

		// Token: 0x040040FA RID: 16634
		public static readonly StorePropertyDefinition PublicFolderDumpsterHolderEntryId = InternalSchema.PublicFolderDumpsterHolderEntryId;

		// Token: 0x040040FB RID: 16635
		public static readonly StorePropertyDefinition ElcFolderLocalizedName = InternalSchema.ElcFolderLocalizedName;

		// Token: 0x040040FC RID: 16636
		public static readonly StorePropertyDefinition OutOfOfficeHistory = InternalSchema.OutOfOfficeHistory;

		// Token: 0x040040FD RID: 16637
		private static FolderSchema instance = null;

		// Token: 0x040040FE RID: 16638
		public static readonly PropertyTagPropertyDefinition SearchFolderMessageCount = InternalSchema.SearchFolderItemCount;

		// Token: 0x040040FF RID: 16639
		public static readonly StorePropertyDefinition AggregationSyncProgress = InternalSchema.AggregationSyncProgress;

		// Token: 0x04004100 RID: 16640
		public static readonly StorePropertyDefinition SecurityDescriptor = InternalSchema.SecurityDescriptor;

		// Token: 0x04004101 RID: 16641
		public static readonly PropertyDefinition OutlookSearchFolderClsId = InternalSchema.OutlookSearchFolderClsId;

		// Token: 0x04004102 RID: 16642
		public static readonly PropertyDefinition SearchFolderAllowAgeout = InternalSchema.SearchFolderAllowAgeout;

		// Token: 0x04004103 RID: 16643
		public static readonly PropertyDefinition IPMFolder = InternalSchema.IPMFolder;

		// Token: 0x04004104 RID: 16644
		public static readonly PropertyDefinition SearchFolderAgeOutTimeout = InternalSchema.SearchFolderAgeOutTimeout;

		// Token: 0x04004105 RID: 16645
		public static readonly PropertyDefinition AssociatedSearchFolderId = InternalSchema.AssociatedSearchFolderId;

		// Token: 0x04004106 RID: 16646
		public static readonly StorePropertyDefinition OwnerLogonUserConfigurationCache = InternalSchema.OwnerLogonUserConfigurationCache;

		// Token: 0x04004107 RID: 16647
		public static readonly StorePropertyDefinition FolderFlags = InternalSchema.FolderFlags;

		// Token: 0x04004108 RID: 16648
		[Autoload]
		public static readonly NativeStorePropertyDefinition PermissionChangeBlocked = CoreFolderSchema.PermissionChangeBlocked;

		// Token: 0x04004109 RID: 16649
		public static readonly PropertyDefinition OwaViewStateSortColumn = InternalSchema.OwaViewStateSortColumn;

		// Token: 0x0400410A RID: 16650
		public static readonly PropertyDefinition OwaViewStateSortOrder = InternalSchema.OwaViewStateSortOrder;

		// Token: 0x0400410B RID: 16651
		public static readonly PropertyDefinition ReplicaList = CoreFolderSchema.ReplicaList;

		// Token: 0x0400410C RID: 16652
		public static readonly PropertyDefinition ReplicaListBinary = InternalSchema.ReplicaListBinary;

		// Token: 0x0400410D RID: 16653
		public static readonly PropertyDefinition ResolveMethod = InternalSchema.ResolveMethod;

		// Token: 0x0400410E RID: 16654
		[Autoload]
		public static readonly PropertyDefinition RecentBindingHistory = CoreFolderSchema.RecentBindingHistory;

		// Token: 0x0400410F RID: 16655
		[Autoload]
		public static readonly StorePropertyDefinition LinkedUrl = InternalSchema.LinkedUrl;

		// Token: 0x04004110 RID: 16656
		[Autoload]
		public static readonly StorePropertyDefinition LinkedId = InternalSchema.LinkedId;

		// Token: 0x04004111 RID: 16657
		[Autoload]
		public static readonly StorePropertyDefinition SharePointChangeToken = InternalSchema.SharePointChangeToken;

		// Token: 0x04004112 RID: 16658
		[Autoload]
		public static readonly StorePropertyDefinition LinkedSiteUrl = InternalSchema.LinkedSiteUrl;

		// Token: 0x04004113 RID: 16659
		[Autoload]
		public static readonly StorePropertyDefinition LinkedListId = InternalSchema.LinkedListId;

		// Token: 0x04004114 RID: 16660
		[Autoload]
		public static readonly StorePropertyDefinition LinkedSiteAuthorityUrl = InternalSchema.LinkedSiteAuthorityUrl;

		// Token: 0x04004115 RID: 16661
		[Autoload]
		public static readonly StorePropertyDefinition LinkedLastFullSyncTime = InternalSchema.LinkedLastFullSyncTime;

		// Token: 0x04004116 RID: 16662
		[Autoload]
		public static readonly StorePropertyDefinition IsDocumentLibraryFolder = InternalSchema.IsDocumentLibraryFolder;

		// Token: 0x04004117 RID: 16663
		public static readonly StorePropertyDefinition MailEnabled = InternalSchema.MailEnabled;

		// Token: 0x04004118 RID: 16664
		public static readonly StorePropertyDefinition ProxyGuid = InternalSchema.ProxyGuid;

		// Token: 0x04004119 RID: 16665
		[Autoload]
		public static readonly GuidNamePropertyDefinition SubscriptionLastSuccessfulSyncTime = InternalSchema.SubscriptionLastSuccessfulSyncTime;

		// Token: 0x0400411A RID: 16666
		[Autoload]
		public static readonly GuidIdPropertyDefinition SubscriptionLastAttemptedSyncTime = InternalSchema.SharingLastSync;

		// Token: 0x0400411B RID: 16667
		[Autoload]
		public static readonly PropertyDefinition ConversationTopicHashEntries = InternalSchema.ConversationTopicHashEntries;

		// Token: 0x0400411C RID: 16668
		public static readonly PropertyDefinition SearchBacklinkNames = InternalSchema.SearchBacklinkNames;

		// Token: 0x0400411D RID: 16669
		public static readonly PropertyDefinition PeopleHubSortGroupPriority = InternalSchema.PeopleHubSortGroupPriority;

		// Token: 0x0400411E RID: 16670
		public static readonly PropertyDefinition PeopleHubSortGroupPriorityVersion = InternalSchema.PeopleHubSortGroupPriorityVersion;

		// Token: 0x0400411F RID: 16671
		public static readonly GuidNamePropertyDefinition IsPeopleConnectSyncFolder = InternalSchema.IsPeopleConnectSyncFolder;

		// Token: 0x04004120 RID: 16672
		public static readonly PropertyDefinition PartOfContentIndexing = InternalSchema.PartOfContentIndexing;

		// Token: 0x04004121 RID: 16673
		public static readonly PropertyDefinition ContentAggregationFlags = InternalSchema.ContentAggregationFlags;

		// Token: 0x04004122 RID: 16674
		public static readonly PropertyDefinition PeopleIKnowEmailAddressCollection = InternalSchema.PeopleIKnowEmailAddressCollection;

		// Token: 0x04004123 RID: 16675
		public static readonly PropertyDefinition PeopleIKnowEmailAddressRelevanceScoreCollection = InternalSchema.PeopleIKnowEmailAddressRelevanceScoreCollection;

		// Token: 0x04004124 RID: 16676
		public static readonly PropertyDefinition GalContactsFolderState = InternalSchema.GalContactsFolderState;

		// Token: 0x04004125 RID: 16677
		[Autoload]
		public static readonly PropertyDefinition UnClutteredViewFolderEntryId = InternalSchema.UnClutteredViewFolderEntryId;

		// Token: 0x04004126 RID: 16678
		[Autoload]
		public static readonly PropertyDefinition ClutteredViewFolderEntryId = InternalSchema.ClutteredViewFolderEntryId;

		// Token: 0x04004127 RID: 16679
		[Autoload]
		public static readonly StorePropertyDefinition CalendarFolderVersion = InternalSchema.CalendarFolderVersion;

		// Token: 0x04004128 RID: 16680
		[Autoload]
		public static readonly StorePropertyDefinition WorkingSetSourcePartitionInternal = InternalSchema.WorkingSetSourcePartitionInternal;

		// Token: 0x04004129 RID: 16681
		[Autoload]
		public static readonly StorePropertyDefinition OfficeGraphLocation = InternalSchema.OfficeGraphLocation;
	}
}
