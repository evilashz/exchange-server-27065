using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000027 RID: 39
	internal abstract class ListViewViewState : ModuleViewState
	{
		// Token: 0x0600010F RID: 271 RVA: 0x0000941C File Offset: 0x0000761C
		public ListViewViewState(NavigationModule navigationModule, StoreObjectId folderId, string folderType, int pageNumber) : base(navigationModule, folderId, folderType)
		{
			this.pageNumber = pageNumber;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000110 RID: 272 RVA: 0x0000942F File Offset: 0x0000762F
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00009437 File Offset: 0x00007637
		public int PageNumber
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

		// Token: 0x06000112 RID: 274 RVA: 0x00009440 File Offset: 0x00007640
		public override PreFormActionResponse ToPreFormActionResponse()
		{
			PreFormActionResponse preFormActionResponse = base.ToPreFormActionResponse();
			if (this.pageNumber > 0)
			{
				preFormActionResponse.AddParameter("pg", this.pageNumber.ToString());
			}
			return preFormActionResponse;
		}

		// Token: 0x040000B4 RID: 180
		private int pageNumber;
	}
}
