using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000013 RID: 19
	internal sealed class AddressBookSearchViewState : AddressBookViewState, ISearchViewState
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00005078 File Offset: 0x00003278
		public AddressBookSearchViewState(ClientViewState lastClientViewState, AddressBook.Mode mode, string addressBookToSearch, string searchString, int pageNumber, StoreObjectId itemId, string itemChangeKey, RecipientItemType recipientWell, ColumnId sortColumnId, SortOrder sortOrder) : base(lastClientViewState, mode, pageNumber, itemId, itemChangeKey, recipientWell, sortColumnId, sortOrder)
		{
			this.searchLocation = addressBookToSearch;
			this.searchString = searchString;
			ISearchViewState searchViewState = lastClientViewState as ISearchViewState;
			if (searchViewState != null)
			{
				this.lastClientViewStateBeforeSearch = searchViewState.ClientViewStateBeforeSearch();
				return;
			}
			this.lastClientViewStateBeforeSearch = lastClientViewState;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000050C6 File Offset: 0x000032C6
		public string SearchString
		{
			get
			{
				return this.searchString;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000050CE File Offset: 0x000032CE
		public string SearchLocation
		{
			get
			{
				return this.searchLocation;
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000050D8 File Offset: 0x000032D8
		public override PreFormActionResponse ToPreFormActionResponse()
		{
			PreFormActionResponse preFormActionResponse = base.ToPreFormActionResponse();
			if (!string.IsNullOrEmpty(this.searchString))
			{
				preFormActionResponse.AddParameter("sch", this.searchString);
				if (!string.IsNullOrEmpty(this.searchLocation))
				{
					preFormActionResponse.AddParameter("ab", this.searchLocation);
				}
				string value = OwaContext.Current.UserContext.Key.Canary.CloneRenewed().ToString();
				preFormActionResponse.AddParameter("canary", value);
			}
			return preFormActionResponse;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005154 File Offset: 0x00003354
		public string ClearSearchQueryString()
		{
			if (!AddressBook.IsEditingMode(base.Mode) || this.lastClientViewStateBeforeSearch is AddressBookViewState)
			{
				return this.lastClientViewStateBeforeSearch.ToQueryString();
			}
			return "?" + base.ToPreFormActionResponse().GetUrl();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005191 File Offset: 0x00003391
		public ClientViewState ClientViewStateBeforeSearch()
		{
			return this.lastClientViewStateBeforeSearch;
		}

		// Token: 0x04000058 RID: 88
		private ClientViewState lastClientViewStateBeforeSearch;

		// Token: 0x04000059 RID: 89
		private string searchLocation;

		// Token: 0x0400005A RID: 90
		private string searchString;
	}
}
