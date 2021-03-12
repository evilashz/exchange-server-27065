using System;
using System.Collections;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000362 RID: 866
	internal class DumpsterVirtualListView : VirtualListView2
	{
		// Token: 0x0600208D RID: 8333 RVA: 0x000BC9BE File Offset: 0x000BABBE
		internal DumpsterVirtualListView(UserContext userContext, string id, ColumnId sortedColumn, SortOrder sortOrder, Folder folder) : base(userContext, id, true, sortedColumn, sortOrder, false)
		{
			this.folder = folder;
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x0600208E RID: 8334 RVA: 0x000BC9D5 File Offset: 0x000BABD5
		protected override Folder DataFolder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x0600208F RID: 8335 RVA: 0x000BC9DD File Offset: 0x000BABDD
		public override ViewType ViewType
		{
			get
			{
				return ViewType.Dumpster;
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06002090 RID: 8336 RVA: 0x000BC9E1 File Offset: 0x000BABE1
		protected override bool IsMultiLine
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06002091 RID: 8337 RVA: 0x000BC9E4 File Offset: 0x000BABE4
		public override string OehNamespace
		{
			get
			{
				return "DumpsterVLV";
			}
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x000BC9EC File Offset: 0x000BABEC
		public SortBy[] GetSortByProperties()
		{
			Column column = ListViewColumns.GetColumn(base.SortedColumn);
			SortBy[] result;
			if (base.SortedColumn == ColumnId.DeletedOnTime)
			{
				result = new SortBy[]
				{
					new SortBy(StoreObjectSchema.LastModifiedTime, base.SortOrder)
				};
			}
			else
			{
				result = new SortBy[]
				{
					new SortBy(column[0], base.SortOrder),
					new SortBy(StoreObjectSchema.LastModifiedTime, SortOrder.Descending)
				};
			}
			return result;
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x000BCA58 File Offset: 0x000BAC58
		protected override ListViewContents2 CreateListViewContents()
		{
			return new MessageMultiLineList2(DumpsterVirtualListView.dumpsterViewDescriptor, base.SortedColumn, base.SortOrder, base.UserContext, SearchScope.SelectedFolder);
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x000BCA84 File Offset: 0x000BAC84
		protected override IListViewDataSource CreateDataSource(Hashtable properties)
		{
			return new FolderListViewDataSource(base.UserContext, properties, this.folder, this.GetSortByProperties());
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x000BCA9E File Offset: 0x000BAC9E
		protected override void InternalRenderData(TextWriter writer)
		{
			base.InternalRenderData(writer);
		}

		// Token: 0x04001772 RID: 6002
		private static readonly ViewDescriptor dumpsterViewDescriptor = new ViewDescriptor(ColumnId.DeletedOnTime, false, new ColumnId[]
		{
			ColumnId.ObjectIcon,
			ColumnId.Importance,
			ColumnId.HasAttachment,
			ColumnId.ObjectDisplayName,
			ColumnId.DeletedOnTime,
			ColumnId.From
		});

		// Token: 0x04001773 RID: 6003
		private Folder folder;
	}
}
