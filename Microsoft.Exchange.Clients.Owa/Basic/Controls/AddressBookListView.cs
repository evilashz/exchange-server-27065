using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x0200000C RID: 12
	public class AddressBookListView : ListView
	{
		// Token: 0x0600006E RID: 110 RVA: 0x000048B4 File Offset: 0x00002AB4
		public AddressBookListView(string searchString, UserContext userContext, ColumnId sortedColumn, SortOrder sortOrder, AddressBookBase addressBook, AddressBookBase.RecipientCategory recipientCategory) : base(userContext, sortedColumn, sortOrder, ListView.ViewType.ADContentsListView)
		{
			this.addressBook = addressBook;
			this.searchString = searchString;
			this.recipientCategory = recipientCategory;
			base.FilteredView = !string.IsNullOrEmpty(searchString);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000048E7 File Offset: 0x00002AE7
		public override void Initialize(int startRange, int endRange)
		{
			this.InitializeListViewContents();
			this.InitializeDataSource();
			base.DataSource.LoadData(startRange, endRange);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004902 File Offset: 0x00002B02
		protected override void InitializeListViewContents()
		{
			base.ViewDescriptor = AddressBookListView.AddressBookViewDescriptor;
			base.Contents = new AddressBookItemList(base.ViewDescriptor, base.SortedColumn, base.SortOrder, base.UserContext);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004932 File Offset: 0x00002B32
		protected override void InitializeDataSource()
		{
			base.DataSource = new AddressBookDataSource(base.Contents.Properties, this.searchString, this.addressBook, this.recipientCategory, base.UserContext);
		}

		// Token: 0x04000040 RID: 64
		private static readonly ViewDescriptor AddressBookViewDescriptor = new ViewDescriptor(ColumnId.DisplayNameAD, true, new ColumnId[]
		{
			ColumnId.CheckBoxAD,
			ColumnId.DisplayNameAD,
			ColumnId.AliasAD,
			ColumnId.PhoneAD,
			ColumnId.OfficeAD,
			ColumnId.TitleAD,
			ColumnId.CompanyAD
		});

		// Token: 0x04000041 RID: 65
		private string searchString;

		// Token: 0x04000042 RID: 66
		private AddressBookBase addressBook;

		// Token: 0x04000043 RID: 67
		private AddressBookBase.RecipientCategory recipientCategory;
	}
}
