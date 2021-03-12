using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000064 RID: 100
	public class MessageListView : ListView
	{
		// Token: 0x060002CE RID: 718 RVA: 0x00018860 File Offset: 0x00016A60
		internal MessageListView(UserContext userContext, ColumnId sortedColumn, SortOrder sortOrder, Folder folder) : base(userContext, sortedColumn, sortOrder, ListView.ViewType.MessageListView)
		{
			this.folder = folder;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00018874 File Offset: 0x00016A74
		internal MessageListView(UserContext userContext, ColumnId sortedColumn, SortOrder sortOrder, Folder folder, SearchScope searchScope) : this(userContext, sortedColumn, sortOrder, folder)
		{
			base.FilteredView = true;
			this.searchScope = searchScope;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00018890 File Offset: 0x00016A90
		protected override void InitializeListViewContents()
		{
			DefaultFolderType defaultFolderType = Utilities.GetDefaultFolderType(this.folder);
			if (defaultFolderType == DefaultFolderType.SentItems || defaultFolderType == DefaultFolderType.Outbox || defaultFolderType == DefaultFolderType.Drafts)
			{
				base.ViewDescriptor = MessageListView.To;
			}
			else
			{
				base.ViewDescriptor = MessageListView.From;
			}
			bool showFolderNameTooltip = base.FilteredView && this.searchScope != SearchScope.SelectedFolder;
			base.Contents = new MessageListViewContents(base.ViewDescriptor, base.SortedColumn, base.SortOrder, showFolderNameTooltip, base.UserContext);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0001890C File Offset: 0x00016B0C
		protected override void InitializeDataSource()
		{
			Column column = ListViewColumns.GetColumn(base.SortedColumn);
			SortBy[] sortBy;
			if (base.SortedColumn == ColumnId.DeliveryTime)
			{
				sortBy = new SortBy[]
				{
					new SortBy(ItemSchema.ReceivedTime, base.SortOrder)
				};
			}
			else
			{
				sortBy = new SortBy[]
				{
					new SortBy(column[0], base.SortOrder),
					new SortBy(ItemSchema.ReceivedTime, SortOrder.Descending)
				};
			}
			base.DataSource = new MessageListViewDataSource(base.Contents.Properties, this.folder, sortBy);
		}

		// Token: 0x04000210 RID: 528
		private static readonly ViewDescriptor From = new ViewDescriptor(ColumnId.DeliveryTime, false, new ColumnId[]
		{
			ColumnId.Importance,
			ColumnId.MailIcon,
			ColumnId.HasAttachment,
			ColumnId.CheckBox,
			ColumnId.From,
			ColumnId.Subject,
			ColumnId.DeliveryTime,
			ColumnId.Size
		});

		// Token: 0x04000211 RID: 529
		private static readonly ViewDescriptor To = new ViewDescriptor(ColumnId.SentTime, false, new ColumnId[]
		{
			ColumnId.Importance,
			ColumnId.MailIcon,
			ColumnId.HasAttachment,
			ColumnId.CheckBox,
			ColumnId.To,
			ColumnId.Subject,
			ColumnId.SentTime,
			ColumnId.Size
		});

		// Token: 0x04000212 RID: 530
		private Folder folder;

		// Token: 0x04000213 RID: 531
		private SearchScope searchScope;
	}
}
