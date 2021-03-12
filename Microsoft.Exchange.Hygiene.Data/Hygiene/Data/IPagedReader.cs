using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x0200006A RID: 106
	internal interface IPagedReader<TResult> : IEnumerable<!0>, IEnumerable where TResult : IConfigurable, new()
	{
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600041B RID: 1051
		bool RetrievedAllData { get; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600041C RID: 1052
		int PagesReturned { get; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600041D RID: 1053
		int PageSize { get; }

		// Token: 0x0600041E RID: 1054
		TResult[] ReadAllPages();

		// Token: 0x0600041F RID: 1055
		TResult[] GetNextPage();
	}
}
