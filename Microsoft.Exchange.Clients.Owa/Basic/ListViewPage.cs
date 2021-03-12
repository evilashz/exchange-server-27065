using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.InfoWorker.Common.OOF;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x0200005B RID: 91
	public abstract class ListViewPage : OwaPage
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600025E RID: 606 RVA: 0x000153A8 File Offset: 0x000135A8
		protected override bool UseStrictMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000153AC File Offset: 0x000135AC
		protected ListViewPage(Trace callTracer, Trace algorithmTracer)
		{
			this.callTracer = callTracer;
			this.algorithmTracer = algorithmTracer;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000260 RID: 608
		internal abstract StoreObjectId DefaultFolderId { get; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000261 RID: 609
		protected abstract SortOrder DefaultSortOrder { get; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000262 RID: 610
		protected abstract ColumnId DefaultSortedColumn { get; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000263 RID: 611
		protected abstract string CheckBoxId { get; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000264 RID: 612 RVA: 0x00015403 File Offset: 0x00013603
		// (set) Token: 0x06000265 RID: 613 RVA: 0x0001540B File Offset: 0x0001360B
		internal Folder Folder
		{
			get
			{
				return this.folder;
			}
			set
			{
				this.folder = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00015414 File Offset: 0x00013614
		internal StoreObjectId FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0001541C File Offset: 0x0001361C
		public string FolderName
		{
			get
			{
				return this.folder.DisplayName;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00015429 File Offset: 0x00013629
		protected bool IsInDeletedItems
		{
			get
			{
				return Utilities.IsDefaultFolderId(base.UserContext.MailboxSession, this.FolderId, DefaultFolderType.DeletedItems);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00015442 File Offset: 0x00013642
		protected bool IsInJunkEmail
		{
			get
			{
				return Utilities.IsDefaultFolderId(base.UserContext.MailboxSession, this.FolderId, DefaultFolderType.JunkEmail);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0001545B File Offset: 0x0001365B
		// (set) Token: 0x0600026B RID: 619 RVA: 0x00015463 File Offset: 0x00013663
		protected int FirstItemOnPage
		{
			get
			{
				return this.firstItemOnPage;
			}
			set
			{
				this.firstItemOnPage = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0001546C File Offset: 0x0001366C
		// (set) Token: 0x0600026D RID: 621 RVA: 0x00015474 File Offset: 0x00013674
		protected int LastItemOnPage
		{
			get
			{
				return this.lastItemOnPage;
			}
			set
			{
				this.lastItemOnPage = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0001547D File Offset: 0x0001367D
		// (set) Token: 0x0600026F RID: 623 RVA: 0x00015485 File Offset: 0x00013685
		protected ListView ListView
		{
			get
			{
				return this.listView;
			}
			set
			{
				this.listView = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0001548E File Offset: 0x0001368E
		// (set) Token: 0x06000271 RID: 625 RVA: 0x00015496 File Offset: 0x00013696
		protected int ItemCount
		{
			get
			{
				return this.itemCount;
			}
			set
			{
				this.itemCount = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0001549F File Offset: 0x0001369F
		// (set) Token: 0x06000273 RID: 627 RVA: 0x000154A7 File Offset: 0x000136A7
		protected int PageNumber
		{
			get
			{
				return this.pageNumber;
			}
			set
			{
				this.pageNumber = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000274 RID: 628 RVA: 0x000154B0 File Offset: 0x000136B0
		// (set) Token: 0x06000275 RID: 629 RVA: 0x000154B8 File Offset: 0x000136B8
		public bool FilteredView
		{
			get
			{
				return this.filteredView;
			}
			set
			{
				this.filteredView = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000276 RID: 630 RVA: 0x000154C1 File Offset: 0x000136C1
		protected Infobar Infobar
		{
			get
			{
				return this.infobar;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000277 RID: 631 RVA: 0x000154C9 File Offset: 0x000136C9
		internal Folder SearchFolder
		{
			get
			{
				return this.searchFolder;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000278 RID: 632 RVA: 0x000154D1 File Offset: 0x000136D1
		protected string SearchString
		{
			get
			{
				return this.searchString;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000279 RID: 633 RVA: 0x000154D9 File Offset: 0x000136D9
		internal SearchScope SearchScope
		{
			get
			{
				return this.searchScope;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600027A RID: 634 RVA: 0x000154E1 File Offset: 0x000136E1
		protected int SearchScopeInt
		{
			get
			{
				return (int)this.searchScope;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600027B RID: 635 RVA: 0x000154E9 File Offset: 0x000136E9
		protected SortOrder SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600027C RID: 636 RVA: 0x000154F1 File Offset: 0x000136F1
		protected ColumnId SortedColumn
		{
			get
			{
				return this.sortedColumn;
			}
		}

		// Token: 0x0600027D RID: 637
		protected abstract void CreateListView(ColumnId sortedColumn, SortOrder sortOrder);

		// Token: 0x0600027E RID: 638 RVA: 0x000154FC File Offset: 0x000136FC
		protected override void OnLoad(EventArgs e)
		{
			this.callTracer.TraceDebug((long)this.GetHashCode(), "ListViewPage.OnLoad");
			this.errorMessage = string.Empty;
			this.folderId = QueryStringUtilities.CreateFolderStoreObjectId(base.UserContext.MailboxSession, base.Request, false);
			if (this.folderId == null)
			{
				this.algorithmTracer.TraceDebug((long)this.GetHashCode(), "folderId is null, using default folder");
				this.folderId = this.DefaultFolderId;
			}
			else if (!Folder.IsFolderId(this.folderId))
			{
				throw new OwaInvalidRequestException("The given Id is not a valid folder Id. Folder Id:" + this.folderId);
			}
			bool newSearch = base.UserContext.ForceNewSearch;
			this.GetSearchStringAndScope();
			bool flag = false;
			ColumnId value = this.DefaultSortedColumn;
			SortOrder sortOrder = this.DefaultSortOrder;
			if (base.IsPostFromMyself())
			{
				string formParameter = Utilities.GetFormParameter(base.Request, "hidcmdpst", false);
				string key;
				if ((key = formParameter) != null)
				{
					if (<PrivateImplementationDetails>{30BFC313-DE03-42E1-890C-A081E6097637}.$$method0x6000270-1 == null)
					{
						<PrivateImplementationDetails>{30BFC313-DE03-42E1-890C-A081E6097637}.$$method0x6000270-1 = new Dictionary<string, int>(10)
						{
							{
								"s",
								0
							},
							{
								"delete",
								1
							},
							{
								"markread",
								2
							},
							{
								"markunread",
								3
							},
							{
								"emptyfolder",
								4
							},
							{
								"junk",
								5
							},
							{
								"notjunk",
								6
							},
							{
								"addjnkeml",
								7
							},
							{
								"hideoof",
								8
							},
							{
								"turnoffoof",
								9
							}
						};
					}
					int num;
					if (<PrivateImplementationDetails>{30BFC313-DE03-42E1-890C-A081E6097637}.$$method0x6000270-1.TryGetValue(key, out num))
					{
						switch (num)
						{
						case 0:
							flag = true;
							value = (ColumnId)RequestParser.GetIntValueFromForm(base.Request, "hidcid");
							sortOrder = (SortOrder)RequestParser.GetIntValueFromForm(base.Request, "hidso");
							if (base.UserContext.IsWebPartRequest)
							{
								goto IL_4F2;
							}
							using (UserConfiguration folderConfiguration = UserConfigurationUtilities.GetFolderConfiguration("Owa.BasicFolderOption", base.UserContext, this.folderId))
							{
								if (folderConfiguration != null)
								{
									IDictionary dictionary = folderConfiguration.GetDictionary();
									dictionary["SortColumn"] = ColumnIdParser.GetString(value);
									dictionary["SortOrder"] = (int)sortOrder;
									try
									{
										folderConfiguration.Save();
									}
									catch (StoragePermanentException ex)
									{
										this.algorithmTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Failed to save configuration data. Error: {0}. Stack: {1}.", ex.Message, ex.StackTrace);
									}
									catch (StorageTransientException ex2)
									{
										this.algorithmTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Failed to save configuration data. Error: {0}. Stack: {1}.", ex2.Message, ex2.StackTrace);
									}
								}
								goto IL_4F2;
							}
							break;
						case 1:
							break;
						case 2:
							this.sourceIds = this.GetSelectedItems();
							Utilities.BasicMarkUserMailboxItemsAsRead(base.UserContext, this.sourceIds, this.GetJunkEmailStatus(), false);
							goto IL_4F2;
						case 3:
							this.sourceIds = this.GetSelectedItems();
							Utilities.BasicMarkUserMailboxItemsAsRead(base.UserContext, this.sourceIds, this.GetJunkEmailStatus(), true);
							goto IL_4F2;
						case 4:
							if (this.IsInDeletedItems || this.IsInJunkEmail)
							{
								base.UserContext.MailboxSession.DeleteAllObjects(DeleteItemFlags.SoftDelete, this.folderId);
							}
							if (this.filteredView)
							{
								newSearch = true;
								goto IL_4F2;
							}
							goto IL_4F2;
						case 5:
						{
							if (!base.UserContext.IsJunkEmailEnabled)
							{
								throw new OwaInvalidRequestException(LocalizedStrings.GetNonEncoded(552277155));
							}
							this.sourceIds = this.GetSelectedItems();
							InfobarMessage infobarMessage = JunkEmailHelper.MarkAsJunk(base.UserContext, this.sourceIds);
							if (infobarMessage != null)
							{
								this.infobar.AddMessage(infobarMessage);
							}
							if (this.filteredView)
							{
								newSearch = true;
								goto IL_4F2;
							}
							goto IL_4F2;
						}
						case 6:
						{
							if (!base.UserContext.IsJunkEmailEnabled)
							{
								throw new OwaInvalidRequestException(LocalizedStrings.GetNonEncoded(552277155));
							}
							this.sourceIds = this.GetSelectedItems();
							InfobarMessage infobarMessage2 = JunkEmailHelper.MarkAsNotJunk(base.UserContext, this.sourceIds);
							if (infobarMessage2 != null)
							{
								this.infobar.AddMessage(infobarMessage2);
							}
							if (this.filteredView)
							{
								newSearch = true;
								goto IL_4F2;
							}
							goto IL_4F2;
						}
						case 7:
						{
							if (!base.UserContext.IsJunkEmailEnabled)
							{
								throw new OwaInvalidRequestException(LocalizedStrings.GetNonEncoded(552277155));
							}
							InfobarMessage infobarMessage3 = JunkEmailHelper.AddEmailToSendersList(base.UserContext, base.Request);
							if (infobarMessage3 != null)
							{
								this.infobar.AddMessage(infobarMessage3);
								goto IL_4F2;
							}
							goto IL_4F2;
						}
						case 8:
							if (base.UserContext.IsWebPartRequest)
							{
								throw new OwaInvalidRequestException("Should not show out of office infobar in web part request");
							}
							RenderingFlags.HideOutOfOfficeInfoBar(base.UserContext, true);
							goto IL_4F2;
						case 9:
						{
							if (base.UserContext.IsWebPartRequest)
							{
								throw new OwaInvalidRequestException("Should not show out of office dialog in web part request");
							}
							UserOofSettings userOofSettings = UserOofSettings.GetUserOofSettings(base.UserContext.MailboxSession);
							userOofSettings.OofState = OofState.Disabled;
							userOofSettings.Save(base.UserContext.MailboxSession);
							goto IL_4F2;
						}
						default:
							goto IL_4F2;
						}
						this.sourceIds = this.GetSelectedItems();
						this.DeleteCalendarItems(this.sourceIds);
						if (this.IsInDeletedItems)
						{
							Utilities.DeleteItems(base.UserContext, DeleteItemFlags.SoftDelete, this.sourceIds);
						}
						else
						{
							Utilities.DeleteItems(base.UserContext, DeleteItemFlags.MoveToDeletedItems, this.sourceIds);
						}
						if (this.filteredView)
						{
							newSearch = true;
						}
					}
				}
			}
			IL_4F2:
			this.folder = Folder.Bind(base.UserContext.MailboxSession, this.folderId);
			this.sortOrder = this.DefaultSortOrder;
			this.sortedColumn = this.DefaultSortedColumn;
			if (base.UserContext.IsWebPartRequest)
			{
				if (!flag)
				{
					string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "view", false);
					WebPartModuleViewState webPartModuleViewState = base.UserContext.LastClientViewState as WebPartModuleViewState;
					if (string.IsNullOrEmpty(queryStringParameter) && webPartModuleViewState != null)
					{
						this.sortedColumn = webPartModuleViewState.SortedColumn;
						this.sortOrder = webPartModuleViewState.SortOrder;
					}
					else
					{
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
						}
					}
				}
				else
				{
					this.sortedColumn = value;
					this.sortOrder = sortOrder;
				}
			}
			else
			{
				using (UserConfiguration folderConfiguration2 = UserConfigurationUtilities.GetFolderConfiguration("Owa.BasicFolderOption", base.UserContext, this.folder.Id))
				{
					if (folderConfiguration2 != null)
					{
						IDictionary dictionary2 = folderConfiguration2.GetDictionary();
						object obj = dictionary2["SortColumn"];
						if (obj != null)
						{
							this.sortedColumn = ColumnIdParser.Parse((string)obj);
						}
						if (this.sortedColumn == ColumnId.Count)
						{
							this.sortedColumn = this.DefaultSortedColumn;
						}
						obj = dictionary2["SortOrder"];
						if (obj != null)
						{
							this.sortOrder = (SortOrder)obj;
						}
					}
				}
			}
			if (this.algorithmTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				this.algorithmTracer.TraceDebug((long)this.GetHashCode(), "Creating ListView with sortedColumn={0}, sortOrder={1}, and folder: name={2} id={3}", new object[]
				{
					(int)this.sortedColumn,
					(int)this.sortOrder,
					this.folder.DisplayName,
					this.folder.Id.ToBase64String()
				});
			}
			if (!this.filteredView)
			{
				this.itemCount = this.Folder.ItemCount;
			}
			else
			{
				string queryStringParameter2 = Utilities.GetQueryStringParameter(base.Request, "newSch", false);
				if (!string.IsNullOrEmpty(queryStringParameter2) && string.CompareOrdinal(queryStringParameter2, "1") == 0)
				{
					newSearch = true;
				}
				FolderSearch folderSearch = new FolderSearch();
				this.searchFolder = folderSearch.Execute(base.UserContext, this.Folder, this.searchScope, this.searchString, newSearch, false);
				base.UserContext.ForceNewSearch = false;
				this.itemCount = this.searchFolder.ItemCount;
			}
			this.pageNumber = RequestParser.TryGetIntValueFromQueryString(base.Request, "pg", 1);
			if (this.pageNumber < 1)
			{
				this.pageNumber = 1;
			}
			if (this.itemCount <= 0)
			{
				this.firstItemOnPage = 1;
				this.lastItemOnPage = 1;
				this.pageNumber = 1;
				this.numberOfPages = 1;
			}
			else if (this.itemCount > 0)
			{
				this.numberOfPages = (this.itemCount - 1) / base.UserContext.UserOptions.BasicViewRowCount + 1;
				this.pageNumber = Math.Min(this.pageNumber, this.numberOfPages);
				this.firstItemOnPage = (this.pageNumber - 1) * base.UserContext.UserOptions.BasicViewRowCount + 1;
				this.lastItemOnPage = this.firstItemOnPage + base.UserContext.UserOptions.BasicViewRowCount - 1;
				this.firstItemOnPage = Math.Min(this.firstItemOnPage, this.itemCount);
				this.lastItemOnPage = Math.Min(this.lastItemOnPage, this.itemCount);
				this.firstItemOnPage = Math.Max(this.firstItemOnPage, 1);
				this.lastItemOnPage = Math.Max(this.lastItemOnPage, 1);
			}
			this.CreateListView(this.sortedColumn, this.sortOrder);
			if (this.FilteredView)
			{
				this.BuildSearchInfobarMessage();
			}
			base.OnLoad(e);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00015E48 File Offset: 0x00014048
		private JunkEmailStatus GetJunkEmailStatus()
		{
			if (!this.IsInJunkEmail)
			{
				return JunkEmailStatus.NotJunk;
			}
			if (this.filteredView)
			{
				return JunkEmailStatus.Unknown;
			}
			return JunkEmailStatus.Junk;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00015E5F File Offset: 0x0001405F
		protected override void OnUnload(EventArgs e)
		{
			if (this.folder != null)
			{
				this.folder.Dispose();
			}
			if (this.searchFolder != null)
			{
				this.searchFolder.Dispose();
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00015E87 File Offset: 0x00014087
		protected void InitializeListView()
		{
			this.ListView.Initialize(this.FirstItemOnPage, this.LastItemOnPage);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00015EA0 File Offset: 0x000140A0
		protected void RenderListView()
		{
			this.listView.Render(base.Response.Output, this.errorMessage);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00015EC0 File Offset: 0x000140C0
		private void GetSearchStringAndScope()
		{
			this.searchString = Utilities.GetQueryStringParameter(base.Request, "sch", false);
			if (!string.IsNullOrEmpty(this.searchString))
			{
				this.searchString = this.searchString.Trim();
				if (this.searchString.Length > Globals.MaxSearchStringLength)
				{
					throw new OwaInvalidRequestException("Search string length is more than 256 characters");
				}
				Utilities.VerifySearchCanaryInGetRequest(base.Request);
				this.filteredView = !string.IsNullOrEmpty(this.searchString);
				this.searchScope = (SearchScope)RequestParser.TryGetIntValueFromQueryString(base.Request, "scp", (int)this.searchScope);
				if (this.searchScope != SearchScope.SelectedFolder && this.searchScope != SearchScope.SelectedAndSubfolders && this.searchScope != SearchScope.AllFoldersAndItems)
				{
					throw new OwaInvalidRequestException("Search scope is not supported");
				}
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00015F84 File Offset: 0x00014184
		public void RenderNavigation(NavigationModule navigationModule)
		{
			Navigation navigation = new Navigation(navigationModule, base.OwaContext, base.Response.Output);
			navigation.Render();
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00015FB0 File Offset: 0x000141B0
		protected void RenderPaging(bool renderPageNumbers)
		{
			if (this.pageNumber < 1)
			{
				this.pageNumber = 1;
			}
			if (renderPageNumbers)
			{
				ListView.RenderPageNumbers(base.Response.Output, this.pageNumber, this.numberOfPages);
				base.Response.Write("<td>&nbsp;</td>");
				this.listView.RenderPagingControls(base.Response.Output, this.pageNumber, this.numberOfPages);
				return;
			}
			this.listView.RenderHeaderPagingControls(base.Response.Output, this.pageNumber, this.numberOfPages);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00016044 File Offset: 0x00014244
		private StoreObjectId[] GetSelectedItems()
		{
			string formParameter = Utilities.GetFormParameter(base.Request, this.CheckBoxId);
			string[] array = formParameter.Split(new char[]
			{
				','
			});
			if (array.Length == 0)
			{
				throw new OwaInvalidRequestException("No item is selected");
			}
			StoreObjectId[] array2 = new StoreObjectId[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = Utilities.CreateStoreObjectId(base.UserContext.MailboxSession, array[i]);
			}
			if (base.UserContext.UserOptions.BasicViewRowCount < array2.Length)
			{
				throw new OwaInvalidOperationException(string.Format("This action is not supported for {0} item(s) in a single request", array2.Length));
			}
			return array2;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000160E8 File Offset: 0x000142E8
		private void DeleteCalendarItems(StoreObjectId[] selectedItems)
		{
			if (selectedItems == null)
			{
				throw new ArgumentNullException("selectedItems");
			}
			string formParameter = Utilities.GetFormParameter(base.Request, "hidmtgmsg", false);
			if (formParameter != null)
			{
				for (int i = 0; i < selectedItems.Length; i++)
				{
					if (formParameter.IndexOf(selectedItems[i].ToBase64String(), StringComparison.Ordinal) != -1)
					{
						MeetingUtilities.DeleteMeetingMessageCalendarItem(selectedItems[i]);
					}
				}
			}
		}

		// Token: 0x06000288 RID: 648
		protected abstract SanitizedHtmlString BuildConcretSearchInfobarMessage(int resultsCount, SanitizedHtmlString clearSearchLink);

		// Token: 0x06000289 RID: 649 RVA: 0x00016140 File Offset: 0x00014340
		protected void BuildSearchInfobarMessage()
		{
			SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>();
			sanitizingStringBuilder.Append("<a href=\"#\" onclick=\"return onClkClrLnk();\">");
			sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(1155007962));
			sanitizingStringBuilder.Append("</a>");
			if (this.ListView.TotalCount == 0)
			{
				this.Infobar.AddMessageHtml(SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(-761327948), new object[]
				{
					sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>()
				}), InfobarMessageType.Informational);
			}
			else
			{
				int num = this.ListView.TotalCount;
				object obj = this.SearchFolder.TryGetProperty(FolderSchema.SearchFolderItemCount);
				if (obj is int)
				{
					num = (int)obj;
				}
				this.Infobar.AddMessageHtml(this.BuildConcretSearchInfobarMessage(num, sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>()), InfobarMessageType.Informational);
				if (num > this.ListView.TotalCount)
				{
					this.Infobar.AddMessageLocalized(825345585, InfobarMessageType.Informational);
				}
			}
			if (!base.UserContext.MailboxSession.Mailbox.IsContentIndexingEnabled)
			{
				this.Infobar.AddMessageLocalized(-332074645, InfobarMessageType.Informational);
			}
		}

		// Token: 0x040001AC RID: 428
		internal const string PageNumberQueryStringParameter = "pg";

		// Token: 0x040001AD RID: 429
		internal const string FindAction = "Find";

		// Token: 0x040001AE RID: 430
		internal const string SearchStringQueryStringParameter = "sch";

		// Token: 0x040001AF RID: 431
		internal const string SearchScopeQueryStringParameter = "scp";

		// Token: 0x040001B0 RID: 432
		private const string IsNewSearchQueryParameter = "newSch";

		// Token: 0x040001B1 RID: 433
		private const string IsNewSearchQueryValue = "1";

		// Token: 0x040001B2 RID: 434
		private const string CommandPostValue = "hidcmdpst";

		// Token: 0x040001B3 RID: 435
		private const string SortColumnIdValue = "hidcid";

		// Token: 0x040001B4 RID: 436
		private const string SortOrderValue = "hidso";

		// Token: 0x040001B5 RID: 437
		private const string MeetingMessageIdFormValue = "hidmtgmsg";

		// Token: 0x040001B6 RID: 438
		internal const string BasicFolderOptionConfigurationName = "Owa.BasicFolderOption";

		// Token: 0x040001B7 RID: 439
		internal const string SortColumnKey = "SortColumn";

		// Token: 0x040001B8 RID: 440
		internal const string SortOrderKey = "SortOrder";

		// Token: 0x040001B9 RID: 441
		private Folder folder;

		// Token: 0x040001BA RID: 442
		private StoreObjectId folderId;

		// Token: 0x040001BB RID: 443
		private ListView listView;

		// Token: 0x040001BC RID: 444
		private int itemCount = -1;

		// Token: 0x040001BD RID: 445
		private int pageNumber = -1;

		// Token: 0x040001BE RID: 446
		private int firstItemOnPage;

		// Token: 0x040001BF RID: 447
		private int lastItemOnPage;

		// Token: 0x040001C0 RID: 448
		private StoreObjectId[] sourceIds;

		// Token: 0x040001C1 RID: 449
		private bool filteredView;

		// Token: 0x040001C2 RID: 450
		private string searchString = string.Empty;

		// Token: 0x040001C3 RID: 451
		private SearchScope searchScope;

		// Token: 0x040001C4 RID: 452
		private Infobar infobar = new Infobar();

		// Token: 0x040001C5 RID: 453
		private Folder searchFolder;

		// Token: 0x040001C6 RID: 454
		private string errorMessage = string.Empty;

		// Token: 0x040001C7 RID: 455
		private Trace callTracer;

		// Token: 0x040001C8 RID: 456
		private Trace algorithmTracer;

		// Token: 0x040001C9 RID: 457
		private int numberOfPages = -1;

		// Token: 0x040001CA RID: 458
		private ColumnId sortedColumn;

		// Token: 0x040001CB RID: 459
		private SortOrder sortOrder;
	}
}
