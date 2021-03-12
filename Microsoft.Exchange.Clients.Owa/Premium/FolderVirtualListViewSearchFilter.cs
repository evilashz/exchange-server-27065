using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Search;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004B9 RID: 1209
	[OwaEventStruct("FVLVSF")]
	internal class FolderVirtualListViewSearchFilter
	{
		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06002E21 RID: 11809 RVA: 0x001075FE File Offset: 0x001057FE
		public SearchResultsIn ResultsIn
		{
			get
			{
				return (SearchResultsIn)this.ResultsInInt;
			}
		}

		// Token: 0x04001F8D RID: 8077
		public const string StructNamespace = "FVLVSF";

		// Token: 0x04001F8E RID: 8078
		public const string ReExecuteSearchName = "res";

		// Token: 0x04001F8F RID: 8079
		public const string AsyncSearch = "asrchsup";

		// Token: 0x04001F90 RID: 8080
		public const string SearchScopeName = "scp";

		// Token: 0x04001F91 RID: 8081
		public const string SearchStringName = "srch";

		// Token: 0x04001F92 RID: 8082
		public const string ResultsInName = "ri";

		// Token: 0x04001F93 RID: 8083
		public const string CategoryName = "cat";

		// Token: 0x04001F94 RID: 8084
		public const string RecipientName = "rcp";

		// Token: 0x04001F95 RID: 8085
		public const string RecipientValueName = "rcps";

		// Token: 0x04001F96 RID: 8086
		[OwaEventField("res", true, null)]
		public bool ReExecuteSearch;

		// Token: 0x04001F97 RID: 8087
		[OwaEventField("asrchsup", true, null)]
		public bool IsAsyncSearchEnabled;

		// Token: 0x04001F98 RID: 8088
		[OwaEventField("scp")]
		public SearchScope Scope;

		// Token: 0x04001F99 RID: 8089
		[OwaEventField("srch", true, null)]
		public string SearchString;

		// Token: 0x04001F9A RID: 8090
		[OwaEventField("ri", true, 0)]
		public int ResultsInInt;

		// Token: 0x04001F9B RID: 8091
		[OwaEventField("cat", true, null)]
		public string Category;

		// Token: 0x04001F9C RID: 8092
		[OwaEventField("rcp", true, null)]
		public SearchRecipient RecipientType;

		// Token: 0x04001F9D RID: 8093
		[OwaEventField("rcps", true, null)]
		public string RecipientValue;
	}
}
