using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x0200008C RID: 140
	internal sealed class WebPartModuleViewState : ListViewViewState
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x000225F6 File Offset: 0x000207F6
		public WebPartModuleViewState(StoreObjectId folderId, string folderType, int pageNumber, NavigationModule navigationModule, SortOrder sortOrder, ColumnId sortedColumn) : base(navigationModule, folderId, folderType, pageNumber)
		{
			this.sortOrder = sortOrder;
			this.sortedColumn = sortedColumn;
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00022613 File Offset: 0x00020813
		public SortOrder SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0002261B File Offset: 0x0002081B
		public ColumnId SortedColumn
		{
			get
			{
				return this.sortedColumn;
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00022624 File Offset: 0x00020824
		public override PreFormActionResponse ToPreFormActionResponse()
		{
			PreFormActionResponse preFormActionResponse = base.ToPreFormActionResponse();
			preFormActionResponse.ApplicationElement = ApplicationElement.WebPartFolder;
			return preFormActionResponse;
		}

		// Token: 0x04000343 RID: 835
		private SortOrder sortOrder;

		// Token: 0x04000344 RID: 836
		private ColumnId sortedColumn;
	}
}
