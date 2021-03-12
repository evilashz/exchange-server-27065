using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000389 RID: 905
	internal class FolderTreeNode : TreeNode
	{
		// Token: 0x06002249 RID: 8777 RVA: 0x000C340C File Offset: 0x000C160C
		protected FolderTreeNode(UserContext userContext, OwaStoreObjectId owaStoreObjectId, string displayName, string containerClass, DefaultFolderType folderType) : base(userContext)
		{
			this.folderId = owaStoreObjectId;
			this.displayName = displayName;
			this.folderName = displayName;
			this.containerClass = containerClass;
			this.folderType = folderType;
			this.HasIcon = true;
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x000C345C File Offset: 0x000C165C
		protected FolderTreeNode(UserContext userContext, StoreSession storeSession, object[] values, Dictionary<PropertyDefinition, int> propertyMap) : base(userContext)
		{
			VersionedId propertyValue = this.GetPropertyValue<VersionedId>(values, FolderSchema.Id, propertyMap, null);
			this.folderId = OwaStoreObjectId.CreateFromSessionFolderId(userContext, storeSession, propertyValue.ObjectId);
			MailboxSession mailboxSession = storeSession as MailboxSession;
			if (mailboxSession != null)
			{
				this.folderType = mailboxSession.IsDefaultFolderType(propertyValue.ObjectId);
			}
			this.displayName = (this.folderName = this.GetPropertyValue<string>(values, FolderSchema.DisplayName, propertyMap, string.Empty));
			if (mailboxSession != null)
			{
				this.displayName = Utilities.GetMailboxFolderDisplayName(this.folderType, mailboxSession, this.folderName, false);
			}
			else if (userContext.IsPublicFolderRootId(this.folderId.StoreObjectId))
			{
				this.displayName = LocalizedStrings.GetNonEncoded(-1116491328);
			}
			this.containerClass = this.GetPropertyValue<string>(values, StoreObjectSchema.ContainerClass, propertyMap, "IPF.Note");
			this.extendedFolderFlags = this.GetPropertyValue<ExtendedFolderFlags>(values, FolderSchema.ExtendedFolderFlags, propertyMap, (ExtendedFolderFlags)0);
			this.effectiveRights = this.GetPropertyValue<EffectiveRights>(values, StoreObjectSchema.EffectiveRights, propertyMap, EffectiveRights.None);
			this.isOutlookSearchFolder = this.GetPropertyValue<bool>(values, FolderSchema.IsOutlookSearchFolder, propertyMap, false);
			this.hasChildren = this.GetPropertyValue<bool>(values, FolderSchema.HasChildren, propertyMap, false);
			this.adminFolderFlags = this.GetPropertyValue<int>(values, FolderSchema.AdminFolderFlags, propertyMap, 0);
			this.unreadCount = this.GetPropertyValue<int>(values, FolderSchema.UnreadCount, propertyMap, 0);
			this.itemCount = this.GetPropertyValue<int>(values, FolderSchema.ItemCount, propertyMap, 0);
			this.hasPolicyIds = !string.IsNullOrEmpty(this.GetPropertyValue<string>(values, FolderSchema.ELCPolicyIds, propertyMap, null));
			if (Utilities.IsFlagSet((int)this.extendedFolderFlags, 2))
			{
				this.contentCountDisplayType = FolderTreeNode.ContentCountDisplayType.ItemCount;
			}
			else if (Utilities.IsFlagSet((int)this.extendedFolderFlags, 1))
			{
				this.contentCountDisplayType = FolderTreeNode.ContentCountDisplayType.UnreadCount;
			}
			else if (this.folderType == DefaultFolderType.Root)
			{
				this.contentCountDisplayType = FolderTreeNode.ContentCountDisplayType.None;
			}
			else if (this.folderType == DefaultFolderType.Outbox || this.folderType == DefaultFolderType.Drafts || this.folderType == DefaultFolderType.JunkEmail)
			{
				this.contentCountDisplayType = FolderTreeNode.ContentCountDisplayType.ItemCount;
			}
			else
			{
				this.contentCountDisplayType = FolderTreeNode.ContentCountDisplayType.UnreadCount;
			}
			this.HasIcon = true;
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x000C365A File Offset: 0x000C185A
		internal FolderTreeNode(UserContext userContext, Folder folder) : this(userContext, folder.Session, folder.GetProperties(FolderTreeNode.NodeCreateProperties), FolderTreeNode.nodeCreatePropertyMap)
		{
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x0600224C RID: 8780 RVA: 0x000C3679 File Offset: 0x000C1879
		protected DefaultFolderType FolderType
		{
			get
			{
				return this.folderType;
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x0600224D RID: 8781 RVA: 0x000C3681 File Offset: 0x000C1881
		internal virtual string FolderName
		{
			get
			{
				return this.folderName;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x000C3689 File Offset: 0x000C1889
		internal virtual string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x0600224F RID: 8783 RVA: 0x000C3691 File Offset: 0x000C1891
		internal OwaStoreObjectId FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06002250 RID: 8784 RVA: 0x000C3699 File Offset: 0x000C1899
		internal override bool HasChildren
		{
			get
			{
				return base.HasChildren || this.hasChildren;
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06002251 RID: 8785 RVA: 0x000C36AC File Offset: 0x000C18AC
		protected override bool Visible
		{
			get
			{
				string text = this.ContainerClass;
				return (!ObjectClass.IsCalendarFolder(text) || base.UserContext.IsFeatureEnabled(Feature.Calendar)) && (!ObjectClass.IsTaskFolder(text) || base.UserContext.IsFeatureEnabled(Feature.Tasks)) && (!ObjectClass.IsContactsFolder(text) || base.UserContext.IsFeatureEnabled(Feature.Contacts)) && (!this.IsSearchFolderRoot || this.HasChildren) && base.Visible;
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06002252 RID: 8786 RVA: 0x000C371D File Offset: 0x000C191D
		public override string Id
		{
			get
			{
				return "f" + this.folderId.ToString();
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06002253 RID: 8787 RVA: 0x000C3734 File Offset: 0x000C1934
		protected bool IsSearchFolder
		{
			get
			{
				return this.folderType == DefaultFolderType.SearchFolders || this.isOutlookSearchFolder || this.folderId.StoreObjectType == StoreObjectType.SearchFolder;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06002254 RID: 8788 RVA: 0x000C3758 File Offset: 0x000C1958
		private FolderSharingFlag FolderSharingFlag
		{
			get
			{
				if (Utilities.IsWebCalendarFolder(this.extendedFolderFlags))
				{
					return FolderSharingFlag.WebCalendar;
				}
				if (this.folderId.IsOtherMailbox || this.folderId.IsGSCalendar || (Utilities.IsOneOfTheFolderFlagsSet(this.extendedFolderFlags, new ExtendedFolderFlags[]
				{
					ExtendedFolderFlags.SharedIn
				}) && !Utilities.IsOneOfTheFolderFlagsSet(this.extendedFolderFlags, new ExtendedFolderFlags[]
				{
					ExtendedFolderFlags.ICalFolder,
					ExtendedFolderFlags.HasRssItems
				})))
				{
					return FolderSharingFlag.SharedIn;
				}
				if (this.folderId.IsPublic)
				{
					return FolderSharingFlag.None;
				}
				if (Utilities.IsFolderSharedOut(this.extendedFolderFlags) || Utilities.IsPublishedOutFolder(this.extendedFolderFlags))
				{
					return FolderSharingFlag.SharedOut;
				}
				return FolderSharingFlag.None;
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x000C3812 File Offset: 0x000C1A12
		protected bool IsELCFolder
		{
			get
			{
				return Utilities.IsELCFolder(this.adminFolderFlags);
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06002256 RID: 8790 RVA: 0x000C381F File Offset: 0x000C1A1F
		protected virtual string ContainerClass
		{
			get
			{
				return this.containerClass;
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06002257 RID: 8791 RVA: 0x000C3827 File Offset: 0x000C1A27
		protected int ItemCount
		{
			get
			{
				return this.itemCount;
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06002258 RID: 8792 RVA: 0x000C382F File Offset: 0x000C1A2F
		protected int UnreadCount
		{
			get
			{
				return this.unreadCount;
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06002259 RID: 8793 RVA: 0x000C3837 File Offset: 0x000C1A37
		private bool IsSearchFolderRoot
		{
			get
			{
				return this.folderType == DefaultFolderType.SearchFolders;
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x0600225A RID: 8794 RVA: 0x000C3843 File Offset: 0x000C1A43
		internal override bool Selectable
		{
			get
			{
				return !this.IsSearchFolderRoot && (this.folderType != DefaultFolderType.Root || !this.FolderId.IsOtherMailbox);
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x0600225B RID: 8795 RVA: 0x000C3869 File Offset: 0x000C1A69
		protected virtual FolderTreeNode.ContentCountDisplayType ContentCountDisplay
		{
			get
			{
				return this.contentCountDisplayType;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x0600225C RID: 8796 RVA: 0x000C3871 File Offset: 0x000C1A71
		protected bool IsNonCacheNode
		{
			get
			{
				return this.folderType == DefaultFolderType.Drafts || this.folderType == DefaultFolderType.SentItems || this.folderType == DefaultFolderType.DeletedItems || this.folderType == DefaultFolderType.Outbox;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x0600225D RID: 8797 RVA: 0x000C389B File Offset: 0x000C1A9B
		private EffectiveRights EffectiveRights
		{
			get
			{
				return this.effectiveRights;
			}
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x000C38A4 File Offset: 0x000C1AA4
		internal static NavigationTreeDirtyFlag GetAffectedTreeFromContainerClass(string folderClass)
		{
			if (string.IsNullOrEmpty(folderClass) || ObjectClass.IsOfClass(folderClass, "IPF.Note"))
			{
				return NavigationTreeDirtyFlag.Favorites;
			}
			if (ObjectClass.IsOfClass(folderClass, "IPF.Appointment"))
			{
				return NavigationTreeDirtyFlag.Calendar;
			}
			if (ObjectClass.IsOfClass(folderClass, "IPF.Contact"))
			{
				return NavigationTreeDirtyFlag.Contact;
			}
			if (ObjectClass.IsOfClass(folderClass, "IPF.Task"))
			{
				return NavigationTreeDirtyFlag.Task;
			}
			return NavigationTreeDirtyFlag.None;
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x000C38F8 File Offset: 0x000C1AF8
		private static void AddChildCount(Dictionary<StoreObjectId, int> childCounts, object parentEntryId)
		{
			if (parentEntryId is byte[])
			{
				StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId((byte[])parentEntryId, StoreObjectType.Folder);
				if (!childCounts.ContainsKey(storeObjectId))
				{
					childCounts[storeObjectId] = 0;
				}
				StoreObjectId key;
				childCounts[key = storeObjectId] = childCounts[key] + 1;
			}
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x000C393F File Offset: 0x000C1B3F
		internal static FolderTreeNode Load(UserContext userContext, OwaStoreObjectId owaStoreObjectId, FolderTreeRenderType renderType)
		{
			return FolderTreeNode.Load(userContext, owaStoreObjectId, false, null, renderType);
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x000C394B File Offset: 0x000C1B4B
		private static FolderTreeNode Load(UserContext userContext, StoreSession storeSession, StoreObjectId storeObjectId)
		{
			return FolderTreeNode.Load(userContext, storeSession, storeObjectId, false, null, FolderTreeRenderType.None);
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x000C3958 File Offset: 0x000C1B58
		private static FolderTreeNode Load(UserContext userContext, StoreSession storeSession, StoreObjectId storeObjectId, bool loadChildren, QueryFilter queryFilter, FolderTreeRenderType renderType)
		{
			FolderTreeNode result;
			using (Folder folder = Folder.Bind(storeSession, storeObjectId, FolderTreeNode.NodeCreateProperties))
			{
				if (!FolderTreeNode.CheckFolderClassInRenderType(folder.ClassName, renderType, false))
				{
					result = null;
				}
				else
				{
					FolderTreeNode folderTreeNode = new FolderTreeNode(userContext, folder);
					if (loadChildren)
					{
						if (Utilities.IsFlagSet((int)renderType, 2) && (Utilities.GetDefaultFolderType(folder) == DefaultFolderType.DeletedItems || Utilities.IsItemInDefaultFolder(folder, DefaultFolderType.DeletedItems)))
						{
							renderType = FolderTreeRenderType.None;
						}
						folderTreeNode.LoadChildren(folder, queryFilter, folderTreeNode.IsSearchFolderRoot, renderType);
					}
					result = folderTreeNode;
				}
			}
			return result;
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x000C39E0 File Offset: 0x000C1BE0
		private static FolderTreeNode Load(UserContext userContext, OwaStoreObjectId owaStoreObjectId, bool loadChildren, QueryFilter queryFilter, FolderTreeRenderType renderType)
		{
			return FolderTreeNode.Load(userContext, owaStoreObjectId.GetSession(userContext), owaStoreObjectId.StoreObjectId, loadChildren, queryFilter, renderType);
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x000C39FC File Offset: 0x000C1BFC
		private static bool CheckFolderClassInRenderType(string folderClass, FolderTreeRenderType renderType, bool isUnderSearchFolderRoot)
		{
			if (Utilities.IsFlagSet((int)renderType, 4) || isUnderSearchFolderRoot)
			{
				if (!string.IsNullOrEmpty(folderClass) && !ObjectClass.IsOfClass(folderClass, "IPF.Note"))
				{
					return false;
				}
			}
			else if (Utilities.IsFlagSet((int)renderType, 2) && (ObjectClass.IsOfClass(folderClass, "IPF.Appointment") || ObjectClass.IsOfClass(folderClass, "IPF.Contact") || ObjectClass.IsOfClass(folderClass, "IPF.Task")))
			{
				return false;
			}
			return true;
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x000C3A60 File Offset: 0x000C1C60
		private static QueryFilter AddFeatureEnabledCondition(UserContext userContext, QueryFilter queryFilter, FolderTreeRenderType renderType)
		{
			List<QueryFilter> list = new List<QueryFilter>();
			if (queryFilter != null)
			{
				list.Add(queryFilter);
			}
			if (Utilities.IsFlagSet((int)renderType, 4))
			{
				list.Add(FolderTreeNode.mailFolderTextFilter);
			}
			else
			{
				if (!userContext.IsFeatureEnabled(Feature.StickyNotes))
				{
					list.Add(new NotFilter(FolderTreeNode.stickyNotesFolderTextFilter));
				}
				if (!userContext.IsFeatureEnabled(Feature.Contacts) || Utilities.IsFlagSet((int)renderType, 2))
				{
					list.Add(new NotFilter(FolderTreeNode.contactsFolderTextFilter));
				}
				if (!userContext.IsFeatureEnabled(Feature.Journal))
				{
					list.Add(new NotFilter(FolderTreeNode.journalFolderTextFilter));
				}
				if (!userContext.IsFeatureEnabled(Feature.Calendar) || Utilities.IsFlagSet((int)renderType, 2))
				{
					list.Add(new NotFilter(FolderTreeNode.calendarFolderTextFilter));
				}
				if (!userContext.IsFeatureEnabled(Feature.Tasks) || Utilities.IsFlagSet((int)renderType, 2))
				{
					list.Add(new NotFilter(FolderTreeNode.taskFolderTextFilter));
				}
			}
			if (list.Count > 1)
			{
				return new AndFilter(list.ToArray());
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			return null;
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x000C3B5C File Offset: 0x000C1D5C
		internal static FolderTreeNode CreateStartPageArchiveMailboxRootNode(UserContext userContext, FolderList deepHierarchyFolderList, FolderList searchFolderList)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (deepHierarchyFolderList == null)
			{
				throw new ArgumentNullException("deepHierarchyFolderList");
			}
			StoreObjectId[] folderIds = deepHierarchyFolderList.GetFolderIds();
			Dictionary<StoreObjectId, int> childCounts = new Dictionary<StoreObjectId, int>(folderIds.Length);
			foreach (StoreObjectId storeObjectId in folderIds)
			{
				if ((int)deepHierarchyFolderList.GetFolderProperty(storeObjectId, FolderSchema.FolderHierarchyDepth) == 2)
				{
					FolderTreeNode.AddChildCount(childCounts, deepHierarchyFolderList.GetFolderProperty(storeObjectId, StoreObjectSchema.ParentEntryId));
				}
			}
			MailboxSession mailboxSession = deepHierarchyFolderList.MailboxSession;
			StoreObjectId rootFolderId = userContext.GetRootFolderId(mailboxSession);
			FolderTreeNode folderTreeNode = new FolderTreeNode(userContext, OwaStoreObjectId.CreateFromArchiveMailboxFolderId(rootFolderId, mailboxSession.MailboxOwnerLegacyDN), Utilities.GetMailboxOwnerDisplayName(mailboxSession), "IPF.Note", DefaultFolderType.Root);
			FolderTreeNode folderTreeNode2 = folderTreeNode;
			folderTreeNode2.NodeClassName += " trNdGpHd";
			folderTreeNode.HasIcon = false;
			folderTreeNode.IsRootNode = true;
			folderTreeNode.IsExpanded = false;
			FolderTreeNode.AddMyUserFoldersToRoot(userContext, deepHierarchyFolderList, folderTreeNode, childCounts, FolderTreeRenderType.HideGeekFoldersWithSpecificOrder);
			FolderTreeNode.AddMyDefaultFolderToRoot(userContext, mailboxSession, DefaultFolderType.DeletedItems, deepHierarchyFolderList, folderTreeNode, childCounts, true);
			if (userContext.IsFeatureEnabled(Feature.SearchFolders) && searchFolderList != null)
			{
				bool flag = false;
				for (int j = 0; j < searchFolderList.Count; j++)
				{
					if (FolderTreeNode.CheckFolderClassInRenderType(searchFolderList.GetPropertyValue(j, StoreObjectSchema.ContainerClass) as string, FolderTreeRenderType.HideGeekFoldersWithSpecificOrder, true))
					{
						object propertyValue = searchFolderList.GetPropertyValue(j, FolderSchema.SearchFolderAllowAgeout);
						object propertyValue2 = searchFolderList.GetPropertyValue(j, FolderSchema.IsOutlookSearchFolder);
						bool flag2 = propertyValue is bool && (bool)propertyValue;
						bool flag3 = propertyValue2 is bool && (bool)propertyValue2;
						if (flag3 && !flag2)
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					folderTreeNode.AddChild(new FolderTreeNode(userContext, userContext.GetSearchFoldersId(mailboxSession), LocalizedStrings.GetNonEncoded(1545482161), "IPF.Note", DefaultFolderType.SearchFolders)
					{
						hasChildren = true
					});
				}
			}
			if (userContext.IsPushNotificationsEnabled)
			{
				userContext.MapiNotificationManager.SubscribeForFolderCounts(null, mailboxSession);
			}
			if (userContext.IsPullNotificationsEnabled)
			{
				userContext.NotificationManager.CreateArchiveOwaFolderCountAdvisor(mailboxSession);
			}
			return folderTreeNode;
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x000C3D50 File Offset: 0x000C1F50
		internal static FolderTreeNode CreateStartPageDummyArchiveMailboxRootNode(UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			StoreObjectId dummyId = StoreObjectId.DummyId;
			FolderTreeNode folderTreeNode = new FolderTreeNode(userContext, OwaStoreObjectId.CreateFromArchiveMailboxFolderId(dummyId, userContext.ArchiveMailboxOwnerLegacyDN), userContext.ArchiveMailboxDisplayName, "IPF.Note", DefaultFolderType.Root);
			FolderTreeNode folderTreeNode2 = folderTreeNode;
			folderTreeNode2.NodeClassName += " trNdGpHd";
			folderTreeNode.HasIcon = false;
			folderTreeNode.IsRootNode = true;
			folderTreeNode.IsExpanded = false;
			folderTreeNode.hasChildren = true;
			folderTreeNode.IsDummy = true;
			return folderTreeNode;
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x000C3DCC File Offset: 0x000C1FCC
		internal static FolderTreeNode CreateFolderPickerDummyArchiveMailboxRootNode(UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			StoreObjectId dummyId = StoreObjectId.DummyId;
			FolderTreeNode folderTreeNode = new FolderTreeNode(userContext, OwaStoreObjectId.CreateFromArchiveMailboxFolderId(dummyId, userContext.ArchiveMailboxOwnerLegacyDN), userContext.ArchiveMailboxDisplayName, "IPF.Note", DefaultFolderType.Root);
			folderTreeNode.IsRootNode = true;
			folderTreeNode.IsExpanded = false;
			folderTreeNode.hasChildren = true;
			FolderTreeNode folderTreeNode2 = folderTreeNode;
			folderTreeNode2.HighlightClassName += " trNdGpHdHl";
			folderTreeNode.IsDummy = true;
			return folderTreeNode;
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x000C3E40 File Offset: 0x000C2040
		internal static FolderTreeNode CreateOtherMailboxRootNode(UserContext userContext, OtherMailboxConfigEntry entry, bool loadChildren)
		{
			OwaStoreObjectId owaStoreObjectId = null;
			try
			{
				owaStoreObjectId = OwaStoreObjectId.CreateFromString(entry.RootFolderId);
			}
			catch (OwaInvalidIdFormatException arg)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug<string, OwaInvalidIdFormatException>(0L, "Invalid format for Folder ID string: {0}. Exception: {1}", entry.RootFolderId, arg);
			}
			if (owaStoreObjectId == null || !owaStoreObjectId.IsOtherMailbox)
			{
				return null;
			}
			return FolderTreeNode.CreateOtherMailboxRootNode(userContext, owaStoreObjectId, entry.DisplayName, loadChildren);
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x000C3EA4 File Offset: 0x000C20A4
		internal static FolderTreeNode CreateOtherMailboxRootNode(UserContext userContext, OwaStoreObjectId rootFolderId, string displayName, bool loadChildren)
		{
			if (!rootFolderId.IsOtherMailbox)
			{
				throw new ArgumentException("Should pass an folder Id that is in other's mailbox.");
			}
			FolderTreeNode folderTreeNode = new FolderTreeNode(userContext, rootFolderId, displayName, "IPF.Note", DefaultFolderType.Root);
			folderTreeNode.hasChildren = true;
			FolderTreeNode folderTreeNode2 = folderTreeNode;
			folderTreeNode2.NodeClassName += " trNdGpHd";
			folderTreeNode.HasIcon = false;
			folderTreeNode.IsRootNode = true;
			if (loadChildren)
			{
				folderTreeNode.IsExpanded = true;
				try
				{
					MailboxSession mailboxSession = (MailboxSession)rootFolderId.GetSession(userContext);
					StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox);
					if (defaultFolderId == null)
					{
						return null;
					}
					FolderTreeNode folderTreeNode3 = FolderTreeNode.Load(userContext, mailboxSession, defaultFolderId);
					folderTreeNode3.hasChildren = false;
					folderTreeNode.AddChild(folderTreeNode3);
					OwaStoreObjectId delegateFolderId = OwaStoreObjectId.CreateFromOtherUserMailboxFolderId(defaultFolderId, mailboxSession.MailboxOwnerLegacyDN);
					if (userContext.IsPushNotificationsEnabled)
					{
						userContext.MapiNotificationManager.SubscribeForFolderCounts(delegateFolderId, mailboxSession);
					}
					if (userContext.IsPullNotificationsEnabled)
					{
						OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromOtherUserMailboxFolderId(defaultFolderId, mailboxSession.MailboxOwnerLegacyDN);
						userContext.NotificationManager.CreateDelegateOwaFolderCountAdvisor(mailboxSession, owaStoreObjectId, EventObjectType.Folder, EventType.ObjectModified);
					}
				}
				catch (ObjectNotFoundException)
				{
					return null;
				}
				return folderTreeNode;
			}
			return folderTreeNode;
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x000C3FA8 File Offset: 0x000C21A8
		private static bool CheckFolderHasChild(Dictionary<StoreObjectId, int> childCounts, StoreObjectId folderId)
		{
			int num = 0;
			return childCounts.TryGetValue(folderId, out num) && num > 0;
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x000C3FC8 File Offset: 0x000C21C8
		private static bool AddMyDefaultFolderToRoot(UserContext userContext, MailboxSession mailboxSession, DefaultFolderType type, FolderList deepHierarchyFolderList, FolderTreeNode rootNode, Dictionary<StoreObjectId, int> childCounts, bool isArchiveMailbox)
		{
			StoreObjectId storeObjectId = Utilities.TryGetDefaultFolderId(mailboxSession, type);
			if (storeObjectId != null)
			{
				object[] folderProperties = deepHierarchyFolderList.GetFolderProperties(storeObjectId);
				if (folderProperties != null)
				{
					FolderTreeNode folderTreeNode = new FolderTreeNode(userContext, mailboxSession, folderProperties, deepHierarchyFolderList.QueryPropertyMap);
					if (!Utilities.IsDefaultFolderId(mailboxSession, storeObjectId, DefaultFolderType.DeletedItems))
					{
						folderTreeNode.hasChildren = FolderTreeNode.CheckFolderHasChild(childCounts, storeObjectId);
					}
					else if (!isArchiveMailbox)
					{
						FolderTreeNode folderTreeNode2 = folderTreeNode;
						folderTreeNode2.HighlightClassName += " trNdDelFol";
					}
					rootNode.AddChild(folderTreeNode);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x000C403C File Offset: 0x000C223C
		private static void AddMyUserFoldersToRoot(UserContext userContext, FolderList deepHierarchyFolderList, FolderTreeNode rootNode, Dictionary<StoreObjectId, int> childCounts, FolderTreeRenderType renderType)
		{
			int num = deepHierarchyFolderList.QueryPropertyMap[StoreObjectSchema.ContainerClass];
			int num2 = deepHierarchyFolderList.QueryPropertyMap[FolderSchema.FolderHierarchyDepth];
			int num3 = deepHierarchyFolderList.QueryPropertyMap[FolderSchema.AdminFolderFlags];
			StoreObjectId[] folderIds = deepHierarchyFolderList.GetFolderIds();
			List<FolderTreeNode> list = new List<FolderTreeNode>(folderIds.Length);
			MailboxSession mailboxSession = deepHierarchyFolderList.MailboxSession;
			foreach (StoreObjectId storeObjectId in folderIds)
			{
				object[] folderProperties = deepHierarchyFolderList.GetFolderProperties(storeObjectId);
				if (folderProperties != null && (int)folderProperties[num2] == 1 && !Utilities.IsSpecialFolderForSession(mailboxSession, storeObjectId) && !Utilities.IsELCRootFolder(folderProperties[num3]))
				{
					string text = folderProperties[num] as string;
					if (string.IsNullOrEmpty(text) || FolderTreeNode.CheckFolderClassInRenderType(text, renderType, false))
					{
						list.Add(new FolderTreeNode(userContext, mailboxSession, folderProperties, deepHierarchyFolderList.QueryPropertyMap)
						{
							hasChildren = FolderTreeNode.CheckFolderHasChild(childCounts, storeObjectId)
						});
					}
				}
			}
			list.Sort(new FolderTreeNode.FolderTreeNodeComparer());
			foreach (FolderTreeNode child in list)
			{
				rootNode.AddChild(child);
			}
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x000C4184 File Offset: 0x000C2384
		private static void AddRestOfFoldersToRoot(UserContext userContext, FolderList deepHierarchyFolderList, FolderList searchFolderList, FolderTreeNode rootNode, Dictionary<StoreObjectId, int> childCounts, FolderTreeRenderType renderType)
		{
			int num = deepHierarchyFolderList.QueryPropertyMap[StoreObjectSchema.ContainerClass];
			int num2 = deepHierarchyFolderList.QueryPropertyMap[FolderSchema.FolderHierarchyDepth];
			int num3 = deepHierarchyFolderList.QueryPropertyMap[FolderSchema.AdminFolderFlags];
			StoreObjectId[] folderIds = deepHierarchyFolderList.GetFolderIds();
			List<FolderTreeNode> list = new List<FolderTreeNode>(folderIds.Length);
			MailboxSession mailboxSession = deepHierarchyFolderList.MailboxSession;
			foreach (StoreObjectId storeObjectId in folderIds)
			{
				object[] folderProperties = deepHierarchyFolderList.GetFolderProperties(storeObjectId);
				if (folderProperties != null && (int)folderProperties[num2] == 1)
				{
					object obj = folderProperties[num];
					if (Utilities.IsSpecialFolderForSession(mailboxSession, storeObjectId))
					{
						DefaultFolderType defaultFolderType = Utilities.GetDefaultFolderType(mailboxSession, storeObjectId);
						if (!FolderTreeNode.defaultFolderList.Contains(defaultFolderType))
						{
							goto IL_C7;
						}
					}
					list.Add(new FolderTreeNode(userContext, mailboxSession, folderProperties, deepHierarchyFolderList.QueryPropertyMap)
					{
						hasChildren = FolderTreeNode.CheckFolderHasChild(childCounts, storeObjectId)
					});
				}
				IL_C7:;
			}
			if (userContext.IsFeatureEnabled(Feature.SearchFolders) && searchFolderList != null)
			{
				bool flag = false;
				for (int j = 0; j < searchFolderList.Count; j++)
				{
					if (FolderTreeNode.CheckFolderClassInRenderType(searchFolderList.GetPropertyValue(j, StoreObjectSchema.ContainerClass) as string, FolderTreeRenderType.HideGeekFoldersWithSpecificOrder, true))
					{
						object propertyValue = searchFolderList.GetPropertyValue(j, FolderSchema.SearchFolderAllowAgeout);
						object propertyValue2 = searchFolderList.GetPropertyValue(j, FolderSchema.IsOutlookSearchFolder);
						bool flag2 = propertyValue is bool && (bool)propertyValue;
						bool flag3 = propertyValue2 is bool && (bool)propertyValue2;
						if (flag3 && !flag2)
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					list.Add(new FolderTreeNode(userContext, userContext.GetSearchFoldersId(userContext.MailboxSession), LocalizedStrings.GetNonEncoded(1545482161), "IPF.Note", DefaultFolderType.SearchFolders)
					{
						hasChildren = true
					});
				}
			}
			list.Sort(new FolderTreeNode.FolderTreeNodeComparer());
			foreach (FolderTreeNode child in list)
			{
				rootNode.AddChild(child);
			}
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x000C4390 File Offset: 0x000C2590
		internal static FolderTreeNode CreateStartPageMailboxRootNode(UserContext userContext, FolderList deepHierarchyFolderList, FolderList searchFolderList)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (deepHierarchyFolderList == null)
			{
				throw new ArgumentNullException("deepHierarchyFolderList");
			}
			StoreObjectId[] folderIds = deepHierarchyFolderList.GetFolderIds();
			Dictionary<StoreObjectId, int> childCounts = new Dictionary<StoreObjectId, int>(folderIds.Length);
			foreach (StoreObjectId storeObjectId in folderIds)
			{
				if ((int)deepHierarchyFolderList.GetFolderProperty(storeObjectId, FolderSchema.FolderHierarchyDepth) == 2)
				{
					FolderTreeNode.AddChildCount(childCounts, deepHierarchyFolderList.GetFolderProperty(storeObjectId, StoreObjectSchema.ParentEntryId));
				}
			}
			StoreObjectId rootFolderId = userContext.GetRootFolderId(userContext.MailboxSession);
			FolderTreeNode folderTreeNode = new FolderTreeNode(userContext, OwaStoreObjectId.CreateFromMailboxFolderId(rootFolderId), Utilities.GetMailboxOwnerDisplayName(userContext.MailboxSession), "IPF.Note", DefaultFolderType.Root);
			FolderTreeNode folderTreeNode2 = folderTreeNode;
			folderTreeNode2.NodeClassName += " trNdGpHd";
			folderTreeNode.HasIcon = false;
			folderTreeNode.IsRootNode = true;
			object folderProperty = deepHierarchyFolderList.GetFolderProperty(rootFolderId, ViewStateProperties.TreeNodeCollapseStatus);
			if (folderProperty is int)
			{
				folderTreeNode.IsExpanded = !Utilities.IsFlagSet((int)folderProperty, 1);
			}
			else
			{
				folderTreeNode.IsExpanded = true;
			}
			DefaultFolderType[] array2 = new DefaultFolderType[]
			{
				DefaultFolderType.Inbox,
				DefaultFolderType.Drafts,
				DefaultFolderType.SentItems,
				DefaultFolderType.DeletedItems
			};
			foreach (DefaultFolderType type in array2)
			{
				FolderTreeNode.AddMyDefaultFolderToRoot(userContext, userContext.MailboxSession, type, deepHierarchyFolderList, folderTreeNode, childCounts, false);
			}
			FolderTreeNode.AddRestOfFoldersToRoot(userContext, deepHierarchyFolderList, searchFolderList, folderTreeNode, childCounts, FolderTreeRenderType.MailFoldersOnly);
			return folderTreeNode;
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x000C44F8 File Offset: 0x000C26F8
		internal static FolderTreeNode CreateMailboxFolderTreeRootNode(UserContext userContext, MailboxSession mailboxSession, FolderTreeRenderType renderType)
		{
			FolderTreeNode folderTreeNode = FolderTreeNode.Load(userContext, mailboxSession, userContext.GetRootFolderId(mailboxSession), true, FolderTreeNode.nonHiddenExcludeUnsupportedFilter, renderType);
			if (userContext.IsFeatureEnabled(Feature.SearchFolders) && !Utilities.IsFlagSet((int)renderType, 1))
			{
				StoreObjectId storeObjectId = Utilities.TryGetDefaultFolderId(mailboxSession, DefaultFolderType.SearchFolders);
				if (storeObjectId != null)
				{
					FolderTreeNode folderTreeNode2 = FolderTreeNode.Load(userContext, mailboxSession, storeObjectId);
					if (folderTreeNode2.HasChildren)
					{
						folderTreeNode.AddChild(folderTreeNode2);
					}
				}
			}
			return folderTreeNode;
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x000C4557 File Offset: 0x000C2757
		internal static FolderTreeNode CreateMailboxFolderTreeNode(UserContext userContext, MailboxSession mailboxSession, StoreObjectId storeObjectId, FolderTreeRenderType renderType)
		{
			if (Utilities.IsDefaultFolderId(mailboxSession, storeObjectId, DefaultFolderType.Root))
			{
				return FolderTreeNode.CreateMailboxFolderTreeRootNode(userContext, mailboxSession, renderType);
			}
			return FolderTreeNode.Load(userContext, mailboxSession, storeObjectId, true, FolderTreeNode.nonHiddenExcludeUnsupportedFilter, renderType);
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x000C457C File Offset: 0x000C277C
		internal static List<FolderTreeNode> CreateDeletedNodesWithDirtyCheck(UserContext userContext, ICollection<OwaStoreObjectId> deletedFolderIds, out NavigationTreeDirtyFlag dirtyFlag)
		{
			dirtyFlag = NavigationTreeDirtyFlag.None;
			List<FolderTreeNode> list = new List<FolderTreeNode>();
			List<OwaStoreObjectId> list2 = new List<OwaStoreObjectId>();
			foreach (OwaStoreObjectId owaStoreObjectId in deletedFolderIds)
			{
				MailboxSession mailboxSession = owaStoreObjectId.GetSession(userContext) as MailboxSession;
				if (mailboxSession != null)
				{
					StoreObjectId storeObjectId = Utilities.TryGetDefaultFolderId(mailboxSession, DefaultFolderType.DeletedItems);
					if (storeObjectId != null && !list2.Contains(OwaStoreObjectId.CreateFromSessionFolderId(userContext, mailboxSession, storeObjectId)))
					{
						list2.Add(OwaStoreObjectId.CreateFromSessionFolderId(userContext, mailboxSession, storeObjectId));
					}
				}
			}
			foreach (OwaStoreObjectId owaStoreObjectId2 in list2)
			{
				Dictionary<PropertyDefinition, int> folderTreePropertyIndexes = FolderList.FolderTreePropertyIndexes;
				int num = folderTreePropertyIndexes[FolderSchema.FolderHierarchyDepth];
				int num2 = folderTreePropertyIndexes[FolderSchema.Id];
				int num3 = folderTreePropertyIndexes[StoreObjectSchema.ContainerClass];
				MailboxSession mailboxSession2 = (MailboxSession)owaStoreObjectId2.GetSession(userContext);
				Folder folder = Utilities.SafeFolderBind(mailboxSession2, owaStoreObjectId2.StoreObjectId, new PropertyDefinition[0]);
				if (folder != null)
				{
					using (folder)
					{
						object[][] array;
						using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.DeepTraversal, FolderTreeNode.nonHiddenExcludeUnsupportedFilter, null, FolderList.FolderTreeQueryProperties))
						{
							array = Utilities.FetchRowsFromQueryResult(queryResult, 10000);
						}
						bool flag = false;
						for (int i = 0; i < array.Length; i++)
						{
							if ((int)array[i][num] == 1)
							{
								StoreObjectId objectId = ((VersionedId)array[i][num2]).ObjectId;
								OwaStoreObjectId item = OwaStoreObjectId.CreateFromSessionFolderId(userContext, mailboxSession2, objectId);
								flag = deletedFolderIds.Contains(item);
								if (flag)
								{
									list.Add(new FolderTreeNode(userContext, mailboxSession2, array[i], folderTreePropertyIndexes));
								}
							}
							if (flag)
							{
								dirtyFlag |= FolderTreeNode.GetAffectedTreeFromContainerClass(array[i][num3] as string);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x000C47BC File Offset: 0x000C29BC
		internal static FolderTreeNode CreatePublicFolderTreeRootNode(UserContext userContext)
		{
			return FolderTreeNode.CreatePublicFolderTreeNode(userContext, userContext.PublicFolderRootId);
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x000C47CA File Offset: 0x000C29CA
		internal static FolderTreeNode CreatePublicFolderTreeNode(UserContext userContext, StoreObjectId storeObjectId)
		{
			return FolderTreeNode.Load(userContext, userContext.DefaultPublicFolderSession, storeObjectId, true, FolderTreeNode.nonHiddenFilter, FolderTreeRenderType.None);
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x000C47E0 File Offset: 0x000C29E0
		private T GetPropertyValue<T>(object[] properties, PropertyDefinition propertyDefinition, Dictionary<PropertyDefinition, int> propertyMap, T defaultValue)
		{
			if (!propertyMap.ContainsKey(propertyDefinition))
			{
				return defaultValue;
			}
			object obj = properties[propertyMap[propertyDefinition]];
			if (obj == null || obj is PropertyError)
			{
				return defaultValue;
			}
			return (T)((object)obj);
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x000C4818 File Offset: 0x000C2A18
		protected override void RenderAdditionalProperties(TextWriter writer)
		{
			writer.Write(" _t=\"folderTreeNode\"");
			if (this.IsSearchFolder)
			{
				writer.Write(" _osf=1");
			}
			if (this.FolderType != DefaultFolderType.None)
			{
				writer.Write(" _ft=");
				writer.Write((int)this.FolderType);
			}
			if (this.adminFolderFlags > 0)
			{
				writer.Write(" _iAFlg=");
				writer.Write(this.adminFolderFlags);
			}
			if (this.FolderId.IsPublic)
			{
				writer.Write(" _iER=");
				writer.Write((int)this.effectiveRights);
			}
			if (this.FolderId.IsPublic && base.UserContext.IsPublicFolderRootId(this.FolderId.StoreObjectId))
			{
				writer.Write(" _iPFR=1");
			}
			if (this.hasPolicyIds)
			{
				writer.Write(" _fPlcy=1");
			}
			writer.Write(" _eff=");
			writer.Write((int)this.extendedFolderFlags);
			writer.Write(" _fcc=\"");
			Utilities.HtmlEncode(this.ContainerClass, writer);
			writer.Write("\"");
			if (this.ContentCountDisplay == FolderTreeNode.ContentCountDisplayType.UnreadCount || this.ContentCountDisplay == FolderTreeNode.ContentCountDisplayType.ItemCount)
			{
				writer.Write(" ");
				writer.Write("_ccdt");
				writer.Write("=");
				writer.Write((int)this.ContentCountDisplay);
			}
			writer.Write(" _iUC=");
			writer.Write(this.UnreadCount);
			writer.Write(" _iTC=");
			writer.Write(this.ItemCount);
			if (this.IsNonCacheNode)
			{
				writer.Write(" _nc=1");
			}
			ExchangePrincipal exchangePrincipal;
			if (this.FolderId.IsOtherMailbox && base.Parent != null && base.UserContext.DelegateSessionManager.TryGetExchangePrincipal(this.FolderId.MailboxOwnerLegacyDN, out exchangePrincipal))
			{
				writer.Write(" guid=");
				writer.Write(exchangePrincipal.MailboxInfo.MailboxGuid.ToString());
			}
			base.RenderAdditionalProperties(writer);
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x000C4A08 File Offset: 0x000C2C08
		protected override void RenderIcon(TextWriter writer, params string[] extraAttributes)
		{
			string text = null;
			if (this.folderId.IsArchive && this.folderId.StoreObjectId.IsFolderId && this.ContainerClass != "IPF.Appointment" && this.ContainerClass != "IPF.Contact" && this.ContainerClass != "IPF.Task")
			{
				text = "IPF.Note";
			}
			RenderingUtilities.RenderSpecialFolderIcon(writer, base.UserContext, this.folderType, text ?? this.ContainerClass, this.IsSearchFolder, this.IsELCFolder, this.FolderSharingFlag, extraAttributes);
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x000C4AA4 File Offset: 0x000C2CA4
		protected override void RenderContent(TextWriter writer)
		{
			writer.Write("<span id=\"spnFldrNm\"");
			if (!string.IsNullOrEmpty(this.folderName))
			{
				writer.Write(" fldrNm=\"");
				Utilities.HtmlEncode(this.folderName, writer, false);
				writer.Write("\"");
			}
			if ((this.ContentCountDisplay == FolderTreeNode.ContentCountDisplayType.ItemCount && this.ItemCount > 0) || (this.ContentCountDisplay == FolderTreeNode.ContentCountDisplayType.UnreadCount && this.UnreadCount > 0))
			{
				writer.Write(" class=\"");
				writer.Write(FolderTreeNode.folderFontWeightClass);
				writer.Write("\"");
			}
			writer.Write(">");
			Utilities.HtmlEncode(this.DisplayName, writer, true);
			writer.Write("</span>");
			if (this.ContentCountDisplay == FolderTreeNode.ContentCountDisplayType.ItemCount && this.ItemCount > 0)
			{
				writer.Write("<span id=spnC>");
				writer.Write(base.UserContext.DirectionMark);
				writer.Write("[<span id=spnCV>");
				writer.Write(this.ItemCount);
				writer.Write("</span>]");
				writer.Write(base.UserContext.DirectionMark);
				writer.Write("</span>");
				return;
			}
			if (this.ContentCountDisplay == FolderTreeNode.ContentCountDisplayType.UnreadCount && this.UnreadCount > 0)
			{
				writer.Write("<span id=spnUC>");
				writer.Write(base.UserContext.DirectionMark);
				writer.Write("(<span id=spnCV>");
				writer.Write(this.UnreadCount);
				writer.Write("</span>)");
				writer.Write(base.UserContext.DirectionMark);
				writer.Write("</span>");
			}
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x000C4C30 File Offset: 0x000C2E30
		private void LoadChildren(Folder folder, QueryFilter queryFilter, bool isUnderOutlookSearchFolder, FolderTreeRenderType renderType)
		{
			Dictionary<PropertyDefinition, int> folderTreePropertyIndexes = FolderList.FolderTreePropertyIndexes;
			int num = folderTreePropertyIndexes[FolderSchema.Id];
			int num2 = folderTreePropertyIndexes[FolderSchema.IsOutlookSearchFolder];
			int num3 = folderTreePropertyIndexes[FolderSchema.FolderHierarchyDepth];
			int num4 = folderTreePropertyIndexes[StoreObjectSchema.ParentEntryId];
			int num5 = folderTreePropertyIndexes[StoreObjectSchema.ContainerClass];
			int num6 = folderTreePropertyIndexes[FolderSchema.SearchFolderAllowAgeout];
			bool flag = false;
			queryFilter = FolderTreeNode.AddFeatureEnabledCondition(base.UserContext, queryFilter, renderType);
			if (Utilities.IsFlagSet((int)renderType, 2) || Utilities.IsFlagSet((int)renderType, 4))
			{
				flag = true;
			}
			object[][] array;
			using (QueryResult queryResult = folder.FolderQuery(flag ? FolderQueryFlags.DeepTraversal : FolderQueryFlags.None, queryFilter, null, FolderList.FolderTreeQueryProperties))
			{
				array = Utilities.FetchRowsFromQueryResult(queryResult, 10000);
			}
			Dictionary<StoreObjectId, int> childCounts = new Dictionary<StoreObjectId, int>(array.Length);
			if (flag)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if ((int)array[i][num3] == 2)
					{
						FolderTreeNode.AddChildCount(childCounts, array[i][num4]);
					}
				}
			}
			for (int j = 0; j < array.Length; j++)
			{
				if ((!flag || (int)array[j][num3] == 1) && (!isUnderOutlookSearchFolder || !(array[j][num2] is bool) || (bool)array[j][num2]) && (!isUnderOutlookSearchFolder || !(array[j][num6] is bool) || !(bool)array[j][num6]) && FolderTreeNode.CheckFolderClassInRenderType(array[j][num5] as string, renderType, isUnderOutlookSearchFolder))
				{
					StoreObjectId objectId = ((VersionedId)array[j][num]).ObjectId;
					MailboxSession mailboxSession = folder.Session as MailboxSession;
					if (mailboxSession == null || !objectId.Equals(Utilities.GetDefaultFolderId(mailboxSession, DefaultFolderType.Outbox)))
					{
						FolderTreeNode folderTreeNode = new FolderTreeNode(base.UserContext, folder.Session, array[j], folderTreePropertyIndexes);
						if (flag && (Utilities.IsFlagSet((int)renderType, 4) || !Utilities.IsDefaultFolderId(folder.Session, objectId, DefaultFolderType.DeletedItems)))
						{
							folderTreeNode.hasChildren = FolderTreeNode.CheckFolderHasChild(childCounts, objectId);
						}
						base.AddChild(folderTreeNode);
					}
				}
			}
		}

		// Token: 0x04001813 RID: 6163
		private const string ContentCountDisplayTypeProperty = "_ccdt";

		// Token: 0x04001814 RID: 6164
		internal const string NewFolderCustomAttribute = " _NF=1";

		// Token: 0x04001815 RID: 6165
		private static readonly ComparisonFilter nonHiddenFilter = new ComparisonFilter(ComparisonOperator.NotEqual, FolderSchema.IsHidden, true);

		// Token: 0x04001816 RID: 6166
		private static readonly PropertyDefinition[] NodeCreateProperties = new PropertyDefinition[]
		{
			FolderSchema.Id,
			FolderSchema.DisplayName,
			FolderSchema.UnreadCount,
			FolderSchema.ItemCount,
			StoreObjectSchema.ContainerClass,
			FolderSchema.HasChildren,
			FolderSchema.ExtendedFolderFlags,
			FolderSchema.IsOutlookSearchFolder,
			FolderSchema.AdminFolderFlags,
			FolderSchema.ELCPolicyIds,
			StoreObjectSchema.EffectiveRights
		};

		// Token: 0x04001817 RID: 6167
		private static readonly Dictionary<PropertyDefinition, int> nodeCreatePropertyMap = Utilities.GetPropertyToIndexMap(FolderTreeNode.NodeCreateProperties);

		// Token: 0x04001818 RID: 6168
		private static readonly DefaultFolderType[] OtherDefaultSystemFolderTypes = new DefaultFolderType[]
		{
			DefaultFolderType.CommunicatorHistory,
			DefaultFolderType.ElcRoot,
			DefaultFolderType.Notes,
			DefaultFolderType.JunkEmail,
			DefaultFolderType.RssSubscription
		};

		// Token: 0x04001819 RID: 6169
		private static readonly List<DefaultFolderType> defaultFolderList = new List<DefaultFolderType>(FolderTreeNode.OtherDefaultSystemFolderTypes);

		// Token: 0x0400181A RID: 6170
		private readonly OwaStoreObjectId folderId;

		// Token: 0x0400181B RID: 6171
		private readonly string folderName;

		// Token: 0x0400181C RID: 6172
		private readonly string displayName;

		// Token: 0x0400181D RID: 6173
		private readonly string containerClass = "IPF.Note";

		// Token: 0x0400181E RID: 6174
		private static readonly QueryFilter stickyNotesFolderTextFilter = Utilities.GetObjectClassTypeFilter(true, new string[]
		{
			"IPF.StickyNote"
		});

		// Token: 0x0400181F RID: 6175
		private static readonly QueryFilter calendarFolderTextFilter = Utilities.GetObjectClassTypeFilter(true, new string[]
		{
			"IPF.Appointment"
		});

		// Token: 0x04001820 RID: 6176
		private static readonly QueryFilter contactsFolderTextFilter = Utilities.GetObjectClassTypeFilter(true, new string[]
		{
			"IPF.Contact"
		});

		// Token: 0x04001821 RID: 6177
		private static readonly QueryFilter taskFolderTextFilter = Utilities.GetObjectClassTypeFilter(true, new string[]
		{
			"IPF.Task"
		});

		// Token: 0x04001822 RID: 6178
		private static readonly QueryFilter journalFolderTextFilter = Utilities.GetObjectClassTypeFilter(true, new string[]
		{
			"IPF.Journal"
		});

		// Token: 0x04001823 RID: 6179
		private static readonly QueryFilter mailFolderTextFilter = new TextFilter(StoreObjectSchema.ContainerClass, "IPF.Note", MatchOptions.Prefix, MatchFlags.IgnoreCase);

		// Token: 0x04001824 RID: 6180
		private static readonly QueryFilter nonHiddenExcludeUnsupportedFilter = new AndFilter(new QueryFilter[]
		{
			FolderTreeNode.nonHiddenFilter,
			new NotFilter(FolderTreeNode.journalFolderTextFilter)
		});

		// Token: 0x04001825 RID: 6181
		private static string folderFontWeightClass = "fufw";

		// Token: 0x04001826 RID: 6182
		private int unreadCount;

		// Token: 0x04001827 RID: 6183
		private int itemCount;

		// Token: 0x04001828 RID: 6184
		private bool hasChildren;

		// Token: 0x04001829 RID: 6185
		private ExtendedFolderFlags extendedFolderFlags;

		// Token: 0x0400182A RID: 6186
		private bool isOutlookSearchFolder;

		// Token: 0x0400182B RID: 6187
		private int adminFolderFlags;

		// Token: 0x0400182C RID: 6188
		private bool hasPolicyIds;

		// Token: 0x0400182D RID: 6189
		private EffectiveRights effectiveRights;

		// Token: 0x0400182E RID: 6190
		private DefaultFolderType folderType;

		// Token: 0x0400182F RID: 6191
		private FolderTreeNode.ContentCountDisplayType contentCountDisplayType;

		// Token: 0x0200038A RID: 906
		internal enum ContentCountDisplayType
		{
			// Token: 0x04001831 RID: 6193
			None,
			// Token: 0x04001832 RID: 6194
			ItemCount,
			// Token: 0x04001833 RID: 6195
			UnreadCount
		}

		// Token: 0x0200038B RID: 907
		private class FolderTreeNodeComparer : IComparer<FolderTreeNode>
		{
			// Token: 0x0600227B RID: 8827 RVA: 0x000C4FF8 File Offset: 0x000C31F8
			public int Compare(FolderTreeNode x, FolderTreeNode y)
			{
				return string.Compare(x.DisplayName, y.DisplayName, StringComparison.CurrentCultureIgnoreCase);
			}
		}
	}
}
