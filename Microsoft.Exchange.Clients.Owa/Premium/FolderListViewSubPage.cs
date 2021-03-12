using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000381 RID: 897
	public abstract class FolderListViewSubPage : ListViewSubPage
	{
		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x060021CB RID: 8651 RVA: 0x000C1544 File Offset: 0x000BF744
		protected static int StoreObjectTypeMessage
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x000C1548 File Offset: 0x000BF748
		protected FolderListViewSubPage(Trace callTracer, Trace algorithmTracer) : base(callTracer, algorithmTracer)
		{
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x060021CD RID: 8653 RVA: 0x000C156F File Offset: 0x000BF76F
		protected bool IsFilteredViewInFavorites
		{
			get
			{
				return this.favoritesFilterParameter != null;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x060021CE RID: 8654 RVA: 0x000C157D File Offset: 0x000BF77D
		internal Folder Folder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x060021CF RID: 8655 RVA: 0x000C1588 File Offset: 0x000BF788
		internal DefaultFolderType FolderType
		{
			get
			{
				if (this.folderType == null)
				{
					if (this.favoritesFilterParameter != null)
					{
						this.folderType = new DefaultFolderType?(Utilities.GetDefaultFolderType(this.Folder.Session, this.favoritesFilterParameter.SourceFolderId.StoreObjectId));
					}
					else
					{
						this.folderType = new DefaultFolderType?(Utilities.GetDefaultFolderType(this.folder));
					}
				}
				return this.folderType.Value;
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x060021D0 RID: 8656 RVA: 0x000C15F8 File Offset: 0x000BF7F8
		protected bool IsInDeleteItems
		{
			get
			{
				return Utilities.IsDefaultFolder(this.Folder, DefaultFolderType.DeletedItems);
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x060021D1 RID: 8657 RVA: 0x000C1606 File Offset: 0x000BF806
		protected bool IsDeletedItemsSubFolder
		{
			get
			{
				return Utilities.IsItemInDefaultFolder(this.Folder, DefaultFolderType.DeletedItems);
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x060021D2 RID: 8658
		internal abstract StoreObjectId DefaultFolderId { get; }

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x060021D3 RID: 8659 RVA: 0x000C1614 File Offset: 0x000BF814
		protected override int ViewWidth
		{
			get
			{
				return Math.Max(this.viewWidth, 325);
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x060021D4 RID: 8660 RVA: 0x000C1628 File Offset: 0x000BF828
		protected override int ListViewTop
		{
			get
			{
				int num = 31;
				if (this.ShouldRenderSearch && !this.IsPublicFolder)
				{
					num = 60;
				}
				if (!this.ShouldRenderToolbar)
				{
					num -= this.ToolbarHeight;
				}
				return num;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x060021D5 RID: 8661 RVA: 0x000C165D File Offset: 0x000BF85D
		protected override int ViewHeight
		{
			get
			{
				return this.viewHeight;
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x060021D6 RID: 8662 RVA: 0x000C1665 File Offset: 0x000BF865
		protected override SortOrder SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x060021D7 RID: 8663 RVA: 0x000C166D File Offset: 0x000BF86D
		protected override ColumnId SortedColumn
		{
			get
			{
				return this.sortedColumn;
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x060021D8 RID: 8664 RVA: 0x000C1675 File Offset: 0x000BF875
		protected override ReadingPanePosition ReadingPanePosition
		{
			get
			{
				return this.readingPanePosition;
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x060021D9 RID: 8665 RVA: 0x000C167D File Offset: 0x000BF87D
		protected override bool IsMultiLine
		{
			get
			{
				return this.isMultiLine;
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x060021DA RID: 8666 RVA: 0x000C1685 File Offset: 0x000BF885
		protected override bool ShouldRenderSearch
		{
			get
			{
				return !base.UserContext.IsWebPartRequest;
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x060021DB RID: 8667 RVA: 0x000C1695 File Offset: 0x000BF895
		protected virtual bool ShouldRenderELCCommentAndQuotaLink
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x060021DC RID: 8668 RVA: 0x000C1698 File Offset: 0x000BF898
		protected override bool AllowAdvancedSearch
		{
			get
			{
				return this.IsPublicFolder || this.IsOtherMailboxFolder || ((MailboxSession)this.Folder.Session).Mailbox.IsContentIndexingEnabled;
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x060021DD RID: 8669 RVA: 0x000C16C6 File Offset: 0x000BF8C6
		protected string ELCFolderIdValue
		{
			get
			{
				if (this.elcFolderIdValue == null)
				{
					return string.Empty;
				}
				return Utilities.JavascriptEncode(this.elcFolderIdValue);
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x060021DE RID: 8670 RVA: 0x000C16E1 File Offset: 0x000BF8E1
		protected long ELCFolderQuota
		{
			get
			{
				return this.elcFolderQuota;
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x060021DF RID: 8671 RVA: 0x000C16E9 File Offset: 0x000BF8E9
		protected bool IsELCFolderWithQuota
		{
			get
			{
				return this.isELCFolderWithQuota;
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x060021E0 RID: 8672 RVA: 0x000C16F1 File Offset: 0x000BF8F1
		protected bool HasELCComment
		{
			get
			{
				return this.elcFolderComment != null && !string.IsNullOrEmpty(this.elcFolderComment.Trim());
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x060021E1 RID: 8673 RVA: 0x000C1710 File Offset: 0x000BF910
		protected bool ShouldRenderELCInfobar
		{
			get
			{
				return this.HasELCComment || this.IsELCFolderWithQuota;
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x060021E2 RID: 8674 RVA: 0x000C1722 File Offset: 0x000BF922
		protected bool IsELCInfobarVisible
		{
			get
			{
				return this.elcShowComment;
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x060021E3 RID: 8675 RVA: 0x000C172C File Offset: 0x000BF92C
		protected override string ContainerName
		{
			get
			{
				if (!this.IsPublicFolder && !this.IsOtherMailboxFolder && this.folder.Id.ObjectId.Equals(base.UserContext.GetRootFolderId((MailboxSession)this.Folder.Session)))
				{
					return Utilities.GetMailboxOwnerDisplayName((MailboxSession)this.Folder.Session);
				}
				if (this.IsPublicFolder && base.UserContext.IsPublicFolderRootId(this.folder.Id.ObjectId))
				{
					return LocalizedStrings.GetNonEncoded(-1116491328);
				}
				if (this.IsOtherMailboxFolder)
				{
					return Utilities.GetFolderNameWithSessionName(this.folder);
				}
				return this.folder.DisplayName;
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x060021E4 RID: 8676 RVA: 0x000C17E0 File Offset: 0x000BF9E0
		protected bool IsPublicFolder
		{
			get
			{
				if (this.isPublicFolder == null)
				{
					this.isPublicFolder = new bool?(Utilities.IsPublic(this.Folder));
				}
				return this.isPublicFolder.Value;
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x060021E5 RID: 8677 RVA: 0x000C1810 File Offset: 0x000BFA10
		protected bool IsArchiveMailboxFolder
		{
			get
			{
				if (this.isArchiveMailboxFolder == null)
				{
					this.isArchiveMailboxFolder = new bool?(Utilities.IsInArchiveMailbox(this.Folder));
				}
				return this.isArchiveMailboxFolder.Value;
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x060021E6 RID: 8678 RVA: 0x000C1840 File Offset: 0x000BFA40
		protected bool IsOtherMailboxFolder
		{
			get
			{
				if (this.isOtherMailboxFolder == null)
				{
					this.isOtherMailboxFolder = new bool?(base.UserContext.IsInOtherMailbox(this.Folder) || Utilities.IsWebPartDelegateAccessRequest(OwaContext.Current));
				}
				return this.isOtherMailboxFolder.Value;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x060021E7 RID: 8679 RVA: 0x000C1890 File Offset: 0x000BFA90
		protected int FolderEffectiveRights
		{
			get
			{
				return (int)Utilities.GetFolderProperty<EffectiveRights>(this.folder, StoreObjectSchema.EffectiveRights, EffectiveRights.None);
			}
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x000C18B0 File Offset: 0x000BFAB0
		protected override void LoadViewState()
		{
			OwaStoreObjectId owaStoreObjectId = null;
			if (base.SerializedContainerId != null)
			{
				if (OwaStoreObjectId.IsDummyArchiveFolder(base.SerializedContainerId))
				{
					owaStoreObjectId = base.UserContext.GetArchiveRootFolderId();
					this.archiveRootFolderId = owaStoreObjectId.ToString();
				}
				else
				{
					owaStoreObjectId = OwaStoreObjectId.CreateFromString(base.SerializedContainerId);
				}
			}
			if (owaStoreObjectId == null)
			{
				base.AlgorithmTracer.TraceDebug((long)this.GetHashCode(), "folder Id is null, using default folder");
				owaStoreObjectId = OwaStoreObjectId.CreateFromMailboxFolderId(this.DefaultFolderId);
			}
			PropertyDefinition[] array = new PropertyDefinition[]
			{
				FolderSchema.DisplayName,
				FolderSchema.ItemCount,
				FolderSchema.UnreadCount,
				ViewStateProperties.ReadingPanePosition,
				ViewStateProperties.ViewWidth,
				ViewStateProperties.ViewHeight,
				ViewStateProperties.MultiLine,
				ViewStateProperties.SortColumn,
				ViewStateProperties.SortOrder,
				ViewStateProperties.ViewFilter,
				ViewStateProperties.FilteredViewLabel,
				FolderSchema.SearchFolderAllowAgeout,
				FolderSchema.IsOutlookSearchFolder,
				FolderSchema.AdminFolderFlags,
				FolderSchema.FolderQuota,
				FolderSchema.FolderSize,
				FolderSchema.ELCFolderComment,
				FolderSchema.ELCPolicyIds,
				FolderSchema.ExtendedFolderFlags,
				StoreObjectSchema.EffectiveRights,
				FolderSchema.OutlookSearchFolderClsId
			};
			this.folder = Utilities.GetFolderForContent<Folder>(base.UserContext, owaStoreObjectId, array);
			this.favoritesFilterParameter = Utilities.GetFavoritesFilterViewParameter(base.UserContext, this.Folder);
			if (this.folder is SearchFolder && this.favoritesFilterParameter != null && !this.favoritesFilterParameter.IsCurrentVersion)
			{
				this.favoritesFilterParameter.UpgradeFilter(this.folder as SearchFolder, array);
			}
			this.sortOrder = this.DefaultSortOrder;
			this.sortedColumn = this.DefaultSortedColumn;
			this.isMultiLine = this.DefaultMultiLineSetting;
			this.readingPanePosition = this.DefaultReadingPanePosition;
			FolderViewStates folderViewStates = base.UserContext.GetFolderViewStates(this.folder);
			if (base.UserContext.IsWebPartRequest)
			{
				string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "view", false);
				WebPartListView webPartListView = WebPartUtilities.LookUpWebPartView(this.folder.Id.ObjectId.ObjectType, this.folder.ClassName, queryStringParameter);
				if (webPartListView != null)
				{
					if (webPartListView.ColumnId != null)
					{
						this.sortedColumn = (ColumnId)webPartListView.ColumnId.Value;
					}
					if (webPartListView.SortOrder != null)
					{
						this.sortOrder = (SortOrder)webPartListView.SortOrder.Value;
					}
					if (webPartListView.IsMultiLine != null)
					{
						this.isMultiLine = webPartListView.IsMultiLine.Value;
					}
				}
			}
			else
			{
				this.viewWidth = folderViewStates.ViewWidth;
				this.viewHeight = folderViewStates.ViewHeight;
				this.sortOrder = folderViewStates.GetSortOrder(this.DefaultSortOrder);
				this.isMultiLine = folderViewStates.GetMultiLine(this.DefaultMultiLineSetting);
				string sortColumn = folderViewStates.GetSortColumn(null);
				if (sortColumn != null)
				{
					ColumnId columnId = ColumnIdParser.Parse(sortColumn);
					if (columnId < ColumnId.Count && (!this.isMultiLine || ListViewColumns.GetColumn(columnId).SortBoundaries != null))
					{
						this.sortedColumn = columnId;
					}
				}
			}
			if (ConversationUtilities.IsConversationSortColumn(this.sortedColumn) && !ConversationUtilities.ShouldAllowConversationView(base.UserContext, this.Folder))
			{
				this.sortedColumn = ColumnId.DeliveryTime;
			}
			this.readingPanePosition = folderViewStates.GetReadingPanePosition(this.DefaultReadingPanePosition);
			this.LoadELCData();
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x000C1C20 File Offset: 0x000BFE20
		protected override IListViewDataSource CreateDataSource(ListView listView)
		{
			SortBy[] sortByProperties = listView.GetSortByProperties();
			return new FolderListViewDataSource(base.UserContext, listView.Properties, this.folder, sortByProperties);
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x000C1C4C File Offset: 0x000BFE4C
		protected override void OnUnload(EventArgs e)
		{
			if (this.folder != null)
			{
				this.folder.Dispose();
				this.folder = null;
			}
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x000C1C68 File Offset: 0x000BFE68
		protected override OwaQueryStringParameters GetDefaultItemParameters()
		{
			IListViewDataSource dataSource = ((VirtualListView2)this.listView).DataSource;
			if (dataSource.RangeCount < 1 || Utilities.IsPublic(this.Folder))
			{
				return null;
			}
			OwaQueryStringParameters owaQueryStringParameters = new OwaQueryStringParameters();
			dataSource.MoveToItem(0);
			bool flag = Utilities.IsDefaultFolder(this.Folder, DefaultFolderType.JunkEmail);
			int itemProperty = dataSource.GetItemProperty<int>(ItemSchema.EdgePcl, 1);
			bool itemProperty2 = dataSource.GetItemProperty<bool>(ItemSchema.LinkEnabled, false);
			bool flag2 = JunkEmailUtilities.IsSuspectedPhishingItem(itemProperty) && !itemProperty2;
			string itemClass = dataSource.GetItemClass();
			if (ObjectClass.IsOfClass(itemClass, "IPM.Sharing") && !flag && !flag2)
			{
				owaQueryStringParameters.SetApplicationElement("PreFormAction");
				owaQueryStringParameters.Action = "Preview";
			}
			else
			{
				owaQueryStringParameters.SetApplicationElement("Item");
				owaQueryStringParameters.Action = "Preview";
			}
			if ((!flag && !flag2) || ObjectClass.IsOfClass(itemClass, "IPM.Conversation"))
			{
				bool itemProperty3 = dataSource.GetItemProperty<bool>(MessageItemSchema.IsDraft, false);
				bool itemProperty4 = dataSource.GetItemProperty<bool>(MessageItemSchema.HasBeenSubmitted, false);
				TaskType itemProperty5 = (TaskType)dataSource.GetItemProperty<int>(TaskSchema.TaskType, 0);
				bool flag3 = TaskUtilities.IsAssignedTaskType(itemProperty5);
				owaQueryStringParameters.ItemClass = itemClass;
				if ((itemProperty3 && !itemProperty4) || ObjectClass.IsContact(itemClass) || ObjectClass.IsDistributionList(itemClass))
				{
					owaQueryStringParameters.State = "Draft";
				}
				else if (ObjectClass.IsTask(itemClass) && flag3)
				{
					owaQueryStringParameters.State = "Assigned";
				}
			}
			owaQueryStringParameters.Id = dataSource.GetItemId();
			return owaQueryStringParameters;
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x000C1DCC File Offset: 0x000BFFCC
		protected override void RenderSearch()
		{
			base.RenderSearch(this.folder);
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x000C1DDA File Offset: 0x000BFFDA
		protected void SetSortOrder(SortOrder sortOrder)
		{
			this.sortOrder = sortOrder;
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x000C1DE4 File Offset: 0x000BFFE4
		private void LoadELCData()
		{
			if (this.folder is SearchFolder)
			{
				return;
			}
			this.elcFolderIdValue = Utilities.GetQueryStringParameter(base.Request, "elcId", false);
			if (string.IsNullOrEmpty(this.elcFolderIdValue))
			{
				this.GetELCFolderData(this.folder);
			}
			else
			{
				StoreObjectId folderId = Utilities.CreateStoreObjectId(base.UserContext.MailboxSession, this.elcFolderIdValue);
				using (Folder folder = Folder.Bind(base.UserContext.MailboxSession, folderId, new PropertyDefinition[]
				{
					FolderSchema.AdminFolderFlags,
					FolderSchema.FolderQuota,
					FolderSchema.FolderSize,
					FolderSchema.ELCFolderComment,
					FolderSchema.ELCPolicyIds
				}))
				{
					this.GetELCFolderData(folder);
				}
			}
			if ((this.elcAdminFolderFlags & 8) > 0 && this.elcFolderQuota > 0L)
			{
				this.isELCFolderWithQuota = true;
			}
			this.elcDisplayName = this.GetFolderDisplayName();
			this.SetElcShowComment();
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x000C1EDC File Offset: 0x000C00DC
		private void GetELCFolderData(Folder folder)
		{
			this.elcAdminFolderFlags = 0;
			this.elcFolderComment = string.Empty;
			this.elcMustDisplayComment = false;
			this.elcFolderQuota = 0L;
			this.elcFolderSize = 0L;
			this.elcPolicyIds = string.Empty;
			object obj = folder.TryGetProperty(FolderSchema.AdminFolderFlags);
			if (!(obj is PropertyError))
			{
				this.elcAdminFolderFlags = (int)obj;
			}
			obj = folder.TryGetProperty(FolderSchema.ELCPolicyIds);
			if (!(obj is PropertyError))
			{
				this.elcPolicyIds = (string)obj;
			}
			if (Utilities.IsELCFolder(this.elcAdminFolderFlags) || (Utilities.IsSpecialFolder(folder.Id.ObjectId, base.UserContext) && !string.IsNullOrEmpty(this.elcPolicyIds)))
			{
				this.elcMustDisplayComment = ((this.elcAdminFolderFlags & 4) > 0);
				obj = folder.TryGetProperty(FolderSchema.ELCFolderComment);
				if (!(obj is PropertyError))
				{
					this.elcFolderComment = (string)obj;
				}
			}
			else if (!Utilities.IsPublic(folder))
			{
				this.elcMustDisplayComment = base.UserContext.AllFoldersPolicyMustDisplayComment;
				this.elcFolderComment = base.UserContext.AllFoldersPolicyComment;
			}
			if ((this.elcAdminFolderFlags & 8) > 0)
			{
				obj = folder.TryGetProperty(FolderSchema.FolderQuota);
				if (!(obj is PropertyError))
				{
					this.elcFolderQuota = (long)((int)obj) * 1024L;
				}
				obj = folder.TryGetProperty(FolderSchema.FolderSize);
				if (!(obj is PropertyError))
				{
					this.elcFolderSize = (long)((int)obj);
				}
			}
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x000C2042 File Offset: 0x000C0242
		private string GetFolderDisplayName()
		{
			return this.ContainerName;
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x000C204C File Offset: 0x000C024C
		private void SetElcShowComment()
		{
			object obj = this.folder.TryGetProperty(FolderSchema.ExtendedFolderFlags);
			this.elcShowComment = true;
			if (!(obj is PropertyError) && Utilities.IsFlagSet((int)obj, 32))
			{
				this.elcShowComment = false;
			}
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x000C2090 File Offset: 0x000C0290
		private bool IsOutlookSearchFolder()
		{
			bool result = false;
			object obj = this.folder.TryGetProperty(FolderSchema.IsOutlookSearchFolder);
			if (!(obj is PropertyError))
			{
				result = (bool)obj;
			}
			return result;
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x000C20C0 File Offset: 0x000C02C0
		protected void RenderJavascriptEncodedLegacyDN()
		{
			Utilities.JavascriptEncode(base.UserContext.ExchangePrincipal.LegacyDn, base.Response.Output);
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x000C20E4 File Offset: 0x000C02E4
		protected string RenderELCComment()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<div class=\"divElcComponent\" id=\"divElcComment\">");
			stringBuilder.Append("<div class=\"divIBTxt\">");
			stringBuilder.Append(Utilities.HtmlEncode(this.elcDisplayName));
			stringBuilder.Append(": ");
			if (this.HasELCComment)
			{
				stringBuilder.Append(Utilities.HtmlEncode(this.elcFolderComment));
			}
			if (!this.elcMustDisplayComment)
			{
				stringBuilder.Append("</div><div class=\"divIBTxt\"><a href=# id=\"lnkHdELC\">");
				stringBuilder.Append(LocalizedStrings.GetHtmlEncoded(1303059585) + "</a>");
			}
			stringBuilder.Append("</div></div>");
			return stringBuilder.ToString();
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x000C2188 File Offset: 0x000C0388
		protected string RenderELCQuota()
		{
			if (!this.isELCFolderWithQuota)
			{
				return string.Empty;
			}
			QuotaLevel quotaLevel = QuotaLevel.Normal;
			int num = (int)Math.Round((double)this.elcFolderSize / (double)this.elcFolderQuota * 100.0);
			if (num >= 100)
			{
				quotaLevel = QuotaLevel.Exceeded;
			}
			else if (num >= 75)
			{
				quotaLevel = QuotaLevel.AboveWarning;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<div class=\"divElcComponent\" id=\"divElcQuota\">");
			stringBuilder.Append("<div class=\"divIBTxt\" id=\"divQuotaBar\">");
			StringBuilder stringBuilder2 = new StringBuilder();
			using (StringWriter stringWriter = new StringWriter(stringBuilder2))
			{
				RenderingUtilities.RenderQuotaBar(stringWriter, base.UserContext, num, quotaLevel);
			}
			stringBuilder.Append(stringBuilder2.ToString());
			stringBuilder.Append("</div> ");
			stringBuilder.Append("<div class=\"divIBTxt\" id=\"divFldUsg\" ");
			stringBuilder.Append((num < 100) ? string.Empty : "style=\"display:none;\" ");
			stringBuilder.Append(">");
			stringBuilder2 = new StringBuilder();
			using (StringWriter stringWriter2 = new StringWriter(stringBuilder2))
			{
				Utilities.RenderSizeWithUnits(stringWriter2, this.elcFolderQuota, false);
			}
			stringBuilder.AppendFormat(LocalizedStrings.GetHtmlEncoded(-659755432), "<span id=spnFldPrcntUsd>" + num + "</span>", stringBuilder2.ToString() + "</span>");
			stringBuilder.Append("</div> <div class=\"divIBTxt\" id=\"divFldExcd\" ");
			stringBuilder.Append((num < 100) ? "style=\"display:none;\" " : string.Empty);
			stringBuilder.Append(">");
			stringBuilder.Append(LocalizedStrings.GetHtmlEncoded(231890609));
			stringBuilder.Append("</div></div>");
			return stringBuilder.ToString();
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x000C2338 File Offset: 0x000C0538
		protected void RenderJavascriptEncodedFolderId()
		{
			Utilities.JavascriptEncode(Utilities.GetIdAsString(this.Folder), base.Response.Output);
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x000C2355 File Offset: 0x000C0555
		protected void RenderJavascriptEncodedContainerName()
		{
			Utilities.JavascriptEncode(this.ContainerName, base.Response.Output);
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x000C236D File Offset: 0x000C056D
		protected void RenderJavascriptEncodedContainerMetadataString()
		{
			base.Response.Output.Write(LocalizedStrings.GetJavascriptEncoded(-648401288));
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x000C2389 File Offset: 0x000C0589
		protected void RenderJavascriptEncodedFlaggedItemsAndTasksFolderId()
		{
			Utilities.JavascriptEncode(base.UserContext.FlaggedItemsAndTasksFolderId.ToBase64String(), base.Response.Output);
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x000C23AC File Offset: 0x000C05AC
		protected void RenderReplaceFolderId()
		{
			if (!string.IsNullOrEmpty(this.archiveRootFolderId))
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("<div id=\"divReplaceFolderId\" style=\"display:none\" oldfolderid =\"f");
				stringBuilder.Append(Utilities.HtmlEncode(base.SerializedContainerId));
				stringBuilder.Append("\" newfolderid =\"f");
				stringBuilder.Append(Utilities.HtmlEncode(this.archiveRootFolderId));
				stringBuilder.Append("\"></div>");
				base.Response.Write(stringBuilder.ToString());
				NavigationHost.RenderFavoritesAndNavigationTrees(base.Writer, base.UserContext, null, new NavigationNodeGroupSection[]
				{
					NavigationNodeGroupSection.First
				});
			}
		}

		// Token: 0x040017DA RID: 6106
		private const string ElcFolderIdQueryParameter = "elcId";

		// Token: 0x040017DB RID: 6107
		private const int ElcFolderQuotaWarningPercentage = 75;

		// Token: 0x040017DC RID: 6108
		private const int ElcFolderQuotaMaximumPercentage = 100;

		// Token: 0x040017DD RID: 6109
		private Folder folder;

		// Token: 0x040017DE RID: 6110
		private bool isMultiLine = true;

		// Token: 0x040017DF RID: 6111
		private bool? isPublicFolder;

		// Token: 0x040017E0 RID: 6112
		private bool? isArchiveMailboxFolder;

		// Token: 0x040017E1 RID: 6113
		private bool? isOtherMailboxFolder;

		// Token: 0x040017E2 RID: 6114
		private DefaultFolderType? folderType;

		// Token: 0x040017E3 RID: 6115
		private ReadingPanePosition readingPanePosition;

		// Token: 0x040017E4 RID: 6116
		protected int viewWidth = 450;

		// Token: 0x040017E5 RID: 6117
		private int viewHeight = 250;

		// Token: 0x040017E6 RID: 6118
		private ColumnId sortedColumn;

		// Token: 0x040017E7 RID: 6119
		private SortOrder sortOrder;

		// Token: 0x040017E8 RID: 6120
		private FolderVirtualListViewFilter favoritesFilterParameter;

		// Token: 0x040017E9 RID: 6121
		private string elcFolderIdValue;

		// Token: 0x040017EA RID: 6122
		private string elcDisplayName;

		// Token: 0x040017EB RID: 6123
		private int elcAdminFolderFlags;

		// Token: 0x040017EC RID: 6124
		private string elcFolderComment;

		// Token: 0x040017ED RID: 6125
		private bool elcMustDisplayComment;

		// Token: 0x040017EE RID: 6126
		private long elcFolderSize;

		// Token: 0x040017EF RID: 6127
		private long elcFolderQuota;

		// Token: 0x040017F0 RID: 6128
		private string elcPolicyIds;

		// Token: 0x040017F1 RID: 6129
		private bool elcShowComment;

		// Token: 0x040017F2 RID: 6130
		private bool isELCFolderWithQuota;

		// Token: 0x040017F3 RID: 6131
		private string archiveRootFolderId;
	}
}
