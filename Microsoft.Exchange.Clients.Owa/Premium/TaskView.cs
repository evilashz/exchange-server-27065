using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200047D RID: 1149
	public class TaskView : FolderListViewSubPage, IRegistryOnlyForm
	{
		// Token: 0x06002C17 RID: 11287 RVA: 0x000F5EE0 File Offset: 0x000F40E0
		public TaskView() : base(ExTraceGlobals.TasksCallTracer, ExTraceGlobals.TasksTracer)
		{
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06002C18 RID: 11288 RVA: 0x000F5F2A File Offset: 0x000F412A
		protected static int StoreObjectTypeTasksFolder
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06002C19 RID: 11289 RVA: 0x000F5F2D File Offset: 0x000F412D
		protected static int ImportanceLow
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06002C1A RID: 11290 RVA: 0x000F5F30 File Offset: 0x000F4130
		protected static int ImportanceNormal
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06002C1B RID: 11291 RVA: 0x000F5F33 File Offset: 0x000F4133
		protected static int ImportanceHigh
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06002C1C RID: 11292 RVA: 0x000F5F38 File Offset: 0x000F4138
		protected override string ContainerName
		{
			get
			{
				if (base.FolderType == DefaultFolderType.ToDoSearch)
				{
					return LocalizedStrings.GetNonEncoded(-1954334922);
				}
				if (base.IsArchiveMailboxFolder)
				{
					return string.Format(LocalizedStrings.GetNonEncoded(-83764036), base.Folder.DisplayName, Utilities.GetMailboxOwnerDisplayName((MailboxSession)base.Folder.Session));
				}
				if (base.IsOtherMailboxFolder)
				{
					return Utilities.GetFolderNameWithSessionName(base.Folder);
				}
				return base.Folder.DisplayName;
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06002C1D RID: 11293 RVA: 0x000F5FB1 File Offset: 0x000F41B1
		internal StoreObjectId FolderId
		{
			get
			{
				return base.Folder.Id.ObjectId;
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06002C1E RID: 11294 RVA: 0x000F5FC3 File Offset: 0x000F41C3
		internal override StoreObjectId DefaultFolderId
		{
			get
			{
				return base.UserContext.FlaggedItemsAndTasksFolderId;
			}
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06002C1F RID: 11295 RVA: 0x000F5FD0 File Offset: 0x000F41D0
		protected override SortOrder DefaultSortOrder
		{
			get
			{
				return SortOrder.Ascending;
			}
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06002C20 RID: 11296 RVA: 0x000F5FD3 File Offset: 0x000F41D3
		protected override ColumnId DefaultSortedColumn
		{
			get
			{
				return ColumnId.DueDate;
			}
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06002C21 RID: 11297 RVA: 0x000F5FD7 File Offset: 0x000F41D7
		protected override ReadingPanePosition DefaultReadingPanePosition
		{
			get
			{
				return ReadingPanePosition.Right;
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06002C22 RID: 11298 RVA: 0x000F5FDA File Offset: 0x000F41DA
		protected override bool DefaultMultiLineSetting
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06002C23 RID: 11299 RVA: 0x000F5FDD File Offset: 0x000F41DD
		protected override bool FindBarOn
		{
			get
			{
				return !base.IsPublicFolder && base.UserContext.UserOptions.MailFindBarOn;
			}
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06002C24 RID: 11300 RVA: 0x000F5FF9 File Offset: 0x000F41F9
		protected TaskViewContextMenu ContextMenu
		{
			get
			{
				if (this.contextMenu == null)
				{
					this.contextMenu = new TaskViewContextMenu(base.UserContext);
				}
				return this.contextMenu;
			}
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06002C25 RID: 11301 RVA: 0x000F601A File Offset: 0x000F421A
		protected TaskViewArrangeByMenu ArrangeByMenu
		{
			get
			{
				if (this.arrangeByMenu == null)
				{
					this.arrangeByMenu = new TaskViewArrangeByMenu();
				}
				return this.arrangeByMenu;
			}
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x000F6038 File Offset: 0x000F4238
		protected override void LoadViewState()
		{
			base.LoadViewState();
			if (!base.UserContext.IsWebPartRequest)
			{
				this.viewWidth = base.UserContext.GetFolderViewStates(base.Folder).GetViewWidth(381);
			}
			if (!base.UserContext.IsWebPartRequest)
			{
				this.filterType = Utilities.GetFolderProperty<TaskFilterType>(base.Folder, ViewStateProperties.ViewFilter, TaskFilterType.All);
				return;
			}
			if (this.IsFlaggedMailAndTasks && this.SortedColumn == ColumnId.DueDate)
			{
				base.SetSortOrder(SortOrder.Ascending);
			}
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x000F60B8 File Offset: 0x000F42B8
		protected override IListView CreateListView(ColumnId sortedColumn, SortOrder sortOrder)
		{
			TaskVirtualListView taskVirtualListView = new TaskVirtualListView(base.UserContext, "divVLV", sortedColumn, sortOrder, base.Folder, TaskView.GetFilter(this.filterType), (base.Folder is SearchFolder) ? SearchScope.AllFoldersAndItems : SearchScope.SelectedFolder, this.CanCreateItem);
			VirtualListView2 virtualListView = taskVirtualListView;
			string name = "iFltr";
			int num = (int)this.filterType;
			virtualListView.AddAttribute(name, num.ToString(CultureInfo.InvariantCulture));
			taskVirtualListView.LoadData(0, 50);
			return taskVirtualListView;
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x000F6128 File Offset: 0x000F4328
		protected override Toolbar CreateListToolbar()
		{
			return new TaskViewListToolbar(base.IsPublicFolder, base.IsOtherMailboxFolder, base.UserContext.IsWebPartRequest, this.ReadingPanePosition);
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x000F6159 File Offset: 0x000F4359
		protected override Toolbar CreateActionToolbar()
		{
			return null;
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x000F615C File Offset: 0x000F435C
		protected void RenderPontStrings()
		{
			if (base.UserContext.UserOptions.IsPontEnabled(PontType.DeleteFlaggedMessage))
			{
				RenderingUtilities.RenderStringVariable(base.Response.Output, "L_PntMsg", 1701858762);
			}
			if (base.UserContext.UserOptions.IsPontEnabled(PontType.DeleteFlaggedContacts))
			{
				RenderingUtilities.RenderStringVariable(base.Response.Output, "L_PntCnt", -1776379122);
			}
			if (base.UserContext.UserOptions.IsPontEnabled(PontType.DeleteFlaggedItems))
			{
				RenderingUtilities.RenderStringVariable(base.Response.Output, "L_PntMlt", 259109454);
			}
			if (base.UserContext.UserOptions.IsPontEnabled(PontType.DeleteFlaggedContacts) || base.UserContext.UserOptions.IsPontEnabled(PontType.DeleteFlaggedMessage) || base.UserContext.UserOptions.IsPontEnabled(PontType.DeleteFlaggedItems))
			{
				RenderingUtilities.RenderStringVariable(base.Response.Output, "L_Wrn", 1861340610);
				RenderingUtilities.RenderStringVariable(base.Response.Output, "L_Cntnu", -1719707164);
				RenderingUtilities.RenderStringVariable(base.Response.Output, "L_DntShw", -1294868987);
			}
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x000F6278 File Offset: 0x000F4478
		protected void RenderArrangeByMenu()
		{
			TaskViewArrangeByMenu taskViewArrangeByMenu = new TaskViewArrangeByMenu();
			taskViewArrangeByMenu.Render(base.Response.Output, base.UserContext);
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x000F62A4 File Offset: 0x000F44A4
		protected void RenderContextMenu()
		{
			TaskViewContextMenu taskViewContextMenu = new TaskViewContextMenu(base.UserContext);
			taskViewContextMenu.Render(base.Response.Output);
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06002C2D RID: 11309 RVA: 0x000F62CE File Offset: 0x000F44CE
		protected bool IsFlaggedMailAndTasks
		{
			get
			{
				return base.FolderType == DefaultFolderType.ToDoSearch;
			}
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06002C2E RID: 11310 RVA: 0x000F62DA File Offset: 0x000F44DA
		protected bool CanCreateItem
		{
			get
			{
				return Utilities.CanCreateItemInFolder(base.Folder);
			}
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x000F62E8 File Offset: 0x000F44E8
		internal static QueryFilter GetFilter(TaskFilterType filterType)
		{
			switch (filterType)
			{
			case TaskFilterType.All:
				return null;
			case TaskFilterType.Active:
				return TaskView.activeFilter;
			case TaskFilterType.Overdue:
			{
				QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.LessThan, TaskSchema.DueDate, DateTimeUtilities.GetLocalTime().Date);
				return new AndFilter(new QueryFilter[]
				{
					queryFilter,
					TaskView.activeFilter
				});
			}
			case TaskFilterType.Completed:
				return TaskView.completeFilter;
			default:
				throw new OwaInvalidRequestException("Unknown value for TaskFilterType");
			}
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x000F6361 File Offset: 0x000F4561
		internal static void RenderSecondaryNavigation(TextWriter output, UserContext userContext)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			TaskView.RenderSecondaryNavigationFilter(output, "divTskFlt");
			NavigationHost.RenderNavigationTreeControl(output, userContext, NavigationModule.Tasks);
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x000F6394 File Offset: 0x000F4594
		internal static void RenderSecondaryNavigationFilter(TextWriter output, string divId)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (string.IsNullOrEmpty(divId))
			{
				throw new ArgumentException("divId should not be null or empty");
			}
			SecondaryNavigationFilter secondaryNavigationFilter = new SecondaryNavigationFilter(divId, LocalizedStrings.GetNonEncoded(-428271462), "onClkTskFlt(\"" + Utilities.JavascriptEncode(divId) + "\")");
			secondaryNavigationFilter.AddFilter(LocalizedStrings.GetNonEncoded(1912141011), 1, false);
			secondaryNavigationFilter.AddFilter(LocalizedStrings.GetNonEncoded(868758546), 2, false);
			secondaryNavigationFilter.AddFilter(LocalizedStrings.GetNonEncoded(-1626869372), 3, false);
			secondaryNavigationFilter.AddFilter(LocalizedStrings.GetNonEncoded(-1035255369), 4, false);
			secondaryNavigationFilter.Render(output);
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06002C32 RID: 11314 RVA: 0x000F6437 File Offset: 0x000F4637
		public override IEnumerable<string> ExternalScriptFiles
		{
			get
			{
				return this.externalScriptFiles;
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06002C33 RID: 11315 RVA: 0x000F643F File Offset: 0x000F463F
		public override SanitizedHtmlString Title
		{
			get
			{
				return new SanitizedHtmlString(this.ContainerName);
			}
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06002C34 RID: 11316 RVA: 0x000F644C File Offset: 0x000F464C
		public override string PageType
		{
			get
			{
				return "TaskViewPage";
			}
		}

		// Token: 0x04001D10 RID: 7440
		private static readonly QueryFilter activeFilter = new AndFilter(new QueryFilter[]
		{
			new NotFilter(new ExistsFilter(ItemSchema.CompleteDate)),
			new NotFilter(new ExistsFilter(ItemSchema.FlagCompleteTime))
		});

		// Token: 0x04001D11 RID: 7441
		private static readonly QueryFilter completeFilter = new OrFilter(new QueryFilter[]
		{
			new ExistsFilter(ItemSchema.CompleteDate),
			new ExistsFilter(ItemSchema.FlagCompleteTime),
			new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.FlagStatus, FlagStatus.Complete)
		});

		// Token: 0x04001D12 RID: 7442
		private TaskFilterType filterType = TaskFilterType.All;

		// Token: 0x04001D13 RID: 7443
		private TaskViewContextMenu contextMenu;

		// Token: 0x04001D14 RID: 7444
		private TaskViewArrangeByMenu arrangeByMenu;

		// Token: 0x04001D15 RID: 7445
		private string[] externalScriptFiles = new string[]
		{
			"uview.js",
			"vlv.js",
			"taskvw.js"
		};
	}
}
