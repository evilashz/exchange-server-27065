using System;
using System.Collections;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000424 RID: 1060
	internal class TaskVirtualListView : VirtualListView2
	{
		// Token: 0x060025D8 RID: 9688 RVA: 0x000DB2F0 File Offset: 0x000D94F0
		public TaskVirtualListView(UserContext userContext, string id, ColumnId sortedColumn, SortOrder sortOrder, Folder dataFolder, QueryFilter queryFilter, SearchScope folderScope, bool userCanCreateItem) : this(userContext, id, sortedColumn, sortOrder, dataFolder, queryFilter, folderScope, userCanCreateItem, false)
		{
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x000DB311 File Offset: 0x000D9511
		public TaskVirtualListView(UserContext userContext, string id, ColumnId sortedColumn, SortOrder sortOrder, Folder dataFolder, QueryFilter queryFilter, SearchScope folderScope, bool userCanCreateItem, bool isFiltered) : base(userContext, id, false, sortedColumn, sortOrder, isFiltered)
		{
			this.dataFolder = dataFolder;
			this.queryFilter = queryFilter;
			this.folderScope = folderScope;
			this.userCanCreateItem = userCanCreateItem;
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x060025DA RID: 9690 RVA: 0x000DB341 File Offset: 0x000D9541
		protected override Folder DataFolder
		{
			get
			{
				return this.dataFolder;
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x060025DB RID: 9691 RVA: 0x000DB349 File Offset: 0x000D9549
		public override ViewType ViewType
		{
			get
			{
				return ViewType.Task;
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x060025DC RID: 9692 RVA: 0x000DB34C File Offset: 0x000D954C
		public override string OehNamespace
		{
			get
			{
				return "TskVLV";
			}
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x000DB354 File Offset: 0x000D9554
		protected override ListViewContents2 CreateListViewContents()
		{
			ListViewContents2 listViewContents = new TaskSingleLineList(TaskVirtualListView.taskViewDescriptor, base.SortedColumn, base.SortOrder, base.UserContext, this.folderScope);
			ColumnId sortedColumn = base.SortedColumn;
			if (sortedColumn == ColumnId.DueDate)
			{
				listViewContents = new TimeGroupByList2(ColumnId.DueDate, base.SortOrder, (ItemList2)listViewContents, base.UserContext);
			}
			else
			{
				Column column = ListViewColumns.GetColumn(base.SortedColumn);
				if (column.GroupType == GroupType.Expanded)
				{
					listViewContents = new GroupByList2(base.SortedColumn, base.SortOrder, (ItemList2)listViewContents, base.UserContext);
				}
			}
			return listViewContents;
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x000DB3E0 File Offset: 0x000D95E0
		protected override IListViewDataSource CreateDataSource(Hashtable properties)
		{
			return new FolderListViewDataSource(base.UserContext, properties, this.dataFolder, this.GetSortByProperties(), this.queryFilter);
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x000DB400 File Offset: 0x000D9600
		protected override void OnBeforeRender()
		{
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
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x000DB491 File Offset: 0x000D9691
		protected override void InternalRenderData(TextWriter writer)
		{
			base.InternalRenderData(writer);
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x000DB49A File Offset: 0x000D969A
		public override void RenderListViewClasses(TextWriter writer)
		{
			if (this.userCanCreateItem)
			{
				writer.Write(" class=\"taskListView\"");
				return;
			}
			writer.Write(" class=\"taskListViewNoQuickTask\"");
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x000DB4BC File Offset: 0x000D96BC
		protected override void RenderInlineControl(TextWriter writer)
		{
			if (this.userCanCreateItem)
			{
				writer.Write("<div id=divQTsk dtTdy=\"");
				writer.Write(DateTimeUtilities.GetJavascriptDate(DateTimeUtilities.GetLocalTime().Date));
				writer.Write("\" sDtFmt=\"");
				Utilities.HtmlEncode(base.UserContext.UserOptions.DateFormat, writer);
				writer.Write("\"><div id=\"divAddTask\" tabindex=\"0\" class=\"fltAfter\">");
				base.UserContext.RenderThemeImageWithToolTip(writer, ThemeFileId.AddTask, string.Empty, -2096871403, new string[0]);
				writer.Write("</div><div id=\"divDueDateImage\" class=\"fltAfter\">");
				base.UserContext.RenderThemeImage(writer, ThemeFileId.DownArrowGrey);
				writer.Write(string.Format(CultureInfo.InvariantCulture, "</div><div id=\"{0}\" class=\"fltAfter\" tabindex=\"0\" nowrap>", new object[]
				{
					"divDueDate"
				}));
				writer.Write(LocalizedStrings.GetHtmlEncoded(-481406887));
				writer.Write("</div><div id=\"divSbj\"><input id=\"txtTaskSubject\" type=\"text\" value=\"");
				writer.Write(LocalizedStrings.GetHtmlEncoded(488278548));
				writer.Write("\" maxLength=\"255\"></div></div>");
				return;
			}
			writer.Write(string.Format(CultureInfo.InvariantCulture, "<div id=\"{0}\" sytle=\"display:none\"></div>", new object[]
			{
				"divDueDate"
			}));
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x000DB5E4 File Offset: 0x000D97E4
		private SortBy[] GetSortByProperties()
		{
			Column column = ListViewColumns.GetColumn(base.SortedColumn);
			if (base.SortedColumn == ColumnId.DueDate)
			{
				return new SortBy[]
				{
					new SortBy(TaskSchema.DueDate, base.SortOrder)
				};
			}
			if (base.SortedColumn == ColumnId.TaskIcon)
			{
				return new SortBy[]
				{
					new SortBy(StoreObjectSchema.ItemClass, base.SortOrder),
					new SortBy(ItemSchema.IconIndex, base.SortOrder),
					new SortBy(TaskSchema.DueDate, SortOrder.Ascending)
				};
			}
			return new SortBy[]
			{
				new SortBy(column[0], base.SortOrder),
				new SortBy(TaskSchema.DueDate, SortOrder.Ascending)
			};
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x060025E4 RID: 9700 RVA: 0x000DB695 File Offset: 0x000D9895
		protected override bool HasInlineControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001A1E RID: 6686
		public const string DueDateId = "divDueDate";

		// Token: 0x04001A1F RID: 6687
		private static readonly ViewDescriptor taskViewDescriptor = new ViewDescriptor(ColumnId.DueDate, false, new ColumnId[]
		{
			ColumnId.TaskIcon,
			ColumnId.MarkCompleteCheckbox,
			ColumnId.Importance,
			ColumnId.HasAttachment,
			ColumnId.Subject,
			ColumnId.DueDate,
			ColumnId.Categories,
			ColumnId.TaskFlag
		});

		// Token: 0x04001A20 RID: 6688
		private Folder dataFolder;

		// Token: 0x04001A21 RID: 6689
		private QueryFilter queryFilter;

		// Token: 0x04001A22 RID: 6690
		private SearchScope folderScope;

		// Token: 0x04001A23 RID: 6691
		private bool userCanCreateItem;
	}
}
