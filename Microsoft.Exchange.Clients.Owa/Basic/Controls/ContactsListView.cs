using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x0200002B RID: 43
	public class ContactsListView : ListView
	{
		// Token: 0x0600011B RID: 283 RVA: 0x00009619 File Offset: 0x00007819
		internal ContactsListView(UserContext userContext, ColumnId sortedColumn, SortOrder sortOrder, Folder folder) : base(userContext, sortedColumn, sortOrder, ListView.ViewType.ContactsListView)
		{
			this.folder = folder;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000962D File Offset: 0x0000782D
		internal ContactsListView(UserContext userContext, ColumnId sortedColumn, SortOrder sortOrder, Folder folder, SearchScope searchScope) : this(userContext, sortedColumn, sortOrder, folder)
		{
			base.FilteredView = true;
			this.searchScope = searchScope;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000964C File Offset: 0x0000784C
		protected override void InitializeListViewContents()
		{
			base.ViewDescriptor = ContactsListView.Contacts;
			bool showFolderNameTooltip = base.FilteredView && this.searchScope != SearchScope.SelectedFolder;
			base.Contents = new ContactsListViewContents(base.ViewDescriptor, base.SortedColumn, base.SortOrder, showFolderNameTooltip, base.UserContext);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000096A0 File Offset: 0x000078A0
		protected override void InitializeDataSource()
		{
			SortBy[] array;
			if (base.SortedColumn == ColumnId.FileAs)
			{
				array = new SortBy[]
				{
					new SortBy(ContactBaseSchema.FileAs, base.SortOrder)
				};
			}
			else
			{
				array = new SortBy[2];
				Column column = ListViewColumns.GetColumn(base.SortedColumn);
				array[0] = new SortBy(column[0], base.SortOrder);
				array[1] = new SortBy(ContactBaseSchema.FileAs, base.SortOrder);
			}
			base.DataSource = new MessageListViewDataSource(base.Contents.Properties, this.folder, array);
		}

		// Token: 0x040000B9 RID: 185
		private static readonly ViewDescriptor Contacts = new ViewDescriptor(ColumnId.FileAs, true, new ColumnId[]
		{
			ColumnId.CheckBoxContact,
			ColumnId.FileAs,
			ColumnId.EmailAddresses,
			ColumnId.PhoneNumbers,
			ColumnId.Title,
			ColumnId.CompanyName
		});

		// Token: 0x040000BA RID: 186
		private Folder folder;

		// Token: 0x040000BB RID: 187
		private SearchScope searchScope;
	}
}
