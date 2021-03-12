using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000068 RID: 104
	internal sealed class MessageModuleSearchViewState : MessageModuleViewState, ISearchViewState
	{
		// Token: 0x060002DF RID: 735 RVA: 0x00018E38 File Offset: 0x00017038
		public MessageModuleSearchViewState(ClientViewState lastClientViewState, StoreObjectId folderId, string folderType, SecondaryNavigationArea selectedUsing, int pageNumber, string searchString, SearchScope searchScope) : base(folderId, folderType, selectedUsing, pageNumber)
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

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00018E7F File Offset: 0x0001707F
		public string SearchString
		{
			get
			{
				return this.searchString;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00018E87 File Offset: 0x00017087
		public SearchScope SearchScope
		{
			get
			{
				return this.searchScope;
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00018E90 File Offset: 0x00017090
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

		// Token: 0x060002E3 RID: 739 RVA: 0x00018F07 File Offset: 0x00017107
		public string ClearSearchQueryString()
		{
			return this.lastClientViewStateBeforeSearch.ToQueryString();
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00018F14 File Offset: 0x00017114
		public ClientViewState ClientViewStateBeforeSearch()
		{
			return this.lastClientViewStateBeforeSearch;
		}

		// Token: 0x04000216 RID: 534
		private ClientViewState lastClientViewStateBeforeSearch;

		// Token: 0x04000217 RID: 535
		private string searchString;

		// Token: 0x04000218 RID: 536
		private SearchScope searchScope;
	}
}
