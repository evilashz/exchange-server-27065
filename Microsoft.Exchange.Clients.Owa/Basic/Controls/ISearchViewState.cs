using System;
using Microsoft.Exchange.Clients.Owa.Core.Controls;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000012 RID: 18
	internal interface ISearchViewState
	{
		// Token: 0x06000081 RID: 129
		string ClearSearchQueryString();

		// Token: 0x06000082 RID: 130
		ClientViewState ClientViewStateBeforeSearch();
	}
}
