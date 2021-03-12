using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000029 RID: 41
	internal sealed class ContactModuleSearchViewState : ContactModuleViewState, ISearchViewState
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00009480 File Offset: 0x00007680
		public ContactModuleSearchViewState(ClientViewState lastClientViewState, StoreObjectId folderId, string folderType, int pageNumber, string searchString, SearchScope searchScope) : base(folderId, folderType, pageNumber)
		{
			this.searchString = searchString;
			this.searchScope = searchScope;
			ISearchViewState searchViewState = lastClientViewState as ISearchViewState;
			if (searchViewState != null)
			{
				this.lastClientViewStateBeforeSearch = searchViewState.ClientViewStateBeforeSearch();
				return;
			}
			this.lastClientViewStateBeforeSearch = lastClientViewState;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000115 RID: 277 RVA: 0x000094C5 File Offset: 0x000076C5
		public string SearchString
		{
			get
			{
				return this.searchString;
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000094D0 File Offset: 0x000076D0
		public override PreFormActionResponse ToPreFormActionResponse()
		{
			PreFormActionResponse preFormActionResponse = base.ToPreFormActionResponse();
			if (!string.IsNullOrEmpty(this.SearchString))
			{
				preFormActionResponse.AddParameter("sch", this.SearchString);
				PreFormActionResponse preFormActionResponse2 = preFormActionResponse;
				string name = "scp";
				int num = (int)this.searchScope;
				preFormActionResponse2.AddParameter(name, num.ToString());
				string value = OwaContext.Current.UserContext.Key.Canary.CloneRenewed().ToString();
				preFormActionResponse.AddParameter("canary", value);
			}
			return preFormActionResponse;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00009547 File Offset: 0x00007747
		public string ClearSearchQueryString()
		{
			return this.lastClientViewStateBeforeSearch.ToQueryString();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00009554 File Offset: 0x00007754
		public ClientViewState ClientViewStateBeforeSearch()
		{
			return this.lastClientViewStateBeforeSearch;
		}

		// Token: 0x040000B5 RID: 181
		private ClientViewState lastClientViewStateBeforeSearch;

		// Token: 0x040000B6 RID: 182
		private string searchString;

		// Token: 0x040000B7 RID: 183
		private SearchScope searchScope;
	}
}
