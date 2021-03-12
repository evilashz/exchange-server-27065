using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007AD RID: 1965
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISearchFolder : IFolder, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x060049F3 RID: 18931
		SearchFolderCriteria GetSearchCriteria();

		// Token: 0x060049F4 RID: 18932
		void ApplyContinuousSearch(SearchFolderCriteria searchFolderCriteria);

		// Token: 0x060049F5 RID: 18933
		void ApplyOneTimeSearch(SearchFolderCriteria searchFolderCriteria);

		// Token: 0x060049F6 RID: 18934
		IAsyncResult BeginApplyContinuousSearch(SearchFolderCriteria searchFolderCriteria, AsyncCallback asyncCallback, object state);

		// Token: 0x060049F7 RID: 18935
		void EndApplyContinuousSearch(IAsyncResult asyncResult);

		// Token: 0x060049F8 RID: 18936
		IAsyncResult BeginApplyOneTimeSearch(SearchFolderCriteria searchFolderCriteria, AsyncCallback asyncCallback, object state);

		// Token: 0x060049F9 RID: 18937
		void EndApplyOneTimeSearch(IAsyncResult asyncResult);
	}
}
