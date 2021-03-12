using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003BD RID: 957
	internal class MessageVirtualListView2 : VirtualListView2
	{
		// Token: 0x060023C3 RID: 9155 RVA: 0x000CE000 File Offset: 0x000CC200
		public MessageVirtualListView2(UserContext userContext, string id, bool isMultiLine, ColumnId sortedColumn, SortOrder sortOrder, Folder contextFolder, Folder dataFolder, QueryFilter queryFilter, SearchScope folderScope, bool isInSearch, FolderVirtualListViewFilter folderFilter) : base(userContext, id, isMultiLine, sortedColumn, sortOrder)
		{
			this.contextFolder = contextFolder;
			this.dataFolder = dataFolder;
			this.queryFilter = queryFilter;
			this.folderScope = folderScope;
			this.isInSearch = isInSearch;
			this.folderFilter = folderFilter;
			this.favoritesFolderFilter = Utilities.GetFavoritesFilterViewParameter(base.UserContext, this.contextFolder);
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x000CE064 File Offset: 0x000CC264
		public MessageVirtualListView2(UserContext userContext, string id, bool isMultiLine, ColumnId sortedColumn, SortOrder sortOrder, Folder contextFolder, Folder dataFolder, QueryFilter queryFilter, SearchScope folderScope) : this(userContext, id, isMultiLine, sortedColumn, sortOrder, contextFolder, dataFolder, queryFilter, folderScope, false, null)
		{
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x060023C5 RID: 9157 RVA: 0x000CE088 File Offset: 0x000CC288
		protected override bool IsInSearch
		{
			get
			{
				return this.isInSearch;
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x060023C6 RID: 9158 RVA: 0x000CE090 File Offset: 0x000CC290
		protected override Folder DataFolder
		{
			get
			{
				return this.dataFolder;
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x060023C7 RID: 9159 RVA: 0x000CE098 File Offset: 0x000CC298
		public override ViewType ViewType
		{
			get
			{
				return ViewType.Message;
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x060023C8 RID: 9160 RVA: 0x000CE09B File Offset: 0x000CC29B
		public override string OehNamespace
		{
			get
			{
				return "MsgVLV2";
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x060023C9 RID: 9161 RVA: 0x000CE0A2 File Offset: 0x000CC2A2
		protected override bool IsMultiLine
		{
			get
			{
				return this.IsConversationView || base.IsMultiLine;
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x060023CA RID: 9162 RVA: 0x000CE0B4 File Offset: 0x000CC2B4
		private bool IsConversationView
		{
			get
			{
				return ConversationUtilities.IsConversationSortColumn(base.SortedColumn);
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x060023CB RID: 9163 RVA: 0x000CE0C1 File Offset: 0x000CC2C1
		private bool IsFilteredByUnread
		{
			get
			{
				return (this.folderFilter != null && this.folderFilter.IsUnread) || (this.favoritesFolderFilter != null && this.favoritesFolderFilter.IsUnread);
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x060023CC RID: 9164 RVA: 0x000CE0F0 File Offset: 0x000CC2F0
		private bool IsContextFolderFilteredByUnread
		{
			get
			{
				if (this.IsOutlookSearchFolderFilteredByUnread(this.contextFolder))
				{
					return true;
				}
				SearchFolder searchFolder = this.contextFolder as SearchFolder;
				if (searchFolder == null)
				{
					return false;
				}
				StoreId[] array = searchFolder.GetSearchCriteria().FolderScope;
				foreach (StoreId storeId in array)
				{
					StoreObjectId folderId = storeId as StoreObjectId;
					using (Folder folder = Folder.Bind(this.contextFolder.Session, folderId))
					{
						if (this.IsOutlookSearchFolderFilteredByUnread(folder))
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x060023CD RID: 9165 RVA: 0x000CE194 File Offset: 0x000CC394
		protected override bool ShouldSubscribeForFolderContentChanges
		{
			get
			{
				return Globals.IsPushNotificationsEnabled && base.UserContext.IsPushNotificationsEnabled && OwaMapiNotificationManager.IsNotificationEnabled(base.UserContext) && Globals.IsFolderContentNotificationsEnabled && !this.isInSearch && !base.UserContext.IsWebPartRequest && !ConversationUtilities.IsMultiValuedConversationSortColumn(base.SortedColumn) && !Utilities.IsOtherMailbox(this.contextFolder) && !Utilities.IsArchiveMailbox(this.contextFolder.Session) && !Utilities.IsPublic(this.contextFolder);
			}
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x000CE21C File Offset: 0x000CC41C
		protected override ListViewContents2 CreateListViewContents()
		{
			DefaultFolderType defaultFolderType = Utilities.GetDefaultFolderType(this.contextFolder);
			bool renderLastModifiedTime = MessagePrefetchConfiguration.IsMessagePrefetchEnabledForSession(base.UserContext, this.dataFolder.Session);
			ListViewContents2 listViewContents;
			if (this.IsConversationView)
			{
				listViewContents = new ConversationItemList2(base.SortedColumn, base.SortOrder, base.UserContext, this.folderScope, defaultFolderType, OwaStoreObjectId.CreateFromStoreObject(this.contextFolder), renderLastModifiedTime);
				switch (base.SortedColumn)
				{
				case ColumnId.ConversationLastDeliveryTime:
					listViewContents = new TimeGroupByList2(ColumnId.ConversationLastDeliveryTime, base.SortOrder, (ItemList2)listViewContents, base.UserContext);
					break;
				case ColumnId.ConversationIcon:
				case ColumnId.ConversationSubject:
				case ColumnId.ConversationHasAttachment:
				case ColumnId.ConversationSenderList:
				case ColumnId.ConversationImportance:
				case ColumnId.ConversationFlagStatus:
				case ColumnId.ConversationToList:
					listViewContents = base.CreateGroupByList(listViewContents);
					break;
				case ColumnId.ConversationSize:
					listViewContents = new SizeGroupByList2(ColumnId.ConversationSize, base.SortOrder, (ItemList2)listViewContents, base.UserContext);
					break;
				}
				return listViewContents;
			}
			ViewDescriptor viewDescriptor;
			if (defaultFolderType == DefaultFolderType.Drafts)
			{
				viewDescriptor = MessageVirtualListView2.MultiLineToDrafts;
			}
			else if (defaultFolderType == DefaultFolderType.DeletedItems)
			{
				viewDescriptor = MessageVirtualListView2.MultiLineFromDeletedItems;
			}
			else if (defaultFolderType == DefaultFolderType.SentItems || defaultFolderType == DefaultFolderType.Outbox)
			{
				viewDescriptor = MessageVirtualListView2.MultiLineTo;
			}
			else
			{
				viewDescriptor = MessageVirtualListView2.MultiLineFrom;
			}
			listViewContents = new MessageMultiLineList2(viewDescriptor, base.SortedColumn, base.SortOrder, base.UserContext, this.folderScope, renderLastModifiedTime);
			ColumnId sortedColumn = base.SortedColumn;
			switch (sortedColumn)
			{
			case ColumnId.DeliveryTime:
				listViewContents = new TimeGroupByList2(ColumnId.DeliveryTime, base.SortOrder, (ItemList2)listViewContents, base.UserContext);
				break;
			case ColumnId.SentTime:
				listViewContents = new TimeGroupByList2(ColumnId.SentTime, base.SortOrder, (ItemList2)listViewContents, base.UserContext);
				break;
			case ColumnId.Size:
				listViewContents = new SizeGroupByList2(base.SortOrder, (ItemList2)listViewContents, base.UserContext);
				break;
			default:
				switch (sortedColumn)
				{
				case ColumnId.FlagDueDate:
					listViewContents = new FlagGroupByList2(ColumnId.FlagDueDate, base.SortOrder, (ItemList2)listViewContents, base.UserContext);
					break;
				case ColumnId.FlagStartDate:
					listViewContents = new FlagGroupByList2(ColumnId.FlagStartDate, base.SortOrder, (ItemList2)listViewContents, base.UserContext);
					break;
				default:
					listViewContents = base.CreateGroupByList(listViewContents);
					break;
				}
				break;
			}
			return listViewContents;
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x000CE424 File Offset: 0x000CC624
		protected override IListViewDataSource CreateDataSource(Hashtable properties)
		{
			return new FolderListViewDataSource(base.UserContext, this.IsConversationView, properties, this.dataFolder, this.GetSortByProperties(), this.queryFilter, this.GetQueryResult(), !this.ShouldSubscribeForFolderContentChanges);
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x000CE464 File Offset: 0x000CC664
		protected override void OnBeforeRender()
		{
			base.AddAttribute("iUC", base.Contents.DataSource.UnreadCount.ToString(CultureInfo.InvariantCulture));
			base.AddAttribute("fCV", this.IsConversationView ? "1" : "0");
			base.AddAttribute("fOutlookFUR", this.IsContextFolderFilteredByUnread ? "1" : "0");
			base.AddAttribute("fFUR", this.IsFilteredByUnread ? "1" : "0");
			base.MakePropertyPublic("t");
			base.MakePropertyPublic("read");
			base.MakePropertyPublic("MM");
			base.MakePropertyPublic("fPhsh");
			base.MakePropertyPublic("fMR");
			base.MakePropertyPublic("fRR");
			base.MakePropertyPublic("fDoR");
			base.MakePropertyPublic("fAT");
			base.MakePropertyPublic("s");
			base.MakePropertyPublic("fRplR");
			base.MakePropertyPublic("fRAR");
			base.MakePropertyPublic("fFR");
			base.MakePropertyPublic("fExclCnv");
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x000CE588 File Offset: 0x000CC788
		protected override void InternalRenderData(TextWriter writer)
		{
			base.InternalRenderData(writer);
			VirtualListView2.RenderAttribute(writer, "iUC", base.Contents.DataSource.UnreadCount);
			VirtualListView2.RenderAttribute(writer, "fCV", this.IsConversationView ? 1 : 0);
			VirtualListView2.RenderAttribute(writer, "fFUR", this.IsFilteredByUnread ? 1 : 0);
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x000CE5E8 File Offset: 0x000CC7E8
		private SortBy[] GetSortByProperties()
		{
			SortBy[] result;
			if (base.SortedColumn == ColumnId.DeliveryTime)
			{
				result = new SortBy[]
				{
					new SortBy(ItemSchema.ReceivedTime, base.SortOrder)
				};
			}
			else if (base.SortedColumn == ColumnId.ConversationLastDeliveryTime)
			{
				result = new SortBy[]
				{
					new SortBy(ConversationItemSchema.ConversationLastDeliveryTime, base.SortOrder)
				};
			}
			else if (base.SortedColumn == ColumnId.ConversationFlagStatus)
			{
				result = new SortBy[]
				{
					new SortBy(ConversationItemSchema.ConversationFlagStatus, base.SortOrder),
					new SortBy(ConversationItemSchema.ConversationFlagCompleteTime, base.SortOrder),
					new SortBy(ConversationItemSchema.ConversationLastDeliveryTime, base.SortOrder)
				};
			}
			else if (ConversationUtilities.IsConversationSortColumn(base.SortedColumn))
			{
				Column column = ListViewColumns.GetColumn(base.SortedColumn);
				result = new SortBy[]
				{
					new SortBy(column[0], base.SortOrder),
					new SortBy(ConversationItemSchema.ConversationLastDeliveryTime, SortOrder.Descending)
				};
			}
			else if (base.SortedColumn == ColumnId.FlagDueDate || base.SortedColumn == ColumnId.FlagStartDate)
			{
				PropertyDefinition columnDefinition = (base.SortedColumn == ColumnId.FlagDueDate) ? ItemSchema.UtcDueDate : ItemSchema.UtcStartDate;
				result = new SortBy[]
				{
					new SortBy(ItemSchema.FlagStatus, base.SortOrder),
					new SortBy(columnDefinition, (base.SortOrder == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending),
					new SortBy(ItemSchema.ItemColor, base.SortOrder),
					new SortBy(ItemSchema.ReceivedTime, base.SortOrder)
				};
			}
			else
			{
				Column column2 = ListViewColumns.GetColumn(base.SortedColumn);
				result = new SortBy[]
				{
					new SortBy(column2[0], base.SortOrder),
					new SortBy(ItemSchema.ReceivedTime, SortOrder.Descending)
				};
			}
			return result;
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x000CE798 File Offset: 0x000CC998
		protected override void SubscribeForFolderContentChanges()
		{
			OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromSessionFolderId(base.UserContext, this.contextFolder.Session, this.contextFolder.Id.ObjectId);
			if (OwaMapiNotificationManager.IsNotificationEnabled(base.UserContext) && !Utilities.IsPublic(this.contextFolder))
			{
				OwaStoreObjectId dataFolderId = OwaStoreObjectId.CreateFromSessionFolderId(base.UserContext, this.dataFolder.Session, this.dataFolder.Id.ObjectId);
				if (base.UserContext.IsPushNotificationsEnabled)
				{
					MailboxSession mailboxSession = this.contextFolder.Session as MailboxSession;
					if (mailboxSession != null)
					{
						Dictionary<PropertyDefinition, int> propertyMap = null;
						ExchangeListViewDataSource exchangeListViewDataSource = (ExchangeListViewDataSource)base.Contents.DataSource;
						PropertyDefinition[] requestedProperties = exchangeListViewDataSource.GetRequestedProperties(true, ref propertyMap);
						if (this.ShouldSubscribeForFolderContentChanges)
						{
							base.UserContext.MapiNotificationManager.SubscribeForFolderContentChanges(mailboxSession, owaStoreObjectId, dataFolderId, exchangeListViewDataSource.QueryResult, this.CreateListViewContents(), requestedProperties, propertyMap, this.GetSortByProperties(), this.folderFilter, this.IsConversationView);
						}
						else
						{
							base.UserContext.MapiNotificationManager.SubscribeForFolderChanges(owaStoreObjectId, mailboxSession);
						}
					}
				}
			}
			if (base.UserContext.IsPullNotificationsEnabled)
			{
				Dictionary<OwaStoreObjectId, OwaConditionAdvisor> conditionAdvisorTable = base.UserContext.NotificationManager.ConditionAdvisorTable;
				if (conditionAdvisorTable == null || !conditionAdvisorTable.ContainsKey(owaStoreObjectId))
				{
					base.UserContext.NotificationManager.CreateOwaConditionAdvisor(base.UserContext, this.contextFolder.Session as MailboxSession, owaStoreObjectId, EventObjectType.None, EventType.None);
				}
			}
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x000CE900 File Offset: 0x000CCB00
		internal override void UnSubscribeForFolderContentChanges()
		{
			if (OwaMapiNotificationManager.IsNotificationEnabled(base.UserContext) && !Utilities.IsPublic(this.contextFolder))
			{
				OwaStoreObjectId folderId = OwaStoreObjectId.CreateFromSessionFolderId(base.UserContext, this.contextFolder.Session, this.contextFolder.Id.ObjectId);
				if (base.UserContext.IsPushNotificationsEnabled)
				{
					base.UserContext.MapiNotificationManager.UnsubscribeFolderContentChanges(folderId);
				}
			}
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x000CE96C File Offset: 0x000CCB6C
		private QueryResult GetQueryResult()
		{
			if (!this.ShouldSubscribeForFolderContentChanges)
			{
				return null;
			}
			OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromSessionFolderId(base.UserContext, this.dataFolder.Session, this.dataFolder.Id.ObjectId);
			if (this.IsQueryResultObsolete(owaStoreObjectId))
			{
				base.UserContext.MapiNotificationManager.UnsubscribeFolderContentChanges(owaStoreObjectId);
				return null;
			}
			QueryResult folderQueryResult = base.UserContext.MapiNotificationManager.GetFolderQueryResult(owaStoreObjectId);
			if (folderQueryResult != null)
			{
				try
				{
					folderQueryResult.SeekToOffset(SeekReference.OriginBeginning, 0);
				}
				catch (MapiExceptionObjectDisposed)
				{
					base.UserContext.MapiNotificationManager.UnsubscribeFolderContentChanges(owaStoreObjectId);
					return null;
				}
				catch (ObjectDisposedException)
				{
					base.UserContext.MapiNotificationManager.UnsubscribeFolderContentChanges(owaStoreObjectId);
					return null;
				}
				return folderQueryResult;
			}
			return folderQueryResult;
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x000CEA34 File Offset: 0x000CCC34
		private bool IsQueryResultObsolete(OwaStoreObjectId contextFolderId)
		{
			SortBy[] sortByProperties = this.GetSortByProperties();
			SortBy[] folderSortBy = base.UserContext.MapiNotificationManager.GetFolderSortBy(contextFolderId);
			if (folderSortBy == null || sortByProperties.Length != folderSortBy.Length)
			{
				return true;
			}
			for (int i = 0; i < sortByProperties.Length; i++)
			{
				if (sortByProperties[i].ColumnDefinition != folderSortBy[i].ColumnDefinition || sortByProperties[i].SortOrder != folderSortBy[i].SortOrder)
				{
					return true;
				}
			}
			FolderVirtualListViewFilter folderVirtualListViewFilter = base.UserContext.MapiNotificationManager.GetFolderFilter(contextFolderId);
			return folderVirtualListViewFilter != this.folderFilter || (this.folderFilter != null && !this.folderFilter.Equals(folderVirtualListViewFilter));
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x000CEAD0 File Offset: 0x000CCCD0
		public OwaStoreObjectId GetNewSelectionId()
		{
			OwaStoreObjectId result = null;
			FolderListViewDataSource folderListViewDataSource = base.Contents.DataSource as FolderListViewDataSource;
			if (folderListViewDataSource != null)
			{
				result = folderListViewDataSource.NewSelectionId;
			}
			return result;
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x000CEAFC File Offset: 0x000CCCFC
		private bool IsOutlookSearchFolderFilteredByUnread(Folder folder)
		{
			OutlookSearchFolder outlookSearchFolder = folder as OutlookSearchFolder;
			if (outlookSearchFolder == null)
			{
				return false;
			}
			QueryFilter queryFilter = null;
			try
			{
				queryFilter = outlookSearchFolder.GetSearchCriteria().SearchQuery;
			}
			catch (CorruptDataException)
			{
				return true;
			}
			return queryFilter != null && this.IsUnreadFilter(queryFilter);
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x000CEB48 File Offset: 0x000CCD48
		private bool IsUnreadFilter(QueryFilter filter)
		{
			if (filter is ComparisonFilter)
			{
				ComparisonFilter comparisonFilter = (ComparisonFilter)filter;
				return comparisonFilter.Property.Name.Contains("IsRead");
			}
			if (filter is CompositeFilter)
			{
				CompositeFilter compositeFilter = (CompositeFilter)filter;
				foreach (QueryFilter filter2 in compositeFilter.Filters)
				{
					if (this.IsUnreadFilter(filter2))
					{
						return true;
					}
				}
				return false;
			}
			return filter is NotFilter && this.IsUnreadFilter(((NotFilter)filter).Filter);
		}

		// Token: 0x040018DA RID: 6362
		private const string FilteredByUnreadAttribute = "fFUR";

		// Token: 0x040018DB RID: 6363
		private const string OutlookFilteredByUnreadAttribute = "fOutlookFUR";

		// Token: 0x040018DC RID: 6364
		private const string UnreadFilterPropertyName = "IsRead";

		// Token: 0x040018DD RID: 6365
		public static ViewDescriptor MultiLineFrom = new ViewDescriptor(ColumnId.DeliveryTime, false, new ColumnId[]
		{
			ColumnId.Importance,
			ColumnId.MailIcon,
			ColumnId.HasAttachment,
			ColumnId.From,
			ColumnId.Subject,
			ColumnId.DeliveryTime,
			ColumnId.FlagDueDate,
			ColumnId.Categories
		});

		// Token: 0x040018DE RID: 6366
		public static ViewDescriptor MultiLineFromDeletedItems = new ViewDescriptor(ColumnId.DeliveryTime, false, new ColumnId[]
		{
			ColumnId.Importance,
			ColumnId.MailIcon,
			ColumnId.HasAttachment,
			ColumnId.From,
			ColumnId.Subject,
			ColumnId.DeliveryTime,
			ColumnId.Categories
		});

		// Token: 0x040018DF RID: 6367
		public static ViewDescriptor MultiLineTo = new ViewDescriptor(ColumnId.SentTime, false, new ColumnId[]
		{
			ColumnId.Importance,
			ColumnId.MailIcon,
			ColumnId.HasAttachment,
			ColumnId.To,
			ColumnId.Subject,
			ColumnId.SentTime,
			ColumnId.FlagDueDate,
			ColumnId.Categories
		});

		// Token: 0x040018E0 RID: 6368
		public static ViewDescriptor MultiLineToDrafts = new ViewDescriptor(ColumnId.SentTime, false, new ColumnId[]
		{
			ColumnId.Importance,
			ColumnId.MailIcon,
			ColumnId.HasAttachment,
			ColumnId.To,
			ColumnId.Subject,
			ColumnId.SentTime,
			ColumnId.Categories
		});

		// Token: 0x040018E1 RID: 6369
		public static ViewDescriptor ConversationView = new ViewDescriptor(ColumnId.DeliveryTime, false, new ColumnId[]
		{
			ColumnId.HasAttachment,
			ColumnId.MailIcon,
			ColumnId.From,
			ColumnId.DeliveryTime,
			ColumnId.FlagDueDate,
			ColumnId.Categories
		});

		// Token: 0x040018E2 RID: 6370
		public static ViewDescriptor ConversationViewNoFlag = new ViewDescriptor(ColumnId.DeliveryTime, false, new ColumnId[]
		{
			ColumnId.HasAttachment,
			ColumnId.MailIcon,
			ColumnId.From,
			ColumnId.DeliveryTime,
			ColumnId.Categories
		});

		// Token: 0x040018E3 RID: 6371
		private Folder contextFolder;

		// Token: 0x040018E4 RID: 6372
		private Folder dataFolder;

		// Token: 0x040018E5 RID: 6373
		private QueryFilter queryFilter;

		// Token: 0x040018E6 RID: 6374
		private bool isInSearch;

		// Token: 0x040018E7 RID: 6375
		private SearchScope folderScope;

		// Token: 0x040018E8 RID: 6376
		private FolderVirtualListViewFilter folderFilter;

		// Token: 0x040018E9 RID: 6377
		private FolderVirtualListViewFilter favoritesFolderFilter;
	}
}
